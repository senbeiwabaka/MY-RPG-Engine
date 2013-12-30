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
        private GameObject _none = new GameObject { ID = -1, Name = "--NONE--" };
        private bool _isObjectSelected;

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
            _isObjectSelected = false;

            lock (Engine.GameEngine.Manager)
            {
                List<GameObject> list = new List<GameObject>(Engine.GameEngine.Manager.GameObjects);
                list.Insert(0, _none);
                cmbObjectList.DataSource = list;
            }

            Engine.GameEngine.Exception.Information.CollectionChanged += Information_CollectionChanged;
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
            int prevX = _mouseLocation.X;
            int PrevY = _mouseLocation.Y;
            int objectIndex = -1;

            lock (Engine.GameEngine.Manager)
            {
                foreach (GameObject item in Engine.GameEngine.Manager.GameObjects)
                {
                    _isObjectSelected = Engine.GameEngine.Camera.RayIntersection(new SlimDX.Vector2(e.X, e.Y), item.MeshObject);

                    if (_isObjectSelected)
                    {
                        objectIndex = Engine.GameEngine.Manager.GameObjects.FindIndex(x => x.ID == item.ID && x.Name == x.Name);
                    }
                }
            }

            if (e.Button == MouseButtons.Left & !_isObjectSelected)
            {
                float DeltaX = e.X - prevX;
                float DeltaY = e.Y - PrevY;

                if (!_firstMouse)
                {
                    DeltaX = 0;
                    DeltaY = 0;
                    _firstMouse = true;
                }

                Engine.GameEngine.Camera.MoveEye(x: (DeltaX / 40f), y: (-DeltaY / 40f));

                //save current mouse location
                _mouseLocation.X = e.X;
                _mouseLocation.Y = e.Y;

                UpdateCameraLocation();
            }
            else if (e.Button == MouseButtons.Left & _isObjectSelected)
            {
                float DeltaX = e.X - Engine.GameEngine.Manager.GameObjects[objectIndex].MeshObject.ObjectPosition.X;
                float DeltaY = e.Y - Engine.GameEngine.Manager.GameObjects[objectIndex].MeshObject.ObjectPosition.Y;

                Engine.GameEngine.Manager.GameObjects[objectIndex].MeshObject.Translate(x: (DeltaX / 40f), y: (-DeltaY / 40f));

                lblLocation.Text = Engine.GameEngine.Manager.GameObjects[objectIndex].MeshObject.ObjectPosition.ToString();

                _mouseLocation.X = e.X;
                _mouseLocation.Y = e.Y;
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

            List<GameObject> list = new List<GameObject>();

            lock (Engine.GameEngine.Manager)
            {
                list = new List<GameObject>(Engine.GameEngine.Manager.GameObjects);
            }

            tvObjectHirarchy.Nodes.Clear();

            foreach (GameObject gameObject in list)
            {
                tvObjectHirarchy.Nodes.Add(gameObject.Name);
            }

            list.Insert(0, _none);
            cmbObjectList.DataSource = list;
        }

        #region Events

        private void addCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject go = new GameObject("Cube");

            if (Engine.GameEngine.Manager.AddObject(go))
            {
                Add_RemoveObject("Cube Added");
            }
        }

        private void addTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameObject go = new GameObject("Triangle");

            if (Engine.GameEngine.Manager.AddObject(go))
            {
                Add_RemoveObject("Triangle Added");
            }
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
                    GameObject objclass = new GameObject(fileName: open.SafeFileName, path: open.FileName);
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
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);
            }

            if (index >= 0)
            {
                txtName.Text = Engine.GameEngine.Manager.GameObjects[index].Name;
                lblLocation.Text = "Location: " + Engine.GameEngine.Manager.GameObjects[index].MeshObject.ObjectPosition.ToString();
                
                if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                {
                    ckbxLightOnOff.Visible = true;
                    ckbxLightOnOff.Checked = (Engine.GameEngine.Manager.GameObjects[index] as LightClass).IsLightEnabled;
                    lblColor.Text = string.Empty;
                    btnColor.Visible = false;
                }
                else
                {
                    ckbxLightOnOff.Visible = false;
                    lblColor.Text = "Color: " + Engine.GameEngine.Manager.GameObjects[index].MeshObject.MeshColorasString;
                    btnColor.Visible = true;
                }
            }
            else
            {
                lblLocation.Text = string.Empty;
                lblColor.Text = string.Empty;
                btnColor.Visible = false;
            }
        }

        private void globalLightsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GlobalLights();
        }

        private void wireframOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.WireFrame();
        }

        void Information_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    txtError_Info.AppendText(item.ToString() + Environment.NewLine);
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                List<GameObject> list = new List<GameObject>();
                int index = -1;

                lock (Engine.GameEngine.Manager)
                {
                    index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);

                    Engine.GameEngine.Manager.GameObjects[index].Name = txtName.Text;

                    list = new List<GameObject>(Engine.GameEngine.Manager.GameObjects);
                }

                tvObjectHirarchy.Nodes.Clear();

                foreach (GameObject gameObject in list)
                {
                    tvObjectHirarchy.Nodes.Add(gameObject.Name);
                }

                list.Insert(0, _none);
                cmbObjectList.DataSource = list;
            }
        }

        private void resetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.Camera.ResetEye();

            UpdateCameraLocation();
        }

        #endregion Events

        private void ckbxLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);
            }

            if (index >= 0)
            {
                if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                {
                    (Engine.GameEngine.Manager.GameObjects[index] as LightClass).LightOnOff();
                }
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);
            }

            if (index >= 0)
            {
                if (Engine.GameEngine.Manager.GameObjects[index] is GameObject)
                {
                    colorDialog1.Color = Engine.GameEngine.Manager.GameObjects[index].MeshObject.MeshColor;
                }
            }

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (index >= 0)
                {
                    if (Engine.GameEngine.Manager.GameObjects[index] is GameObject)
                    {
                        Engine.GameEngine.Manager.GameObjects[index].MeshObject.ApplyColor(colorDialog1.Color);
                    }
                }
            }
        }
    }
}