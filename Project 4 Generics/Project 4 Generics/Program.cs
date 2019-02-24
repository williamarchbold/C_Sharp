using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    class Program
    {
        static Library<Audio> audioLibrary = new Library<Audio>();
        static Library<Image> imageLibrary = new Library<Image>();
        static Library<Video> videoLibrary = new Library<Video>();
        static IFileLibrary allProxy = new All(audioLibrary, imageLibrary, videoLibrary);

        static void Main(string[] args)
        {
            Menu();




        }
        

        static void Menu()
        {
            Boolean loop = true;
            while (loop)
            {
                Console.WriteLine("Please choose from the following options:\n" +
                    "1. Scan for videos\n2. Scan for audio\n3. Scan for images\n" +
                    "4. Scan for all\n5. Access video library\n6. Access audio library\n" +
                    "7. Access image library\n8. Close program");
                int selection = Convert.ToInt32(Console.ReadLine());

                string whichDirectory;

                switch (selection)
                {

                    case 1:
                        whichDirectory = WhichDirectory();
                        SearchDirectory(Video.GetExtensions(), videoLibrary, whichDirectory);
                        break;
                    case 2:
                        whichDirectory = WhichDirectory();
                        SearchDirectory(Audio.GetExtensions(), audioLibrary, whichDirectory);
                        break;
                    case 3:
                        whichDirectory = WhichDirectory();
                        SearchDirectory(Image.GetExtensions(), imageLibrary, whichDirectory);
                        break;
                    case 4:
                        whichDirectory = WhichDirectory();
                        List<string> all = Video.GetExtensions();
                        all.AddRange(Audio.GetExtensions());
                        all.AddRange(Image.GetExtensions());
                        SearchDirectory(all, allProxy, whichDirectory);
                        break;
                    case 5:
                        videoLibrary.Access();
                        break;
                    case 6:
                        audioLibrary.Access();
                        break;
                    case 7:
                        imageLibrary.Access();
                        break;
                    case 8:
                        loop = false;
                        break;
                    default:
                        break;
                }

            }
        }

        static string WhichDirectory()
        {
            Console.WriteLine("Please input the directory path to search: ");
            return Console.ReadLine();
        }

        static void SearchDirectory(List<string> types, IFileLibrary library, string path)
        {
            Console.WriteLine("Visiting directory: " + path);
            foreach (DirectoryInfo directory in new DirectoryInfo(path).GetDirectories())
            {
                SearchDirectory(types, library, path);
            }
            Console.WriteLine("Visiting Files...");
            foreach (FileInfo file in new DirectoryInfo(path).GetFiles())
            {
                foreach (string filetype in types)
                {
                    if (string.Compare(file.Extension, filetype, true) == 0)
                    {
                        Console.WriteLine("{0} found - {1}", filetype, file.FullName);
                        library.HaveFile(file.FullName, DateTime.Now, filetype);
                    }
                    
                }
            }
        }
    }


}
