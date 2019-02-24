using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    abstract class Media : IComparable<Media>
    {



        public string fileName;
        public DateTime lastAccessed;
        public string extension;

        //abstract public List<string> GetExtensions();


        public void setFileInfo(string f, DateTime l, string e)
        {
            fileName = f;
            lastAccessed = l;
            extension = e;

            
        }

        public void Touch(DateTime now)
        {
            lastAccessed = now;
        }

        

        //this is extra credit for audio only therefore should it be in Media abstract class or just in audio class? 
        //public abstract void Play();

        public int CompareTo(Media other)
        {
            return fileName.CompareTo(other.fileName);
        }

        static public int CompareExtension(Media media1, Media other)
        {
            return media1.extension.CompareTo(other.extension);
        }

        static public int CompareLastAccessed(Media media1, Media other)
        {
            return media1.lastAccessed.CompareTo(other.extension);
        }
    }

}
