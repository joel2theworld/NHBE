using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeighbourhoodHelp.Infrastructure.Interfaces;
using NLog;


namespace NeighbourhoodHelp.Infrastructure.Services
{
    public class LoggerManagerService : ILoggerManagerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManagerService()
        {
            
        }
        public void logDebug(string message)
        {
            logger.Debug(message);

        }

        public void logError(string message)
        {
            logger.Error(message);
        }

        public void logInfo(string message)
        {
            logger.Info(message);
        }

        public void logWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
