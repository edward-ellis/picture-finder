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
                string destinationDirectory = Path.Combine(myPicturesPath, image.WxHString);
                Directory.CreateDirectory(destinationDirectory);
                string destinationPath = Path.Combine(destinationDirectory, image.FileName);
                try
                {
                    image.CopyTo(destinationPath);
                }
                catch (IOException originalException)
                {
                    string message = $"Copying image from {image.FileName} to {destinationPath}";
                    throw new ApplicationException(message, originalException);
                }
            }
        }
    }
}
