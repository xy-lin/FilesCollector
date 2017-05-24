
namespace FilesUtility
{
    class Program
    {
        private static string _sourceDirectory = Configuration.GetAppSetting("file_source");
        private static string _destinationDirectory = Configuration.GetAppSetting("file_destination");
        private static string _logFile = Configuration.GetAppSetting("log_file");

        static void Main(string[] args)
        {
            if (_sourceDirectory == null || _destinationDirectory == null)
                return;
            
            IMessanger logger = new Messanger(_logFile);
            logger.Start();

            FilesCollector filesCollector = new FilesCollector(logger);

            filesCollector.Collect(_sourceDirectory, _destinationDirectory);
        }
    }
}
