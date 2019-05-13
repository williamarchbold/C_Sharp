using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private TCPConnection _connection = new TCPConnection();

        static void Main(string[] args)
        {
            var game = new Battleship_Game();
            game.SetupGame().GetAwaiter().GetResult(); //Add GetAwaiter and GetResult to not exit game immediately. Exit safely. 
        }

        public Battleship_Game()
        {
            _myPlayer = new Player();
        }

        private int GetDigit(string purpose)
        {
            Console.WriteLine("Please enter a number between 1 and 10 for the {0}", purpose);
            var potentialDigit = Console.ReadLine();
            if (int.TryParse(potentialDigit, out var digit) && 0 < digit && digit <= 10)
            {
                return digit;
            }

            return GetDigit(purpose);
        }

        private PlayerTurnResult _lastHitOnMe;

        public async Task SetupGame()
        {
            while (await Menu(_myPlayer))
            {
                while (!IsOver)
                {
                    // play the game
                    Console.Clear();
                    if (_isMyTurn && _lastHitOnMe != null)
                    {
                        Console.WriteLine("The last attack was {0}", _lastHitOnMe.Hit ? "a hit!!" : "a miss!");
                    }
                    Console.WriteLine("Your board looks like");
                    _myPlayer.board.Print_Board();
                    Console.WriteLine("Your opponents board looks like:");
                    _myPlayer.opponentsBoard.Print_Board();

                    if (_isMyTurn)
                    {
                        Console.WriteLine("Its your turn, please enter a row and column to hit");
                        var row = GetDigit("Row");
                        var column = GetDigit("Column");

                        var turn = new PlayerTurn
                        {
                            TurnNumber = _turnNumber++,
                            IsHost = _isHost,
                            Column = column,
                            Row = row,
                        };
                        //_turns.Add(_turnNumber, turn);
                        var result = await _connection.SendTurn(turn);
                        _myPlayer.ApplyResult(turn.Row, turn.Column, result);
                    }
                    else
                    {
                        Console.WriteLine("Waiting for your opponent to strike!");
                        // wait for response
                        var turn = await _connection.OpponentsTurn();
                        // check to see if valid turn in TCP order
                        //_turns.Add(turn.TurnNumber, turn);
                        var result = _lastHitOnMe = _myPlayer.ApplyOpponentsTurn(turn);
                        _connection.CompleteOpponentsTurn(result);

                        _myPlayer.board.Print_Board();
                    }

                    _isMyTurn = !_isMyTurn;
                }
                if (_myPlayer.IsWinner)
                {
                    Console.WriteLine("Game over. You win!");
                }
                if (_myPlayer.Is_Loser())
                {                   
                    Console.WriteLine("Game over. You lose!");                    
                }
                
            }
        }

        public bool IsOver => _myPlayer.IsOver;



        public async Task<bool> Menu(Player player)
        {
            Console.Write("Would you like to play a round of battleship? (y/n): ");
            var _playGame = char.Parse(Console.ReadLine());
            if (_playGame == 'n')
            {
                return false;
            }

            await SetupConnection();

            var i = 0;
            foreach (Ships ship in player.ships)
            {
                //Place_Ship(player, ship);
                ship.column = i++;
                ship.row = 0;
                ship.direction = Direction.South;
                player.board.Place_Ship(ship);
            }

            return true;   
            
        }

        private async Task SetupConnection()
        {
            Console.WriteLine("Do you want to host?");
            var wantsToHost = DoesWantToHost(Console.ReadLine());
            if (wantsToHost)
            {
                Console.WriteLine("Setting up connection");
                _isHost = true;
                _isMyTurn = true;
                await _connection.Host_Game();
                return;
            }

            var addresss = GetIpAddress();
            await _connection.Join_Game(addresss);

            IPAddress GetIpAddress()
            {
                Console.WriteLine("Please enter the IP address of the host");
                var potentialIpAddress = Console.ReadLine();
                if (potentialIpAddress.Trim() == string.Empty)
                {
                    return IPAddress.Loopback;
                }

                if (IPAddress.TryParse(potentialIpAddress, out var address)) {
                    return address;
                }

                return GetIpAddress();
            }

            bool DoesWantToHost(string input)
            {
                var doesWantTo = new[] { "y", "yes" };
                return doesWantTo.Contains(input.Trim().ToLowerInvariant());
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
