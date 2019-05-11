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


    }
}