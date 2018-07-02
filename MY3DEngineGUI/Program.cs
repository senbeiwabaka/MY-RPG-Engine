using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
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

            Engine.GameEngine.Exception.AddException(exception);

            ExitApplication();
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;

            Engine.GameEngine.Exception.AddException(exception);

            ExitApplication();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            Engine.GameEngine.Exception.AddException(exception);

            ExitApplication();
        }

        private static void ExitApplication()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            System.Windows.Forms.Application.Exit();
            System.Environment.Exit(0);
            proc.Kill();
        }
    }
}