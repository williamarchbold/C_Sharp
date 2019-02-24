using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    class Library <T> : IFileLibrary where T : Media, new() //why new()?
    {
        List<T> libraryMembers = new List<T>(); 

        public void HaveFile(string fileName, DateTime lastAccessed, string fileType) //implemented from IFileLibrary
        {
            
            
            //add to library -- scan some foler
            // wheen you find a file of the right type
            var m = new T();
            m.setFileInfo(fileName, lastAccessed, fileType);
            libraryMembers.Add(m);
        }

        public void Access()
        {
            for (; ; )
            {
                for (int i = 0; i < libraryMembers.Count; i++)
                {
                    T member = libraryMembers[i];
                    Console.WriteLine("{0}. {1} {2} {3}", i+1, member.fileName, member.extension, member.lastAccessed);
                }


                Console.WriteLine("Select integer from following options:\n1.Sort by name\n2. Sort by extension\n" +
                    "3. Sort by Date last accessed\n4. Touch file\n5. Remove file\n6. Back to main menu");

                int selection = Convert.ToInt32(Console.ReadLine());

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Sorting by name...");
                        libraryMembers.Sort();
                        break;
                    case 2:
                        Console.WriteLine("Sorting by extension...");
                        libraryMembers.Sort(Media.CompareExtension);
                        break;
                    case 3:
                        Console.WriteLine("Sorting by date last accessed...");
                        libraryMembers.Sort(Media.CompareLastAccessed);
                        break;
                    case 4:
                        Console.WriteLine("Input integer to represent which file you want to touch: ");
                        int touch = Convert.ToInt32(Console.ReadLine());
                        libraryMembers[touch - 1].Touch(DateTime.Now);
                        break;
                    case 5:
                        Console.WriteLine("Input integer for which file you want to remove from library:");
                        int remove = Convert.ToInt32(Console.ReadLine());
                        libraryMembers.RemoveAt(remove - 1);
                        break;
                    default:
                        return;
                }

            }
        }



    }
}
