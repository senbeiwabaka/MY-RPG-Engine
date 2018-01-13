﻿using MY3DEngine;
using System;
using System.Windows.Forms;

namespace MY3DEngineGUI.HelperForms
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

                Engine.GameEngine.FolderLocation = this.fbdSelectFProject.SelectedPath;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}