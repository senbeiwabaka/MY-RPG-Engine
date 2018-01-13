using System.ComponentModel;

namespace MY3DEngine
{
    /// <summary>
    /// Holds all of the game engine exceptions
    /// </summary>
    public sealed class ExceptionManager
    {
        /// <summary>
        /// List of exceptions that occur
        /// </summary>
        public BindingList<ExceptionData> Exceptions { get; } = new BindingList<ExceptionData>();
    }
}