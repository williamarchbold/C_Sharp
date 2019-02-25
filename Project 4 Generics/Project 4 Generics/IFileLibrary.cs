using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    interface IFileLibrary //use an interface because C# doesn't allow inheritence from multiple classes
    {
        void HaveFile(string fileName, DateTime lastAccessed, string fileType);
    }
}
