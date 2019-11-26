using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageAttributes
{
    public static class ExtractAttributes
    {
        public static Repository ExtractFromFile(FileInfo info)
        {
            Repository repository = new Repository(info);
            string name = info.Name;
            try
            {
                IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(info.FullName);
                AddFileType(repository, directories);
                AddImageDimensions(repository, directories);
                EnumerateAttributes(repository, directories);
            }
            catch(ImageProcessingException /*e*/)
            {
                //Console.WriteLine($"Couldn't process {info.Name}, {e.Message}");
            }
            return repository;
        }

        private static void EnumerateAttributes(Repository repository, IEnumerable<MetadataExtractor.Directory> directories)
        {
            foreach (MetadataExtractor.Directory directory in directories)
            {
                foreach (Tag tag in directory.Tags)
                {
                    string attributeName = String.Format("[{0}] {1}", directory.Name, tag.Name);
                    repository.SetAttribute(attributeName, tag.Description);
                }
            }
        }

        private static void AddImageDimensions(Repository repository, IEnumerable<MetadataExtractor.Directory> directories)
        {
            MetadataExtractor.Formats.Jpeg.JpegDirectory jpegDirectory = directories.OfType<MetadataExtractor.Formats.Jpeg.JpegDirectory>().FirstOrDefault();
            if (jpegDirectory != null)
            {
                int height = jpegDirectory.GetInt32(MetadataExtractor.Formats.Jpeg.JpegDirectory.TagImageHeight);
                int width = jpegDirectory.GetInt32(MetadataExtractor.Formats.Jpeg.JpegDirectory.TagImageWidth);
                repository.AddDimensions(height, width);
            }
        }

        private static void AddFileType(Repository repository, IEnumerable<MetadataExtractor.Directory> directories)
        {
            MetadataExtractor.Formats.FileType.FileTypeDirectory fileTypeDirectory = directories.OfType<MetadataExtractor.Formats.FileType.FileTypeDirectory>().FirstOrDefault();
            string fileType = fileTypeDirectory.GetDescription(MetadataExtractor.Formats.FileType.FileTypeDirectory.TagDetectedFileTypeName);
            string fileExtension = fileTypeDirectory.GetDescription(MetadataExtractor.Formats.FileType.FileTypeDirectory.TagExpectedFileNameExtension);
            repository.AddFileType(fileType, fileExtension);
        }

        private static Boolean IsJpeg(IEnumerable<MetadataExtractor.Directory> directories)
        {
            Boolean jpegFound = false;
            MetadataExtractor.Formats.FileType.FileTypeDirectory fileTypeDirectory = directories.OfType<MetadataExtractor.Formats.FileType.FileTypeDirectory>().FirstOrDefault();
            string fileType = fileTypeDirectory.GetDescription(MetadataExtractor.Formats.FileType.FileTypeDirectory.TagDetectedFileTypeName);
            if (fileType == "JPEG")
            {
                jpegFound = true;
            }
            return jpegFound;
        }
    }
}
