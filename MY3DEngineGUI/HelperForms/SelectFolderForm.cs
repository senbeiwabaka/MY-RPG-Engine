using System;
using System.Windows.Forms;

namespace MY3DEngine.GUI.HelperForms
{
    public partial class SelectFolderForm : Form
    {
        public SelectFolderForm()
        {
            this.InitializeComponent();
        }

        private void BSelectFolder_Click(object sender, EventArgs e)
        {
            var dialogResult = this.fbdSelectFProject.ShowDialog();

            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                this.UseWaitCursor = true;

                this.bSelectFolder.UseWaitCursor = true;
                this.bSelectFolder.Enabled = false;

                var gameModel = Build.Load.LoadProject(this.fbdSelectFProject.SelectedPath);
                
                Engine.GameEngine.FolderLocation = this.fbdSelectFProject.SelectedPath;
                Engine.GameEngine.SettingsManager.Initialize();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}