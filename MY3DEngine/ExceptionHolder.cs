using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MY3DEngine
{
    public class ExceptionHolder
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