namespace MY3DEngine.Managers
{
    using System;
    using System.ComponentModel;
    using MY3DEngine.Interfaces;
    using MY3DEngine.Models;

    /// <summary>
    /// Holds all of the game engine exceptions
    /// </summary>
    public sealed class ExceptionManager : IExceptionManager
    {
        /// <summary>
        /// List of exceptions that occur
        /// </summary>
        public BindingList<ErrorModel> Exceptions { get; } = new BindingList<ErrorModel>();

        /// <summary>
        /// Add compiler error to the system error system for user display
        /// </summary>
        /// <param name="fileName">The name of the file with the error</param>
        /// <param name="line">The line of the error</param>
        /// <param name="column">The character column of the error</param>
        /// <param name="errorNumber">The CS error code #</param>
        /// <param name="errorText">The error description</param>
        public void AddCompilerErrors(string fileName, int line, int column, string errorNumber, string errorText)
        {
            this.AddErrorMessage($"{fileName} has had an error compiling.", $"On line {line} in column {column}. The error code is {errorNumber}.", errorText);
        }

        /// <summary>
        /// Add an error message to the system error system for user display
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="source">The source of the error</param>
        /// <param name="stackTrace">The StackTrace of the error</param>
        public void AddErrorMessage(string message, string source, string stackTrace)
        {
            if (Engine.IsDebugginTurnedOn)
            {
                Exceptions.Add(new ErrorModel(message, source, stackTrace));
            }
        }

        /// <summary>
        /// Add exception to the system error system for user display
        /// </summary>
        /// <param name="e">The exception to add</param>
        public void AddException(Exception e)
        {
            if (Engine.IsDebugginTurnedOn)
            {
                var exception = e;

                this.AddErrorMessage(exception.Message, exception.Source, exception.StackTrace);

                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;

                    this.AddErrorMessage(exception.Message, exception.Source, exception.StackTrace);
                }
            }
        }
    }
}
