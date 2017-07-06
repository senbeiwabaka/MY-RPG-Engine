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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rendererPnl = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabEditPlay = new System.Windows.Forms.TabControl();
            this.tbEdit = new System.Windows.Forms.TabPage();
            this.tvObjectHirarchy = new System.Windows.Forms.TreeView();
            this.pnlObjectProperties = new System.Windows.Forms.Panel();
            this.btnColor = new System.Windows.Forms.Button();
            this.ckbxLightOnOff = new System.Windows.Forms.CheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtError_Info = new System.Windows.Forms.TextBox();
            this.cmbObjectList = new System.Windows.Forms.ComboBox();
            this.lblAddRemove = new System.Windows.Forms.Label();
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabEditPlay.SuspendLayout();
            this.tbEdit.SuspendLayout();
            this.pnlObjectProperties.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rendererPnl
            // 
            this.rendererPnl.Location = new System.Drawing.Point(237, 6);
            this.rendererPnl.Name = "rendererPnl";
            this.rendererPnl.Size = new System.Drawing.Size(720, 480);
            this.rendererPnl.TabIndex = 1;
            this.rendererPnl.MouseEnter += new System.EventHandler(this.rendererPnl_MouseEnter);
            this.rendererPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rendererPnl_MouseMove);
            this.rendererPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rendererPnl_MouseUp);
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
            this.dataGridView1.Location = new System.Drawing.Point(8, 507);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(369, 75);
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
            this.tbEdit.Controls.Add(this.tvObjectHirarchy);
            this.tbEdit.Controls.Add(this.pnlObjectProperties);
            this.tbEdit.Controls.Add(this.txtError_Info);
            this.tbEdit.Controls.Add(this.cmbObjectList);
            this.tbEdit.Controls.Add(this.lblAddRemove);
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
            // tvObjectHirarchy
            // 
            this.tvObjectHirarchy.Location = new System.Drawing.Point(11, 390);
            this.tvObjectHirarchy.Name = "tvObjectHirarchy";
            this.tvObjectHirarchy.Size = new System.Drawing.Size(220, 111);
            this.tvObjectHirarchy.TabIndex = 9;
            // 
            // pnlObjectProperties
            // 
            this.pnlObjectProperties.Controls.Add(this.btnColor);
            this.pnlObjectProperties.Controls.Add(this.ckbxLightOnOff);
            this.pnlObjectProperties.Controls.Add(this.lblColor);
            this.pnlObjectProperties.Controls.Add(this.lblLocation);
            this.pnlObjectProperties.Controls.Add(this.txtName);
            this.pnlObjectProperties.Controls.Add(this.label2);
            this.pnlObjectProperties.Location = new System.Drawing.Point(11, 125);
            this.pnlObjectProperties.Name = "pnlObjectProperties";
            this.pnlObjectProperties.Size = new System.Drawing.Size(220, 258);
            this.pnlObjectProperties.TabIndex = 8;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(128, 59);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(89, 23);
            this.btnColor.TabIndex = 5;
            this.btnColor.Text = "Change Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Visible = false;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // ckbxLightOnOff
            // 
            this.ckbxLightOnOff.AutoSize = true;
            this.ckbxLightOnOff.Location = new System.Drawing.Point(6, 90);
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
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(45, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 1;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            // 
            // txtError_Info
            // 
            this.txtError_Info.Location = new System.Drawing.Point(383, 507);
            this.txtError_Info.Multiline = true;
            this.txtError_Info.Name = "txtError_Info";
            this.txtError_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError_Info.Size = new System.Drawing.Size(424, 75);
            this.txtError_Info.TabIndex = 7;
            // 
            // cmbObjectList
            // 
            this.cmbObjectList.FormattingEnabled = true;
            this.cmbObjectList.Location = new System.Drawing.Point(11, 97);
            this.cmbObjectList.Name = "cmbObjectList";
            this.cmbObjectList.Size = new System.Drawing.Size(121, 21);
            this.cmbObjectList.TabIndex = 6;
            this.cmbObjectList.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lblAddRemove
            // 
            this.lblAddRemove.AutoSize = true;
            this.lblAddRemove.Location = new System.Drawing.Point(4, 71);
            this.lblAddRemove.Name = "lblAddRemove";
            this.lblAddRemove.Size = new System.Drawing.Size(0, 13);
            this.lblAddRemove.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 54);
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
            this.tbPlay.Size = new System.Drawing.Size(949, 585);
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
            this.playGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveLevelToolStripMenuItem
            // 
            this.saveLevelToolStripMenuItem.Name = "saveLevelToolStripMenuItem";
            this.saveLevelToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveLevelToolStripMenuItem.Text = "Save Level";
            // 
            // playGameToolStripMenuItem
            // 
            this.playGameToolStripMenuItem.Name = "playGameToolStripMenuItem";
            this.playGameToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.playGameToolStripMenuItem.Text = "Play \"Game\"";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // shapesObjectsToolStripMenuItem
            // 
            this.shapesObjectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectToolStripMenuItem,
            this.addCubeToolStripMenuItem,
            this.addTriangleToolStripMenuItem});
            this.shapesObjectsToolStripMenuItem.Name = "shapesObjectsToolStripMenuItem";
            this.shapesObjectsToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.shapesObjectsToolStripMenuItem.Text = "Shapes/Objects";
            // 
            // addObjectToolStripMenuItem
            // 
            this.addObjectToolStripMenuItem.Name = "addObjectToolStripMenuItem";
            this.addObjectToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addObjectToolStripMenuItem.Text = "Add Object";
            this.addObjectToolStripMenuItem.Click += new System.EventHandler(this.addObjectToolStripMenuItem_Click);
            // 
            // addCubeToolStripMenuItem
            // 
            this.addCubeToolStripMenuItem.Name = "addCubeToolStripMenuItem";
            this.addCubeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addCubeToolStripMenuItem.Text = "Add Cube";
            this.addCubeToolStripMenuItem.Click += new System.EventHandler(this.addCubeToolStripMenuItem_Click);
            // 
            // addTriangleToolStripMenuItem
            // 
            this.addTriangleToolStripMenuItem.Name = "addTriangleToolStripMenuItem";
            this.addTriangleToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.addTriangleToolStripMenuItem.Text = "Add Triangle";
            this.addTriangleToolStripMenuItem.Click += new System.EventHandler(this.addTriangleToolStripMenuItem_Click);
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRandomTerrainToolStripMenuItem});
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.terrainToolStripMenuItem.Text = "Terrain";
            // 
            // addRandomTerrainToolStripMenuItem
            // 
            this.addRandomTerrainToolStripMenuItem.Name = "addRandomTerrainToolStripMenuItem";
            this.addRandomTerrainToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.addRandomTerrainToolStripMenuItem.Text = "Add Random Terrain";
            // 
            // lightsToolStripMenuItem
            // 
            this.lightsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPointLightToolStripMenuItem,
            this.addDirectionalLightToolStripMenuItem});
            this.lightsToolStripMenuItem.Name = "lightsToolStripMenuItem";
            this.lightsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.lightsToolStripMenuItem.Text = "Lights";
            // 
            // addPointLightToolStripMenuItem
            // 
            this.addPointLightToolStripMenuItem.Name = "addPointLightToolStripMenuItem";
            this.addPointLightToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addPointLightToolStripMenuItem.Text = "Add Point Light";
            this.addPointLightToolStripMenuItem.Click += new System.EventHandler(this.addPointLightToolStripMenuItem_Click);
            // 
            // addDirectionalLightToolStripMenuItem
            // 
            this.addDirectionalLightToolStripMenuItem.Name = "addDirectionalLightToolStripMenuItem";
            this.addDirectionalLightToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addDirectionalLightToolStripMenuItem.Text = "Add Directional Light";
            this.addDirectionalLightToolStripMenuItem.Click += new System.EventHandler(this.addDirectionalLightToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSoundToolStripMenuItem});
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.soundToolStripMenuItem.Text = "Sound";
            // 
            // addSoundToolStripMenuItem
            // 
            this.addSoundToolStripMenuItem.Name = "addSoundToolStripMenuItem";
            this.addSoundToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.addSoundToolStripMenuItem.Text = "Add Sound";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalLightsOnOffToolStripMenuItem,
            this.wireframOnOffToolStripMenuItem,
            this.resetCameraToolStripMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // globalLightsOnOffToolStripMenuItem
            // 
            this.globalLightsOnOffToolStripMenuItem.Name = "globalLightsOnOffToolStripMenuItem";
            this.globalLightsOnOffToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.globalLightsOnOffToolStripMenuItem.Text = "Global Lights On/Off";
            this.globalLightsOnOffToolStripMenuItem.Click += new System.EventHandler(this.globalLightsOnOffToolStripMenuItem_Click);
            // 
            // wireframOnOffToolStripMenuItem
            // 
            this.wireframOnOffToolStripMenuItem.Name = "wireframOnOffToolStripMenuItem";
            this.wireframOnOffToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.wireframOnOffToolStripMenuItem.Text = "Wirefram On/Off";
            this.wireframOnOffToolStripMenuItem.Click += new System.EventHandler(this.wireframOnOffToolStripMenuItem_Click);
            // 
            // resetCameraToolStripMenuItem
            // 
            this.resetCameraToolStripMenuItem.Name = "resetCameraToolStripMenuItem";
            this.resetCameraToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.resetCameraToolStripMenuItem.Text = "Reset Camera";
            this.resetCameraToolStripMenuItem.Click += new System.EventHandler(this.resetCameraToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 639);
            this.Controls.Add(this.tabEditPlay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabEditPlay.ResumeLayout(false);
            this.tbEdit.ResumeLayout(false);
            this.tbEdit.PerformLayout();
            this.pnlObjectProperties.ResumeLayout(false);
            this.pnlObjectProperties.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Label lblAddRemove;
        private System.Windows.Forms.ComboBox cmbObjectList;
        private System.Windows.Forms.TextBox txtError_Info;
        private System.Windows.Forms.Panel pnlObjectProperties;
        private System.Windows.Forms.TreeView tvObjectHirarchy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ToolStripMenuItem resetCameraToolStripMenuItem;
        private System.Windows.Forms.CheckBox ckbxLightOnOff;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}

