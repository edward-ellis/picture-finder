using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageAttributes;

namespace PictureFinder
{
    public class RepositoryList
    {
        private readonly Dictionary<string, Repository> _list;

        public RepositoryList()
        {
            _list = new Dictionary<string, Repository>();
        }

        public bool ContainsKey(string key)
        {
            return _list.ContainsKey(key);
        }

        public void Add(string key, Repository value)
        {
            _list.Add(key, value);
        }

        public IEnumerable<String> Keys => _list.Keys;

        public IEnumerable<Repository> Values => _list.Values;

        public int Count => _list.Count;

        public Repository this[String key] => _list[key];
    }
}
