using NLog;
using System;

namespace MY3DEngine.Logging
{
    public class BaseLogging
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Exception(string message, Exception exception)
        {
            logger.Error(exception, message);
        }

        public void Shutdown()
        {
            LogManager.Shutdown();
        }
    }
}