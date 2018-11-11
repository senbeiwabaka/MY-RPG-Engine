namespace MY3DEngine.Models
{
    using System;

    /// <summary>Provides the base class for value types.</summary>
    public struct ErrorModel
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="stacktrace"></param>
        public ErrorModel(string message, string source, string stacktrace)
        {
            Message = message;
            Source = source;
            StackTrace = stacktrace;
        }

        /// <summary>
        ///
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///
        /// </summary>
        public string Source { get; }

        /// <summary>
        ///
        /// </summary>
        public string StackTrace { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(this.Message)}: {this.Message} {Environment.NewLine} Source: {Environment.NewLine} \t {this.Source} {Environment.NewLine} {nameof(this.StackTrace)}: {Environment.NewLine} {this.StackTrace}";
        }
    }
}