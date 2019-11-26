using ImageAttributes;
using System.Collections.Generic;

namespace PictureFinder
{
    public static class FilterImageFiles
    {
        public static IList<Repository> Filter(
            IDictionary<string, Repository> source, 
            IDictionary<string, Repository> destination
            )
        {
            List<Repository> list = new List<Repository>();
            foreach(string fileName in source.Keys)
            {
                bool found = false;
                Repository sourceRepository = source[fileName];
                if (destination.ContainsKey(fileName))
                {
                    Repository destinationRepository = destination[fileName];
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
