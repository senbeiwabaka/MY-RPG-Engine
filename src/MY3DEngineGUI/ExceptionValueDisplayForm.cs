// <copyright file="ExceptionValueDisplayForm.cs" company="MY Soft Games LLC">
//      Copyright (c) MY Soft Games LLC. All rights reserved.
// </copyright>

namespace MY3DEngine.GUI
{
    using System.Windows.Forms;

    public partial class ExceptionValueDisplayForm : Form
    {
        public ExceptionValueDisplayForm()
        {
            InitializeComponent();
        }

        public ExceptionValueDisplayForm(object content)
            : this()
        {
            this.CellMessageContent.Text = content.ToString();
        }
    }
}