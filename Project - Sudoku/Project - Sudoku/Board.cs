using System.Collections.Generic;
using System.Linq;

namespace Project___Sudoku
{
    public class Board
    {

        enum SquareType {NONE, SET, FIXED }
        private int[,] board = new int[9,9]; //9*9 multidimensional array

        private SquareType[,] squareTypes = new SquareType[9, 9]; //this initializes a 2D array with each value set to NONE


        public void SetSquare(int y, int x, int number)
        {
            board[y, x] = number;

        }

        public void SetFixedSquares(int y, int x, int number)
        {
            SetSquare(y, x, number);
            squareTypes[y, x] = SquareType.FIXED;
        }

        public void ClearSquare(int y, int x)
        {
            board[y, x] = 0;
        }

        //public bool ValidateBoard()
        //{

        //}



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

        public bool ValidateSection(int x, int y)
        {
            var seeNumbers = new System.Collections.Generic.HashSet<int>(); //hashset uses gethashcode algorithm so will ensure only unique ints are added


            for (int i = 1; i <= 3; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    var number = board[x * i - 1, j * y - 1]; //multiply by x and y to move back into appropriate subgrid
                    if (number == 0)
                    {
                        return false;
                    }
                    seeNumbers.Add(number);
                }
            }

            return seeNumbers.Count == 9;
        }

        private bool Solve(int x, int y)
        {
            if (x == 9)
            {
                var count = 0;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        count += squareTypes[i, j] != SquareType.NONE ? 1 : 0;
                    }
                }
                return count == 81;
            }
            if (squareTypes[y,x] != SquareType.NONE)
            {
                int nextX = x;
                int nextY = y + 1;
                if (nextX == 9)
                {
                    nextX = x+ 1;
                    nextY = 0;
                }
                Solve(nextX, nextY);
            }
            else
            {
                var candidates = CheckCandidates(x, y);
                foreach (var candidate in candidates)
                {
                    board[y, x] = candidate;
                    squareTypes[y, x] = SquareType.SET;
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
                        board[y, x] = 0;
                        squareTypes[y, x] = SquareType.NONE;
                    }

                }
            }


            return false;
        }

        //IEnumerable because not interested in what's coming out of the list

        private IEnumerable<int> CheckCandidates(int x, int y)
        {
            
            var candidates = new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            for (int i = 0; i < 9; i++)
            {
                if (squareTypes[y, i] != SquareType.NONE)
                {
                    candidates.Remove(board[y, i]);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (squareTypes[i, x] != SquareType.NONE)
                {
                    candidates.Remove(board[i, x]);
                }
            }

            for (int i = x - (x%3); i < x - (x%3)+3; i++)
            {
                for (int j = y; j < y-(y%3)+3; j++)
                {
                    if (squareTypes[i, j] != SquareType.NONE)
                    {
                        candidates.Remove(board[i, j]);
                    }
                }
            }

            return candidates;



        }


        public bool Solve(out int[,] result) //acting like multiple return types 
        {
            
            var solve = Solve(0, 0);
            result = board;
            return solve;

            

        }

    }




        /*public bool ValidateBoard(int x, int y)
        {


        }*/
}
