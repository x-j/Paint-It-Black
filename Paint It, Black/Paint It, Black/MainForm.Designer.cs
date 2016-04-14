namespace Paint_It__Black {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.colorButtonExplainingLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.segmentLabel = new System.Windows.Forms.Label();
            this.colorPickerButton = new System.Windows.Forms.Button();
            this.hexButton = new System.Windows.Forms.Button();
            this.quadrangleButton = new System.Windows.Forms.Button();
            this.triangleButton = new System.Windows.Forms.Button();
            this.segmentButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(783, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 27);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.colorButtonExplainingLabel);
            this.splitContainer.Panel1.Controls.Add(this.label3);
            this.splitContainer.Panel1.Controls.Add(this.label2);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.segmentLabel);
            this.splitContainer.Panel1.Controls.Add(this.colorPickerButton);
            this.splitContainer.Panel1.Controls.Add(this.hexButton);
            this.splitContainer.Panel1.Controls.Add(this.quadrangleButton);
            this.splitContainer.Panel1.Controls.Add(this.triangleButton);
            this.splitContainer.Panel1.Controls.Add(this.segmentButton);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.canvas);
            this.splitContainer.Size = new System.Drawing.Size(759, 395);
            this.splitContainer.SplitterDistance = 349;
            this.splitContainer.TabIndex = 1;
            // 
            // colorButtonExplainingLabel
            // 
            this.colorButtonExplainingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorButtonExplainingLabel.AutoSize = true;
            this.colorButtonExplainingLabel.Location = new System.Drawing.Point(3, 323);
            this.colorButtonExplainingLabel.Name = "colorButtonExplainingLabel";
            this.colorButtonExplainingLabel.Size = new System.Drawing.Size(119, 13);
            this.colorButtonExplainingLabel.TabIndex = 3;
            this.colorButtonExplainingLabel.Tag = "nothing to see here";
            this.colorButtonExplainingLabel.Text = "Color of the new shape:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(126, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 36);
            this.label3.TabIndex = 2;
            this.label3.Tag = "4";
            this.label3.Text = "Click 2 more points to draw a segment.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(126, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 36);
            this.label2.TabIndex = 2;
            this.label2.Tag = "3";
            this.label2.Text = "Click 2 more points to draw a segment.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(126, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 36);
            this.label1.TabIndex = 2;
            this.label1.Tag = "2";
            this.label1.Text = "Click 2 more points to draw a segment.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // segmentLabel
            // 
            this.segmentLabel.Location = new System.Drawing.Point(126, 20);
            this.segmentLabel.Name = "segmentLabel";
            this.segmentLabel.Size = new System.Drawing.Size(182, 36);
            this.segmentLabel.TabIndex = 2;
            this.segmentLabel.Tag = "1";
            this.segmentLabel.Text = "Click 2 more points to draw a segment.";
            this.segmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.segmentLabel.Visible = false;
            // 
            // colorPickerButton
            // 
            this.colorPickerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorPickerButton.BackColor = System.Drawing.Color.Black;
            this.colorPickerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorPickerButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.colorPickerButton.Location = new System.Drawing.Point(6, 339);
            this.colorPickerButton.Name = "colorPickerButton";
            this.colorPickerButton.Size = new System.Drawing.Size(134, 38);
            this.colorPickerButton.TabIndex = 1;
            this.colorPickerButton.TabStop = false;
            this.colorPickerButton.Tag = "5";
            this.colorPickerButton.UseVisualStyleBackColor = false;
            this.colorPickerButton.Click += new System.EventHandler(this.colorPickerButton_Click);
            // 
            // hexButton
            // 
            this.hexButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.hexButton.FlatAppearance.BorderSize = 2;
            this.hexButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexButton.Location = new System.Drawing.Point(3, 146);
            this.hexButton.Name = "hexButton";
            this.hexButton.Size = new System.Drawing.Size(117, 36);
            this.hexButton.TabIndex = 0;
            this.hexButton.TabStop = false;
            this.hexButton.Tag = "4";
            this.hexButton.Text = "Hexagon";
            this.hexButton.UseVisualStyleBackColor = false;
            // 
            // quadrangleButton
            // 
            this.quadrangleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.quadrangleButton.FlatAppearance.BorderSize = 2;
            this.quadrangleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quadrangleButton.Location = new System.Drawing.Point(3, 104);
            this.quadrangleButton.Name = "quadrangleButton";
            this.quadrangleButton.Size = new System.Drawing.Size(117, 36);
            this.quadrangleButton.TabIndex = 0;
            this.quadrangleButton.TabStop = false;
            this.quadrangleButton.Tag = "3";
            this.quadrangleButton.Text = "Quadrangle";
            this.quadrangleButton.UseVisualStyleBackColor = false;
            // 
            // triangleButton
            // 
            this.triangleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.triangleButton.FlatAppearance.BorderSize = 2;
            this.triangleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triangleButton.Location = new System.Drawing.Point(3, 62);
            this.triangleButton.Name = "triangleButton";
            this.triangleButton.Size = new System.Drawing.Size(117, 36);
            this.triangleButton.TabIndex = 0;
            this.triangleButton.TabStop = false;
            this.triangleButton.Tag = "2";
            this.triangleButton.Text = "Triangle";
            this.triangleButton.UseVisualStyleBackColor = false;
            // 
            // segmentButton
            // 
            this.segmentButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.segmentButton.FlatAppearance.BorderSize = 2;
            this.segmentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segmentButton.Location = new System.Drawing.Point(3, 20);
            this.segmentButton.Name = "segmentButton";
            this.segmentButton.Size = new System.Drawing.Size(117, 36);
            this.segmentButton.TabIndex = 0;
            this.segmentButton.TabStop = false;
            this.segmentButton.Tag = "1";
            this.segmentButton.Text = "Segment";
            this.segmentButton.UseVisualStyleBackColor = false;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(406, 395);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Choose a folder. A file drawing.bmp will be saved there.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(783, 434);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(700, 380);
            this.Name = "MainForm";
            this.Text = "This is not Paint";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label segmentLabel;
        private System.Windows.Forms.Button colorPickerButton;
        private System.Windows.Forms.Button hexButton;
        private System.Windows.Forms.Button quadrangleButton;
        private System.Windows.Forms.Button triangleButton;
        private System.Windows.Forms.Button segmentButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label colorButtonExplainingLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.PictureBox canvas;
    }
}

