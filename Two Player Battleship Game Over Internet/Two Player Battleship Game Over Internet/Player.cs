using System;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public class Player
    {
        Ships[] ships = new Ships[5];
        Board board;
        Board opponentsBoard;

        public Player()
        {
            ships[0] = new Submarine();
            ships[1] = new Battleship();
            ships[2] = new Destroyer();
            ships[3] = new Cruiser();
            ships[4] = new Carrier();

            board = new Board();
            opponentsBoard = new Board();
        }

        public bool Is_Loser()
        {
            return ships[0].isSunk == ships[1].isSunk == ships[2].isSunk == ships[3].isSunk == ships[4].isSunk == ships[5].isSunk == true ? true : false;
        }

        public void Place_Ships()
        {
            foreach(Ships ship in ships)
            {
                bool isFree = true;
                Console.WriteLine("Select orientation of your {0} (h for horizontal or v for vertical: ", ship.name);
                char orientation = Char.Parse(Console.ReadLine());
                Console.WriteLine("\nSelect the row number for the bow (front): ");
                int row = int.Parse(Console.ReadLine());
                Console.WriteLine("\nSelect the column number for the bow (front): ");
                int column = int.Parse(Console.ReadLine());
                Coordinate cord = new Coordinate();
                if (orientation == 'h')
                /*{
                    for (int i = 0; i < ship.length; i++)
                    {
                        if (board[cord, cord])
                    {

                        }
                    }
                }*/
                {

                }

                
                while ((row < 1 || row > 10))
                {
                    Console.WriteLine("Invalid entry. Try again");
                    row = int.Parse(Console.ReadLine());
                }
               


            }
        }


    }
}