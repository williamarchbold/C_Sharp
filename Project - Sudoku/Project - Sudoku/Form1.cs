using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project___Sudoku
{

    /*
     * This class dictates what happens when a user interacts with the Sudoku board. The primary objeject used throughtout 
     * this class is the DataGridView object called "puzzle." When something happens or changes on puzzle then something similar
     * has to happen to the behind the scenes board object named "board" and vice versa. 
     * 
     * @author William Archbold
     */
    public partial class Form1 : Form
    {

        private Board board = new Board();

        public Form1()
        {
            InitializeComponent();


        }

        /*
         *  Method lets you know that everything has been set up with various toolkits and safe to play with UI as designed. 
         *  This is the typical starting point for building a winform. 
         *  This method is primarily used to build the DataGridView object named "puzzle" which will be the user's visual 
         *  representation for the sudoku board
         */
        protected override void OnLoad(EventArgs e) 
        {
            base.OnLoad(e);
            //on load build up the datagrid view form instance called puzzle and set characteristics of the puzzle 
            puzzle.RowTemplate.Height = puzzle.Height / 9; //whenever a row is added it's set to height 9 
            puzzle.ColumnCount = 9; //set the column number to 9
            puzzle.Rows.Add(9); //add 9 rows to the puzzle

            //two loops to set the background color of the board to visually mark the 9 subgrids
            //resets a var odd everytime through loop using XOR 
            //then uses ternary operator to set cell color 
            for (int i = 0; i < 9; i++) 
            {
                for (int j = 0; j < 9; j++)
                {
                    var odd = i / 3 % 2 == 0 ^ j / 3 % 2 == 0; //exclusive or (^) where 1 statement must be false and 1 must be true 
                    puzzle.Rows[i].Cells[j].Style.BackColor = odd ? Color.LightBlue : Color.LightCyan;
                }
            }
        }

        private int importedSet = 0; //private variable that will be called in newFilebutton_Click

        /*
         * This method will import text from a file provided by the professor that has lines of text numbers
         * each line is 81 characters long representing the total number of squares in a Sudoku board
         * Method will be called each time the Import Board button is pushed. 
         * Each time the button is clicked, private int variable importedSet will increment once so that the 
         * method will know which line from the txt file to import from next
         */
        private void newFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var lines = File.ReadLines("examples.txt");
                var firstPuzzle = lines.Skip(importedSet).FirstOrDefault() ; //this should create an array of 81 characters
                if (firstPuzzle == null)
                {
                    importedSet = 0;
                    firstPuzzle = lines.Skip(importedSet).FirstOrDefault();
                }
                SetCurrentBoard(Board.FromString(firstPuzzle)); //FromString is a static method of Board class
                importedSet++;
               
                //board.ValidateRow(0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//winforms equivalent of Console.WriteLine(ex);
            }
        }

        /*
         * This method will use the board(multidimensional array that the program sees, but not the user)'s
         * current cell's value to populate the puzzle(what the user sees)
         */
        private void SetCurrentBoard(Board board)
        {
            this.board = board;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var number = board[j, i];

                    puzzle[j, i].Value = number > 0 ? number.ToString() : string.Empty; //either set square value to number or empty string if value 0
                    puzzle[j, i].ErrorText = string.Empty; //if the asterisk in a stop sign shows up, delete it
                }
            }
        }

        /*
         * This method is used to validate what the user inputs onto the puzzle(what the user sees) 
         * and ensure it will be a valid int on the board(what the program sees)
         */
        private void puzzle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var currentCell = puzzle.Rows[e.RowIndex].Cells[e.ColumnIndex];

            var rawValue = currentCell.EditedFormattedValue.ToString().Trim();

            UpdateBoard(e.RowIndex, e.ColumnIndex, rawValue);
        }

        /*
         * <p> This method will update the board's current cell's value based on either the user's input 
         * or if a value was provided by an imported board (isFixed becomes true) or if the user changes
         * the difficulty and creates a randomly prepopulated board (isFixed becomes true) </p>
         */
        private void UpdateBoard(int y, int x, string rawValue, bool isFixed = false)//last arugment is optional default false
        {
            var currentCell = puzzle.Rows[y].Cells[x];

            //if raw value in puzzle's cell is empty, clear the square on the board
            if (rawValue == string.Empty)
            {
                currentCell.ErrorText = string.Empty;
                board.ClearSquare(x, y);
            }

            //this will ensure user's input is between 1 and 9 
            //if not a little asterisk will appear and clear board's cell's value
            if ((int.TryParse(rawValue, out var value) && value >= 1 && value <= 9))
            {
                currentCell.ErrorText = string.Empty;
                
                if (isFixed)
                {
                    board.SetFixedSquares(x, y, value);
                }
                else
                {
                    board.SetSquare(x, y, value);
                }

            }
            else
            {
                currentCell.ErrorText = "!";
                board.ClearSquare(x, y);
            }
            

        }

        public static void Solve(DataGridView puzzle)
        {
            
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            if (board.ValidateBoard())
            {
                MessageBox.Show("Valid Board!");
            }
            else
            {
                MessageBox.Show("Invalid Board");
            }
        }

        private void CreateSolution_Click(object sender, EventArgs e)
        {
            if (board.Solve())
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        puzzle[j, i].Value = board[j, i];
                    }
                }
            }
            else
            {
                MessageBox.Show("failed to build");
            }

            ;
        }

        private void RandomNine_Click(object sender, EventArgs e)
        {
            SetCurrentBoard(Board.FromRandom());

        }

        private void RemoveNFixedSquares_Click(object sender, EventArgs e)
        {

            int number = (int)numericUpDown1.Value;
            SetCurrentBoard(Board.FromRandom(number));



        }
    }
}
