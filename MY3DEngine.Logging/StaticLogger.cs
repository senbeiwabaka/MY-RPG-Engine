using NLog;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MY3DEngine.Logging
{
    [ExcludeFromCodeCoverage]
    public static class StaticLogger
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