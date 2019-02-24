using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    class Audio : Media
    {
        public static List<string> GetExtensions()
        {
            return new List<string>() { ".mp3"};
        }
    }
}
