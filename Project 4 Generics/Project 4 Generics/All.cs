using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_4_Generics
{
    class All : IFileLibrary
    {
        IFileLibrary audio, video, image;

        public All(IFileLibrary audio, IFileLibrary video, IFileLibrary image)
        {
            this.audio = audio;
            this.video = video;
            this.image = image;
        }
        public void HaveFile(string fileNmae, DateTime lastAccessed, string filetype) //this is a proxy acting like 1 object to forward to three different sets of HaveFile
        {
            audio.HaveFile(fileNmae, lastAccessed, filetype);
            video.HaveFile(fileNmae, lastAccessed, filetype);
            image.HaveFile(fileNmae, lastAccessed, filetype);

        }


    }
}
