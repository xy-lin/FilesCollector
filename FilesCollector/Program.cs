
namespace FilesUtility
{
    class Program
    {
        private static string _sourceDirectory = Configuration.GetAppSetting("file_source");
        private static string _destinationDirectory = Configuration.GetAppSetting("file_destination");

        static void Main(string[] args)
        {
            if (_sourceDirectory == null || _destinationDirectory == null)
                return;
            
            FilesCollector filesCollector = new FilesCollector();

            filesCollector.Collect(_sourceDirectory, _destinationDirectory);
        }
    }
}
