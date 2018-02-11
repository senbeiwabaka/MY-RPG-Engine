using MY3DEngine.BaseObjects;
using MY3DEngine.GUI.HelperForms;
using MY3DEngine.Models;
using MY3DEngine.Primitives;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    public partial class MainWindow : Form
    {
        private bool _firstMouse;
        private Point _mouseLocation;
        private string className;
        private string gamePath;
        private bool isObjectSelected;
        private bool gameGeneratedSuccessfully;

        public MainWindow()
        {
            this.InitializeComponent();
            this.LoadOrCreateProject();

            this.useVsyncToolStripMenuItem.Checked = true;
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

        #region Camera -- FIX

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

        private void ResetCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.Camera.ResetCamera();
        }

        private void TbLeftRight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbLeftRight.Text))
            {
                Engine.GameEngine.Camera.SetPosition(new SharpDX.Vector3(float.Parse(tbLeftRight.Text), Engine.GameEngine.Camera.Position.Y, Engine.GameEngine.Camera.Position.Z));
            }
        }

        private void TbRotateX_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbRotateX.Text))
            {
                Engine.GameEngine.Camera.SetRotation(new SharpDX.Vector3(float.Parse(tbRotateX.Text), Engine.GameEngine.Camera.Rotation.Y, Engine.GameEngine.Camera.Rotation.Z));
            }
        }

        private void TbRotateY_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbRotateY.Text))
            {
                Engine.GameEngine.Camera.SetRotation(new SharpDX.Vector3(Engine.GameEngine.Camera.Rotation.X, float.Parse(tbRotateY.Text), Engine.GameEngine.Camera.Rotation.Z));
            }
        }

        private void TbRotateZ_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbRotateZ.Text))
            {
                Engine.GameEngine.Camera.SetRotation(new SharpDX.Vector3(Engine.GameEngine.Camera.Rotation.X, Engine.GameEngine.Camera.Rotation.Y, float.Parse(tbRotateZ.Text)));
            }
        }

        private void TbUp_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUpDown.Text))
            {
                Engine.GameEngine.Camera.SetPosition(new SharpDX.Vector3(Engine.GameEngine.Camera.Position.X, float.Parse(tbUpDown.Text), Engine.GameEngine.Camera.Position.Z));
            }
        }

        private void TbZoomInOut_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbZoomInOut.Text))
            {
                Engine.GameEngine.Camera.SetPosition(new SharpDX.Vector3(Engine.GameEngine.Camera.Position.X, Engine.GameEngine.Camera.Position.Y, float.Parse(tbZoomInOut.Text)));
            }
        }

        #endregion Camera -- FIX

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
            var open = new OpenFileDialog
            {
                Multiselect = false
            };
            var result = open.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (open.SafeFileName.ToLower().EndsWith(".x", StringComparison.CurrentCulture))
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

        private void ckbxLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((BaseObject)GameObjectListComboBox.SelectedValue);
            }

            if (index >= 0)
            {
                //if (Engine.GameEngine.Manager.GameObjects[index] is LightClass)
                //{
                //    (Engine.GameEngine.Manager.GameObjects[index] as LightClass).LightOnOff();
                //}
            }
        }

        private void globalLightsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GlobalLights();
        }

        #endregion Old Event -- FIX

        #region Events

        private void BtnColor_Click(object sender, EventArgs e)
        {
            int index = -1;
            lock (Engine.GameEngine.Manager)
            {
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((BaseObject)GameObjectListComboBox.SelectedValue);

                if (index >= 0)
                {
                    if (Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                    {
                        var argb = new[] { 0, 0, 0, 0 };
                        if (Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                        {
                            var gameObject = Engine.GameEngine.Manager.GameObjects[index];

                            argb[0] = (int)gameObject.Vertexies[0].Color.W;
                            argb[1] = (int)gameObject.Vertexies[0].Color.X;
                            argb[2] = (int)gameObject.Vertexies[0].Color.Y;
                            argb[3] = (int)gameObject.Vertexies[0].Color.Z;
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

                            for (var i = 0; i < gameObject.Vertexies.Length; ++i)
                            {
                                gameObject.Vertexies[i].Color = new SharpDX.Vector4(red, green, blue, alpha);
                            }

                            gameObject.ApplyColor();
                        }
                    }
                }
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
                index = Engine.GameEngine.Manager.GameObjects.IndexOf((BaseObject)GameObjectListComboBox.SelectedValue);

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

        private void RemoveGameObjectButton_Click(object sender, EventArgs e)
        {
            lock (Engine.GameEngine.Manager)
            {
                var gameObject = (BaseObject)GameObjectListComboBox.SelectedValue;

                Engine.GameEngine.Manager.GameObjects.ToList().ForEach(x => x.IsSelected = false);

                Engine.GameEngine.Manager.RemoveObject(gameObject);

                this.AddRemoveObject($"Game Object '{gameObject.Name}' was removed.");
            }
        }

        private void TbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GameObjectNameLabel.Focus();
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

                    this.AddToInformationDisplay($"Object {gameObject.GetType()} has had its name changed to '{this.tbName.Text}'.");
                }
            }
        }

        #endregion Events

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
            this.gameGeneratedSuccessfully = Build.Build.GenerateCSharpFile(Engine.GameEngine.FolderLocation);

            if (this.gameGeneratedSuccessfully)
            {
                this.AddToInformationDisplay("Game generated successfully.");

                MessageBox.Show("Game generated successfully.", "Information");
            }
            else
            {
                this.AddToInformationDisplay("Game not generated successfully. Please see error log.");

                MessageBox.Show("Game not generated successfully. Please see error log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.UpdateBuildMenuClickUsability();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = new FileInfo(dialog.FileName);
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

        private void UseVsyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GraphicsManager.ChangeVSyncState(useVsyncToolStripMenuItem.Checked);

            this.AddToInformationDisplay($"VSync is {useVsyncToolStripMenuItem.Checked}");
        }

        private void WireframOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.WireFrame(this.wireframOnOffToolStripMenuItem.Checked);
        }

        #endregion Menu Events

        #region New/Load Project

        // TODO -- FINISH
        private void CreateNewProject_Click(object sender, EventArgs e)
        {
            var createNewProjectForm = new CreateNewProjectForm();
            var result = createNewProjectForm.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                this.Controls.RemoveAt(0);

                //this.tlvGameFiles.CanExpandGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                //this.tlvGameFiles.ChildrenGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                var directory = new DirectoryInfo(Engine.GameEngine.FolderLocation);
                var files = directory.EnumerateFiles("*.cs").ToList();
                //this.tlvGameFiles.SetObjects(files);
                //tlvGameFiles.RefreshObjects(files);
                this.tlvGameFiles.Roots = files;

                this.fswClassFileWatcher.Path = Engine.GameEngine.FolderLocation;

                this.LoadGameEngine();
                this.UpdateGenerateMenuClickUsability();
            }
        }

        // TODO -- FINISH
        private void LoadExistingProject_Click(object sender, EventArgs e)
        {
            var selectFolderForm = new SelectFolderForm();
            var result = selectFolderForm.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                this.Controls.RemoveAt(0);

                //this.tlvGameFiles.CanExpandGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                //this.tlvGameFiles.ChildrenGetter = delegate (object x) {
                //    return (x is DirectoryInfo);
                //};

                var directory = new DirectoryInfo(Engine.GameEngine.FolderLocation);
                var files = directory.EnumerateFiles("*.cs").ToList();
                //this.tlvGameFiles.SetObjects(files);
                //tlvGameFiles.RefreshObjects(files);
                this.tlvGameFiles.Roots = files;

                this.fswClassFileWatcher.Path = Engine.GameEngine.FolderLocation;

                this.LoadGameEngine();
                this.UpdateGenerateMenuClickUsability();
            }
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

            lock (Engine.GameEngine.Manager)
            {
                this.TreeListViewSceneGraph.SetObjects(Engine.GameEngine.Manager.GameObjects);

                if (Engine.GameEngine.Manager.GameObjects.Count == 1)
                {
                    Engine.GameEngine.Manager.GameObjects.First().IsSelected = true;
                }

                if (Engine.GameEngine.Manager.GameObjects.Count == 1 && Engine.GameEngine.Manager.GameObjects.First().IsSelected)
                {
                    tbName.Text = Engine.GameEngine.Manager.GameObjects.First().Name;
                }

                if (!Engine.GameEngine.Manager.GameObjects.Any() || !Engine.GameEngine.Manager.GameObjects.Any(x => x.IsSelected))
                {
                    tbName.Text = string.Empty;
                }
            }
        }

        private void AddToInformationDisplay(string message)
        {
            this.tbInformation.AppendText($"{message} {Environment.NewLine}");
        }

        private void LoadGameEngine()
        {
            var graphicsException = new ExceptionData("Engine Graphics not setup correctly", "Engine", string.Empty);
            var exceptions = new BindingList<ExceptionData>();

            Engine.GameEngine.SettingsManager.Initialize();

            if (Engine.GameEngine.InitliazeGraphics(
                this.rendererPnl.Handle,
                this.rendererPnl.Width,
                this.rendererPnl.Height,
                vsyncEnabled: this.useVsyncToolStripMenuItem.Checked,
                fullScreen: false))
            {
                Engine.GameEngine.Initialize();

                exceptions = Engine.GameEngine.Exception.Exceptions;

                _mouseLocation = new Point(0, 0);
                _firstMouse = false;
                isObjectSelected = false;

                lock (Engine.GameEngine.Manager)
                {
                    this.GameObjectBindingSource.DataSource = Engine.GameEngine.Manager.GameObjects;
                    GameObjectListComboBox.DataSource = this.GameObjectBindingSource.DataSource;
                    this.TreeListViewSceneGraph.SetObjects(Engine.GameEngine.Manager.GameObjects, true);
                }
            }
            else
            {
                exceptions.Add(graphicsException);
            }

            this.ExceptionBindingSource.DataSource = exceptions;

            this.AddToInformationDisplay($"Video card memory : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardMemory} MB");
            this.AddToInformationDisplay($"Video card description : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardDescription}");
        }

        private void LoadOrCreateProject()
        {
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
        }

        private void OpenClassBuilder(string fileName, string folderPath = default(string))
        {
            var form = new ClassFileBuilderForm(fileName, folderPath);

            form.Show();
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

        private void UpdateGenerateMenuClickUsability()
        {
            this.generateGameToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(Engine.GameEngine.FolderLocation);
        }

        private void UpdateBuildMenuClickUsability()
        {
            this.buildGameToolStripMenuItem.Enabled = this.gameGeneratedSuccessfully;
        }

        #endregion Helper Methods

        #region Context Menu Items

        private void SetNameForm_ClosingSetNameForm(object sender, ClosingSetNameEventArg args)
        {
            if (args != null)
            {
                className = args.ClassName;
            }
        }

        private void TsmiAddClass_Click(object sender, EventArgs e)
        {
            var setNameForm = new SetNameForm();

            setNameForm.ClosingSetNameForm += this.SetNameForm_ClosingSetNameForm;

            var dialogResult = setNameForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                this.OpenClassBuilder(this.className);
            }
        }

        #endregion Context Menu Items

        #region File(s) Event(s)

        private void FswClassFileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                var fileInfo = new FileInfo(e.FullPath);

                this.tlvGameFiles.AddObject(fileInfo);
            }
        }

        private void TlvGameFiles_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (e.Item != null)
            {
                if (e.Item.RowObject is FileInfo file)
                {
                    this.OpenClassBuilder(file.Name, file.DirectoryName);
                }
            }
        }

        #endregion File(s) Event(s)

        private void buildGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Build.Build.BuildGame(Engine.GameEngine.FolderLocation, Engine.GameEngine.GameName))
            {
                this.AddToInformationDisplay("Game built successfully.");

                MessageBox.Show("Game built successfully.", "Information");
            }
            else
            {
                this.AddToInformationDisplay("Game not built successfully. Please see error log.");

                MessageBox.Show("Game not built successfully. Please see error log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}