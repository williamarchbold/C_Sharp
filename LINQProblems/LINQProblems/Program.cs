using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQProblems
{
    public class Program
    {
        static void Main(string[] args)
        {
            FindStringsInAThatArentInB();
            GetStraightAStudents();
            DetermineWhichItemWasMostProfitable();
            QueryPhoneBookEntries();
            GetAllPNG(@"C:\Users\Ryan\Pictures\"); //NOTE: READ SUMMARY COMMENT ABOVE METHOD
            //GetAllPNG(@"C:\Users\William Archbold\Desktop\FolderTree");
            /*var files = GetAllPNGLambda(@"C:\Users\William Archbold\Desktop\FolderTree");
            foreach (var thing in files)
            {
                Console.WriteLine("{0}", thing);
            }
            */
            GetFibonacciNumbers();
            /*
            foreach (var number in GetFibonacciNumbers())
                Console.WriteLine("{0}", number);
            foreach (var number in GetFiboLambda())
                Console.WriteLine("{0}", number);
            */

            SumPrimes();
            //Console.WriteLine("{0}", SumPrimesLambda());
            //Console.WriteLine("");
            //Console.ReadLine();

        }

        /// <summary>
        /// Result should equal any string in a that isn't also in b.
        /// Result = { "y", "n" }
        /// </summary>
        /*
        public static List<string> FindStringsInAThatArentInB()
        {
            List<string> a = new List<string>() { "r", "y", "a", "n" };
            List<string> b = new List<string>() { "d", "a", "r", "r", "a", "s" };
            List<string> result = new List<string>();

            for (int i = 0; i < a.Count; i++)
            {
                int exists = 0;
                for (int j = 0; j < b.Count; j++)
                {
                    if (b[j] == a[i])
                    {
                        exists = 1;
                    }
                }
                if (exists == 0)
                {
                    result.Add(a[i]);
                }
            }
            return result;
        }
        */

        public static List<string>/*IEnumerable<string>*/ FindStringsInAThatArentInB()
        {
            List<string> a = new List<string>() { "r", "y", "a", "n" };
            List<string> b = new List<string>() { "d", "a", "r", "r", "a", "s" };
            IEnumerable<string> result = new List<string>();
            //result = a.Where(f => b.Any(g => g.All != f.All);
            //result = a.Where(p => !b.Any());


            result = a.Except(b); //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-find-the-set-difference-between-two-lists-linq
            foreach (string letter in result)
            {
                Console.Write("{0}, ", letter);
            }
            return result.ToList();
        }




        /// <summary>
        /// Gets a subset of students that maintain all A's.
        /// Result = { 2, 6, 8 }
        /// </summary>
        /*
        public static List<Student> GetStraightAStudents()
        {
            List<Student> students = new List<Student>()
            {
                new Student(1, 87, 89, 91, 92),
                new Student(2, 90, 92, 93, 99),
                new Student(3, 87, 91, 97, 100),
                new Student(4, 86, 88, 93, 100),
                new Student(5, 86, 89, 94, 98),
                new Student(6, 93, 94, 99, 100),
                new Student(7, 87, 89, 95, 99),
                new Student(8, 91, 94, 95, 97),
                new Student(9, 85, 94, 99, 100),
                new Student(10, 85, 92, 93, 95),
            };

            List<Student> a_students = new List<Student>();

            foreach (Student s in students)
            {
                if (s.MathGrade >= 90.0
                    && s.HistoryGrade >= 90.0
                    && s.ScienceGrade >= 90.0
                    && s.EnglishGrade >= 90.0)
                        a_students.Add(s);
            }
            return a_students;
        }
        */

        public static List<Student>/*IEnumerable<Student>*/ GetStraightAStudents()
        {
            List<Student> students = new List<Student>()
            {
                new Student(1, 87, 89, 91, 92),
                new Student(2, 90, 92, 93, 99),
                new Student(3, 87, 91, 97, 100),
                new Student(4, 86, 88, 93, 100),
                new Student(5, 86, 89, 94, 98),
                new Student(6, 93, 94, 99, 100),
                new Student(7, 87, 89, 95, 99),
                new Student(8, 91, 94, 95, 97),
                new Student(9, 85, 94, 99, 100),
                new Student(10, 85, 92, 93, 95),
            };

            IEnumerable<Student> a_students = students.Where(p => p.EnglishGrade >= 90.0 && p.HistoryGrade >= 90 && p.ScienceGrade >= 90 && p.MathGrade >=90);

            foreach (Student a in a_students)
            {
                Console.Write("{0}, ", a.StudentID);
            }

            return a_students.ToList();

        }



        /// <summary>
        /// Determines which item profited the most
        /// </summary>
        /*
        public static Item DetermineWhichItemWasMostProfitable()
        {
            List<Item> items = new List<Item>()
            {
                new Item("TV", 5, 515.15f),
                new Item("DVD Player", 10, 100.05f),
                new Item("Toy Horse", 2, 25.25f),
                new Item("Shovel", 1, 10.99f),
                new Item("Kite", 4, 5.77f),
                new Item("Stapler", 15, 5.98f),
                new Item("Pen", 25, 2.25f),
                new Item("Candy Bar", 102, 1.5f),
                new Item("DVD", 45, 20),
                new Item("Soda", 66, 1.5f),
            };

            Item highestEarner = new Item("TEMP", 0, 0);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemPrice * items[i].NumberOfSales > highestEarner.ItemPrice * highestEarner.NumberOfSales)
                {
                    highestEarner = items[i];
                }
            }
            return highestEarner;
        }
        */

        public static Item DetermineWhichItemWasMostProfitable()
        {
            List<Item> items = new List<Item>()
            {
                new Item("TV", 5, 515.15f),
                new Item("DVD Player", 10, 100.05f),
                new Item("Toy Horse", 2, 25.25f),
                new Item("Shovel", 1, 10.99f),
                new Item("Kite", 4, 5.77f),
                new Item("Stapler", 15, 5.98f),
                new Item("Pen", 25, 2.25f),
                new Item("Candy Bar", 102, 1.5f),
                new Item("DVD", 45, 20),
                new Item("Soda", 66, 1.5f),
            };


            var pdq = items.Select(
                x => new { Profit = (x.ItemPrice * x.NumberOfSales), Item = x }); //create an anonymous type 

            var mostProfitable = pdq.OrderByDescending(x => x.Profit);
            var topItem = mostProfitable.ElementAt(0);

            return topItem.Item; //return type Item for just item name
        }

        /// <summary>
        /// Queries various things from a phone book
        /// </summary>
        /*
        public static Dictionary<string, List<PhoneBookEntry>> QueryPhoneBookEntries()
        {
            List<PhoneBookEntry> phoneBook = new List<PhoneBookEntry>()
            {
                new PhoneBookEntry("Sarah", "Jones", "1887 Flat Iron Court", "Colorado Springs", "CO", "80921", "(719) 354-1857"),
                new PhoneBookEntry("Josh", "Jones", "1887 Flat Iron Court", "Colorado Springs", "CO", "80921", "(719) 354-1855"),
                new PhoneBookEntry("Bryan", "Adams", "1665 Snowflake Lane", "Boston", "MA", "02101", "(617) 143-1566"),
                new PhoneBookEntry("John", "Smith", "4745 Meadowland Blvd", "San Diego", "CA", "22434", "(619) 354-6543"),
                new PhoneBookEntry("Josh", "Jackson", "1145 Piros Drive", "Orlando", "FL", "32789", "(407) 650-8333"),
                new PhoneBookEntry("Hannah", "Maben", "1710 Main Street", "Boston", "MA", "02101", "(617) 765-1857"),
                new PhoneBookEntry("Harrison", "James", "1010 Maple Lane", "Denver", "CO", "80014", "(720) 123-4567"),
                new PhoneBookEntry("Xavier", "Carlyle", "1552 Washington Avenue", "San Diego", "CA", "22434", "(619) 987-5465"),
                new PhoneBookEntry("Michael", "Jones", "6510 Cherry Creek Lane", "Springfield", "TX", "75853", "(361) 234-985"),
                new PhoneBookEntry("Sarah", "Smith", "1223 Mirage Drive", "Springfield", "TX", "75853", "(361) 127-5643"),
            };

            Dictionary<string, List<PhoneBookEntry>> results = new Dictionary<string, List<PhoneBookEntry>>();


            results.Add("Name", new List<PhoneBookEntry>());
            foreach (PhoneBookEntry entry in phoneBook)
            {
                if (entry.Name == "Josh Jackson")
                    results["Name"].Add(entry);
            }

            results.Add("LastName", new List<PhoneBookEntry>());
            foreach (PhoneBookEntry entry in phoneBook)
            {
                if (entry.LastName == "Jones")
                    results["LastName"].Add(entry);
            }

            results.Add("City", new List<PhoneBookEntry>());
            foreach (PhoneBookEntry entry in phoneBook)
            {
                if (entry.City == "Colorado Springs")
                    results["City"].Add(entry);
            }

            results.Add("PhoneAreaCode", new List<PhoneBookEntry>());
            foreach (PhoneBookEntry entry in phoneBook)
            {
                if (entry.PhoneNumber.Substring(0, 3) == "617")
                    results["PhoneAreaCode"].Add(entry);
            }
            return results;
        }
        */

        public static Dictionary<string, List<PhoneBookEntry>> QueryPhoneBookEntries()
        {
            List<PhoneBookEntry> phoneBook = new List<PhoneBookEntry>()
            {
                new PhoneBookEntry("Sarah", "Jones", "1887 Flat Iron Court", "Colorado Springs", "CO", "80921", "(719) 354-1857"),
                new PhoneBookEntry("Josh", "Jones", "1887 Flat Iron Court", "Colorado Springs", "CO", "80921", "(719) 354-1855"),
                new PhoneBookEntry("Bryan", "Adams", "1665 Snowflake Lane", "Boston", "MA", "02101", "(617) 143-1566"),
                new PhoneBookEntry("John", "Smith", "4745 Meadowland Blvd", "San Diego", "CA", "22434", "(619) 354-6543"),
                new PhoneBookEntry("Josh", "Jackson", "1145 Piros Drive", "Orlando", "FL", "32789", "(407) 650-8333"),
                new PhoneBookEntry("Hannah", "Maben", "1710 Main Street", "Boston", "MA", "02101", "(617) 765-1857"),
                new PhoneBookEntry("Harrison", "James", "1010 Maple Lane", "Denver", "CO", "80014", "(720) 123-4567"),
                new PhoneBookEntry("Xavier", "Carlyle", "1552 Washington Avenue", "San Diego", "CA", "22434", "(619) 987-5465"),
                new PhoneBookEntry("Michael", "Jones", "6510 Cherry Creek Lane", "Springfield", "TX", "75853", "(361) 234-985"),
                new PhoneBookEntry("Sarah", "Smith", "1223 Mirage Drive", "Springfield", "TX", "75853", "(361) 127-5643"),
            };

            Dictionary<string, List<PhoneBookEntry>> results = new Dictionary<string, List<PhoneBookEntry>>();

            results.Add("Name", new List<PhoneBookEntry>());
            results["Name"].AddRange(phoneBook.Where(x => x.Name == "Josh Jackson"));

            results.Add("LastName", new List<PhoneBookEntry>());
            results["LastName"].AddRange(phoneBook.Where(x => x.LastName == "Jones"));

            results.Add("City", new List<PhoneBookEntry>());
            results["City"].AddRange(phoneBook.Where(x => x.City == "Colorado Springs"));

            results.Add("PhoneAreaCode", new List<PhoneBookEntry>());
            results["PhoneAreaCode"].AddRange(phoneBook.Where(x => x.PhoneNumber.Substring(0,3) == "617"));

            return results;
        }


        /// <summary>
        /// HW 3. Finds all jpgs in the given path
        /// 
        /// You will need to change your path in multiple locations to test this.
        /// 1. In Main of this file.
        /// 2. In GetAllPNG_Test in LINQProblems_Test_Cases.cs
        /// 
        /// PLEASE CHANGE BOTH BACK TO @"C:\Users\Ryan\Pictures\" WHEN YOU FINALIZE AND SUMBIT!!!!
        /// Hints for this problem:
             //* new DirectoryInfo(path).EnumerateDirectories("*", SearchOption.AllDirectories);
             //* directory.EnumerateFiles("*.png")
             //* 
             //* However, the enumerated method above might throw exceptions on files that cannot be accessed.
             //* To solve this, just test on a path that you know is safe.
             //</summary>
        /*
        public static List<FileInfo> GetAllPNG(string path)
        {
            

            List<FileInfo> files = new List<FileInfo>();
            foreach (DirectoryInfo directory in new DirectoryInfo(path).GetDirectories())
            {
                files.AddRange(GetAllPNG(directory.FullName));
            }

            foreach (FileInfo file in new DirectoryInfo(path).GetFiles("*.png"))
            {
                files.Add(file);
            }
            return files;
        }
        */

        public static List<FileInfo>/*IEnumerable<FileInfo>*/ GetAllPNG(string path)
        {
            IEnumerable<FileInfo> unions = new List<FileInfo>();


            // create new directory info path then GetDirectories give an IEnumerable and then to use ForEach needed ToList
            // then for each directory form union on old path to new path when recursively calling method. Adding curly braces
            // makes it more clear not returning anything to ForEach
            new DirectoryInfo(path).GetDirectories().ToList().ForEach(directory => { unions = unions.Union(GetAllPNG(directory.FullName)); });

            return unions.Union(new DirectoryInfo(path).GetFiles()).Where(file => file.Extension == ".png").ToList();
        }

        /// <summary>
        /// Gets the fibonacci numbers for a list of integers
        /// </summary>
        /*
        public static List<int> GetFibonacciNumbers()
        {
            List<int> input = new List<int>();
            for (int i = 1; i < 40; i += 5)
                input.Add(i);
            List<int> results = new List<int>();
            
            foreach(int i in input)
            {
                results.Add(Fibonacci(i));
            }
            
            //Local function for recursive search
            int Fibonacci(int i)
            {
                if (i == 1 || i == 2)
                    return 1;
                return Fibonacci(i - 1) + Fibonacci(i - 2);
            }
            return results;
        }
        */

        /*
         * I used https://www.c-sharpcorner.com/code/1568/fibonacci-series-using-linq-in-c-sharp.aspx as the basis for the answer but made modifications
         */
        public static List<int>/*IEnumerable<int>*/ GetFibonacciNumbers()
        {
            int first = 0, second = 1;
            return Enumerable.Range(1, 40).Select(a =>
            {
                int value = first + second;
                first = second;
                second = value;
                
                return new { i = a, fib = first }; //this is returning an anonymous object that will return the iterator and the fib value at index a

            }).Where(x => (x.i - 1) % 5 == 0).Select(x => x.fib).ToList(); //srtip off index number from anonymous type and return just the fib value, but only 
            //where the fib index is divisible by 6. Lastly convert to list to accomodate test

        }

        /// <summary>
        /// Finds and sums the values of all primes between 2 and 1,000
        /// Result = 76,127
        /// </summary>
        /*
        public static int SumPrimes()
        {
            int primeSum = 0;
            for (int i = 2; i < 1000; i++)
            {
                bool isPrime = true;
                for (int c = 2; c < i / 2 + 1; c++)
                {
                    if (i % c == 0)
                    {
                        isPrime = false;
                    }
                }
                if (isPrime == true)
                {
                    primeSum += i;
                }
            }
            return primeSum;
        }
        */
        
        
        /* <p>
         * Used http://codinghelmet.com/articles/linq-all-primes as basis for method, which uses an enumeration loop
         * nested inside another enumeration loop, but I changed the range in the outer loop and added the .Sum at the end
         * </P>
         */
        public static int SumPrimes()
        {
            return
            Enumerable.Range(2, 999)
            .Where(number =>
                Enumerable.Range(2, (int)Math.Sqrt(number) - 1)
                .All(divisor => number % divisor != 0))
                    .Sum(x => x);

        }

    }


}
