// <copyright file="CreateNewProjectForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.GUI.HelperForms
{
    using System;
    using System.Windows.Forms;
    using MY3DEngine.BuildTools;
    using MY3DEngine.GUI.Utilities;
    using MY3DEngine.Models;
    using My3DEngine.Utilities.Services;

    public partial class CreateNewProjectForm : Form
    {
        private string folderLocation = string.Empty;

        public CreateNewProjectForm()
        {
            InitializeComponent();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void BCreate_Click(object sender, EventArgs e)
        {
            var settings = new SettingsModel
            {
                GameName = tbName.Text.Trim(),
                Width = 800,
                Height = 600
            };
            var toolsetGameModel = GameEngineSave.CreateNewProject(folderLocation.Trim(), settings.GameName, settings.Width, settings.Height, settings, new FileService());

            if (!toolsetGameModel.Successful)
            {
                MessageBox.Show("Error", "Error! Please check the error log (if setup).", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ToolsetGameModelManager.ToolsetGameModel = toolsetGameModel;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BSaveFileLocation_Click(object sender, EventArgs e)
        {
            var folderDialogResult = fbdSaveLocationSelector.ShowDialog();

            if (folderDialogResult == DialogResult.OK)
            {
                folderLocation = fbdSaveLocationSelector.SelectedPath;
            }

            EnableOrDisableCreateButton();
        }

        private void EnableOrDisableCreateButton()
        {
            if (!string.IsNullOrWhiteSpace(tbName.Text) && !string.IsNullOrWhiteSpace(fbdSaveLocationSelector.SelectedPath))
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
            EnableOrDisableCreateButton();
        }
    }
}
