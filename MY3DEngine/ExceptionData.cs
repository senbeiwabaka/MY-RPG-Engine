namespace MY3DEngine
{
    /// <summary>Provides the base class for value types.</summary>
    public struct ExceptionData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string StackTrace { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="stacktrace"></param>
        public ExceptionData(string message, string source, string stacktrace) : this()
        {
            Message = message;
            Source = source;
            StackTrace = stacktrace;
        }
    }
}