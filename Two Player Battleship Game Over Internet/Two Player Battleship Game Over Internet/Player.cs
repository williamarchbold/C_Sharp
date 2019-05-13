using System;
using System.Linq;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public class Player
    {
        public Ships[] ships = new Ships[5];
        public Board board;
        public Board opponentsBoard;

        public Player()
        {
            ships[0] = new Submarine();
            ships[1] = new Battleship();
            ships[2] = new Destroyer();
            ships[3] = new Cruiser();
            ships[4] = new Carrier();

            var numberOfHits = ships.Sum(s => s.length);

            board = new Board(numberOfHits);
            opponentsBoard = new Board(numberOfHits);
        }

        public bool IsOver => IsLoser/*()*/ || IsWinner;

        /*public bool IsLoser
        {
            return ships.All(s => s.isSunk);
        }*/

        public bool IsLoser => board.AllShipsSunk;

        public bool IsWinner => opponentsBoard.AllShipsSunk;

        internal void ApplyResult(int row, int column, PlayerTurnResult result)
        {
            opponentsBoard.ApplyHitResult(row, column, result.Hit);
        }

        internal PlayerTurnResult ApplyOpponentsTurn(PlayerTurn turn)
        {
            return new PlayerTurnResult
            {
                TurnNumber = turn.TurnNumber,
                Hit = board.ApplyOpponentsTurn(turn),
            };
        }
    }
}