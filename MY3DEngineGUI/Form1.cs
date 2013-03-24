using System;
using System.Windows.Forms;
using MY3DEngine;

namespace MY3DEngineGUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            Engine.GameEngine = new Engine(Handle);
            Engine.GameEngine.Initliaze();
            Engine.GameEngine.Start();

            var other = Engine.GameEngine.Exception.Exceptions;

            dataGridView1.DataSource = other;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }

        private static void ShutDown()
        {
            Engine.GameEngine.Dispose();
        }
    }
}