using System;
using System.Collections.Generic;
using System.IO;

namespace PictureFinder
{
    public static class FindImageFiles
    {
        public static IDictionary<string, Repository> EnumerateSpotlightImages()
        {
            DirectoryInfo spotlightDirectory = new DirectoryInfo(ImageConfiguration.SpotlightDataPath);
            return EnumerateFiles(spotlightDirectory);
        }
        public static IDictionary<string, Repository> EnumerateRepositoryImages()
        {
            DirectoryInfo mySpotlightDirectory = new DirectoryInfo(ImageConfiguration.MyPicturesPath);
            Directory.CreateDirectory(mySpotlightDirectory.FullName);
            return EnumerateFiles(mySpotlightDirectory);
        }

        private static IDictionary<string, Repository> EnumerateFiles(DirectoryInfo spotlightDirectory)
        {
            Dictionary<string, Repository> list = new Dictionary<string, Repository>();
            IList<FileInfo> files = GetFilesRecursive(spotlightDirectory);
            foreach (FileInfo info in files)
            {
                Repository image = ExtractAttributes.ExtractFromFile(info);
                if (image.IsJpeg)
                {
                    if (image.HasMinimumDimension(ImageConfiguration.MinimumDimension))
                    {
                        if (!list.ContainsKey(image.Key))
                        {
                            list.Add(image.Key, image);
                        }
                    }
                }
            }
            return list;
        }

        private static IList<FileInfo> GetFilesRecursive(DirectoryInfo directory)
        {
            List<FileInfo> list = new List<FileInfo>();
            list.AddRange(directory.GetFiles("*.*"));
            IList<DirectoryInfo> directoryList = directory.GetDirectories();
            foreach(DirectoryInfo info in directoryList)
            {
                list.AddRange(GetFilesRecursive(info));
            }
            return list;
        }

        private static void PrintTags(Repository image)
        {
            foreach (string name in image.AttributeNames)
            {
                Console.WriteLine($"{name} = {image.GetAttribute(name)}");
            }
        }
    }
}
