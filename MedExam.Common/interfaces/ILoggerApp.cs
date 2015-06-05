using System;

namespace MedExam.Common.interfaces
{
    public interface ILoggerApp<T>
    {
        void Log(LogLevel logLevel, string message);
        void Log(LogLevel logLevel, string message, Exception exception);
    }
}