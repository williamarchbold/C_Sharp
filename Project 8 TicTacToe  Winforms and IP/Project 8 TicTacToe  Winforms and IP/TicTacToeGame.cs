using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_8_TicTacToe__Winforms_and_IP
{
    public enum TURN { Player1, Player2 };

    public class TicTacToeGame
    {

        //private TicTacToeBoard board;

        private TicTacToeSquare[,] board = new TicTacToeSquare[3, 3];

        private int turnCount { get; set; }
        private TURN turn { get; set; }

        public TicTacToeGame()
        {
            turnCount = 9;
            turn = TURN.Player1;
        }

        public void Update_Board(object button)
        {
            string [] locationArray = button.ToString().Split(new string[] { " " }, StringSplitOptions.None);
            int row = Convert.ToInt32(locationArray[0]) - 1;
            int column = Convert.ToInt32(locationArray[1]) - 1;
            if (turn == TURN.Player1)
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
            if (Check_Rows() || Check_Rows() || Check_Diagonals())
            {
                return true;
            }
            return false;
        }

        private bool Check_Rows()
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

        private bool Check_Columns()
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
            turn++;
        }

        public TURN Check_Player()
        {
            return turn;
        }

        public void Update_turnCount()
        {
            turnCount--;
        }

        public bool Check_For_Draw()
        {
            return (turnCount == 0);
        }

        public void Clear_Board()
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    board[x, y].value = SQUARE_TYPE.EMPTY;
                }
            }

            Random random = new Random();
            int num = random.Next(2);
            if (num == 0)
            {
                turn = TURN.Player1;
            }
            else
            {
                turn = TURN.Player2;
            }                 
        }
    }
}
