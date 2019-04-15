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
    public partial class Form1 : Form
    {

        private Board board = new Board();

        public Form1()
        {
            InitializeComponent();


        }

        //Method lets you know that everything has been set up with various toolkits and safe to play with UI as designed. 
        //This is the typical starting point for building a winform 
        protected override void OnLoad(EventArgs e) 
        {
            base.OnLoad(e);
            //on load build up the datagrid view form instance called puzzle and set characteristics of the puzzle 
            puzzle.RowTemplate.Height = puzzle.Height / 9; //whenver a row is added it's set to height 9 
            puzzle.ColumnCount = 9;
            puzzle.Rows.Add(9);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var odd = i / 3 % 2 == 0 ^ j / 3 % 2 == 0; //exclusive or (^) 
                    puzzle.Rows[i].Cells[j].Style.BackColor = odd ? Color.LightBlue : Color.LightCyan;
                }
            }
        }

        private int importedSet = 0;

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
                SetCurrentBoard(Board.FromString(firstPuzzle));
                importedSet++;
               
                //board.ValidateRow(0);

            }
            catch (Exception ex)
            {
                //winforms equivalent of Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
            


        }

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

        private void puzzle_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var currentCell = puzzle.Rows[e.RowIndex].Cells[e.ColumnIndex];

            var rawValue = currentCell.EditedFormattedValue.ToString().Trim();

            UpdateBoard(e.RowIndex, e.ColumnIndex, rawValue);

            
            


           

        }

        private void UpdateBoard(int y, int x, string rawValue, bool isFixed = false)//last arugment is optional default false
        {
            var currentCell = puzzle.Rows[y].Cells[x];

            if (rawValue == string.Empty)
            {
                currentCell.ErrorText = string.Empty;
                board.ClearSquare(x, y);
            }

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
