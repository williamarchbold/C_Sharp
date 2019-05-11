using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Player_Battleship_Game_Over_Internet
{
    class Board
    {
        private List<Coordinate> board;

        public Board()
        {
           board = new List<Coordinate>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board.Add(new Coordinate(i, j));
                }
            }
        }
        

        /*
        private Coordinate[,] board;

        public Board()
        {
            board = new Coordinate[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = new Coordinate();
                }
            }
        }
        */
        public void Print_Board(Player player)
        {

        }

    }
}
