using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    class RecursiveFileFinder
    {
        static void Main(string[] args) {

            //string[] directories = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"FolderTree");

            string path = AppDomain.CurrentDomain.BaseDirectory + @"FolderTree";

            //using (StreamReader sr = new StreamReader(path)) {
            string result = findFile(path);
            Console.WriteLine("File location: {0}", result);
            //}
            Console.WriteLine("Type any key to continue...");
            Console.ReadKey();

            

        }

        static string findFile(string path) {
            foreach (DirectoryInfo directory in new DirectoryInfo(path).GetDirectories())
            {
                findFile(path + "\\" + directory);
                //path = findFile(path + "\\" + directory); tried this because i didn't do anything with the result of findFIle(), but VisualStudio didn't like it

                foreach (FileInfo file in new DirectoryInfo(directory.FullName).GetFiles())
                {
                    path+= "\\" + file;
                    return path;
                    
                }
            }
            return path;
        }
    }
}
