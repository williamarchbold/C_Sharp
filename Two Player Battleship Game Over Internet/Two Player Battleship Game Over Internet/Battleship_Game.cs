using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Player_Battleship_Game_Over_Internet
{
    public class InitialGameState
    {
        public bool HostIsGoingFirst { get; set; }
    }

    public class PlayerTurn
    {
        public int TurnNumber { get; set; }
        public bool IsHost { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class PlayerTurnResult
    {
        public int TurnNumber { get; set; }
        public bool Hit { get; set; }
    }

    class Battleship_Game
    {
        private Player _myPlayer;
        private IDictionary<int, PlayerTurn> _turns = new Dictionary<int, PlayerTurn>();
        private bool _isHost;
        private bool _isMyTurn;
        private int _turnNumber;
        private TCPConnection _connection;

        static void Main(string[] args)
        {
            var game = new Battleship_Game();
            game.SetupGame();
        }

        public Battleship_Game()
        {
            _myPlayer = new Player();
        }

        public async Task SetupGame()
        {
            Menu(_myPlayer);
            // de ide if host

            while (!IsOver)
            {
                // play the game
                if (_isMyTurn)
                {
                    var turn = new PlayerTurn
                    {
                        TurnNumber = _turnNumber++,
                        IsHost = _isHost,
                        Column = 0,
                        Row = 0,
                    };
                    _turns.Add(_turnNumber, turn);
                    var result = await  _connection.SendTurn(turn);
                    _myPlayer.ApplyResult(result);
                }
                else
                {
                    // wait for response
                    var turn = await _connection.OpponentsTurn();
                    // check to see if valid turn in TCP order
                    _turns.Add(turn.TurnNumber, turn);
                    var result = _myPlayer.ApplyOpponentsTurn(turn);
                    _connection.CompleteOpponentsTurn(result);
                }
            }

            // Display winner/ loser;
        }

        public bool IsOver => _myPlayer.IsOver;



        public void Menu(Player player)
        {
            //Do you want to play a game?
            //User selects yes or no

            foreach (Ships ship in player.ships)
            {
                Place_Ship(player, ship);
            }

            
            
        }

        static public void Place_Ship(Player player, Ships ship)
        {
            player.board.Print_Board();
            Console.WriteLine("\nSelect the row number for the bow (front) of your {0}: ", ship.name);
            ship.row = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("\nSelect the column number for the bow (front){0}: ", ship.name);
            ship.column = int.Parse(Console.ReadLine()) - 1;
            ship.direction = Orientation_Menu(ship);
            if (player.board.Check_Ship(ship))
            {
                player.board.Place_Ship(ship);
            }
            else
            {
                Console.WriteLine("Invalid input");
                Place_Ship(player, ship);
            }
            
        }

        static public Direction Orientation_Menu(Ships ship)
        {
            Console.WriteLine("Select orientation of your {0}. \n1.North\n2.South\n3.East\n4.West\n", ship.name);
            
            if (Enum.TryParse(Console.ReadLine(), out Direction direction))
            {
                return ship.direction = direction;
            }
            else
            {
                
                return Orientation_Menu(ship);
            }

        }


    }
}
