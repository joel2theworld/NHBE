using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Infrastructure.Interfaces
{
    public interface ILoggerManagerService
    {
        void logInfo (string message);
        void logWarn (string message);
        void logError (string message);
        void logDebug (string message);
    }
}
