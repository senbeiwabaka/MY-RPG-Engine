// <copyright file="MainWindow.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using My3DEngine.Utilities.Services;
using MY3DEngine.BaseObjects;
using MY3DEngine.BuildTools;
using MY3DEngine.GUI.HelperForms;
using MY3DEngine.GUI.Utilities;
using MY3DEngine.Models;
using MY3DEngine.Primitives;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MY3DEngine.GUI
{
    public partial class MainWindow : Form
    {
        private const string EngineTitle = "MY 3D Engine Builder";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ErrorModel graphicsException = new ErrorModel("Engine could not be setup correctly", "Engine", string.Empty);

        private string className;
        private string gamePath;
        private bool gameGeneratedSuccessfully;

        public MainWindow()
        {
            InitializeComponent();
            LoadOrCreateProject();

            useVsyncToolStripMenuItem.Checked = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutDown();
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
                Engine.GameEngine.Camera.SetRotation(
                    new SharpDX.Vector3(float.Parse(tbRotateX.Text, CultureInfo.CurrentCulture), Engine.GameEngine.Camera.Rotation.Y, Engine.GameEngine.Camera.Rotation.Z));
            }
        }

        private void TbRotateY_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbRotateY.Text))
            {
                Engine.GameEngine.Camera.SetRotation(
                    new SharpDX.Vector3(Engine.GameEngine.Camera.Rotation.X, float.Parse(tbRotateY.Text, CultureInfo.CurrentCulture), Engine.GameEngine.Camera.Rotation.Z));
            }
        }

        private void TbRotateZ_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbRotateZ.Text))
            {
                Engine.GameEngine.Camera.SetRotation(
                    new SharpDX.Vector3(Engine.GameEngine.Camera.Rotation.X, Engine.GameEngine.Camera.Rotation.Y, float.Parse(tbRotateZ.Text, CultureInfo.CurrentCulture)));
            }
        }

        private void TbUp_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUpDown.Text))
            {
                Engine.GameEngine.Camera.SetPosition(
                    new SharpDX.Vector3(Engine.GameEngine.Camera.Position.X, float.Parse(tbUpDown.Text, CultureInfo.CurrentCulture), Engine.GameEngine.Camera.Position.Z));
            }
        }

        private void TbZoomInOut_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbZoomInOut.Text))
            {
                Engine.GameEngine.Camera.SetPosition(
                    new SharpDX.Vector3(Engine.GameEngine.Camera.Position.X, Engine.GameEngine.Camera.Position.Y, float.Parse(tbZoomInOut.Text, CultureInfo.CurrentCulture)));
            }
        }

        private void AddDirectionalLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

        private void AddObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

        private void AddPointLightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

        private void CkbxLightOnOff_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

        private void GlobalLightsOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotSupportedException();
        }

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

                        ColorDialogForObjects.Color = Color.FromArgb(argb[0] * 255, argb[1] * 255, argb[2] * 255, argb[3] * 255);
                    }

                    if (ColorDialogForObjects.ShowDialog() == DialogResult.OK && Engine.GameEngine.Manager.GameObjects[index].IsPrimitive)
                    {
                        var gameObject = Engine.GameEngine.Manager.GameObjects[index];
                        var red = ColorDialogForObjects.Color.R / 255.0f;
                        var green = ColorDialogForObjects.Color.G / 255.0f;
                        var blue = ColorDialogForObjects.Color.B / 255.0f;
                        var alpha = ColorDialogForObjects.Color.A / 255.0f;

                        for (var i = 0; i < gameObject.Vertexies.Length; ++i)
                        {
                            gameObject.Vertexies[i].Color = new SharpDX.Vector4(red, green, blue, alpha);
                        }

                        gameObject.ApplyColor();
                    }
                }
            }
        }

        private void ExceptionGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var clickedOnCellContents = ExceptionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
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
                lblLocation.Text = string.Empty;
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

                AddRemoveObject($"Game Object '{gameObject.Name}' was removed.");
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
            if (!string.IsNullOrWhiteSpace(tbName.Text))
            {
                var gameObject = Engine.GameEngine.Manager.GameObjects.FirstOrDefault(x => x.IsSelected);

                if (gameObject != null)
                {
                    gameObject.Name = tbName.Text;

                    AddToInformationDisplay($"Object {gameObject.GetType()} has had its name changed to '{tbName.Text}'.");
                }
            }
        }

        private void AddCubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Cube();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Cube added.");
            }
        }

        private void AddTriangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new Triangle();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Triangle added.");
            }
        }

        private void AddTriangleWithTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new TriangleWithTexture();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Triangle with texture added.");
            }
        }

        private void AddCubeWithTextureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gameObject = new CubeWithTexture();

            if (Engine.GameEngine.Manager.AddObject(gameObject))
            {
                AddRemoveObject("Cube with texture added.");
            }
        }

        private void ClearErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.Exception.Exceptions.Clear();
        }

        private void ClearInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbInformation.Clear();
        }

        private void GenerateGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameGeneratedSuccessfully = Build.GenerateFilesForBuildingGame(Engine.GameEngine.SettingsManager.Settings.MainFolderLocation, new FileService());

            if (gameGeneratedSuccessfully)
            {
                AddToInformationDisplay("Game generated successfully.");

                MessageBox.Show("Game generated successfully.", MessageResources.Information);
            }
            else
            {
                AddToInformationDisplay("Game not generated successfully. Please see error log.");

                MessageBox.Show("Game not generated successfully. Please see error log.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateBuildMenuClickUsability();
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

                var gameLoadResult = GameEngineLoad.LoadProject(dialog.FileName, new FileService());

                if (gameLoadResult.Successful)
                {
                    UpdateButtonsUseability();
                    AddToInformationDisplay("Game loaded successfully.");

                    MessageBox.Show("Game loaded successfully.", MessageResources.Information);

                    generateGameToolStripMenuItem.Enabled = true;
                }
                else
                {
                    AddToInformationDisplay("Game not loaded successfully. Please see error log.");

                    MessageBox.Show("Game not loaded successfully. Please see error log.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;

            if (GameEngineSave.SaveProject(ToolsetGameModelManager.ToolsetGameModel.MainFileFolderLocation, Engine.GameEngine.Manager.GetGameObjects()))
            {
                AddToInformationDisplay("Game saved successfully.");

                MessageBox.Show("Game saved successfully.", MessageResources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AddToInformationDisplay("Game not saved successfully. Please see error log.");

                MessageBox.Show("Game not saved successfully. Please see error log.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UseWaitCursor = false;
        }

        private void TurnDebuggerOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.IsDebugginTurnedOn = !Engine.IsDebugginTurnedOn;

            AddToInformationDisplay(string.Format("Engine debugging is set to {0}", Engine.IsDebugginTurnedOn));
        }

        private void UseVsyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.GraphicsManager.ChangeVSyncState(useVsyncToolStripMenuItem.Checked);

            AddToInformationDisplay($"VSync is {useVsyncToolStripMenuItem.Checked}");
        }

        private void WireframOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.GameEngine.WireFrame(wireframOnOffToolStripMenuItem.Checked);
        }

        private void LoadAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileExplorer = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".bmp",
                Multiselect = true
            };

            var result = fileExplorer.ShowDialog();

            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                for (var i = 0; i < fileExplorer.FileNames.Length; ++i)
                {
                    if (fileExplorer.SafeFileNames[i].EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        new FileService().CopyFile(fileExplorer.FileNames[i], $"{Engine.GameEngine.SettingsManager.Settings.AssetsPath}\\{fileExplorer.SafeFileNames[i]}", true);
                    }
                }
            }

            fileExplorer.Dispose();
        }

        private void BuildGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Build.BuildGame(Engine.GameEngine.SettingsManager.Settings.MainFolderLocation, Engine.GameEngine.SettingsManager.Settings.GameName, new FileService()))
            {
                AddToInformationDisplay("Game built successfully.");

                playGameToolStripMenuItem.Enabled = true;

                MessageBox.Show("Game built successfully.", MessageResources.Information);
            }
            else
            {
                AddToInformationDisplay("Game not built successfully. Please see error log.");

                playGameToolStripMenuItem.Enabled = false;

                MessageBox.Show("Game not built successfully. Please see error log.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var logView = new LogViewer($"{Environment.CurrentDirectory}\\logs\\{DateTime.Today.ToString("yyyy-MM-dd")}_log.log");

            logView.Show(this);
        }

        private void DeleteLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new FileService().DeleteFile($"{Environment.CurrentDirectory}\\logs\\{DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture)}_log.log"))
            {
                AddToInformationDisplay("Log successfully deleted.");

                MessageBox.Show("Log successfully deleted.", "File Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AddToInformationDisplay("Log unsuccessfully deleted.");

                MessageBox.Show("Log unsuccessfully deleted.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start($@"{Engine.GameEngine.SettingsManager.Settings.MainFolderLocation}\Game.exe");
        }

        private void CloseCurrentProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to save the current project before quiting?", "Quite Current Project?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            UseWaitCursor = true;

            if (result == DialogResult.Cancel)
            {
                return;
            }

            if (result == DialogResult.Yes)
            {
                if (GameEngineSave.SaveProject(ToolsetGameModelManager.ToolsetGameModel.FolderLocation, Engine.GameEngine.Manager.GetGameObjects()))
                {
                    MessageBox.Show("Game saved successfully.", MessageResources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    UseWaitCursor = false;

                    MessageBox.Show("Game not saved successfully. Please see error log.", MessageResources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            Engine.GameEngine.Shutdown();
            InitializeComponent();
            LoadOrCreateProject();

            UseWaitCursor = false;
        }

        private void CreateNewProject_Click(object sender, EventArgs e)
        {
            // create an instance of the new project form
            using (var createNewProjectForm = new CreateNewProjectForm())
            {
                var result = createNewProjectForm.ShowDialog(); // display the form

                // if they have hit okay or yes then we can create the new project
                if (result == DialogResult.OK || result == DialogResult.Yes)
                {
                    // this removes the load/create project cover
                    Controls.RemoveAt(0);

                    // get the directory where they created the new project at
                    var directory = new DirectoryInfo(ToolsetGameModelManager.ToolsetGameModel.FolderLocation);
                    var files = directory.EnumerateFiles("*.cs").ToList(); // currently get a list of the c# files

                    // add them to the tree view
                    tlvGameFiles.Roots = files;

                    // add the new project folder location to the watch path for when new files are
                    // created outside of the toolkit so they show up
                    fswClassFileWatcher.Path = ToolsetGameModelManager.ToolsetGameModel.FolderLocation;

                    // load the game engine into the window
                    LoadGameEngine();
                    UpdateGenerateMenuClickUsability();
                }
            }
        }

        // TODO -- FINISH
        private void LoadExistingProject_Click(object sender, EventArgs e)
        {
            using (var selectFolderForm = new SelectFolderForm())
            {
                var result = selectFolderForm.ShowDialog();

                if (result == DialogResult.OK || result == DialogResult.Yes)
                {
                    Controls.RemoveAt(0);

                    var directory = new DirectoryInfo(ToolsetGameModelManager.ToolsetGameModel.FolderLocation);
                    var files = directory.EnumerateFiles("*.cs", SearchOption.AllDirectories).ToList();
                    tlvGameFiles.Roots = files;

                    fswClassFileWatcher.Path = ToolsetGameModelManager.ToolsetGameModel.FolderLocation;

                    LoadGameEngine();
                    UpdateGenerateMenuClickUsability();

                    if (directory.EnumerateFiles("*.exe", SearchOption.AllDirectories).Any())
                    {
                        buildGameToolStripMenuItem.Enabled = playGameToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private static void ShutDown()
        {
            Engine.GameEngine?.Shutdown();

            Engine.GameEngine?.Dispose();

            LogManager.Shutdown();
        }

        private void AddRemoveObject(string message)
        {
            AddToInformationDisplay(message);
            UpdateButtonsUseability();

            lock (Engine.GameEngine.Manager)
            {
                TreeListViewSceneGraph.SetObjects(Engine.GameEngine.Manager.GameObjects);

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
            tbInformation.AppendText($"{message} {Environment.NewLine}");
            Logger.Info($"{message} {Environment.NewLine}");
        }

        /// <summary>
        /// Load the game engine and its required components
        /// </summary>
        private void LoadGameEngine()
        {
            if (!Engine.GameEngine.SettingsManager.Initialize(
                ToolsetGameModelManager.ToolsetGameModel.FolderLocation,
                ToolsetGameModelManager.ToolsetGameModel.GameName,
                ToolsetGameModelManager.ToolsetGameModel.Settings,
                new FileService()))
            {
                AddToInformationDisplay("Settings did not initialize correctly.");
                Logger.Error("Settings did not initialize correctly.");
            }

            GameEngineSave.SaveSettings(
                Engine.GameEngine.SettingsManager.Settings.MainFolderLocation,
                Engine.GameEngine.SettingsManager.Settings.SettingsFileName,
                Engine.GameEngine.SettingsManager.Settings,
                new FileService());

            if (Engine.GameEngine.InitliazeGraphics(
                            rendererPnl.Handle,
                            rendererPnl.Width,
                            rendererPnl.Height,
                            vsyncEnabled: useVsyncToolStripMenuItem.Checked,
                            fullScreen: false)
                            && Engine.GameEngine.Initialize())
            {
                ExceptionBindingSource.DataSource = Engine.GameEngine?.Exception?.Exceptions;

                Text = $"{EngineTitle}";

                if (!string.IsNullOrWhiteSpace(Engine.GameEngine.SettingsManager.Settings.GameName))
                {
                    Text += $" -- Game: {Engine.GameEngine.SettingsManager.Settings.GameName}";
                }

                var gameObjects = new List<object>();
                GameEngineLoad.LoadLevel(Engine.GameEngine.SettingsManager.Settings.LevelsPath, gameObjects);

                lock (Engine.GameEngine.Manager)
                {
                    Engine.GameEngine.Manager.LoadObjects(gameObjects.Cast<BaseObject>());
                    GameObjectBindingSource.DataSource = Engine.GameEngine.Manager.GameObjects;
                    GameObjectListComboBox.DataSource = GameObjectBindingSource.DataSource;
                    TreeListViewSceneGraph.SetObjects(Engine.GameEngine.Manager.GameObjects, true);
                }

                AddToInformationDisplay($"Video card memory : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardMemory} MB");
                AddToInformationDisplay($"Video card description : {Engine.GameEngine.GraphicsManager.GetDirectXManager.VideoCardDescription}");

                return;
            }

            ExceptionBindingSource.DataSource = Engine.GameEngine?.Exception?.Exceptions;

            if (ExceptionBindingSource.DataSource == null)
            {
                ExceptionBindingSource.DataSource = new BindingList<ErrorModel>
                {
                    graphicsException
                };
            }
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
                Width = ClientSize.Width,
                Height = ClientSize.Height
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

            loadExistingProject.Left = (ClientSize.Width - loadExistingProject.Width) / 2;
            loadExistingProject.Top = ((ClientSize.Height - 50) - loadExistingProject.Height) / 2;
            loadExistingProject.Click += LoadExistingProject_Click;

            createNewProject.Left = (ClientSize.Width - createNewProject.Width) / 2;
            createNewProject.Top = ((ClientSize.Height + 50) - createNewProject.Height) / 2;
            createNewProject.Click += CreateNewProject_Click;

            form.Controls.Add(loadExistingProject);
            form.Controls.Add(createNewProject);

            Controls.Add(form);
            Controls.SetChildIndex(form, 0);
        }

        private void OpenClassBuilder(string fileName, string folderPath = default(string))
        {
            if (!fileName.EndsWith(".cs", StringComparison.InvariantCultureIgnoreCase))
            {
                fileName += ".cs";
            }

            var form = new ClassFileBuilderForm(fileName, folderPath);

            form.Show();
        }

        private void UpdateButtonsUseability()
        {
            if (Engine.GameEngine.Manager.GameObjects.Any(x => x.IsPrimitive))
            {
                ChangeGameObjectColorButton.Enabled = RemoveGameObjectButton.Enabled = true;
            }
            else
            {
                ChangeGameObjectColorButton.Enabled = RemoveGameObjectButton.Enabled = false;
            }
        }

        /// <summary>
        /// Set the enabled/disabled status of the generate game menu item click
        /// </summary>
        private void UpdateGenerateMenuClickUsability()
        {
            generateGameToolStripMenuItem.Enabled = !string.IsNullOrWhiteSpace(Engine.GameEngine.SettingsManager.Settings.MainFolderLocation);
        }

        private void UpdateBuildMenuClickUsability()
        {
            buildGameToolStripMenuItem.Enabled = gameGeneratedSuccessfully;
        }

        private void SetNameForm_ClosingSetNameForm(object sender, ClosingSetNameEventArgs args)
        {
            if (args != null)
            {
                className = args.ClassName;
            }
        }

        private void TsmiAddClass_Click(object sender, EventArgs e)
        {
            var setNameForm = new SetNameForm();

            setNameForm.ClosingSetNameForm += SetNameForm_ClosingSetNameForm;

            var dialogResult = setNameForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                OpenClassBuilder(className);
            }
        }

        private void FswClassFileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                var fileInfo = new FileInfo(e.FullPath);

                tlvGameFiles.AddObject(fileInfo);
            }
        }

        private void TlvGameFiles_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            cmsGameFilesRightClickMenu.Items.Clear();

            if (e.Item != null)
            {
                if (e.Item.RowObject is FileInfo file)
                {
                    var openFile = new ToolStripMenuItem("Open File");
                    openFile.Click += OpenFile_Click;
                    openFile.Tag = file;

                    var deleteFile = new ToolStripMenuItem("Delete File");

                    cmsGameFilesRightClickMenu.Items.Add(openFile);
                }
            }
            else
            {
                cmsGameFilesRightClickMenu.Items.Add(tsmiAddClass);
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            var toolStripMenuItem = sender as ToolStripMenuItem;
            var file = toolStripMenuItem.Tag as FileInfo;
            OpenClassBuilder(file.Name, file.DirectoryName);
        }
    }
}
