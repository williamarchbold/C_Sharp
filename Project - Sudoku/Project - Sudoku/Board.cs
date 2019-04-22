using System;
using System.Collections.Generic;
using System.Linq;

namespace Project___Sudoku
{
    /*
     * <p> A Board object will be what the program manipulates to create and solve Sudoku puzzles.
     * Every Board object has 2 9x9 boards: 1 will be for putting integers in and another will 
     * be for keeping track of whether those integers were fixed, i.e. fromt the imported text 
     * and can't be changed, empty (default value) or set.</p>
     * 
     * @author William Archbold
     */
    public class Board
    {

        private int[,] board = new int[9, 9]; //9*9 multidimensional array

        enum SquareType {NONE, SET, FIXED } //these will be the three types of values that a second behind the scenes array will use to keep track of the program's main board's squares' value types
   
        private SquareType[,] squareTypes = new SquareType[9, 9]; //this initializes a 2D array with each value set to NONE

        //indexer get property, which forces user to have to go through the board's known methods to set a square's value
        public int this[int X, int Y]
        {
            get { return board[X, Y]; }
        }

        /*
         * This is a factory method - static which will use object type and create a board object within
         */
        public static Board FromRandom() 
        {
            Random random = new Random();
            var candidates = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            }.OrderBy(x => random.Next()).ToList();

            Board scratchBoard = new Board();

            foreach (var candidate in candidates)
            {
                var set = false;
                while (!set)
                {
                    int x = random.Next(9);
                    int y = random.Next(9);

                    if (scratchBoard.board[x, y] == 0)
                    {
                        scratchBoard.SetFixedSquares(x, y, candidate);
                        set = true;
                    }
                }
            }  
            return scratchBoard;
        }

