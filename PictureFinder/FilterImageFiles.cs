using System;
using ImageAttributes;
using System.Collections.Generic;

namespace PictureFinder
{
    public static class FilterImageFiles
    {
        public static IList<Repository> Filter(
            RepositoryList source,
            RepositoryList destination
            )
        {
            List<Repository> list = new List<Repository>();
            foreach(Repository sourceRepository in source.Values)
            {
                Console.Write(".");
                bool found = false;
                foreach (Repository destinationRepository in destination.Values)
                {
                    if (sourceRepository.Equals(destinationRepository))
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    list.Add(sourceRepository);
                }
            }
            return list;
        }
    }
}
