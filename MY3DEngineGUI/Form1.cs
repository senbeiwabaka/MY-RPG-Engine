using MY3DEngine;
using MY3DEngine.BaseObjects;
using MY3DEngine.Primitives;
using System;
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
        private bool isObjectSelected;

        public Form1()
        {
            InitializeComponent();

            var graphicsException = new ExceptionData("Engine Graphics not setup correctly", "Engine", string.Empty);
            var exceptions = new BindingList<ExceptionData>();

            if (Engine.GameEngine.InitliazeGraphics(
                this.rendererPnl.Handle,
                this.rendererPnl.Width,
                this.rendererPnl.Height,
                false,
                false))
            {
                Engine.GameEngine.Initialize(this.Handle);

                exceptions = Engine.GameEngine.Exception.Exceptions;

                //rendererPnl.MouseWheel += rendererPnl_MouseWheel;

                _mouseLocation = new Point(0, 0);
                _firstMouse = false;
                isObjectSelected = false;

                lock (Engine.GameEngine.Manager)
                {
                    this.GameObjectBindingSource.DataSource = Engine.GameEngine.Manager.GameObjects;
                    GameObjectListComboBox.DataSource = this.GameObjectBindingSource.DataSource;
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
        }

        #endregion Camera

        #region Events

        private void AddCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GameObject go = new GameObject("Cube");

            //if (Engine.GameEngine.Manager.AddObject(go))
            //{
            //    Add_RemoveObject("Cube Added");
            //}
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

        private void AddTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Triangle();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Triangle added.");
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)GameObjectListComboBox.SelectedValue);
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

        private void ckbxLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)GameObjectListComboBox.SelectedValue);
            }

            if (index >= 0)
            {
                //if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                //{
                //    (Engine.GameEngine.Manager.GameObjects[index] as LightClass).LightOnOff();
                //}
            }
        }

        private void GameObjectListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)GameObjectListComboBox.SelectedValue);

                Engine.GameEngine.Manager.GameObjects.ToList().ForEach(x => x.IsSelected = false);
            }

            if (index >= 0)
            {
                tbName.Text = Engine.GameEngine.Manager.GameObjects[index].Name;

                lock (Engine.GameEngine.Manager)
                {
                    Engine.GameEngine.Manager.GameObjects[index].IsSelected = true;
                }
            }
            else
            {
                this.lblLocation.Text = string.Empty;
                lblColor.Text = string.Empty;
            }
        }

        private void globalLightsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GlobalLights();
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

        private void RemoveGameObjectButton_Click(object sender, EventArgs e)
        {
            lock (Engine.GameEngine.Manager)
            {
                var gameObject = (GameObject)GameObjectListComboBox.SelectedValue;
                var index = Engine.GameEngine.Manager.GameObjects.IndexOf(gameObject);

                Engine.GameEngine.Manager.GameObjects.ToList().ForEach(x => x.IsSelected = false);

                Engine.GameEngine.Manager.GameObjects.RemoveAt(index);

                this.AddRemoveObject(string.Format("Game Object '{0}' was removed.", gameObject.Name));
            }
        }

        private void resetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void TxtName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbName.Text))
            {
                var gameObject = Engine.GameEngine.Manager.GameObjects.FirstOrDefault(x => x.IsSelected);

                if (gameObject != null)
                {
                    gameObject.Name = this.tbName.Text;

                    this.AddToInformationDisplay(string.Format("Object {0} has had its name changed to '{1}'.", gameObject.GetType().ToString(), this.tbName.Text));
                }
            }
        }

        private void wireframOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.WireFrame();
        }

        #endregion Events

        #region Menu Events

        private void AddTriangleWithTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new TriangleWithTexture();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Triangle with texture added.");
            }
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.FileName;

                if (Engine.GameEngine.Load(path))
                {
                    this.UpdateButtonsUseability();
                    this.AddToInformationDisplay("Game loaded successfully.");

                    MessageBox.Show("Game loaded successfully.", "Information");
                }
                else
                {
                    this.AddToInformationDisplay("Game not loaded successfully. Please see error log.");

                    MessageBox.Show("Game not loaded successfully. Please see error log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;

                if (Engine.GameEngine.Save(path))
                {
                    this.AddToInformationDisplay("Game saved successfully.");

                    MessageBox.Show("Game saved successfully.", "Information");
                }
                else
                {
                    this.AddToInformationDisplay("Game not saved successfully. Please see error log.");

                    MessageBox.Show("Game not saved successfully. Please see error log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TurnDebuggerOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.IsDebugginTurnedOn = !Engine.IsDebugginTurnedOn;

            this.AddToInformationDisplay(string.Format("Engine debugging is set to {0}", Engine.IsDebugginTurnedOn));
        }

        #endregion Menu Events

        #region Helper Methods

        private void AddRemoveObject(string message)
        {
            this.AddToInformationDisplay(message);
            this.UpdateButtonsUseability();
        }

        private void AddToInformationDisplay(string message)
        {
            tbInformation.AppendText($"{message} {Environment.NewLine}");
        }

        private void UpdateButtonsUseability()
        {
            if (Engine.GameEngine.Manager.GameObjects.Count > 0)
            {
                this.ChangeGameObjectColorButton.Enabled = this.RemoveGameObjectButton.Enabled = true;
            }
            else
            {
                this.ChangeGameObjectColorButton.Enabled = this.RemoveGameObjectButton.Enabled = false;
            }
        }

        #endregion Helper Methods
    }
}