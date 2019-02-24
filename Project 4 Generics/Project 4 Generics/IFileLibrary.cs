using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    interface IFileLibrary
    {
        void HaveFile(string fileName, DateTime lastAccessed, string fileType);
    }
}
