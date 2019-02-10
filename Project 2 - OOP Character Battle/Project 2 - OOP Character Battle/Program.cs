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

            if (turn == 1) {
                MOVE move = turnMenu(turn);
            }
            for (; ; )
            {


                if (move == MOVE.MOVEANDATTACK && turn == 1) {
                    player1.position = executeMove(player1, player2);
                    player2.health = executeAttack(player1, player2);
                }
                else if (move == MOVE.MOVEANDATTACK && turn == 2) {
                    player2.position = executeMove(player2, player1);
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

                Console.Write("1. Warrior \n2. Mage \n3. Archer\nPlayer {0} input integer 1-3 to select character: ", playerOrder);
                int player = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");
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

        static void printBoard(int onePosition, int twoPosition) {

            for (int i = 1; i <= boardLength ; i++) {

                if (i == onePosition) {
                    Console.Write("1");
                }
                else if (i == twoPosition) {
                    Console.Write("2");
                }
                else {
                    Console.Write("-");
                }

            }
            Console.WriteLine("\n");
        }

        enum MOVE {MOVEANDATTACK, SPECIAL }
        static MOVE turnMenu(int whichPlayer, Character attacker) {

            for (; ; )
            {
                Console.Write("1. Move & Attack ({0}) \n2. Special ({1})\nPlayer {2} enter 1 or 2 for your move: ", attacker.GetMovementDescription(), attacker.GetSpecialDescription(), whichPlayer);
                int answer = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");
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

        static int executeMove(Character currentPLayer, Character defender) {

            Console.Write("Enter move distance less than abs value {0}. \nEnter negative integer for move left. Enter positive integer for move right: ", currentPLayer.moveSpeed);
            int distance = Convert.ToInt32(Console.ReadLine());
            //ask for valid move distance
            while (Math.Abs(distance) > currentPLayer.moveSpeed) {
                Console.Write("Invalid. Enter move distance less than abs value {0}. \nEnter negative integer for move left. Enter positive integer for move right:", currentPLayer.moveSpeed);
                distance = Convert.ToInt32(Console.ReadLine());
            }
            //if movement stays within boundaries of the board
            if (currentPLayer.position + distance > 0 && currentPLayer.position + distance <= 50) {

                //if attacker is left of defender and movement plants attacker on defender's location, place attacker one spot left of defender
                if (currentPLayer.position < defender.position && currentPLayer.position + distance == defender.position) {
                    currentPLayer.position = defender.position - 1;
                }
                //if attacker is right of defender and movement plants attacker on defender's location, place attacker one spot right of defender
                else if (currentPLayer.position > defender.position && currentPLayer.position + distance == defender.position) {
                    currentPLayer.position = defender.position + 1;
                }
                //otherwise move current player desired distance
                else {
                    currentPLayer.position = currentPLayer.position + distance;
                }   
            }
            Console.WriteLine("");
            return currentPLayer.position;
        }

        static int executeAttack(Character attacker, Character defender) {

            //check if attack is within range
            
            if (attacker.attackRange < Math.Abs(attacker.position - defender.position)) {
                printOutOfRange("");
            }
            else {
                defender.health = defender.health - attacker.damagePerAttack;
                Console.WriteLine("Attack was successful! Defender's health is now {0}", defender.health);
            }
            return defender.health;
        }

        enum CLASS { ARCHER, WARRIOR, MAGE}

        static void executeSpecialAttack(ref Character attacker, ref Character defender) {
 
            switch (attacker.characterType) {

                case "ARCHER":
                    if (12 <= Math.Abs(attacker.position - defender.position)) {
                        printOutOfRange("Must be within range of 12");
                        printkAttackSuccess(attacker, defender, false);
                    }
                    else {
                        defender.health -= 10;
                        printkAttackSuccess(attacker, defender, true);
                    }
                    break;

                case "WARRIOR":
                    //if within 8 spaces of defender
                    if (Math.Abs(attacker.position - defender.position) <= 8) {
                        //if attacker is to the left of the defender leap right up to 1 spot left of defender
                        if (attacker.position < defender.position) {
                            attacker.position = defender.position - 1;
                        }
                        //else attacker is to the right of the defender leap left up to 1 spot right of the defender
                        else {
                            attacker.position = defender.position + 1;
                        }
                        defender.health -= 30;
                        printkAttackSuccess(attacker, defender, true);
                    }
                    //if outside of 8 spaces of defender
                    else {
                        //attacker is left of defender
                        if (attacker.position < defender.position) {
                            attacker.position += 8;
                        }
                        //attacker is right of defender
                        else {
                            attacker.position -= 8;
                        }
                        //if attacker is within range of 5 after leap 
                        if (Math.Abs(attacker.position - defender.position) <= 5) {
                            defender.health -= 30;
                            printkAttackSuccess(attacker, defender, true);
                        }
                        //if attacker is out of range of 5 after leap
                        else {
                            printOutOfRange("Must be within range of 5.");
                            printkAttackSuccess(attacker, defender, false);
                        }
                    }
                    break;

                case "MAGE":

                    //deal damage if within range
                    if (Math.Abs(attacker.position - defender.position) <= 3)
                    {
                        defender.health -= 3;
                        printkAttackSuccess(attacker, defender, true);

                        //handle cases when attacker is to the left of the defender
                        if (attacker.position < defender.position)
                        {
                            //if defender is nearly at right edge of board
                            if (defender.position >= 46)
                            {
                                defender.position = 50;
                            }
                            //if not at edge of board push right 4 spaces
                            else
                            {
                                defender.position += 4;
                            }
                        }
                        //handle cases when attacker is to the right of the defender
                        else
                        {
                            //if defender is nearly at left edge of board
                            if (defender.position <= 4)
                            {
                                defender.position = 1;
                            }
                            else
                            {
                                defender.position -= 4;
                            }
                        }
                        Console.WriteLine("Attacker pushed the defender back 4!");
                    }

                    //if attacker out of range value 3 then attack fails
                    else {
                        Console.Write("Out of range! Special attack range = 3. ");
                        printkAttackSuccess(attacker, defender, false);
                    }
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

        static void printOutOfRange(string classSpecialRange) {
            Console.Write("Out of range! {0} ", classSpecialRange);
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
