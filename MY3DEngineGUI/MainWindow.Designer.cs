namespace MY3DEngine.GUI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rendererPnl = new System.Windows.Forms.Panel();
            this.ExceptionGridView = new System.Windows.Forms.DataGridView();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackTrace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExceptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabEditPlay = new System.Windows.Forms.TabControl();
            this.tbEdit = new System.Windows.Forms.TabPage();
            this.tbZoomInOut = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRotateZ = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbRotateY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRotateX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbLeftRight = new System.Windows.Forms.TextBox();
            this.tbUpDown = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlvGameFiles = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cmsGameFilesRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddClass = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.TreeListViewSceneGraph = new BrightIdeasSoftware.TreeListView();
            this.olvSceneName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pnlObjectProperties = new System.Windows.Forms.Panel();
            this.RemoveGameObjectButton = new System.Windows.Forms.Button();
            this.ChangeGameObjectColorButton = new System.Windows.Forms.Button();
            this.ckbxLightOnOff = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GameObjectListComboBox = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.GameObjectNameLabel = new System.Windows.Forms.Label();
            this.tbInformation = new System.Windows.Forms.TextBox();
            this.lblCameraPosition = new System.Windows.Forms.Label();
            this.tbPlay = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shapesObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTriangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTriangleWithTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRandomTerrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPointLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDirectionalLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalLightsOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnDebuggerOnOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useVsyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.GameObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fswClassFileWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionBindingSource)).BeginInit();
            this.tabEditPlay.SuspendLayout();
            this.tbEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvGameFiles)).BeginInit();
            this.cmsGameFilesRightClickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListViewSceneGraph)).BeginInit();
            this.pnlObjectProperties.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameObjectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswClassFileWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // rendererPnl
            // 
            this.rendererPnl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rendererPnl.Location = new System.Drawing.Point(237, 6);
            this.rendererPnl.Name = "rendererPnl";
            this.rendererPnl.Size = new System.Drawing.Size(804, 600);
            this.rendererPnl.TabIndex = 1;
            // 
            // ExceptionGridView
            // 
            this.ExceptionGridView.AllowUserToDeleteRows = false;
            this.ExceptionGridView.AllowUserToOrderColumns = true;
            this.ExceptionGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExceptionGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ExceptionGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ExceptionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExceptionGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Message,
            this.StackTrace,
            this.Source});
            this.ExceptionGridView.DataSource = this.ExceptionBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExceptionGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.ExceptionGridView.Location = new System.Drawing.Point(6, 611);
            this.ExceptionGridView.Name = "ExceptionGridView";
            this.ExceptionGridView.ReadOnly = true;
            this.ExceptionGridView.Size = new System.Drawing.Size(424, 90);
            this.ExceptionGridView.TabIndex = 2;
            this.ExceptionGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ExceptionGridView_CellContentClick);
            // 
            // Message
            // 
            this.Message.DataPropertyName = "Message";
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            // 
            // StackTrace
            // 
            this.StackTrace.DataPropertyName = "StackTrace";
            this.StackTrace.HeaderText = "Stack Trace";
            this.StackTrace.Name = "StackTrace";
            this.StackTrace.ReadOnly = true;
            // 
            // Source
            // 
            this.Source.DataPropertyName = "Source";
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            this.Source.ReadOnly = true;
            // 
            // tabEditPlay
            // 
            this.tabEditPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabEditPlay.Controls.Add(this.tbEdit);
            this.tabEditPlay.Controls.Add(this.tbPlay);
            this.tabEditPlay.Location = new System.Drawing.Point(0, 27);
            this.tabEditPlay.Name = "tabEditPlay";
            this.tabEditPlay.SelectedIndex = 0;
            this.tabEditPlay.Size = new System.Drawing.Size(1055, 733);
            this.tabEditPlay.TabIndex = 3;
            // 
            // tbEdit
            // 
            this.tbEdit.Controls.Add(this.tbZoomInOut);
            this.tbEdit.Controls.Add(this.label10);
            this.tbEdit.Controls.Add(this.label9);
            this.tbEdit.Controls.Add(this.tbRotateZ);
            this.tbEdit.Controls.Add(this.label8);
            this.tbEdit.Controls.Add(this.tbRotateY);
            this.tbEdit.Controls.Add(this.label7);
            this.tbEdit.Controls.Add(this.tbRotateX);
            this.tbEdit.Controls.Add(this.label6);
            this.tbEdit.Controls.Add(this.tbLeftRight);
            this.tbEdit.Controls.Add(this.tbUpDown);
            this.tbEdit.Controls.Add(this.label5);
            this.tbEdit.Controls.Add(this.label4);
            this.tbEdit.Controls.Add(this.label3);
            this.tbEdit.Controls.Add(this.tlvGameFiles);
            this.tbEdit.Controls.Add(this.label2);
            this.tbEdit.Controls.Add(this.TreeListViewSceneGraph);
            this.tbEdit.Controls.Add(this.pnlObjectProperties);
            this.tbEdit.Controls.Add(this.tbInformation);
            this.tbEdit.Controls.Add(this.lblCameraPosition);
            this.tbEdit.Controls.Add(this.rendererPnl);
            this.tbEdit.Controls.Add(this.ExceptionGridView);
            this.tbEdit.Location = new System.Drawing.Point(4, 22);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tbEdit.Size = new System.Drawing.Size(1047, 707);
            this.tbEdit.TabIndex = 0;
            this.tbEdit.Text = "Edit";
            this.tbEdit.UseVisualStyleBackColor = true;
            // 
            // tbZoomInOut
            // 
            this.tbZoomInOut.Location = new System.Drawing.Point(85, 479);
            this.tbZoomInOut.Name = "tbZoomInOut";
            this.tbZoomInOut.Size = new System.Drawing.Size(40, 20);
            this.tbZoomInOut.TabIndex = 24;
            this.tbZoomInOut.TextChanged += new System.EventHandler(this.TbZoomInOut_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 482);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Zoom In/Out";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 414);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Rotate:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRotateZ
            // 
            this.tbRotateZ.Location = new System.Drawing.Point(155, 440);
            this.tbRotateZ.Name = "tbRotateZ";
            this.tbRotateZ.Size = new System.Drawing.Size(35, 20);
            this.tbRotateZ.TabIndex = 21;
            this.tbRotateZ.TextChanged += new System.EventHandler(this.TbRotateZ_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(135, 443);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Z";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRotateY
            // 
            this.tbRotateY.Location = new System.Drawing.Point(93, 440);
            this.tbRotateY.Name = "tbRotateY";
            this.tbRotateY.Size = new System.Drawing.Size(35, 20);
            this.tbRotateY.TabIndex = 19;
            this.tbRotateY.TextChanged += new System.EventHandler(this.TbRotateY_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(73, 443);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Y";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRotateX
            // 
            this.tbRotateX.Location = new System.Drawing.Point(31, 440);
            this.tbRotateX.Name = "tbRotateX";
            this.tbRotateX.Size = new System.Drawing.Size(35, 20);
            this.tbRotateX.TabIndex = 17;
            this.tbRotateX.TextChanged += new System.EventHandler(this.TbRotateX_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 443);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "X";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbLeftRight
            // 
            this.tbLeftRight.Location = new System.Drawing.Point(177, 383);
            this.tbLeftRight.Name = "tbLeftRight";
            this.tbLeftRight.Size = new System.Drawing.Size(40, 20);
            this.tbLeftRight.TabIndex = 15;
            this.tbLeftRight.TextChanged += new System.EventHandler(this.TbLeftRight_TextChanged);
            // 
            // tbUpDown
            // 
            this.tbUpDown.Location = new System.Drawing.Point(71, 383);
            this.tbUpDown.Name = "tbUpDown";
            this.tbUpDown.Size = new System.Drawing.Size(40, 20);
            this.tbUpDown.TabIndex = 14;
            this.tbUpDown.TextChanged += new System.EventHandler(this.TbUp_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 387);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Left/Right";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Up/Down";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Files";
            // 
            // tlvGameFiles
            // 
            this.tlvGameFiles.AllColumns.Add(this.olvColumn1);
            this.tlvGameFiles.CellEditUseWholeCell = false;
            this.tlvGameFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.tlvGameFiles.ContextMenuStrip = this.cmsGameFilesRightClickMenu;
            this.tlvGameFiles.Cursor = System.Windows.Forms.Cursors.Default;
            this.tlvGameFiles.Location = new System.Drawing.Point(11, 280);
            this.tlvGameFiles.Name = "tlvGameFiles";
            this.tlvGameFiles.ShowGroups = false;
            this.tlvGameFiles.Size = new System.Drawing.Size(220, 97);
            this.tlvGameFiles.TabIndex = 11;
            this.tlvGameFiles.UseCompatibleStateImageBehavior = false;
            this.tlvGameFiles.View = System.Windows.Forms.View.Details;
            this.tlvGameFiles.VirtualMode = true;
            this.tlvGameFiles.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.TlvGameFiles_CellRightClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.Text = "Name";
            // 
            // cmsGameFilesRightClickMenu
            // 
            this.cmsGameFilesRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddClass});
            this.cmsGameFilesRightClickMenu.Name = "cmsGameFilesRightClickMenu";
            this.cmsGameFilesRightClickMenu.Size = new System.Drawing.Size(127, 26);
            // 
            // tsmiAddClass
            // 
            this.tsmiAddClass.Name = "tsmiAddClass";
            this.tsmiAddClass.Size = new System.Drawing.Size(126, 22);
            this.tsmiAddClass.Text = "Add Class";
            this.tsmiAddClass.Click += new System.EventHandler(this.TsmiAddClass_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Scene Graph";
            // 
            // TreeListViewSceneGraph
            // 
            this.TreeListViewSceneGraph.AllColumns.Add(this.olvSceneName);
            this.TreeListViewSceneGraph.CellEditUseWholeCell = false;
            this.TreeListViewSceneGraph.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvSceneName});
            this.TreeListViewSceneGraph.Location = new System.Drawing.Point(11, 164);
            this.TreeListViewSceneGraph.Name = "TreeListViewSceneGraph";
            this.TreeListViewSceneGraph.ShowGroups = false;
            this.TreeListViewSceneGraph.Size = new System.Drawing.Size(220, 97);
            this.TreeListViewSceneGraph.TabIndex = 9;
            this.TreeListViewSceneGraph.UseCompatibleStateImageBehavior = false;
            this.TreeListViewSceneGraph.View = System.Windows.Forms.View.Details;
            this.TreeListViewSceneGraph.VirtualMode = true;
            // 
            // olvSceneName
            // 
            this.olvSceneName.AspectName = "Name";
            this.olvSceneName.Text = "Name";
            // 
            // pnlObjectProperties
            // 
            this.pnlObjectProperties.Controls.Add(this.RemoveGameObjectButton);
            this.pnlObjectProperties.Controls.Add(this.ChangeGameObjectColorButton);
            this.pnlObjectProperties.Controls.Add(this.ckbxLightOnOff);
            this.pnlObjectProperties.Controls.Add(this.label1);
            this.pnlObjectProperties.Controls.Add(this.GameObjectListComboBox);
            this.pnlObjectProperties.Controls.Add(this.lblColor);
            this.pnlObjectProperties.Controls.Add(this.lblLocation);
            this.pnlObjectProperties.Controls.Add(this.tbName);
            this.pnlObjectProperties.Controls.Add(this.GameObjectNameLabel);
            this.pnlObjectProperties.Location = new System.Drawing.Point(11, 6);
            this.pnlObjectProperties.Name = "pnlObjectProperties";
            this.pnlObjectProperties.Size = new System.Drawing.Size(220, 139);
            this.pnlObjectProperties.TabIndex = 8;
            // 
            // RemoveGameObjectButton
            // 
            this.RemoveGameObjectButton.Enabled = false;
            this.RemoveGameObjectButton.Location = new System.Drawing.Point(106, 85);
            this.RemoveGameObjectButton.Name = "RemoveGameObjectButton";
            this.RemoveGameObjectButton.Size = new System.Drawing.Size(89, 23);
            this.RemoveGameObjectButton.TabIndex = 6;
            this.RemoveGameObjectButton.Text = "Remove";
            this.RemoveGameObjectButton.UseVisualStyleBackColor = true;
            this.RemoveGameObjectButton.Click += new System.EventHandler(this.RemoveGameObjectButton_Click);
            // 
            // ChangeGameObjectColorButton
            // 
            this.ChangeGameObjectColorButton.Enabled = false;
            this.ChangeGameObjectColorButton.Location = new System.Drawing.Point(11, 85);
            this.ChangeGameObjectColorButton.Name = "ChangeGameObjectColorButton";
            this.ChangeGameObjectColorButton.Size = new System.Drawing.Size(89, 23);
            this.ChangeGameObjectColorButton.TabIndex = 5;
            this.ChangeGameObjectColorButton.Text = "Change Color";
            this.ChangeGameObjectColorButton.UseVisualStyleBackColor = true;
            this.ChangeGameObjectColorButton.Click += new System.EventHandler(this.BtnColor_Click);
            // 
            // ckbxLightOnOff
            // 
            this.ckbxLightOnOff.AutoSize = true;
            this.ckbxLightOnOff.Enabled = false;
            this.ckbxLightOnOff.Location = new System.Drawing.Point(11, 114);
            this.ckbxLightOnOff.Name = "ckbxLightOnOff";
            this.ckbxLightOnOff.Size = new System.Drawing.Size(85, 17);
            this.ckbxLightOnOff.TabIndex = 4;
            this.ckbxLightOnOff.Text = "Light On/Off";
            this.ckbxLightOnOff.UseVisualStyleBackColor = true;
            this.ckbxLightOnOff.Visible = false;
            this.ckbxLightOnOff.CheckedChanged += new System.EventHandler(this.ckbxLightOnOff_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Object(s) Information";
            // 
            // GameObjectListComboBox
            // 
            this.GameObjectListComboBox.FormattingEnabled = true;
            this.GameObjectListComboBox.Location = new System.Drawing.Point(12, 32);
            this.GameObjectListComboBox.Name = "GameObjectListComboBox";
            this.GameObjectListComboBox.Size = new System.Drawing.Size(202, 21);
            this.GameObjectListComboBox.TabIndex = 6;
            this.GameObjectListComboBox.SelectedIndexChanged += new System.EventHandler(this.GameObjectListComboBox_SelectedIndexChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(6, 64);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(0, 13);
            this.lblColor.TabIndex = 3;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(6, 40);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(0, 13);
            this.lblLocation.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(50, 59);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(163, 20);
            this.tbName.TabIndex = 1;
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbName_KeyPress);
            this.tbName.Leave += new System.EventHandler(this.TxtName_Leave);
            // 
            // GameObjectNameLabel
            // 
            this.GameObjectNameLabel.AutoSize = true;
            this.GameObjectNameLabel.Location = new System.Drawing.Point(9, 62);
            this.GameObjectNameLabel.Name = "GameObjectNameLabel";
            this.GameObjectNameLabel.Size = new System.Drawing.Size(35, 13);
            this.GameObjectNameLabel.TabIndex = 0;
            this.GameObjectNameLabel.Text = "Name";
            // 
            // tbInformation
            // 
            this.tbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInformation.Location = new System.Drawing.Point(436, 611);
            this.tbInformation.Multiline = true;
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInformation.Size = new System.Drawing.Size(605, 90);
            this.tbInformation.TabIndex = 7;
            // 
            // lblCameraPosition
            // 
            this.lblCameraPosition.AutoSize = true;
            this.lblCameraPosition.Location = new System.Drawing.Point(8, 6);
            this.lblCameraPosition.Name = "lblCameraPosition";
            this.lblCameraPosition.Size = new System.Drawing.Size(0, 13);
            this.lblCameraPosition.TabIndex = 3;
            // 
            // tbPlay
            // 
            this.tbPlay.Location = new System.Drawing.Point(4, 22);
            this.tbPlay.Name = "tbPlay";
            this.tbPlay.Padding = new System.Windows.Forms.Padding(3);
            this.tbPlay.Size = new System.Drawing.Size(1047, 707);
            this.tbPlay.TabIndex = 1;
            this.tbPlay.Text = "Play";
            this.tbPlay.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.shapesObjectsToolStripMenuItem,
            this.terrainToolStripMenuItem,
            this.lightsToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1055, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.playGameToolStripMenuItem,
            this.buildGameToolStripMenuItem,
            this.generateGameToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveLevelToolStripMenuItem.Text = "Save";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.SaveLevelToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // playGameToolStripMenuItem
            // 
            this.playGameToolStripMenuItem.Enabled = false;
            this.playGameToolStripMenuItem.Name = "playGameToolStripMenuItem";
            this.playGameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.playGameToolStripMenuItem.Text = "Play \"Game\"";
            // 
            // buildGameToolStripMenuItem
            // 
            this.buildGameToolStripMenuItem.Enabled = false;
            this.buildGameToolStripMenuItem.Name = "buildGameToolStripMenuItem";
            this.buildGameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.buildGameToolStripMenuItem.Text = "Build Game";
            // 
            // generateGameToolStripMenuItem
            // 
            this.generateGameToolStripMenuItem.Enabled = false;
            this.generateGameToolStripMenuItem.Name = "generateGameToolStripMenuItem";
            this.generateGameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.generateGameToolStripMenuItem.Text = "Generate Game";
            this.generateGameToolStripMenuItem.Click += new System.EventHandler(this.GenerateGameToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // shapesObjectsToolStripMenuItem
            // 
            this.shapesObjectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectToolStripMenuItem,
            this.addCubeToolStripMenuItem,
            this.addTriangleToolStripMenuItem,
            this.addTriangleWithTextureToolStripMenuItem});
            this.shapesObjectsToolStripMenuItem.Name = "shapesObjectsToolStripMenuItem";
            this.shapesObjectsToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.shapesObjectsToolStripMenuItem.Text = "Shapes/Objects";
            // 
            // addObjectToolStripMenuItem
            // 
            this.addObjectToolStripMenuItem.Enabled = false;
            this.addObjectToolStripMenuItem.Name = "addObjectToolStripMenuItem";
            this.addObjectToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addObjectToolStripMenuItem.Text = "Add Object";
            this.addObjectToolStripMenuItem.Click += new System.EventHandler(this.addObjectToolStripMenuItem_Click);
            // 
            // addCubeToolStripMenuItem
            // 
            this.addCubeToolStripMenuItem.Name = "addCubeToolStripMenuItem";
            this.addCubeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addCubeToolStripMenuItem.Text = "Add Cube";
            this.addCubeToolStripMenuItem.Click += new System.EventHandler(this.AddCubeToolStripMenuItem_Click);
            // 
            // addTriangleToolStripMenuItem
            // 
            this.addTriangleToolStripMenuItem.Name = "addTriangleToolStripMenuItem";
            this.addTriangleToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addTriangleToolStripMenuItem.Text = "Add Triangle";
            this.addTriangleToolStripMenuItem.Click += new System.EventHandler(this.AddTriangleToolStripMenuItem_Click);
            // 
            // addTriangleWithTextureToolStripMenuItem
            // 
            this.addTriangleWithTextureToolStripMenuItem.Name = "addTriangleWithTextureToolStripMenuItem";
            this.addTriangleWithTextureToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addTriangleWithTextureToolStripMenuItem.Text = "Add Triangle with Texture";
            this.addTriangleWithTextureToolStripMenuItem.Click += new System.EventHandler(this.AddTriangleWithTextureToolStripMenuItem_Click);
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRandomTerrainToolStripMenuItem});
            this.terrainToolStripMenuItem.Enabled = false;
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.terrainToolStripMenuItem.Text = "Terrain";
            this.terrainToolStripMenuItem.Visible = false;
            // 
            // addRandomTerrainToolStripMenuItem
            // 
            this.addRandomTerrainToolStripMenuItem.Enabled = false;
            this.addRandomTerrainToolStripMenuItem.Name = "addRandomTerrainToolStripMenuItem";
            this.addRandomTerrainToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.addRandomTerrainToolStripMenuItem.Text = "Add Random Terrain";
            // 
            // lightsToolStripMenuItem
            // 
            this.lightsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPointLightToolStripMenuItem,
            this.addDirectionalLightToolStripMenuItem});
            this.lightsToolStripMenuItem.Enabled = false;
            this.lightsToolStripMenuItem.Name = "lightsToolStripMenuItem";
            this.lightsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.lightsToolStripMenuItem.Text = "Lights";
            this.lightsToolStripMenuItem.Visible = false;
            // 
            // addPointLightToolStripMenuItem
            // 
            this.addPointLightToolStripMenuItem.Enabled = false;
            this.addPointLightToolStripMenuItem.Name = "addPointLightToolStripMenuItem";
            this.addPointLightToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addPointLightToolStripMenuItem.Text = "Add Point Light";
            this.addPointLightToolStripMenuItem.Click += new System.EventHandler(this.addPointLightToolStripMenuItem_Click);
            // 
            // addDirectionalLightToolStripMenuItem
            // 
            this.addDirectionalLightToolStripMenuItem.Enabled = false;
            this.addDirectionalLightToolStripMenuItem.Name = "addDirectionalLightToolStripMenuItem";
            this.addDirectionalLightToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addDirectionalLightToolStripMenuItem.Text = "Add Directional Light";
            this.addDirectionalLightToolStripMenuItem.Click += new System.EventHandler(this.addDirectionalLightToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSoundToolStripMenuItem});
            this.soundToolStripMenuItem.Enabled = false;
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.soundToolStripMenuItem.Text = "Sound";
            this.soundToolStripMenuItem.Visible = false;
            // 
            // addSoundToolStripMenuItem
            // 
            this.addSoundToolStripMenuItem.Enabled = false;
            this.addSoundToolStripMenuItem.Name = "addSoundToolStripMenuItem";
            this.addSoundToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.addSoundToolStripMenuItem.Text = "Add Sound";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalLightsOnOffToolStripMenuItem,
            this.wireframOnOffToolStripMenuItem,
            this.resetCameraToolStripMenuItem,
            this.turnDebuggerOnOffToolStripMenuItem,
            this.useVsyncToolStripMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // globalLightsOnOffToolStripMenuItem
            // 
            this.globalLightsOnOffToolStripMenuItem.Enabled = false;
            this.globalLightsOnOffToolStripMenuItem.Name = "globalLightsOnOffToolStripMenuItem";
            this.globalLightsOnOffToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.globalLightsOnOffToolStripMenuItem.Text = "Global Lights On/Off";
            this.globalLightsOnOffToolStripMenuItem.Click += new System.EventHandler(this.globalLightsOnOffToolStripMenuItem_Click);
            // 
            // wireframOnOffToolStripMenuItem
            // 
            this.wireframOnOffToolStripMenuItem.CheckOnClick = true;
            this.wireframOnOffToolStripMenuItem.Name = "wireframOnOffToolStripMenuItem";
            this.wireframOnOffToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.wireframOnOffToolStripMenuItem.Text = "Wirefram On/Off";
            this.wireframOnOffToolStripMenuItem.Click += new System.EventHandler(this.WireframOnOffToolStripMenuItem_Click);
            // 
            // resetCameraToolStripMenuItem
            // 
            this.resetCameraToolStripMenuItem.Name = "resetCameraToolStripMenuItem";
            this.resetCameraToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.resetCameraToolStripMenuItem.Text = "Reset Camera";
            this.resetCameraToolStripMenuItem.Click += new System.EventHandler(this.ResetCameraToolStripMenuItem_Click);
            // 
            // turnDebuggerOnOffToolStripMenuItem
            // 
            this.turnDebuggerOnOffToolStripMenuItem.CheckOnClick = true;
            this.turnDebuggerOnOffToolStripMenuItem.Name = "turnDebuggerOnOffToolStripMenuItem";
            this.turnDebuggerOnOffToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.turnDebuggerOnOffToolStripMenuItem.Text = "Debugger On/Off";
            this.turnDebuggerOnOffToolStripMenuItem.Click += new System.EventHandler(this.TurnDebuggerOnOffToolStripMenuItem_Click);
            // 
            // useVsyncToolStripMenuItem
            // 
            this.useVsyncToolStripMenuItem.CheckOnClick = true;
            this.useVsyncToolStripMenuItem.Name = "useVsyncToolStripMenuItem";
            this.useVsyncToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.useVsyncToolStripMenuItem.Text = "VSync On/Off";
            this.useVsyncToolStripMenuItem.Click += new System.EventHandler(this.UseVsyncToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearInformationToolStripMenuItem,
            this.clearErrorsToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // clearInformationToolStripMenuItem
            // 
            this.clearInformationToolStripMenuItem.Name = "clearInformationToolStripMenuItem";
            this.clearInformationToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.clearInformationToolStripMenuItem.Text = "Clear Information";
            this.clearInformationToolStripMenuItem.Click += new System.EventHandler(this.ClearInformationToolStripMenuItem_Click);
            // 
            // clearErrorsToolStripMenuItem
            // 
            this.clearErrorsToolStripMenuItem.Name = "clearErrorsToolStripMenuItem";
            this.clearErrorsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.clearErrorsToolStripMenuItem.Text = "Clear Errors";
            this.clearErrorsToolStripMenuItem.Click += new System.EventHandler(this.ClearErrorsToolStripMenuItem_Click);
            // 
            // fswClassFileWatcher
            // 
            this.fswClassFileWatcher.EnableRaisingEvents = true;
            this.fswClassFileWatcher.Filter = "*.cs";
            this.fswClassFileWatcher.SynchronizingObject = this;
            this.fswClassFileWatcher.Created += new System.IO.FileSystemEventHandler(this.FswClassFileWatcher_Created);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 761);
            this.Controls.Add(this.tabEditPlay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MY 3D Engine Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionBindingSource)).EndInit();
            this.tabEditPlay.ResumeLayout(false);
            this.tbEdit.ResumeLayout(false);
            this.tbEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tlvGameFiles)).EndInit();
            this.cmsGameFilesRightClickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TreeListViewSceneGraph)).EndInit();
            this.pnlObjectProperties.ResumeLayout(false);
            this.pnlObjectProperties.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameObjectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fswClassFileWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel rendererPnl;
        private System.Windows.Forms.DataGridView ExceptionGridView;
        private System.Windows.Forms.TabControl tabEditPlay;
        private System.Windows.Forms.TabPage tbEdit;
        private System.Windows.Forms.TabPage tbPlay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shapesObjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTriangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRandomTerrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPointLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDirectionalLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLevelToolStripMenuItem;
        private System.Windows.Forms.Label lblCameraPosition;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalLightsOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireframOnOffToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox GameObjectListComboBox;
        private System.Windows.Forms.TextBox tbInformation;
        private System.Windows.Forms.Panel pnlObjectProperties;
        private System.Windows.Forms.Label GameObjectNameLabel;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ToolStripMenuItem resetCameraToolStripMenuItem;
        private System.Windows.Forms.CheckBox ckbxLightOnOff;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button ChangeGameObjectColorButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.BindingSource GameObjectBindingSource;
        private BrightIdeasSoftware.TreeListView TreeListViewSceneGraph;
        private System.Windows.Forms.Button RemoveGameObjectButton;
        private System.Windows.Forms.ToolStripMenuItem addTriangleWithTextureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnDebuggerOnOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.BindingSource ExceptionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackTrace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Source;
        private System.Windows.Forms.ToolStripMenuItem generateGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private BrightIdeasSoftware.TreeListView tlvGameFiles;
        private System.Windows.Forms.ToolStripMenuItem buildGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearErrorsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsGameFilesRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddClass;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ToolStripMenuItem useVsyncToolStripMenuItem;
        private System.IO.FileSystemWatcher fswClassFileWatcher;
        private System.Windows.Forms.TextBox tbLeftRight;
        private System.Windows.Forms.TextBox tbUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private BrightIdeasSoftware.OLVColumn olvSceneName;
        private System.Windows.Forms.TextBox tbRotateZ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbRotateY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbRotateX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbZoomInOut;
        private System.Windows.Forms.Label label10;
    }
}

