using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesUtility
{
    public interface IMessanger : IDisposable
    {
        void Start();

        void Add(string message);
    }


    public class Messanger : IMessanger
    {
        private readonly StreamWriter _streamWriter;

        public Messanger(string fileLog)
        {
            if (!File.Exists(fileLog))
            {
                FileStream logFile = File.Create(fileLog);
                logFile.Close();
            }

            _streamWriter = new StreamWriter(fileLog, true);
            _streamWriter.Write(Environment.NewLine);
            _streamWriter.Write(Environment.NewLine);
            _streamWriter.Write(Environment.NewLine);
        }

        public void Dispose()
        {
            _streamWriter?.Close();
        }

        public void Start()
        {
            string filename = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

            _streamWriter.WriteLine("-----------------[{0}]-----------------", filename);
        }

        public void Add(string message)
        {
            string filename = String.Format("[{0:yyyy-MM-dd}]", DateTime.Now);

            _streamWriter.WriteLine(filename + "   " + message);
        }
    }
}
