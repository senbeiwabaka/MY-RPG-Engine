using MY3DEngine.GUI.Utilities;
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

                var toolsetGameModel = Build.GameEngineLoad.LoadProject(this.fbdSelectFProject.SelectedPath);

                if (toolsetGameModel.Successful)
                {
                    ToolsetGameModelManager.ToolsetGameModel = toolsetGameModel;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Game project not loaded successfully.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
