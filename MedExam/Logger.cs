using System;
using MedExam.Common.interfaces;
using NLog;
using LogLevel = MedExam.Common.LogLevel;

namespace MedExam
{
    public class Logger<T> : ILoggerApp<T>
    {
        private readonly Logger _logger;

        public Logger()
        {
            _logger = LogManager.GetLogger(typeof(T).FullName);
        }

        public void Log(LogLevel logLevel, string message)
        {
            var level = NLog.LogLevel.FromOrdinal((int) logLevel);
            Log(level, message);
        }

        public void Log(LogLevel logLevel, string message, Exception exception)
        {
            var level = NLog.LogLevel.FromOrdinal((int)logLevel);
            Log(level, message, exception);
        }

        private void Log(NLog.LogLevel logLevel, string message)
        {
            _logger.Log(logLevel, message);
        }

        private void Log(NLog.LogLevel logLevel, string message, Exception exception)
        {
            _logger.Log(logLevel, message, exception);
        }
    }
}