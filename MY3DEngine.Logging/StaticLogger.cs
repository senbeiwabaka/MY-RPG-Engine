namespace MY3DEngine.Logging
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using NLog;

    [ExcludeFromCodeCoverage]
    public static class StaticLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Exception(string message, Exception exception)
        {
            Logger.Error(exception, message);
        }

        public static void Shutdown()
        {
            LogManager.Shutdown();
        }
    }
}