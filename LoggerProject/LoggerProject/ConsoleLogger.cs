using System;

namespace LoggerProject
{ 
    public class ConsoleLogger: ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine("INFO. " + message);
        }

        public void Debug(string log)
        {
            Console.WriteLine("DEBUG. " + log);
        }

        public void Warning(string log)
        {
            Console.WriteLine("WARN. " + log);
        }

        public void Error(string log)
        {
            Console.WriteLine("ERROR. " + log);
        }

        public void Dispose() { }
    }
}
