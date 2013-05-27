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
            this.rendererPnl = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabEditPlay = new System.Windows.Forms.TabControl();
            this.tbEdit = new System.Windows.Forms.TabPage();
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
            this.lblCameraPosition = new System.Windows.Forms.Label();
            this.txtInformationOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabEditPlay.SuspendLayout();
            this.tbEdit.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rendererPnl
            // 
            this.rendererPnl.Location = new System.Drawing.Point(293, 6);
            this.rendererPnl.Name = "rendererPnl";
            this.rendererPnl.Size = new System.Drawing.Size(516, 347);
            this.rendererPnl.TabIndex = 1;
            this.rendererPnl.MouseEnter += new System.EventHandler(this.rendererPnl_MouseEnter);
            this.rendererPnl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rendererPnl_MouseMove);
            this.rendererPnl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rendererPnl_MouseUp);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 507);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(801, 75);
            this.dataGridView1.TabIndex = 2;
            // 
            // tabEditPlay
            // 
            this.tabEditPlay.Controls.Add(this.tbEdit);
            this.tabEditPlay.Controls.Add(this.tbPlay);
            this.tabEditPlay.Location = new System.Drawing.Point(0, 27);
            this.tabEditPlay.Name = "tabEditPlay";
            this.tabEditPlay.SelectedIndex = 0;
            this.tabEditPlay.Size = new System.Drawing.Size(823, 611);
            this.tabEditPlay.TabIndex = 3;
            // 
            // tbEdit
            // 
            this.tbEdit.Controls.Add(this.txtInformationOutput);
            this.tbEdit.Controls.Add(this.lblCameraPosition);
            this.tbEdit.Controls.Add(this.rendererPnl);
            this.tbEdit.Controls.Add(this.dataGridView1);
            this.tbEdit.Location = new System.Drawing.Point(4, 22);
            this.tbEdit.Name = "tbEdit";
            this.tbEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tbEdit.Size = new System.Drawing.Size(815, 585);
            this.tbEdit.TabIndex = 0;
            this.tbEdit.Text = "Edit";
            this.tbEdit.UseVisualStyleBackColor = true;
            // 
            // tbPlay
            // 
            this.tbPlay.Location = new System.Drawing.Point(4, 22);
            this.tbPlay.Name = "tbPlay";
            this.tbPlay.Padding = new System.Windows.Forms.Padding(3);
            this.tbPlay.Size = new System.Drawing.Size(815, 585);
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
            this.soundToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(823, 24);
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
            this.addObjectToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.addObjectToolStripMenuItem.Text = "Add Object";
            // 
            // addCubeToolStripMenuItem
            // 
            this.addCubeToolStripMenuItem.Name = "addCubeToolStripMenuItem";
            this.addCubeToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.addCubeToolStripMenuItem.Text = "Add Cube";
            // 
            // addTriangleToolStripMenuItem
            // 
            this.addTriangleToolStripMenuItem.Name = "addTriangleToolStripMenuItem";
            this.addTriangleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.addTriangleToolStripMenuItem.Text = "Add Triangle";
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRandomTerrainToolStripMenuItem});
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.terrainToolStripMenuItem.Text = "Terrain";
            // 
            // addRandomTerrainToolStripMenuItem
            // 
            this.addRandomTerrainToolStripMenuItem.Name = "addRandomTerrainToolStripMenuItem";
            this.addRandomTerrainToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
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
            // 
            // addDirectionalLightToolStripMenuItem
            // 
            this.addDirectionalLightToolStripMenuItem.Name = "addDirectionalLightToolStripMenuItem";
            this.addDirectionalLightToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addDirectionalLightToolStripMenuItem.Text = "Add Directional Light";
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
            // lblCameraPosition
            // 
            this.lblCameraPosition.AutoSize = true;
            this.lblCameraPosition.Location = new System.Drawing.Point(8, 6);
            this.lblCameraPosition.Name = "lblCameraPosition";
            this.lblCameraPosition.Size = new System.Drawing.Size(0, 13);
            this.lblCameraPosition.TabIndex = 3;
            // 
            // txtInformationOutput
            // 
            this.txtInformationOutput.Location = new System.Drawing.Point(6, 446);
            this.txtInformationOutput.Multiline = true;
            this.txtInformationOutput.Name = "txtInformationOutput";
            this.txtInformationOutput.Size = new System.Drawing.Size(803, 55);
            this.txtInformationOutput.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 639);
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
        private System.Windows.Forms.TextBox txtInformationOutput;
    }
}

