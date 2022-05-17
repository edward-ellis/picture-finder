using ImageAttributes;
using System;
using System.Collections.Generic;

namespace PictureFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositoryList source = FindImageFiles.EnumerateSpotlightImages();
            RepositoryList destination = FindImageFiles.EnumerateRepositoryImages();
            IList<Repository> newImages = FilterImageFiles.Filter(source, destination);
            CopyImageFiles.Copy(newImages);
            foreach (Repository image in newImages)
            {
                Console.WriteLine($"{image.FileNameWithoutExtension} {image.WxHString}");
            }
            int numNewImages = newImages.Count;
            int totalImages = destination.Count + numNewImages;
            Console.WriteLine($"Found {numNewImages} new images for a total of {totalImages} saved images.");
        }
    }
}
