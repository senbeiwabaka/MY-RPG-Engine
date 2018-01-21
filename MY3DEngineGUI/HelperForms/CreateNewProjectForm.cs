using System;
using System.Windows.Forms;

namespace MY3DEngine.GUI.HelperForms
{
    public partial class CreateNewProjectForm : Form
    {
        private string folderLocation = string.Empty;

        public CreateNewProjectForm()
        {
            this.InitializeComponent();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void BCreate_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.FolderLocation = this.folderLocation;
            Engine.GameEngine.GameName = this.tbName.Text.Trim();

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void BSaveFileLocation_Click(object sender, EventArgs e)
        {
            var folderDialogResult = fbdSaveLocationSelector.ShowDialog();

            if (folderDialogResult == DialogResult.OK)
            {
                folderLocation = fbdSaveLocationSelector.SelectedPath;
            }

            this.EnableOrDisableCreateButton();
        }

        private void EnableOrDisableCreateButton()
        {
            if (!string.IsNullOrWhiteSpace(this.tbName.Text) && !string.IsNullOrWhiteSpace(fbdSaveLocationSelector.SelectedPath))
            {
                bCreate.Enabled = true;
            }
            else
            {
                bCreate.Enabled = false;
            }
        }

        private void TbName_TextChanged(object sender, EventArgs e)
        {
            this.EnableOrDisableCreateButton();
        }
    }
}