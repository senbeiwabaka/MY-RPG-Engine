using MY3DEngine;
using MY3DEngine.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MY3DEngineGUI
{
    public partial class Form1 : Form
    {
        private bool _firstMouse;
        private Point _mouseLocation;
        //private GameObject _none = new GameObject { ID = -1, Name = "--NONE--" };
        private bool _isObjectSelected;

        public Form1()
        {
            InitializeComponent();

            var graphicsException = new ExceptionData("Engine Graphics not setup correctly", "Engine", string.Empty);
            var exceptions = new BindingList<ExceptionData>();

            if (Engine.GameEngine.InitliazeGraphics(
                this.rendererPnl.Handle,
                this.rendererPnl.Width,
                this.rendererPnl.Height,
                true,
                false))
            {
                Engine.GameEngine.Initialize(this.Handle);

                exceptions = Engine.GameEngine.Exception.Exceptions;

                UpdateCameraLocation();

                //rendererPnl.MouseWheel += rendererPnl_MouseWheel;

                _mouseLocation = new Point(0, 0);
                _firstMouse = false;
                _isObjectSelected = false;

                lock (Engine.GameEngine.Manager)
                {
                    this.GameObjectBindingSource.DataSource = Engine.GameEngine.Manager.GameObjects;
                    cmbObjectList.DataSource = this.GameObjectBindingSource.DataSource;
                    TreeListViewSceneGraph.SetObjects(Engine.GameEngine.Manager.GameObjects, true);
                }
            }
            else
            {
                exceptions.Add(graphicsException);
            }

            Engine.GameEngine.Exception.Information.CollectionChanged += Information_CollectionChanged;

            this.dataGridView1.DataSource = exceptions;

            this.AddToInformationDisplay(string.Format("Video card memory : {0} MB", Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardMemory));
            this.AddToInformationDisplay(string.Format("Video card description : {0}", Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardDescription, Environment.NewLine));
        }

        #region Shutdown/Exit

        private static void ShutDown()
        {
            Engine.GameEngine?.Shutdown();

            Engine.GameEngine?.Dispose();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
        
        #region Events

        private void addCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GameObject go = new GameObject("Cube");

            //if (Engine.GameEngine.Manager.AddObject(go))
            //{
            //    Add_RemoveObject("Cube Added");
            //}
        }

        private void AddTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Triangle();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                Add_RemoveObject("Triangle Added");
            }
        }

        private void addDirectionalLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Engine.GameEngine.Manager.AddObject(new LightClass("Directional")))
            //{
            //    Add_RemoveObject("Directional Light Added");
            //}
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
                    //GameObject objclass = new GameObject(fileName: open.SafeFileName, path: open.FileName);
                    //if (Engine.GameEngine.Manager.AddObject(objclass))
                    //{
                    //    Add_RemoveObject(open.SafeFileName + " added");
                    //}
                }
            }
        }

        private void addPointLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Engine.GameEngine.Manager.AddObject(new LightClass()))
            //{
            //    Add_RemoveObject("Point light added");
            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);

                Engine.GameEngine.Manager.GameObjects.ToList().ForEach(x => x.IsSelected = false);
            }

            if (index >= 0)
            {
                tbName.Text = Engine.GameEngine.Manager.GameObjects[index].Name;
                Engine.GameEngine.Manager.GameObjects[index].IsSelected = true;

                //lblLocation.Text = "Location: " + Engine.GameEngine.Manager.GameObjects[index].MeshObject.ObjectPosition.ToString();

                //if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                //{
                //    ckbxLightOnOff.Visible = true;
                //    ckbxLightOnOff.Checked = (Engine.GameEngine.Manager.GameObjects[index] as LightClass).IsLightEnabled;
                //    lblColor.Text = string.Empty;
                //    btnColor.Visible = false;
                //}
                //else
                //{
                //    ckbxLightOnOff.Visible = false;
                //    lblColor.Text = "Color: " + Engine.GameEngine.Manager.GameObjects[index].MeshObject.MeshColorasString;
                //    btnColor.Visible = true;
                //}
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

        private void Information_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    this.AddToInformationDisplay(item.ToString());
                }
            }
        }

        private void resetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.Camera.ResetEye();

            UpdateCameraLocation();
        }

        private void ckbxLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)cmbObjectList.SelectedValue);
            }

            if (index >= 0)
            {
                //if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                //{
                //    (Engine.GameEngine.Manager.GameObjects[index] as LightClass).LightOnOff();
                //}
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
                    //colorDialog1.Color = Engine.GameEngine.Manager.GameObjects[index].MeshObject.MeshColor;
                }
            }

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (index >= 0)
                {
                    if (Engine.GameEngine.Manager.GameObjects[index] is GameObject)
                    {
                        //Engine.GameEngine.Manager.GameObjects[index].MeshObject.ApplyColor(colorDialog1.Color);
                    }
                }
            }
        }

        private void TxtName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbName.Text))
            {
                var gameObject = Engine.GameEngine.Manager.GameObjects.FirstOrDefault(x => x.IsSelected);

                if (gameObject != null)
                {
                    gameObject.Name = this.tbName.Text;

                    this.AddToInformationDisplay(string.Format("Object {0} has had its name changed to {1}.", gameObject.GetType().ToString(), this.tbName.Text));
                }
            }
        }

        #endregion Events

        #region Helper Methods

        private void Add_RemoveObject(string message)
        {
            lblAddRemove.Text = message;

            this.AddToInformationDisplay(message);
        }

        private void AddToInformationDisplay(string message)
        {
            tbInformation.AppendText($"{message} {Environment.NewLine}");
        }

        #endregion
    }
}