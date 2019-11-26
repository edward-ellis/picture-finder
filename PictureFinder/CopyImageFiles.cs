using ImageAttributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace PictureFinder
{
    public static class CopyImageFiles
    {
        public static void Copy(IList<Repository> images)
        {
            string myPicturesPath = ImageConfiguration.MyPicturesPath;
            foreach(Repository image in images)
            {
                string destinationDirectory = Path.Join(myPicturesPath, image.WxHString);
                Directory.CreateDirectory(destinationDirectory);
                string destinationPath = Path.Join(destinationDirectory, image.FileName);
                image.CopyTo(destinationPath);
            }
        }
    }
}
