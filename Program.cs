using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PictureFinder
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main()
        {
            Log.Debug("Start PictureFinder");
            IDictionary<string, Repository> source = FindImageFiles.EnumerateSpotlightImages();
            Log.Debug("FindImageFiles.EnumerateSpotlightImages Complete");
            IDictionary<string, Repository> destination = FindImageFiles.EnumerateRepositoryImages();
            Log.Debug("FindImageFiles.EnumerateRepositoryImages Complete");
            IList<Repository> newImages = FilterImageFiles.Filter(source, destination);
            Log.Debug("FilterImageFiles.Filter Complete");
            CopyImageFiles.Copy(newImages);
            Log.Debug("CopyImageFiles.Copy Complete");
            foreach (Repository image in newImages)
            {
                Console.WriteLine($"{image.FileNameWithoutExtension} {image.WxHString}");
            }
            int numNewImages = newImages.Count;
            int totalImages = destination.Count + numNewImages;
            string message = $"Found {numNewImages} new images for a total of {totalImages} saved images.";
            Console.WriteLine(message);
            Log.Debug(message);
        }
    }
}
