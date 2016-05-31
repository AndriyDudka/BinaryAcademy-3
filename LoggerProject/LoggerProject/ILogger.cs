using System;

namespace LoggerProject
{
    public interface ILogger : IDisposable
    {
        void Info(string message);
        void Debug(string log);
        void Warning(string log);
        void Error(string log);
    }
}
