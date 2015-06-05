using System;
using Microsoft.Practices.Prism.Logging;
using NLog;

namespace MedExam
{
    public class NLogLoggerAdapter : ILoggerFacade
    {
        private static Logger _logger;
        public NLogLoggerAdapter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        
        public void Log(string message, Category category, Priority priority)
        {
            var messageText = string.Concat("[", priority, "] ", message);
            switch (category)
            {
                case Category.Debug:
                    _logger.Debug(messageText);
                    break;
                case Category.Exception:
                    _logger.Error(messageText);
                    break;
                case Category.Info:
                    _logger.Info(messageText);
                    break;
                case Category.Warn:
                    _logger.Warn(messageText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("category");
            }
        }
    }
}