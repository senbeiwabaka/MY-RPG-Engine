using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MY3DEngine
{
    public sealed class ExceptionHolder
    {
        public BindingList<ExceptionData> Exceptions { get; set; }

        public ObservableCollection<string> Information { get; set; }

        public ExceptionHolder()
        {
            Exceptions = new BindingList<ExceptionData>();
            Information = new ObservableCollection<string>();
        }
    }
}