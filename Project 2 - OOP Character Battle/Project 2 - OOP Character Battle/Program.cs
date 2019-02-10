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
            var player1 = menu(1); //var = type inference
            var player2 = menu(2);

            //board[22].occupant = player1;
            //board[27].occupant = player2;

            player1.position = 22;
            player2.position = 27;

            int turn = 1;
            if (player2.priority < player1.priority)
            {
                turn = 2;
            }
            else if (player1.priority == player2.priority) {
                Random rand = new Random();
                turn = rand.Next(1, 2);
            }

            printBoard(player1.position, player2.position);

            MOVE move = turnMenu(turn);

            for (; ; )
            {


                if (move == MOVE.MOVEANDATTACK && turn == 1) {
                    player1.position = executeMove(player1);
                    player2.health = executeAttack(player1, player2);
                }
                else if (move == MOVE.MOVEANDATTACK && turn == 2) {
                    player2.position = executeMove(player2);
                    player1.health = executeAttack(player2, player1);
                }
                else if (move == MOVE.SPECIAL && turn == 1) {
                    executeSpecialAttack(ref player1, ref player2);
                }
                else {
                    executeSpecialAttack(ref player2, ref player1);
                }


                printBoard(player1.position, player2.position);

                //check if a player is defeated
                if (isPlayerDefeated(player1, player2)) {
                    break;
                }

                turn = switchPlayerControl(turn);

                move = turnMenu(turn);


            }
            


        }



        static Character menu(int playerOrder)
        {
            for (; ; )
            {

                Console.WriteLine("Player {0} input integer 1-3 to select character: \n1. Warrior \n2. Mage \n3. Archer\n\n", playerOrder);
                int player = Convert.ToInt32(Console.ReadLine());
                switch (player)
                {
                    case 1:
                        return new Warrior();
                    case 2:
                        return new Mage();
                    case 3:
                        return new Archer();
                    default:
                        Console.WriteLine("error\n");
                        break;
                }
            }

        }

        static void printBoard(int onePosition, int twoPosition)
        {
            for (int i = 1; i <= boardLength ; i++)
            {
                if (i == onePosition)
                {
                    Console.Write("1");
                }
                else if (i == twoPosition)
                {
                    Console.Write("2");
                }
                else
                {
                    Console.Write("-");
                }

            }
            Console.WriteLine("\n");
        }

        enum MOVE {MOVEANDATTACK, SPECIAL }
        static MOVE turnMenu(int whichPlayer)
        {

            for (; ; )
            {
                Console.WriteLine("Player {0} enter 1 or 2 for your move: \n1. Move & Attack\n2. Special\n", whichPlayer);
                Console.WriteLine(whichPlayer);
                int answer = Convert.ToInt32(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        return MOVE.MOVEANDATTACK;
                    case 2:
                        return MOVE.SPECIAL;
                    default:
                        Console.WriteLine("");
                        break;
                }
            }
        }

        static int executeMove(Character currentPLayer)
        {
            Console.Write("Enter negative integer for move left. Enter positive integer for move right: ");
            int distance = Convert.ToInt32(Console.ReadLine());
            while (distance > currentPLayer.moveSpeed)
            {
                Console.Write("Invalid move distance. Enter negative integer for move left. Enter positive integer for move right:");
                distance = Convert.ToInt32(Console.ReadLine());
            }
            if (currentPLayer.position + distance > 0 && currentPLayer.position + distance <= 50)
            {
                currentPLayer.position = currentPLayer.position + distance;
            }
            Console.WriteLine("");
            return currentPLayer.position;
        }

        static int executeAttack(Character attacker, Character defender) {

            //check if attack is within range
            
            if (attacker.attackRange > Math.Abs(attacker.position - defender.position)) {
                printOutOfRange();
            }
            else {
                defender.health = defender.health - attacker.damagePerAttack;
                Console.WriteLine("Attack was successful! Defender's health is now {0}", defender.health);
            }
            return defender.health;
        }

        enum CLASS { ARCHER, WARRIOR, MAGE}

        static void executeSpecialAttack(ref Character attacker, ref Character defender) {
 
            switch (attacker.characterType)
            {
                case "ARCHER":
                    if (attacker.attackSpecialRange <= Math.Abs(attacker.position - defender.position)) {
                        printOutOfRange();
                    }
                    else {
                        defender.health -= 10;
                    }
                    break;

                case "WARRIOR":
                    if (Math.Abs(attacker.position - defender.position) <= 8) {
                        if (attacker.position < defender.position) {
                            attacker.position = defender.position - 1;
                        }
                        else {
                            attacker.position = defender.position + 1;
                        }
                        defender.health -= 30;
                        printkAttackSuccess(attacker, defender, true);
                    }
                    else {
                        if (attacker.position < defender.position) {
                            attacker.position += 8;
                        }
                        else {
                            attacker.position -= 8;
                        }
                        if (Math.Abs(attacker.position - defender.position) <= 5) {
                            defender.health -= 30;
                            printkAttackSuccess(attacker, defender, true);
                        }
                        else {
                            printkAttackSuccess(attacker, defender, false);
                        }
                    }
                    break;

                case "MAGE":

                    //deal damage if within range
                    if (Math.Abs(attacker.attackSpecialRange - defender.attackSpecialRange) < attacker.attackSpecialRange) {
                        defender.health -= 3; 
                        printkAttackSuccess(attacker, defender, true);
                    }
                    else {
                        printkAttackSuccess(attacker, defender, false);
                    }

                    //move defender back at most 4 places
                    if (attacker.position < defender.position) {
                        if (defender.position >= 46) {
                            defender.position = 50;
                        }
                        else {
                            defender.position += 4;
                        }
                    }
                    else {
                        if (defender.position <= 4) {
                            defender.position = 1;
                        }
                        else {
                            defender.position -= 4;
                        }
                    }
                    Console.WriteLine("Attacker pushed the defender back 4!");
                    break;

                default:
                    break;
            }

            //return defender.health;
        }

        static int switchPlayerControl(int turn) {

            if (turn == 1) {
                return turn = 2;
            }
            else {
                return turn = 1;
            }
        }

        static void printkAttackSuccess(Character attacker, Character defender, Boolean success) {

            if (success == true)
            {
                Console.WriteLine("Attacker's special move was successful! Defender's health is now {0}", defender.health);
            }
            else{
                Console.WriteLine("Attacker's special move unsuccessful! Defender's health is still {0}", defender.health);
            }
        }

        static void printOutOfRange() {
            Console.WriteLine("Out of range!");
        }

        static Boolean isPlayerDefeated(Character player1, Character player2) {

            if (player1.health <= 0) {
                Console.WriteLine("Player 1 defeated!\n");
                return true;
            }
            else if (player2.health <= 0) {
                Console.WriteLine("Player 2 defeated!\n");
                return true;
            }
            else {
                return false;
            }

        }
    }


}
