using MY3DEngine.Utilities;
using System;
using System.Windows.Forms;

namespace MY3DEngine.GUI.HelperForms
{
    public delegate void ClosingSetNameForm(object sender, ClosingSetNameEventArgs args);

    public class ClosingSetNameEventArgs : EventArgs
    {
        public readonly string ClassName;

        public ClosingSetNameEventArgs(string className)
        {
            this.ClassName = className;
        }
    }

    public partial class SetNameForm : Form
    {
        public SetNameForm()
        {
            InitializeComponent();
        }

        internal event ClosingSetNameForm ClosingSetNameForm;

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void BCreate_Click(object sender, EventArgs e)
        {
            var fileName = !tbName.Text.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase) ? $"{tbName.Text}.cs" : tbName.Text;

            FileIO.WriteFileContent($"{Engine.GameEngine.SettingsManager.Settings.MainFolderLocation}\\{fileName}", string.Empty);

            this.DialogResult = DialogResult.OK;

            this.ClosingSetNameForm?.Invoke(this, new ClosingSetNameEventArgs(tbName.Text));

            this.Close();
        }

        private void TbName_TextChanged(object sender, EventArgs e)
        {
            this.bCreate.Enabled = false;

            if (!string.IsNullOrWhiteSpace(this.tbName.Text))
            {
                this.bCreate.Enabled = true;
            }
        }
    }
}
