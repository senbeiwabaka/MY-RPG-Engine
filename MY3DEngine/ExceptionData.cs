using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MY3DEngine
{
    public struct ExceptionData
    {
        public string Message { get; private set; }
        public string Source { get; private set; }
        public string StackTrace { get; private set; }

        public ExceptionData(string message, string source, string stacktrace) : this()
        {
            Message = message;
            Source = source;
            StackTrace = stacktrace;
        }
    }
}
