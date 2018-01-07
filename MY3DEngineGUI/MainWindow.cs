using MY3DEngine;
using MY3DEngine.BaseObjects;
using MY3DEngine.Primitives;
using MY3DEngineGUI.HelperForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MY3DEngineGUI
{
    public partial class MainWindow : Form
    {
        private bool _firstMouse;
        private Point _mouseLocation;
        private string gamePath;
        private bool isObjectSelected;
        private string className;

        public MainWindow()
        {
            InitializeComponent();

            var form = new Form
            {
                TopLevel = false,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.None,
                Visible = true,
                Width = this.ClientSize.Width,
                Height = this.ClientSize.Height
            };
            var loadExistingProject = new Button
            {
                Text = "Load Project",
                AutoSize = true
            };
            var createNewProject = new Button
            {
                Text = "Create Project",
                AutoSize = true
            };

            loadExistingProject.Left = (this.ClientSize.Width - loadExistingProject.Width) / 2;
            loadExistingProject.Top = ((this.ClientSize.Height - 50) - loadExistingProject.Height) / 2;
            loadExistingProject.Click += this.LoadExistingProject_Click;

            createNewProject.Left = (this.ClientSize.Width - createNewProject.Width) / 2;
            createNewProject.Top = ((this.ClientSize.Height + 50) - createNewProject.Height) / 2;
            createNewProject.Click += this.CreateNewProject_Click;

            form.Controls.Add(loadExistingProject);
            form.Controls.Add(createNewProject);

            this.Controls.Add(form);
            this.Controls.SetChildIndex(form, 0);

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

            //Engine.GameEngine.Exception.Information.CollectionChanged += Information_CollectionChanged;

            this.ExceptionBindingSource.DataSource = exceptions;

            this.AddToInformationDisplay($"Video card memory : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardMemory} MB");
            this.AddToInformationDisplay($"Video card description : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardDescription}");
        }

        #region Shutdown/Exit Events

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
        }

        #endregion Shutdown/Exit Events

        #region Old Camera -- FIX

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

        #endregion Old Camera -- FIX

        #region Old Event -- FIX

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

        private void BtnColor_Click(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((GameObject)GameObjectListComboBox.SelectedValue);

                if (index >= 0)
                {
                    if (Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                    {
                        var argb = new[] { 0, 0, 0, 0 };
                        if (Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                        {
                            var gameObject = Engine.GameEngine.Manager.GameObjects[index];

                            argb[0] = (int)gameObject.Vertex[0].Color.W;
                            argb[1] = (int)gameObject.Vertex[0].Color.X;
                            argb[2] = (int)gameObject.Vertex[0].Color.Y;
                            argb[3] = (int)gameObject.Vertex[0].Color.Z;
                        }

                        colorDialog1.Color = Color.FromArgb(argb[0] * 255, argb[1] * 255, argb[2] * 255, argb[3] * 255);
                    }

                    if (colorDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                        {
                            var gameObject = Engine.GameEngine.Manager.GameObjects[index];
                            var red = colorDialog1.Color.R / 255.0f;
                            var green = colorDialog1.Color.G / 255.0f;
                            var blue = colorDialog1.Color.B / 255.0f;
                            var alpha = colorDialog1.Color.A / 255.0f;

                            for (var i = 0; i < gameObject.Vertex.Length; ++i)
                            {
                                gameObject.Vertex[i].Color = new SharpDX.Vector4(red, green, blue, alpha);
                            }

                            gameObject.ApplyColor();
                        }
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

        private void ExceptionGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var clickedOnCellContents = this.ExceptionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            var exceptionValueDisplayForm = new ExceptionValueDisplayForm(clickedOnCellContents);

            exceptionValueDisplayForm.Show(this);
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

                Engine.GameEngine.Manager.GameObjects.ToList().ForEach(x => x.IsSelected = false);

                Engine.GameEngine.Manager.RemoveObject(gameObject);

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

        #endregion Old Event -- FIX

        #region Menu Events

        private void AddCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Cube();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                this.AddRemoveObject("Cube added.");
            }
        }

        private void AddTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Triangle();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                this.AddRemoveObject("Triangle added.");
            }
        }

        private void AddTriangleWithTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new TriangleWithTexture();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                this.AddRemoveObject("Triangle with texture added.");
            }
        }

        private void ClearErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.Exception.Exceptions.Clear();
        }

        private void ClearInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tbInformation.Clear();
        }

        private void GenerateGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MY3DEngine.Build.Build.GenerateCSharpFile(gamePath))
            {
                this.AddToInformationDisplay("Game generated successfully.");

                MessageBox.Show("Game generated successfully.", "Information");
            }
            else
            {
                this.AddToInformationDisplay("Game not generated successfully. Please see error log.");

                MessageBox.Show("Game not generated successfully. Please see error log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var fileInfo = new System.IO.FileInfo(dialog.FileName);
                gamePath = fileInfo.DirectoryName;

                if (Engine.GameEngine.Load(dialog.FileName))
                {
                    this.UpdateButtonsUseability();
                    this.AddToInformationDisplay("Game loaded successfully.");

                    MessageBox.Show("Game loaded successfully.", "Information");

                    generateGameToolStripMenuItem.Enabled = true;
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
                gamePath = dialog.SelectedPath;

                if (Engine.GameEngine.Save(gamePath))
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

        #region New/Load Project

        private void CreateNewProject_Click(object sender, EventArgs e)
        {
            var createNewProjectForm = new CreateNewProjectForm();

            DialogResult result = createNewProjectForm.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                this.Controls.RemoveAt(0);

                //this.tlvGameFiles.CanExpandGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                //this.tlvGameFiles.ChildrenGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                var files = Directory.EnumerateFiles(Engine.GameEngine.FolderLocation, "*.cs").Select(x => new { Path = x }).ToList();
                //this.tlvGameFiles.SetObjects(files);
                //tlvGameFiles.RefreshObjects(files);
                this.tlvGameFiles.Roots = files;
            }
        }

        private void LoadExistingProject_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion New/Load Project

        #region Helper Methods

        private static void ShutDown()
        {
            Engine.GameEngine?.Shutdown();

            Engine.GameEngine?.Dispose();
        }

        private void AddRemoveObject(string message)
        {
            this.AddToInformationDisplay(message);
            this.UpdateButtonsUseability();
        }

        private void AddToInformationDisplay(string message)
        {
            this.tbInformation.AppendText($"{message} {Environment.NewLine}");
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

        #region Content Menu Items

        private void TsmiAddClass_Click(object sender, EventArgs e)
        {
            var setNameForm = new SetNameForm();

            setNameForm.ClosingSetNameForm += this.SetNameForm_ClosingSetNameForm;

            var dialogResult = setNameForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                ClassFileBuilderForm form = new ClassFileBuilderForm(className);

                form.Show();
            }
        }

        private void SetNameForm_ClosingSetNameForm(object sender, ClosingSetNameEventArg args)
        {
            if(args != null)
            {
                className = args.ClassName;
            }
        }

        #endregion Content Menu Items
    }
}