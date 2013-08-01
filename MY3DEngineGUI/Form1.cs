using MY3DEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MY3DEngineGUI
{
    public partial class Form1 : Form
    {
        private bool _firstMouse;
        private Point _mouseLocation;
        private ObjectClass none = new ObjectClass { ID = -1, Name = "--NONE--" };

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

            var list = Engine.GameEngine.Manager.GameObjects;
            list.Insert(0, none);
            cmbObjectList.DataSource = list;
        }

        #region Shutdown/Exit

        private static void ShutDown()
        {
            Engine.GameEngine.Dispose();

            Engine.GameEngine.Shutdown();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }

        #endregion Shutdown/Exit

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

        private void rendererPnl_MouseUp(object sender, MouseEventArgs e)
        {
            _firstMouse = false;
        }

        private void rendererPnl_MouseWheel(object sender, MouseEventArgs e)
        {
            Engine.GameEngine.Camera.MoveEye(z: (e.Delta / 120 * -1));

            UpdateCameraLocation();
        }

        private void UpdateCameraLocation()
        {
            lblCameraPosition.Text = Engine.GameEngine.Camera.CameraLocation();
        }

        #endregion Camera

        private void Add_RemoveObject(string s)
        {
            lblAddRemove.Text = s;
            //var list = Engine.GameEngine.Manager.GameObjects;
            var list = new List<ObjectClass>(Engine.GameEngine.Manager.GameObjects);
            if (!list.Contains(none))
            {
                list.Insert(0, none);
            }
            cmbObjectList.DataSource = list;
        }

        private void addCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void addDirectionalLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.GameEngine.Manager.AddObject(new LightClass("Directional")))
            {
                Add_RemoveObject("Directional Light Added");
            }
        }

        private void addObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;

            DialogResult result = open.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (open.SafeFileName.ToLower().EndsWith(".x"))
                {
                    ObjectClass objclass = new ObjectClass(fileName: open.SafeFileName, path: open.FileName);
                    if (Engine.GameEngine.Manager.AddObject(objclass))
                    {
                        Add_RemoveObject(open.SafeFileName + " added");
                    }
                }
            }
        }

        private void addPointLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.GameEngine.Manager.AddObject(new LightClass()))
            {
                Add_RemoveObject("Point light added");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void globalLightsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GlobalLights();
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
        }

        private void wireframOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.WireFrame();
        }
    }
}