        /*
         * <p> This method is only called when the Create Random Board button is pushed.
         * Method calls static method FromRandom to create a randomly populated board.
         * Then uses the method parameter, which is the number of squares that the user dictated to remove
         * and starts randomly removing that many sqaures' values. Method will return a board that is used 
         * as a parameter to SetCurrentBoard </p>
         */
        public static Board FromRandom(int numberToRemove)
        {
            Random random = new Random();
            int x, y;


            var tempBoard = Board.FromRandom();
            if (tempBoard.Solve())
            {


                for (int i = 0; i < numberToRemove; i++)
                {
                    var set = false;
                    while (!set)
                    {
                        x = random.Next(9);
                        y = random.Next(9);

                        if (tempBoard.board[x, y] != 0)
                        {
                            tempBoard.ClearSquare(x, y);
                            set = true;
                        }
                    }

                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (tempBoard.board[i,j] != 0)
                        {
                            tempBoard.squareTypes[i,j] = SquareType.FIXED;
                        }
                    }
                }
                return tempBoard;

            }
            else
            {
                throw new InvalidOperationException("Solve's if statement did not return true"); //included this throw because Visual Studio wanted an else statement included
            }
        }

        /*
         * This method is called when the user wants to play off an imported puzzle from a file
         */
        public static Board FromString(string fileinput)
        {
            Board importedBoard = new Board();//start with a fresh board 

            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var character = fileinput[count++];
                    if (character == '.')
                    {
                        continue;
                    }
                    if ((int.TryParse(character.ToString(), out var value) && value >= 1 && value <= 9))
                    {
                        importedBoard.SetSquare(i, j, value); //this updates the board in memory
                    }   
                }
            }
            
            return importedBoard;
        }

        /*
         * Set the board's current square's value and set the squareType's enumeration value
         */
        public void SetSquare(int x, int y, int number)
        {
            board[x, y] = number;
            squareTypes[x, y] = SquareType.SET;

        }

       
        /*
         * Called when current square's value is fixed 
         */
        public void SetFixedSquares(int x, int y, int number)
        {
            SetSquare(x, y, number);
            squareTypes[x, y] = SquareType.FIXED;
        }

        /*
         * Clear the square's value and set the squareType's enum to NONE
         */
        public void ClearSquare(int x, int y)
        {
            board[x, y] = 0;
            squareTypes[x, y] = SquareType.NONE;
        }

        /*
         * Method will ensure that the rules of Sudoku are met
         */
        public bool ValidateBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                if (ValidateRow(i))
                {
                    if (ValidateColumn(i))
                    {
                        continue;
                    }
                }
                return false;

            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (ValidateSection(i*3, j*3)) //multiply by 3 to get into next subgrid when need to get into next subgrid
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }

        /*
         * This method validates the current row's values
         */
        public bool ValidateRow(int x)
        {
            var seeNumbers = new System.Collections.Generic.HashSet<int>(); //hashset uses gethashcode algorithm so will ensure only unique ints are added

            for (int i = 0; i < 9; i++)
            {
                if (board[i, x] == 0)//0 represents no input yet, but is a unique number within hashset 
                {
                    return false;
                }
                seeNumbers.Add(board[i,x]); //if Add adds a duplicate number it won't add the number 
            }
            return seeNumbers.Count == 9;
        }

        /*
         * This method validate the current column's values
         */
        private bool ValidateColumn(int y)
        {
            var seeNumbers = new System.Collections.Generic.HashSet<int>(); //hashset uses gethashcode algorithm so will ensure only unique ints are added

            for (int i = 0; i < 9; i++)
            {
                if (board[y, i] == 0)//0 represents no input yet, but is a unique number within hashset 
                {
                    return false;
                }
                seeNumbers.Add(board[y, i]); //if Add adds a duplicate number it won't add the number 
            }
            return seeNumbers.Count == 9;
        }

        /*
         * This method validates the current 3x3 subgrid in accoradance with Sudoku rules
         */
        public bool ValidateSection(int x, int y)
        {
            var seeNumbers = new System.Collections.Generic.HashSet<int>(); //hashset uses gethashcode algorithm so will ensure only unique ints are added

            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                { 
                    var number = board[i,j]; //multiply by x and y to move back into appropriate subgrid
                    if (number == 0)
                    {
                        return false;
                    }
                    seeNumbers.Add(number);
                }
            }

            return seeNumbers.Count == 9;
        }

        /*
         * <p>This is the key method of the program. Recursive method that will bruteforce solve the Sudoku board. 
         * I used the below video as source code for this method, but adapted it to C#</p>
         * https://www.youtube.com/watch?v=gN8xrMwrLSc
         */
        private bool Solve(int x, int y)
        {
            //starting off the recursive function with the desired endstate where we reach the last row 
            if (x == 9)
            {
                var count = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        count += squareTypes[i, j] != SquareType.NONE ? 1 : 0; //ensure that every square has a valid value
                    }
                }
                return count == 81;
            }
            if (squareTypes[x,y] == SquareType.NONE) // if square has no input try putting in a square candidate
            {
                var candidates = CheckCandidates(x, y);
                foreach (var candidate in candidates)
                {
                    board[x, y] = candidate;
                    squareTypes[x, y] = SquareType.SET;
                    int nextX = x;
                    int nextY = y + 1;
                    if (nextY == 9)
                    {
                        nextX = x + 1;
                        nextY = 0;
                    }
                    if (Solve(nextX, nextY))
                    {
                        return true;
                    }
                    else
                    {
                        board[x, y] = 0;
                        squareTypes[x, y] = SquareType.NONE;
                    }

                }
            }
            else
            {
                int nextX = x;
                int nextY = y + 1;
                if (nextY == 9)
                {
                    nextX = x + 1;
                    nextY = 0;
                }
                return Solve(nextX, nextY);
            }
            return false;
        }

        /*
         * <p>This method will check the candidate int values for the current square.
         * Method returns IEnumerable<int> because not interested in what's coming out of the list.</p>
         */
        private IEnumerable<int> CheckCandidates(int x, int y)
        {
            
            var candidates = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            for (int i = 0; i < 9; i++)
            {
                if (squareTypes[x, i] != SquareType.NONE) //checking row
                {
                    candidates.Remove(board[x, i]);
                }
                if (squareTypes[i, y] != SquareType.NONE) //checking column
                {
                    candidates.Remove(board[i, y]);
                }
            }

            for (int i = x - (x%3); i < x - (x%3)+3; i++)
            {
                for (int j = y - (y%3); j < y-(y%3)+3; j++)
                {
                    if (squareTypes[i, j] != SquareType.NONE)
                    {
                        candidates.Remove(board[i, j]);
                    }
                }
            }
            return candidates;
        }


        public bool Solve() //acting like multiple return types 
        {
            
            return Solve(0, 0);
        }
    }
}
