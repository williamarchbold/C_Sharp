using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_8_TicTacToe__Winforms_and_IP
{
    public enum PLAYER { X, O };

    class TicTacToeGame
    {

        //private TicTacToeBoard board;

        private TicTacToeSquare[,] board = new TicTacToeSquare[3, 3];
        public PLAYER myCharacter { get; private set; } 
        private int turnCount { get; set; }
        private PLAYER turn { get; set; }

        public bool IsMyTurn () { return turn == myCharacter; }

        public TicTacToeGame()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y] = new TicTacToeSquare();
                }
            }
            turnCount = 8;
            turn = PLAYER.X;
        }


        public void SetCharacter(PLAYER player)
        {
            myCharacter = player;
        }

        public bool IsEmptySquare(int row, int column)
        {
            return board[row, column].value == SQUARE_TYPE.EMPTY;
        }

        // Enters a players move, and tells if the game is over.
        // The game is over if (1) someone wins, (2) tie/draw (3) network error!!
        public void EnterMove(int row, int column)
        {
            if (turn == PLAYER.X)
            {
                board[row, column].value = SQUARE_TYPE.X;
            }
            else
            {
                board[row, column].value = SQUARE_TYPE.Y;
            }
        }

        public bool Check_Winner()
        {
            if (Check_Rows() || Check_Columns() || Check_Diagonals())
            {
                return true;
            }
            return false;
        }

        private bool Check_Columns()
        {
            int x = 0;
            for (int y = 0; y < 3; y++)
            {
                if ((board[x, y].value == board[x + 1, y].value) &&
                    (board[x, y].value == board[x + 2, y].value) && (board[x, y].value != SQUARE_TYPE.EMPTY))
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        private bool Check_Rows()
        {
            int y = 0;
            for (int x = 0; x < 3; x++)
            {
                if ((board[x, y].value == board[x, y + 1].value) &&
               (board[x, y].value == board[x, y + 2].value) && (board[x, y].value != SQUARE_TYPE.EMPTY))
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        private bool Check_Diagonals()
        {
            int x = 0, y = 0;
            //check upperleft to lowerright diagonal
            if ((board[x, y].value == board[x + 1, y + 1].value) &&
                (board[x, y].value == board[x + 2, y + 2].value) && (board[x, y].value != SQUARE_TYPE.EMPTY))
            {
                return true;
            }
            //check lowerleft to upperright diagonal
            if ((board[x + 2, y].value == board[x + 1, y + 1].value) &&
                (board[x + 2, y].value == board[x, y + 2].value) && (board[x + 2, y].value != SQUARE_TYPE.EMPTY))
            {
                return true;
            }
            return false;
        }       

        public void Update_Player()
        {
            turn = turn == PLAYER.X? PLAYER.O:PLAYER.X;
        }

        public PLAYER Check_Player()
        {
            return turn;
        }

        public void Set_Turn(PLAYER player)
        {
            turn = player;
        }

        public void Update_turnCount()
        {
            turnCount--;
        }

        public bool Check_For_Draw()
        {
            return (turnCount == 0);
        }

        //public void Clear_Board(TCPConnection connection)
        public void Clear_Board()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y].value = SQUARE_TYPE.EMPTY;
                }
            }
            /*
            if (connection.isHost) //host is player X
            {
                    myCharacter = PLAYER.X;
            }
            
            */
            turnCount = 8;
        }

        public PLAYER Who_Goes_First()
        {
            Random random = new Random();
            int num = random.Next(0, 2);
            if (num == 0)
            {
                turn = PLAYER.X;
            }
            else
            {
                turn = PLAYER.O;
            }
            return turn;
        }
    }
}
