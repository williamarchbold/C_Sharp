using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project_5_LINQ_and_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Dictionary<string, string>> media = new List<Dictionary<string, string>>();

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //using (StreamReader sr = new StreamReader(@"C: \Users\William Archbold\Desktop\CS 3020 C#.NET\IMDB-Movie-Data.txt"))
            using (StreamReader sr = new StreamReader(currentDirectory + "\\IMDB-Movie-Data.txt"))
            {
                if (!sr.EndOfStream)
                {
                    string firstLine = sr.ReadLine();
                    string[] columnNames = firstLine.Split('\t'); //split deliminator is TAB button
                    while (!sr.EndOfStream)
                    {
                        string dataLine = sr.ReadLine();
                        string[] rowValues = dataLine.Split('\t'); //split deliminator is TAB button
                        Dictionary<string, string> row = new Dictionary<string, string>();
                        for (int i = 0; i < rowValues.Length; i++)
                        {
                            row.Add(columnNames[i], rowValues[i]); // columnnames, whole row values
                        }
                        media.Add(row);
                    }


                }
                Console.WriteLine("Presenting movie titles for year 2010");

                IEnumerable<Dictionary<string, string>> answers = getData(media, "2010");

                int j = 0;
                foreach (var item in answers)
                {
                    foreach (var value in item)
                    {
                        Console.Write(value.Value);
                        Console.Write('\t');
                    }
                    Console.WriteLine();
                    j++;

                }

                Console.WriteLine("{0} System pause..", j);


                IEnumerable<Dictionary<string, string>> answerLinq = getDataLinq(media, "2010");
                int k = 0;
                foreach (var item in answerLinq)
                {
                    foreach (var value in item)
                    {
                        Console.Write(value.Value);
                        Console.Write('\t');
                    }
                    Console.WriteLine();
                    k++;
                }


                Console.WriteLine("{0} {1} System pause..", k, j);
                Console.ReadLine();


            }
            


        }
        static IEnumerable<Dictionary<string, string>> getData(List<Dictionary<string, string>> media, string year)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            foreach (var row in media)
            {
                if (row["Year"] == year)
                {
                    result.Add(row);
                }
            }

            return result;

        }

        static IEnumerable<Dictionary<string, string>> getDataLinq(List<Dictionary<string, string>> media, string year)
        {
            //return media.Where(t => t["Year"] == year);//.Select(t => t); // => is lambda that checks boolean for whether row will be in or out 

            return media.Where(t => t["Year"] == year).Select(t => new Dictionary<string, string>() {{ "Title", t["Title"] } });
        }

    }
}
