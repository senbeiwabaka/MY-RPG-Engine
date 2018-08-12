using MY3DEngine.Models;
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
            var settings = new SettingsModel
            {
                GameName = this.tbName.Text.Trim(),
                Width = 800,
                Height = 600,
                MainFolderLocation = this.folderLocation.Trim()
            };

            if (!Build.GameEngineSave.CreateNewProject(settings.MainFolderLocation, settings.GameName, settings.Width, settings.Height, settings))
            {
                MessageBox.Show("Error", "Error! Please check the error log (if setup).", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

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
