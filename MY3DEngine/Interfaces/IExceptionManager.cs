namespace MY3DEngine.Interfaces
{
    using System;
    using System.ComponentModel;
    using MY3DEngine.Models;

    public interface IExceptionManager
    {
        /// <summary>
        /// Gets list of exceptions that occur
        /// </summary>
        BindingList<ErrorModel> Exceptions { get; }

        /// <summary>
        /// Add compiler error to the system error system for user display
        /// </summary>
        /// <param name="fileName">The name of the file with the error</param>
        /// <param name="line">The line of the error</param>
        /// <param name="column">The character column of the error</param>
        /// <param name="errorNumber">The CS error code #</param>
        /// <param name="errorText">The error description</param>
        void AddCompilerErrors(string fileName, int line, int column, string errorNumber, string errorText);

        /// <summary>
        /// Add an error message to the system error system for user display
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="source">The source of the error</param>
        /// <param name="stackTrace">The StackTrace of the error</param>
        void AddErrorMessage(string message, string source, string stackTrace);

        /// <summary>
        /// Add exception to the system error system for user display
        /// </summary>
        /// <param name="e">The exception to add</param>
        void AddException(Exception e);
    }
}
