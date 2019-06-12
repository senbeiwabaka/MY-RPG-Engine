// <copyright file="LogViewer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MY3DEngine.GUI.HelperForms
{
    using System.Windows.Forms;
    using My3DEngine.Utilities.Services;

    public partial class LogViewer : Form
    {
        public LogViewer(string file) : this()
        {
            rtbLogContent.Text = new FileService().GetFileContent(file);
        }

        public LogViewer()
        {
            InitializeComponent();
        }
    }
}
