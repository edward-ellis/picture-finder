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
            foreach(string key in source.Keys)
            {
                bool found = false;
                Repository sourceRepository = source[key];
                if (destination.ContainsKey(key))
                {
                    Repository destinationRepository = destination[key];
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
