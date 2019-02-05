using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2___OOP_Character_Battle
{
    class Program
    {
        static readonly int boardLength = 50;
        static void Main(string[] args)
        {
            //Tile[] board = new Tile[50];
            var player1 = menu(); //var = type inference
            var player2 = menu();

            //board[22].occupant = player1;
            //board[27].occupant = player2;

            player1.position = 22;
            player2.position = 27;

            int turn = 1;
            if (player2.priority < player1.priority)
            {
                turn = 2;
            }

            for (; ; )
            {
                printBoard(player1.position, player2.position);
                //print which player's turn it is
                var move = turnMenu();
                

            }
            


        }



        static Character menu()
        {
            for (; ; )
            {

                Console.WriteLine("Player 1 select character: ");
                string player = Console.ReadLine();
                switch (player)
                {
                    case "Warrior":
                        return new Warrior();
                    case "Mage":
                        return new Mage();
                    case "Archer":
                        return new Archer();
                    default:
                        Console.WriteLine("error");
                        break;
                }
            }

        }

        static void printBoard(int onePosition, int twoPosition)
        {
            for (int i = 0; i < boardLength ; i++)
            {
                if (i == onePosition)
                {
                    Console.WriteLine("1");
                }
                else if (i == twoPosition)
                {
                    Console.WriteLine("2");
                }
                else
                {
                    Console.WriteLine("-");
                }

            }
        }

        enum MOVE {MOVEANDATTACK, SPECIAL }
        static MOVE turnMenu()
        {

            for (; ; )
            {
                Console.WriteLine("Selection");
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "string":
                        return MOVE.MOVEANDATTACK;
                    case "string":
                        return MOVE.SPECIAL;
                    default:
                        Console.WriteLine("");
                        break;
                }
            }
        }


    }


}
