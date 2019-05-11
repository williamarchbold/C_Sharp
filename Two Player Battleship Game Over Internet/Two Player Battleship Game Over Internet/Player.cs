using System;
using System.Linq;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public class Player
    {
        public Ships[] ships = new Ships[5];
        public Board board;
        Board opponentsBoard;

        public Player()
        {
            ships[0] = new Submarine();
            ships[1] = new Battleship();
            ships[2] = new Destroyer();
            ships[3] = new Cruiser();
            ships[4] = new Carrier();

            var numberOfHits = ships.Sum(s => s.hits);

            board = new Board(numberOfHits);
            opponentsBoard = new Board(numberOfHits);
        }

        public bool IsOver => Is_Loser() || IsWinner;

        public bool Is_Loser()
        {
            return ships[0].isSunk == ships[1].isSunk == ships[2].isSunk == ships[3].isSunk == ships[4].isSunk == ships[5].isSunk == true ? true : false;
        }

        public bool IsWinner => opponentsBoard.AllShipsSunk;

        internal void ApplyResult(PlayerTurnResult result)
        {
            // update opponents board
            throw new NotImplementedException();
        }

        internal PlayerTurnResult ApplyOpponentsTurn(PlayerTurn turn)
        {
            return new PlayerTurnResult
            {
                TurnNumber = turn.TurnNumber,
                Hit = false,
            };
        }
    }
}