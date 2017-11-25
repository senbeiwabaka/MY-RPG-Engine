using System;
using System.Windows.Forms;

namespace MY3DEngineGUI
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;

            MY3DEngine.Engine.GameEngine.Exception.Exceptions.Add(new MY3DEngine.ExceptionData(exception?.Message, exception?.Source, exception?.StackTrace));
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;

            MY3DEngine.Engine.GameEngine.Exception.Exceptions.Add(new MY3DEngine.ExceptionData(exception?.Message, exception?.Source, exception?.StackTrace));
        }
    }
}
