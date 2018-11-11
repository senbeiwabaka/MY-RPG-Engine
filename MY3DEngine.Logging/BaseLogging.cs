namespace MY3DEngine.Logging
{
    using System;
    using NLog;

    public abstract class BaseLogging
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