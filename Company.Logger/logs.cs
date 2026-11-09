using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Logger
{
    public static class logs
    {
        public static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();  
        public static void LogTrace(string message)
        {
            logger.Trace(message);
        }
        public static void LogInfo(string message)
        {
            logger.Info(message);
        }
        public static void LogWarning(string message)
        {
            logger.Warn(message);
        }
        public static void LogFatal(string message)
        {
            logger.Fatal(message);
        }
        public static void LogError(string message)
        {
            logger.Error(message);
        }
        public static void LogError(Exception exception)
        {
            logger.Trace(exception.Message+exception.StackTrace);
        }
    }
}
