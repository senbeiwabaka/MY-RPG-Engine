// <copyright file="LogViewer.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.GUI.HelperForms
{
    using System.Windows.Forms;
    using My3DEngine.Utilities.Services;

    public partial class LogViewer : Form
    {
        public LogViewer(string file)
            : this()
        {
            rtbLogContent.Text = new FileService().GetFileContent(file);
        }

        public LogViewer()
        {
            InitializeComponent();
        }
    }
}
