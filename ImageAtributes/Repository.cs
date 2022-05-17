using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.IO;
using SimpleImageComparisonClassLibrary;

namespace ImageAttributes
{
    public class Repository : IEquatable<Repository>
    {
        private Dictionary<string, string> _repository = new Dictionary<string, string>();
        private string _fileType;
        private FileInfo info;
        public string CryptoHash { get; private set; }
        public string FileExtension { get; private set; }
        public string WxHString { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Repository(FileInfo info)
        {
            this.info = info;
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.Name);
            CryptoHash = CalculateCryptoHash();
        }

        public void CopyTo(string destinationPath)
        {
            info.CopyTo(destinationPath);
        }

        private string CalculateCryptoHash()
        {
            byte[] myHash;
            String hash = String.Empty;
            using (var ha = MD5.Create())
            using (var stream = info.OpenRead())
                myHash = ha.ComputeHash(stream);
            foreach (byte b in myHash) hash += b.ToString("x2").ToLower();

            return hash;
        }

        public void SetAttribute(string attributeName, string attributeValue)
        {
            _repository[attributeName] = attributeValue;
        }

        public string GetAttribute(string attributeName)
        {
            if (!_repository.ContainsKey(attributeName))
            {
                throw new ArgumentException("This attribute doesn't exist.", attributeName);
            }
            return _repository[attributeName];
        }

        public IEnumerable<string> AttributeNames
        {
            get { return _repository.Keys; }
        }

        public string FileNameWithoutExtension { get; private set; }
        public string FileName
        {
            get 
            {
                string name = info.Name;
                if (info.Extension != FileExtension)
                {
                    name = String.Format("{0}{1}", info.Name, FileExtension);
                }
                return name;
            }
        }

        public string Key
        {
            get { return CryptoHash; }
        }

        public bool IsJpeg
        {
            get { return _fileType == "JPEG"; }
        }

        public bool HasMinimumDimension(int minimumDimension)
        {
            return Math.Min(Height, Width) >= minimumDimension;
        }

        internal void AddDimensions(int height, int width)
        {
            Height = height;
            Width = width;
            WxHString = String.Format("{0}x{1}", width, height);
        }

        internal void AddFileType(string fileType, string fileExtension)
        {
            _fileType = fileType;
            FileExtension = string.Format(".{0}", fileExtension);
        }

        public bool Equals([AllowNull] Repository other)
        {
            if (Height != other.Height)
            {
                return false;
            }
            if (Width != other.Width)
            {
                return false;
            }
            int percentageDifference = (int)(ImageTool.GetPercentageDifference(info.FullName, other.info.FullName) * 100);
            if (percentageDifference > 5)
            {
                return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Repository repository = (Repository)obj;
                return Equals(repository);
            }
        }

        public override int GetHashCode()
        {
            return FileNameWithoutExtension.GetHashCode();
        }
    }
}
