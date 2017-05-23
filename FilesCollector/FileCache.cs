using System;
using System.Collections.Generic;
using System.IO;

namespace FilesUtility
{
    public interface IFileCache : IDisposable
    {
        bool Contains(FileStream fileStream);

        void AddEntry(double id);
    }
 

    public class FileCache : IFileCache
    {
        private readonly List<double> _sizesCache = new List<double>();

        private readonly StreamWriter _streamWriter;

        public FileCache(string fileCacheName)
        {
            _streamWriter = new StreamWriter(fileCacheName, true);

            string[] existingFiles = File.ReadAllLines(fileCacheName);

            foreach (string existingFile in existingFiles)
            {
                _sizesCache.Add(Double.Parse(existingFile));
            }
        }


        public FileCache(string detDir, string fileCacheName, string fileType)
        {
            FileStream fileSizeCache = File.Create(fileCacheName);
            fileSizeCache.Close();

            string[] files = Directory.GetFiles(detDir, fileType);

            _streamWriter = new StreamWriter(fileCacheName, true);

            foreach (string imgName in files)
            {
                using (FileStream imgFile = new FileStream(imgName, FileMode.Open, FileAccess.Read))
                {
                    _sizesCache.Add(imgFile.Length);
                    _streamWriter.WriteLine(imgFile.Length);
                }
            }
        }

        public bool Contains(FileStream fileStream)
        {
            return _sizesCache != null && _sizesCache.Contains(fileStream.Length);
        }

        public void AddEntry(double id)
        {
            if (_sizesCache != null && _streamWriter != null)
            {
                _sizesCache.Add(id);

                _streamWriter.WriteLine(id);
            }
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}
