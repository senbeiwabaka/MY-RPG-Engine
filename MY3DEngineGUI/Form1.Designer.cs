namespace MY3DEngineGUI
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabEditPlay = new System.Windows.Forms.TabControl();
            this.tbEdit = new System.Windows.Forms.TabPage();
            this.TreeListViewSceneGraph = new BrightIdeasSoftware.TreeListView();
            this.pnlObjectProperties = new System.Windows.Forms.Panel();
            this.RemoveGameObjectButton = new System.Windows.Forms.Button();
            this.ChangeGameObjectColorButton = new System.Windows.Forms.Button();
            this.ckbxLightOnOff = new System.Windows.Forms.CheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.GameObjectNameLabel = new System.Windows.Forms.Label();
            this.tbInformation = new System.Windows.Forms.TextBox();
            this.GameObjectListComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCameraPosition = new System.Windows.Forms.Label();
            this.tbPlay = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.GameObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabEditPlay.SuspendLayout();
            this.tbEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListViewSceneGraph)).BeginInit();
            this.pnlObjectProperties.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameObjectBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rendererPnl
            // 
            this.rendererPnl.Location = new System.Drawing.Point(237, 6);
            this.rendererPnl.Name = "rendererPnl";
            this.rendererPnl.Size = new System.Drawing.Size(720, 480);
            this.rendererPnl.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(8, 492);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(463, 90);
            this.dataGridView1.TabIndex = 2;
            // 
            // tabEditPlay
            // 
            this.tabEditPlay.Controls.Add(this.tbEdit);
            this.tabEditPlay.Controls.Add(this.tbPlay);
            this.tabEditPlay.Location = new System.Drawing.Point(0, 27);
            this.tabEditPlay.Name = "tabEditPlay";
            this.tabEditPlay.SelectedIndex = 0;
            this.tabEditPlay.Size = new System.Drawing.Size(970, 611);
            this.tabEditPlay.TabIndex = 3;
            // 
            // tbEdit
            // 
            this.tbEdit.Controls.Add(this.TreeListViewSceneGraph);
            this.tbEdit.Controls.Add(this.pnlObjectProperties);
            this.tbEdit.Controls.Add(this.tbInformation);
            this.tbEdit.Controls.Add(this.GameObjectListComboBox);
            this.tbEdit.Controls.Add(this.label1);
            this.tbEdit.Controls.Add(this.lblCameraPosition);
            this.tbEdit.Controls.Add(this.rendererPnl);
            this.tbEdit.Controls.Add(this.dataGridView1);
            this.tbEdit.Location = new System.Drawing.Point(4, 22);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tbEdit.Size = new System.Drawing.Size(962, 585);
            this.tbEdit.TabIndex = 0;
            this.tbEdit.Text = "Edit";
            this.tbEdit.UseVisualStyleBackColor = true;
            // 
            // TreeListViewSceneGraph
            // 
            this.TreeListViewSceneGraph.CellEditUseWholeCell = false;
            this.TreeListViewSceneGraph.Location = new System.Drawing.Point(11, 389);
            this.TreeListViewSceneGraph.Name = "TreeListViewSceneGraph";
            this.TreeListViewSceneGraph.ShowGroups = false;
            this.TreeListViewSceneGraph.Size = new System.Drawing.Size(217, 97);
            this.TreeListViewSceneGraph.TabIndex = 9;
            this.TreeListViewSceneGraph.UseCompatibleStateImageBehavior = false;
            this.TreeListViewSceneGraph.View = System.Windows.Forms.View.Details;
            this.TreeListViewSceneGraph.VirtualMode = true;
            // 
            // pnlObjectProperties
            // 
            this.pnlObjectProperties.Controls.Add(this.RemoveGameObjectButton);
            this.pnlObjectProperties.Controls.Add(this.ChangeGameObjectColorButton);
            this.pnlObjectProperties.Controls.Add(this.ckbxLightOnOff);
            this.pnlObjectProperties.Controls.Add(this.lblColor);
            this.pnlObjectProperties.Controls.Add(this.lblLocation);
            this.pnlObjectProperties.Controls.Add(this.tbName);
            this.pnlObjectProperties.Controls.Add(this.GameObjectNameLabel);
            this.pnlObjectProperties.Location = new System.Drawing.Point(11, 125);
            this.pnlObjectProperties.Name = "pnlObjectProperties";
            this.pnlObjectProperties.Size = new System.Drawing.Size(220, 258);
            this.pnlObjectProperties.TabIndex = 8;
            // 
            // RemoveGameObjectButton
            // 
            this.RemoveGameObjectButton.Enabled = false;
            this.RemoveGameObjectButton.Location = new System.Drawing.Point(12, 65);
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
            this.ChangeGameObjectColorButton.Location = new System.Drawing.Point(12, 36);
            this.ChangeGameObjectColorButton.Name = "ChangeGameObjectColorButton";
            this.ChangeGameObjectColorButton.Size = new System.Drawing.Size(89, 23);
            this.ChangeGameObjectColorButton.TabIndex = 5;
            this.ChangeGameObjectColorButton.Text = "Change Color";
            this.ChangeGameObjectColorButton.UseVisualStyleBackColor = true;
            this.ChangeGameObjectColorButton.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ckbxLightOnOff
            // 
            this.ckbxLightOnOff.AutoSize = true;
            this.ckbxLightOnOff.Location = new System.Drawing.Point(12, 94);
            this.ckbxLightOnOff.Name = "ckbxLightOnOff";
            this.ckbxLightOnOff.Size = new System.Drawing.Size(85, 17);
            this.ckbxLightOnOff.TabIndex = 4;
            this.ckbxLightOnOff.Text = "Light On/Off";
            this.ckbxLightOnOff.UseVisualStyleBackColor = true;
            this.ckbxLightOnOff.Visible = false;
            this.ckbxLightOnOff.CheckedChanged += new System.EventHandler(this.ckbxLightOnOff_CheckedChanged);
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
            this.tbName.Location = new System.Drawing.Point(45, 10);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(163, 20);
            this.tbName.TabIndex = 1;
            this.tbName.Leave += new System.EventHandler(this.TxtName_Leave);
            // 
            // GameObjectNameLabel
            // 
            this.GameObjectNameLabel.AutoSize = true;
            this.GameObjectNameLabel.Location = new System.Drawing.Point(3, 13);
            this.GameObjectNameLabel.Name = "GameObjectNameLabel";
            this.GameObjectNameLabel.Size = new System.Drawing.Size(35, 13);
            this.GameObjectNameLabel.TabIndex = 0;
            this.GameObjectNameLabel.Text = "Name";
            // 
            // tbInformation
            // 
            this.tbInformation.Location = new System.Drawing.Point(477, 492);
            this.tbInformation.Multiline = true;
            this.tbInformation.Name = "tbInformation";
            this.tbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbInformation.Size = new System.Drawing.Size(477, 90);
            this.tbInformation.TabIndex = 7;
            // 
            // GameObjectListComboBox
            // 
            this.GameObjectListComboBox.FormattingEnabled = true;
            this.GameObjectListComboBox.Location = new System.Drawing.Point(11, 97);
            this.GameObjectListComboBox.Name = "GameObjectListComboBox";
            this.GameObjectListComboBox.Size = new System.Drawing.Size(217, 21);
            this.GameObjectListComboBox.TabIndex = 6;
            this.GameObjectListComboBox.SelectedIndexChanged += new System.EventHandler(this.GameObjectListComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Object Information";
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
            this.tbPlay.Size = new System.Drawing.Size(962, 585);
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
            this.propertiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(970, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.playGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveLevelToolStripMenuItem.Text = "Save";
            this.saveLevelToolStripMenuItem.Click += new System.EventHandler(this.SaveLevelToolStripMenuItem_Click);
            // 
            // playGameToolStripMenuItem
            // 
            this.playGameToolStripMenuItem.Enabled = false;
            this.playGameToolStripMenuItem.Name = "playGameToolStripMenuItem";
            this.playGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playGameToolStripMenuItem.Text = "Play \"Game\"";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
            this.addCubeToolStripMenuItem.Enabled = false;
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
            this.turnDebuggerOnOffToolStripMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // globalLightsOnOffToolStripMenuItem
            // 
            this.globalLightsOnOffToolStripMenuItem.Enabled = false;
            this.globalLightsOnOffToolStripMenuItem.Name = "globalLightsOnOffToolStripMenuItem";
            this.globalLightsOnOffToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.globalLightsOnOffToolStripMenuItem.Text = "Global Lights On/Off";
            this.globalLightsOnOffToolStripMenuItem.Click += new System.EventHandler(this.globalLightsOnOffToolStripMenuItem_Click);
            // 
            // wireframOnOffToolStripMenuItem
            // 
            this.wireframOnOffToolStripMenuItem.Enabled = false;
            this.wireframOnOffToolStripMenuItem.Name = "wireframOnOffToolStripMenuItem";
            this.wireframOnOffToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.wireframOnOffToolStripMenuItem.Text = "Wirefram On/Off";
            this.wireframOnOffToolStripMenuItem.Click += new System.EventHandler(this.wireframOnOffToolStripMenuItem_Click);
            // 
            // resetCameraToolStripMenuItem
            // 
            this.resetCameraToolStripMenuItem.Enabled = false;
            this.resetCameraToolStripMenuItem.Name = "resetCameraToolStripMenuItem";
            this.resetCameraToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.resetCameraToolStripMenuItem.Text = "Reset Camera";
            this.resetCameraToolStripMenuItem.Click += new System.EventHandler(this.resetCameraToolStripMenuItem_Click);
            // 
            // turnDebuggerOnOffToolStripMenuItem
            // 
            this.turnDebuggerOnOffToolStripMenuItem.CheckOnClick = true;
            this.turnDebuggerOnOffToolStripMenuItem.Name = "turnDebuggerOnOffToolStripMenuItem";
            this.turnDebuggerOnOffToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.turnDebuggerOnOffToolStripMenuItem.Text = "Turn Debugger On/Off";
            this.turnDebuggerOnOffToolStripMenuItem.Click += new System.EventHandler(this.TurnDebuggerOnOffToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 639);
            this.Controls.Add(this.tabEditPlay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabEditPlay.ResumeLayout(false);
            this.tbEdit.ResumeLayout(false);
            this.tbEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreeListViewSceneGraph)).EndInit();
            this.pnlObjectProperties.ResumeLayout(false);
            this.pnlObjectProperties.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GameObjectBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel rendererPnl;
        private System.Windows.Forms.DataGridView dataGridView1;
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
    }
}

