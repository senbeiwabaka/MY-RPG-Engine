namespace MY3DEngine.GUI.HelperForms
{
    partial class LogViewer
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
            this.rtbLogContent = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbLogContent
            // 
            this.rtbLogContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogContent.Location = new System.Drawing.Point(0, 0);
            this.rtbLogContent.Name = "rtbLogContent";
            this.rtbLogContent.ReadOnly = true;
            this.rtbLogContent.Size = new System.Drawing.Size(784, 361);
            this.rtbLogContent.TabIndex = 0;
            this.rtbLogContent.Text = "";
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.rtbLogContent);
            this.Name = "LogViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Viewer";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLogContent;
    }
}