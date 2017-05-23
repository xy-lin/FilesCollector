using System;
using System.Drawing;
using System.IO;

namespace FilesUtility
{
    public class FilesCollector
    {
        private static readonly string _fileSizeCache = Configuration.GetAppSetting("file_cache");
        private static readonly string _fileType = Configuration.GetAppSetting("file_type");

        internal void Collect(string sourceDir, string dstDir)
        {
            if (sourceDir == null || dstDir == null)
                return;

            IFileCache fileCache = InitFileCache(dstDir);

            if (Directory.Exists(sourceDir))
            {
                string fileName;
                string destFile;
                string[] files = Directory.GetFiles(sourceDir);
                
                foreach (string sourceFile in files)
                {
                    using (FileStream sourceFileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                    {
                        if( fileCache.Contains(sourceFileStream))
                            continue;

                        try
                        {
                            using (Image img = Image.FromStream(sourceFileStream))
                            {
                                // Use static Path methods to extract only the file name from the path.
                                fileName = Path.GetFileName(sourceFile) + _fileType;
                                destFile = Path.Combine(dstDir, fileName);

                                if (CanCopy(img))
                                {
                                    File.Copy(sourceFile, destFile, false);
                                      
                                    fileCache.AddEntry(sourceFileStream.Length);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            fileCache.Dispose();
        }

        private static bool CanCopy(Image img)
        {
            return img.Width > 200 && img.Height > 200 && img.Width > img.Height;
        }

        private IFileCache InitFileCache(string dstDir)
        {
            return !File.Exists(_fileSizeCache) || new FileInfo(_fileSizeCache).Length == 0
                ? new FileCache(dstDir, _fileSizeCache, _fileType)
                : new FileCache(_fileSizeCache);
        }
    }
}
