using System.ComponentModel;

namespace MY3DEngine
{
    public class ExceptionHolder
    {
        public BindingList<ExceptionData> Exceptions { get; set; }

        public ExceptionHolder()
        {
            Exceptions = new BindingList<ExceptionData>();
        }
    }
}