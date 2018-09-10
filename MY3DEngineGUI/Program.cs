﻿using NLog;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    internal static class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;

            logger.Error(exception, $"Unhandled exception in {nameof(Program)}.{nameof(Application_ThreadException)} with message: {exception.Message}");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            logger.Error(exception, $"Unhandled exception in {nameof(Program)}.{nameof(CurrentDomain_UnhandledException)} with message: {exception.Message}");
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += Application_ThreadException;

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception;

            logger.Error(exception, $"Unhandled exception in {nameof(Program)}.{nameof(TaskScheduler_UnobservedTaskException)} with message: {exception.Message}");
        }
    }
}
