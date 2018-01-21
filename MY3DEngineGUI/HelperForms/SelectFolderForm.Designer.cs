namespace MY3DEngine.GUI.HelperForms
{
    partial class SelectFolderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bSelectFolder = new System.Windows.Forms.Button();
            this.fbdSelectFProject = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // bSelectFolder
            // 
            this.bSelectFolder.Location = new System.Drawing.Point(79, 12);
            this.bSelectFolder.Name = "bSelectFolder";
            this.bSelectFolder.Size = new System.Drawing.Size(121, 23);
            this.bSelectFolder.TabIndex = 0;
            this.bSelectFolder.Text = "Select Folder";
            this.bSelectFolder.UseVisualStyleBackColor = true;
            this.bSelectFolder.Click += new System.EventHandler(this.BSelectFolder_Click);
            // 
            // fbdSelectFProject
            // 
            this.fbdSelectFProject.ShowNewFolderButton = false;
            // 
            // SelectFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 46);
            this.Controls.Add(this.bSelectFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectFolderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Project to Load";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bSelectFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectFProject;
    }
}