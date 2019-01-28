using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1___Basic_Menu_Loop {

    class Program {

        static void Main(string[] args) {

            DisplayMenu();
           
        }


        static void DisplayMenu() {
            int selection = 0;

            while (selection != 5) {

                Console.Write("Please select one of the five following options: \n" + 
                "1. Convert a fahrenheit value to celsius \n" +
                "2. Calculate the volume of a sphere\n" +
                "3. Print values between 0 and n that are multiples of 3 and 5\n" +
                "4. Determine if a user inputted string is a palindrome\n" +
                "5. Exit program\n\n");

                selection = Convert.ToInt32(Console.ReadLine());   //https://stackoverflow.com/questions/24443827/reading-an-integer-from-user-input

                while (selection < 1 || selection > 5) {
                    Console.Write("Invalid Input. Please select one of the five following options: \n" +
                    "1. Convert a fahrenheit value to celsius \n" +
                    "2. Calculate the volume of a sphere\n" +
                    "3. Print values between 0 and n that are multiples of 3 and 5\n" +
                    "4. Determine if a user inputted string is a palindrome\n" +
                    "5. Exit program\n\n");

                    selection = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n");
                }

                switch (selection) {
                    case 1:
                        ConvertTemp();
                        break;
                    case 2:
                        SphereVolume();
                        break;
                    case 3:
                        Multiples();
                        break;
                    case 4:
                        Palindrome();
                        break;
                    default:
                        break;
                }
            }
        }

        static void ConvertTemp() {
            Console.Write("Please enter a value in Fahrenheit: ");
            double input = Convert.ToDouble(Console.ReadLine());
            double celsius = (input - 32.0) * (5.0 / 9.0);
            Console.WriteLine("\n{0} Fahrenheit is {1} Celsius\n", input, celsius);
        }

        static void SphereVolume() {
            Console.Write("Please enter the radius of a sphere: ");
            double input = Convert.ToDouble(Console.ReadLine());

            while (input < 0) {
                Console.Write("\nValue must be positive. Re-enter radius of a sphere: ");
                input = Convert.ToDouble(Console.ReadLine());
            }

            double pi = Math.PI; //https://www.dotnetperls.com/pi
            double volume = (4.0 / 3.0) * pi * Math.Pow(input, 3);  //https://www.dotnetperls.com/math-pow
            Console.WriteLine("\nSphere Area: {0}\n", volume);
        }

        static void Multiples() {
            Console.Write("Please ener an integer greater than zero: ");
            int input = Convert.ToInt32(Console.ReadLine());

            while (input < 0) {

                Console.Write("\nInteger must be positive. Re-enter positive integer: ");
                input = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine();

            for (int i = 1; i < input; i++) {
                if (i%3 == 0 || i%5 == 0) {
                    Console.Write("{0}, ", i);
                }
            }

            Console.WriteLine("\n");

        }

        static void Palindrome() {

            Console.Write("\nPLease enter a string to see wehther the string is a palindrome: ");
            string input = Console.ReadLine();

            //input.SequenceEqual(input.Reverse()) ? Console.WriteLine("Yes, that's a palindrome!") : Console.WriteLine("No, not a palindrome!");

            bool palindrome = input.SequenceEqual(input.Reverse()); //https://stackoverflow.com/questions/9790749/check-if-a-string-is-a-palindrome

            if (palindrome == true) {
                Console.WriteLine("Yes, that's a palindrome!\n");
            }
            else {
                Console.WriteLine("No, not a palindrome!\n");
            }

        }
    }
}
