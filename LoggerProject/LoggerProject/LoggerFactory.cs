using System;

namespace LoggerProject
{
    public class LoggerFactory
    {
        private readonly string _fileLoggerLogPath;

        public LoggerFactory(string fileLoggerLogPath)
        {
            _fileLoggerLogPath = fileLoggerLogPath;
        }

        public ILogger CreateLogger(int logType)
        {
            switch (logType)
            {
                case 0:
                    return new ConsoleLogger();
                case 1:
                    return new FileLogger(_fileLoggerLogPath);
                default:
                    throw new Exception("Some exception");
            }
        }
    }
}