using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3_IO_and_Recursion
{
    class FileFinder
    {
        static void Main(string[] args) {

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Found Files";
            //currentDirectory = "Found Files";
            //Directory.CreateDirectory(currentDirectory);
            //Directory.SetCurrentDirectory(currentDirectory);
            //currentDirectory = ".";
            Console.Write("Please enter types of files you want searched with spaces(.jpg .pdf): ");
            string typesToSearch = Console.ReadLine();
            List<string> stringTypeElements = typesToSearch.Split(' ').ToList();

            Console.WriteLine("\n");

            List<string> stringDirectoryElements = new List<string>();
            
            while (true) {
                Console.Write("Please enter the directories you want to search one per line each path.\n" +
                    "Recommend searching trees with shorter paths i.e. desktop folders.\n" +
                    "Press return when last path is entered: ");
                string searchDirectory = Console.ReadLine();
                if (searchDirectory == "") {
                    break;
                }
                else {
                    stringDirectoryElements.Add(searchDirectory);
                }
            }






            //using (StreamReader sr = new StreamReader(path)) {
            int count = 0;
            foreach (var directory in stringDirectoryElements) {
                count += findFile(stringTypeElements, directory, currentDirectory);
            }

            Console.WriteLine("{0} files moved to a recreated directory tree in \\Found Files", count);
            //}
            Console.WriteLine("Type any key to continue...");
            Console.ReadKey();

        }

        /*
         * <p> This method will search a folder tree, recreate the folder tree in a specified location and
         * copy all specified file types to the recreated folder tree </p>
         * 
         * <param> List<string> types       This is the list of types the user wants to look for </param>
         * <param> string path              This is the directory to begin the search from </param>
         * <param> string targetDirectory   This is the place to begin rebuilding the directory tree </param>
         * 
         * <return> int count               The total number of files moved. I used this to verify my work. </return>
         * 
         */
        static int findFile(List<string> types, string path, string targetDirectory) {
            int count = 0;

            Console.WriteLine("Visiting directory: " + path);
            //create a string that will be used as the starting point to recreate the folder tree
            string mashUp = targetDirectory + "\\" + path.Substring(0, 1) + path.Substring(2);
            //I ran into a problem with below line. Path had too many characters so I could only 
            //search treees that were on the desktop
            Directory.CreateDirectory(mashUp);
            foreach (DirectoryInfo directory in new DirectoryInfo(path).GetDirectories()) {
                count += findFile(types, directory.FullName, targetDirectory);
            }
            foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
            {
                Console.WriteLine("Visiting files");
                foreach (string fileType in types)
                {
                    if (fileType == file.Extension)
                    {
                        Console.WriteLine("found file: " + file.FullName);
                        //copying file to destination
                        string targetFile = mashUp + "\\" + file.ToString();
                        count++;
                        File.Copy(file.FullName, targetFile, true);
                    }
                }
            }

            return count;

            /*
            foreach (string item in types) {
                foreach (string path in path) {
                    foreach (DirectoryInfo directory in new DirectoryInfo(path).GetDirectories()) {
                        foreach (FileInfo file in new DirectoryInfo(directory.FullName).GetFiles()) {
                            if (file.Extension == item) {
                                if (!Directory.Exists(targetDirectory + "\\Results")) {
                                    targetDirectory += "\\Results";
                                    Directory.CreateDirectory(targetDirectory);
                                }
                                string fileToMovePath = directory.FullName + "\\" + file;
                                File.Copy(fileToMovePath, targetDirectory, true);
                            }
                            if (directory.GetDirectories() !=null) {
                                findFile(types, path, targetDirectory);
                            }



                        }
                    }
                }
            }
            */
          

        }

    }
}

