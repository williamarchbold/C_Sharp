using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    class Video : Media
    {
        public static List<string> GetExtensions()
        {
            return new List<string>() { ".mp4",".flv",".avi" }; //calls .add method for the string in each set of curly braces
        }

         
    }
}
