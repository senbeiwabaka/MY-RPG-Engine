using MY3DEngine.Utilities;
using System.Windows.Forms;

namespace MY3DEngine.GUI.HelperForms
{
    public partial class LogViewer : Form
    {
        public LogViewer(string file) : this()
        {
            rtbLogContent.Text = new FileIO().GetFileContent(file);
        }

        public LogViewer()
        {
            InitializeComponent();
        }
    }
}
