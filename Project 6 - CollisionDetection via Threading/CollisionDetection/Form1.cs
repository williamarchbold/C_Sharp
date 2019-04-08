using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using Timer = System.Windows.Forms.Timer;


namespace CollisionDetection
{
    public partial class Form1 : Form
    {
        Timer loop = new Timer();
        static int N = Environment.ProcessorCount;  //number of threads to optimize for number of processor cores. source: https://stackoverflow.com/questions/1542213/how-to-find-the-number-of-cpu-cores-via-net-c
        Thread[] threads = new Thread[N];

        List<Square> squares = new List<Square>();

        Bitmap map;
        Pen black = new Pen(Color.Black, 1);
        Pen red = new Pen(Color.Red, 1);

        Random rand = new Random(8675309);

        Stopwatch totalCalcTime = new Stopwatch();
        Stopwatch frameCounter = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            loop.Enabled = true;

            loop.Tick += new EventHandler(Update);
            loop.Interval = 1;

            frameCounter.Start();

            map = new Bitmap(CollisionMap.Width, CollisionMap.Height);
            Square.Bounds = new Vector2(CollisionMap.Width, CollisionMap.Height);
        }

        /// <summary>
        /// Called as fast as it possibly can be called (up to every 1ms)
        /// </summary>
        public void Update(object o, EventArgs e)
        {
            totalCalcTime.Reset(); totalCalcTime.Start();
            map = new Bitmap(CollisionMap.Width, CollisionMap.Height);
            for (int i = 0; i < 20; i++)
                squares.Add(new Square(rand.Next(3, 5), new Vector2(rand.Next(0, CollisionMap.Width), rand.Next(0, CollisionMap.Height)), new Vector2(rand.Next(-3, 4), rand.Next(-3, 4))));

            foreach(Square s in squares)
            {
                s.Move();
            }
            /**********************************************************************/
            /* SWITCH THESE LINES OF CODE TO TEST THE DIFFERENT METHODS */
            //CollisionDetection();
            CollisionDetectionParallel();
            //CollisionDetectionParallelOptimized();
            /**********************************************************************/
            DrawSquares(map);

            totalCalcTime.Stop();
            NumSquaresLabel.Invoke(new MethodInvoker(delegate ()
            {
                FPSLabel.Text = "Frames Per Second = " + 1000f / frameCounter.ElapsedMilliseconds;
                NumSquaresLabel.Text = "Num Squares = " + squares.Count;
                CalcTimeLabel.Text = "Total Calculation Time = " + totalCalcTime.ElapsedMilliseconds + "ms";
            }));
            frameCounter.Reset(); frameCounter.Start();
        }

        /// <summary>
        /// Collision detection without tasks
        /// </summary>
        public void CollisionDetection()
        {
            //Reset the color of squares to black.
            for (int i = 0; i < squares.Count; i++)
                squares[i].Color = Color.Black;

            for(int i = 0; i < squares.Count; i++)
            {
                for (int j = 0; j < squares.Count; j++)
                {
                    if (squares[i] != squares[j] && squares[i].IsCollidingWith(squares[j]))
                    {
                        squares[i].Color = Color.Red;
                        squares[j].Color = Color.Red;
                    }
                }
            }
        }

        public void CollisionDetectionParallelOptimized() //teacher's solution
        {
            //Reset the color of squares to black.
            for (int i = 0; i < squares.Count; i++)
                squares[i].Color = Color.Black;
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < squares.Count; i+=20) //add by 20 sized chunks
            {
                int h = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int x = h; x < h + 20; x++)
                    {
                        for (int j = x+1; j < squares.Count; j++)
                        {
                            if (squares[x].IsCollidingWith(squares[j]))
                            {
                                squares[x].Color = Color.Red;
                                squares[j].Color = Color.Red;
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Parallelized collision detection
        /// YOUR CODE GOES HERE!
        /// </summary>
        public void CollisionDetectionParallel()
        {
            int lowerBound = 0;
            int incrementToUpperBound = squares.Count / N;

            //Reset the color of squares to black without parallelism. Parallelism seemed to cost too much overhead for resetting to black 
            for (int i = 0; i < squares.Count; i++)
                squares[i].Color = Color.Black;

            /*for (int n = 0; n < N; n++)
            {
                int lower = lowerBound;
                int upper = lower + incrementToUpperBound;
                lowerBound = upper;
                if (n + 1 == N)
                {
                    upper = squares.Count; //if division leaves out a couple elements, ensure that all remaining elements are handled by last thread
                }
                var t = new Thread(
                () => //adding a lambda turns the for loop into a method so that it can be invoked by Thread.Start
                {
                    for (int i = lower; i < upper; i++)
                    {
                        squares[i].Color = Color.Black;
                    }
                }
                );
                t.Start();
                threads[n] = t;
            }
            for (int n = 0; n < N; n++)
            {
                //https://stackoverflow.com/questions/4190949/create-multiple-threads-and-wait-all-of-them-to-complete source for join 
                threads[n].Join(); //join tells main thread block(wait) until thread sub n completes 
            }
            */



            lowerBound = 0;
            for (int n = 0; n < N; n++)
            {
                int lower = lowerBound;
                int upper = lower + incrementToUpperBound;
                lowerBound = upper;
                if (n + 1 == N)
                {
                    upper = squares.Count; //if division leaves out a couple elements, ensure that all remaining elements are handled by last thread
                }
                var t = new Thread(
                () => //adding a lambda turns the for loop into a method so that it can be invoked by Thread.Start
                {
                    for (int i = lower; i < upper; i++)
                    {
                        //for (int j = 0; j < squares.Count; j++)
                        for (int j = 0; j < i; j++)//check at j=i+1 to eliminate duplicates
                        {
                            if (squares[i] != squares[j] && squares[i].IsCollidingWith(squares[j]))
                            {
                                squares[i].Color = Color.Red;
                                squares[j].Color = Color.Red;
                            }
                        }
                    }
                }
                );
                t.Start();
                threads[n] = t;
            }
            for (int n = 0; n < N; n++)
            {
                //https://stackoverflow.com/questions/4190949/create-multiple-threads-and-wait-all-of-them-to-complete source for join 
                threads[n].Join(); //join tells main thread block(wait) until thread sub n completes 
            }
        }

        /// <summary>
        /// Draws the squares on the screen
        /// </summary>
        /// <param name="bitmap">The bitmap to update</param>
        public void DrawSquares(Image bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            foreach (Square s in squares.Where(t => t.Color == Color.Red))
            {
                g.DrawRectangle(red, s.Position.X, s.Position.Y, s.Size, s.Size);
            }
            foreach (Square s in squares.Where(t => t.Color == Color.Black))
            {
                g.DrawRectangle(black, s.Position.X, s.Position.Y, s.Size, s.Size);
            }
            CollisionMap.Image = bitmap;
        }
    }
}
