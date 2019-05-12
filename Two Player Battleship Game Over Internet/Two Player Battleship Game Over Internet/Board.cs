using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public class Board
    {       
        public Coordinate[,] board;
        private int _numberOfHits;

        public Board(int numberOfHits)
        {
            _numberOfHits = numberOfHits;
            board = new Coordinate[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    board[i, j] = new Coordinate(i,j);
                }
            }
        }
        
        public void Print_Board()
        {
            Console.Write("     ");
            for (int i = 1; i < 11; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            int _RowTracker = 0;
            for (int row = 0; row < 10; row++)
            {                
                for (int column = 0; column < 10; column++)
                {
                    if (column == 0)
                    {
                        if (row !=9)
                        {
                            Console.Write("  " + (++_RowTracker + "  "));
                        }
                        else
                        {
                            Console.Write("  " + (++_RowTracker + " "));
                        }                       
                    }
                    Console.Write(board[row,column] + " ");
                }
                Console.WriteLine();
            }
        }

        internal void ApplyHitResult(int row, int column, bool hit)
        {
            var coordinate = board[row - 1, column - 1];
            if (hit)
            {
                coordinate.WasHit();
            }
            else
            {
                coordinate.WasMiss();
            }
        }

        internal bool ApplyOpponentsTurn(PlayerTurn turn)
        {
            var coordinate = board[turn.Row - 1, turn.Column - 1];
            return coordinate.Hit();
        }

        public bool AllShipsSunk
        {
            get
            {
                var hits = 0;
                for (var row = 0; row < 10; row++)
                {
                    for (var column = 0; column < 10; column++)
                    {
                        if (board[row, column].value == VALUE.Hit)
                        {
                            hits++;
                        }

                    }
                }

                return hits == _numberOfHits;
            }
        }


        public bool Check_Ship(Ships ship)
        {

            for (int i = 0; i < ship.length; i++)
            {
                Coordinate candidatePosition = Iterate_Directions(ship, i); 
                
                if (candidatePosition?.Is_Occupied ?? true) //if candidate position is null ignore the rest of the statement
                {
                    Console.WriteLine("\nCan't place ship here.");
                    return false;
                }

            }
            return true;
        }

        public void Place_Ship(Ships ship)
        {
            for (int i = 0; i < ship.length; i++)
            {
                Coordinate candidatePosition = Iterate_Directions(ship, i);
                candidatePosition.value = ship.value;

            }
        }

        public Coordinate Iterate_Directions(Ships ship, int i)
        {
            try
            {
                switch (ship.direction)
                {
                    case Direction.North:
                        return board[ship.row - i, ship.column];

                    case Direction.South:
                        return board[ship.row + i, ship.column];

                    case Direction.East:
                        return board[ship.row, ship.column + i];

                    case Direction.West:
                        return board[ship.row, ship.column - i];

                    default:
                        return null;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        
               


    }
}
