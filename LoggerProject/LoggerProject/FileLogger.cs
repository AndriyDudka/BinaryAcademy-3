using System.IO;

namespace LoggerProject
{
    public class FileLogger : ILogger
    {
        private readonly StreamWriter _fileWriter;

        public FileLogger(string filePath)
        {
            var directoryInfo = Directory.GetParent(filePath);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            _fileWriter = new StreamWriter(filePath, true) { AutoFlush = true };
        }

        private void WriteToFile(string tag, string message)
        {
            _fileWriter.WriteLine(tag + message);
        }

        public void Info(string message)
        {
            WriteToFile("INFO. ", message);
        }

        public void Debug(string message)
        {
            WriteToFile("DEBUG. ", message);
        }

        public void Warning(string message)
        {
            WriteToFile("WARN. ", message);
        }

        public void Error(string message)
        {
            WriteToFile("ERROR. ", message);
        }

        public void Dispose()
        {
            if (_fileWriter != StreamWriter.Null)
                _fileWriter.Close();
        }
    }
}