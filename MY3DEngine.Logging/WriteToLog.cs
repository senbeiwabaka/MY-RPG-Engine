using NLog;
using System;

namespace MY3DEngine.Logging
{
    public static class WriteToLog
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Exception(string message, Exception exception)
        {
            logger.Error(exception, message);
        }

        public static void Shutdown()
        {
            LogManager.Shutdown();
        }
    }
}