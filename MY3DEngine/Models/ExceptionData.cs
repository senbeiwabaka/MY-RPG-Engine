namespace MY3DEngine.Models
{
    using System;

    /// <summary>Provides the base class for value types.</summary>
    public struct ErrorModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorModel"/> struct.
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
        /// Gets </summary>
        public string Message { get; }

        /// <summary>
        ///
        /// Gets </summary>
        public string Source { get; }

        /// <summary>
        ///
        /// Gets </summary>
        public string StackTrace { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(this.Message)}: {this.Message} {Environment.NewLine} Source: {Environment.NewLine} \t {this.Source} {Environment.NewLine} {nameof(this.StackTrace)}: {Environment.NewLine} {this.StackTrace}";
        }
    }
}