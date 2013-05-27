using System;
using System.Windows.Forms;
using MY3DEngine;
using System.Drawing;

namespace MY3DEngineGUI
{
    public partial class Form1 : Form
    {
        private Point _mouseLocation;
        private bool _firstMouse;

        public Form1()
        {
            InitializeComponent();

            Engine.GameEngine = new Engine(rendererPnl.Handle);
            Engine.GameEngine.Initliaze(rendererPnl.Width, rendererPnl.Height);
            UpdateCameraLocation();

            var other = Engine.GameEngine.Exception.Exceptions;

            dataGridView1.DataSource = other;

            rendererPnl.MouseWheel += rendererPnl_MouseWheel;

            _mouseLocation = new Point(0, 0);
            _firstMouse = false;
        }

        #region Shutdown/Exit

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

            Engine.GameEngine.Shutdown();
        }

        #endregion

        #region Camera

        private void rendererPnl_MouseEnter(object sender, EventArgs e)
        {
            rendererPnl.Focus();
        }

        private void rendererPnl_MouseMove(object sender, MouseEventArgs e)
        {
            //get previous mouse location
            var prevX = _mouseLocation.X;
            var PrevY = _mouseLocation.Y;

            if (e.Button == MouseButtons.Left)
            {
                float DeltaX = e.X - prevX;
                float DeltaY = e.Y - PrevY;

                if (!_firstMouse)
                {
                    DeltaX = 0;
                    DeltaY = 0;
                    _firstMouse = true;
                }

                Engine.GameEngine.Camera.MoveEye(x: (DeltaX / 40), y: (-DeltaY / 40));

                //save current mouse location
                _mouseLocation.X = e.X;
                _mouseLocation.Y = e.Y;

                UpdateCameraLocation();
            }
        }

        private void rendererPnl_MouseWheel(object sender, MouseEventArgs e)
        {
            Engine.GameEngine.Camera.MoveEye(z: (e.Delta / 120 * -1));

            UpdateCameraLocation();
        }

        private void rendererPnl_MouseUp(object sender, MouseEventArgs e)
        {
            _firstMouse = false;
        }

        private void UpdateCameraLocation()
        {
            lblCameraPosition.Text = Engine.GameEngine.Camera.CameraLocation();
        }

        #endregion
        
    }
}