// <copyright file="SelectFolderForm.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.GUI.HelperForms
{
    using System;
    using System.Windows.Forms;
    using MY3DEngine.BuildTools;
    using MY3DEngine.GUI.Utilities;
    using MY3DEngine.Utilities;

    public partial class SelectFolderForm : Form
    {
        public SelectFolderForm()
        {
            InitializeComponent();
        }

        private void BSelectFolder_Click(object sender, EventArgs e)
        {
            var dialogResult = fbdSelectFProject.ShowDialog();

            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                UseWaitCursor = true;

                bSelectFolder.UseWaitCursor = true;
                bSelectFolder.Enabled = false;

                var toolsetGameModel = GameEngineLoad.LoadProject(fbdSelectFProject.SelectedPath, new FileIO());

                if (!toolsetGameModel.Successful)
                {
                    MessageBox.Show("Game project not loaded successfully.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                ToolsetGameModelManager.ToolsetGameModel = toolsetGameModel;

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
