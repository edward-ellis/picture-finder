using ImageAttributes;
using System;
using System.Collections.Generic;

namespace PictureFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, Repository> source = FindImageFiles.EnumerateSpotlightImages();
            IDictionary<string, Repository> destination = FindImageFiles.EnumerateRepositoryImages();
            IList<Repository> newImages = FilterImageFiles.Filter(source, destination);
            CopyImageFiles.Copy(newImages);
            foreach (Repository image in newImages)
            {
                Console.WriteLine($"{image.FileNameWithoutExtension} {image.WxHString}");
            }
        }
    }
}
