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
            this.floodFillingCheckBox = new System.Windows.Forms.CheckBox();
            this.standardFillingCheckbox = new System.Windows.Forms.CheckBox();
            this.clippingCheckBox = new System.Windows.Forms.CheckBox();
            this.supersamplingUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fillColorPickerButton = new System.Windows.Forms.Button();
            this.radiusPicker = new System.Windows.Forms.NumericUpDown();
            this.rectangleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.antialiasCheckBox = new System.Windows.Forms.CheckBox();
            this.rectangleButton = new System.Windows.Forms.Button();
            this.circleLabel = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.colorButtonExplainingLabel = new System.Windows.Forms.Label();
            this.circleButton = new System.Windows.Forms.Button();
            this.segmentLabel = new System.Windows.Forms.Label();
            this.quadrangleButton = new System.Windows.Forms.Button();
            this.lineColorPickerButton = new System.Windows.Forms.Button();
            this.segmentButton = new System.Windows.Forms.Button();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.cleanClipButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supersamplingUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
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
            this.splitContainer.Panel1.Controls.Add(this.floodFillingCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.standardFillingCheckbox);
            this.splitContainer.Panel1.Controls.Add(this.clippingCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.supersamplingUpDown);
            this.splitContainer.Panel1.Controls.Add(this.label2);
            this.splitContainer.Panel1.Controls.Add(this.label5);
            this.splitContainer.Panel1.Controls.Add(this.fillColorPickerButton);
            this.splitContainer.Panel1.Controls.Add(this.radiusPicker);
            this.splitContainer.Panel1.Controls.Add(this.rectangleLabel);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.antialiasCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.rectangleButton);
            this.splitContainer.Panel1.Controls.Add(this.circleLabel);
            this.splitContainer.Panel1.Controls.Add(this.numericUpDown);
            this.splitContainer.Panel1.Controls.Add(this.label4);
            this.splitContainer.Panel1.Controls.Add(this.colorButtonExplainingLabel);
            this.splitContainer.Panel1.Controls.Add(this.circleButton);
            this.splitContainer.Panel1.Controls.Add(this.segmentLabel);
            this.splitContainer.Panel1.Controls.Add(this.quadrangleButton);
            this.splitContainer.Panel1.Controls.Add(this.lineColorPickerButton);
            this.splitContainer.Panel1.Controls.Add(this.segmentButton);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.canvas);
            this.splitContainer.Size = new System.Drawing.Size(759, 437);
            this.splitContainer.SplitterDistance = 285;
            this.splitContainer.TabIndex = 1;
            // 
            // floodFillingCheckBox
            // 
            this.floodFillingCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.floodFillingCheckBox.AutoSize = true;
            this.floodFillingCheckBox.Location = new System.Drawing.Point(139, 322);
            this.floodFillingCheckBox.Name = "floodFillingCheckBox";
            this.floodFillingCheckBox.Size = new System.Drawing.Size(78, 17);
            this.floodFillingCheckBox.TabIndex = 19;
            this.floodFillingCheckBox.Text = "Flood filling";
            this.floodFillingCheckBox.UseVisualStyleBackColor = true;
            // 
            // standardFillingCheckbox
            // 
            this.standardFillingCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.standardFillingCheckbox.AutoSize = true;
            this.standardFillingCheckbox.Location = new System.Drawing.Point(139, 293);
            this.standardFillingCheckbox.Name = "standardFillingCheckbox";
            this.standardFillingCheckbox.Size = new System.Drawing.Size(93, 17);
            this.standardFillingCheckbox.TabIndex = 18;
            this.standardFillingCheckbox.Text = "Scanline filling";
            this.standardFillingCheckbox.UseVisualStyleBackColor = true;
            // 
            // clippingCheckBox
            // 
            this.clippingCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.clippingCheckBox.AutoSize = true;
            this.clippingCheckBox.Location = new System.Drawing.Point(120, 261);
            this.clippingCheckBox.Name = "clippingCheckBox";
            this.clippingCheckBox.Size = new System.Drawing.Size(63, 17);
            this.clippingCheckBox.TabIndex = 14;
            this.clippingCheckBox.Text = "Clipping";
            this.clippingCheckBox.UseVisualStyleBackColor = true;
            // 
            // supersamplingUpDown
            // 
            this.supersamplingUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.supersamplingUpDown.Location = new System.Drawing.Point(139, 382);
            this.supersamplingUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.supersamplingUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.supersamplingUpDown.Name = "supersamplingUpDown";
            this.supersamplingUpDown.Size = new System.Drawing.Size(100, 20);
            this.supersamplingUpDown.TabIndex = 11;
            this.supersamplingUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 17;
            this.label2.Tag = "nothing to see here";
            this.label2.Text = "Fill color:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Tag = "go away";
            this.label5.Text = "Circle radius:";
            // 
            // fillColorPickerButton
            // 
            this.fillColorPickerButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.fillColorPickerButton.BackColor = System.Drawing.Color.Black;
            this.fillColorPickerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fillColorPickerButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.fillColorPickerButton.Location = new System.Drawing.Point(139, 224);
            this.fillColorPickerButton.Name = "fillColorPickerButton";
            this.fillColorPickerButton.Size = new System.Drawing.Size(107, 20);
            this.fillColorPickerButton.TabIndex = 16;
            this.fillColorPickerButton.TabStop = false;
            this.fillColorPickerButton.Tag = "color picker";
            this.fillColorPickerButton.UseVisualStyleBackColor = false;
            this.fillColorPickerButton.Click += new System.EventHandler(this.fillColorPickerButton_Click);
            // 
            // radiusPicker
            // 
            this.radiusPicker.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.radiusPicker.Location = new System.Drawing.Point(13, 382);
            this.radiusPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.radiusPicker.Name = "radiusPicker";
            this.radiusPicker.Size = new System.Drawing.Size(100, 20);
            this.radiusPicker.TabIndex = 9;
            this.radiusPicker.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rectangleLabel
            // 
            this.rectangleLabel.Location = new System.Drawing.Point(123, 102);
            this.rectangleLabel.Name = "rectangleLabel";
            this.rectangleLabel.Size = new System.Drawing.Size(159, 36);
            this.rectangleLabel.TabIndex = 13;
            this.rectangleLabel.Tag = "2";
            this.rectangleLabel.Text = "Click 2 more points to draw a rectangle.";
            this.rectangleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rectangleLabel.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Tag = "nope";
            this.label1.Text = "Multisampling";
            // 
            // antialiasCheckBox
            // 
            this.antialiasCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.antialiasCheckBox.AutoSize = true;
            this.antialiasCheckBox.Location = new System.Drawing.Point(18, 261);
            this.antialiasCheckBox.Name = "antialiasCheckBox";
            this.antialiasCheckBox.Size = new System.Drawing.Size(82, 17);
            this.antialiasCheckBox.TabIndex = 6;
            this.antialiasCheckBox.Text = "Anti-aliasing";
            this.antialiasCheckBox.UseVisualStyleBackColor = true;
            // 
            // rectangleButton
            // 
            this.rectangleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.rectangleButton.FlatAppearance.BorderSize = 2;
            this.rectangleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rectangleButton.Location = new System.Drawing.Point(3, 102);
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(117, 36);
            this.rectangleButton.TabIndex = 12;
            this.rectangleButton.TabStop = false;
            this.rectangleButton.Tag = "2";
            this.rectangleButton.Text = "Rectangle";
            this.rectangleButton.UseVisualStyleBackColor = false;
            // 
            // circleLabel
            // 
            this.circleLabel.Location = new System.Drawing.Point(126, 62);
            this.circleLabel.Name = "circleLabel";
            this.circleLabel.Size = new System.Drawing.Size(156, 36);
            this.circleLabel.TabIndex = 8;
            this.circleLabel.Tag = "5";
            this.circleLabel.Text = "Click!";
            this.circleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.circleLabel.Visible = false;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDown.Location = new System.Drawing.Point(13, 322);
            this.numericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(100, 20);
            this.numericUpDown.TabIndex = 4;
            this.numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 5;
            this.label4.Tag = "nope";
            this.label4.Text = "Thickness:";
            // 
            // colorButtonExplainingLabel
            // 
            this.colorButtonExplainingLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.colorButtonExplainingLabel.AutoSize = true;
            this.colorButtonExplainingLabel.Location = new System.Drawing.Point(10, 208);
            this.colorButtonExplainingLabel.Name = "colorButtonExplainingLabel";
            this.colorButtonExplainingLabel.Size = new System.Drawing.Size(56, 13);
            this.colorButtonExplainingLabel.TabIndex = 3;
            this.colorButtonExplainingLabel.Tag = "nothing to see here";
            this.colorButtonExplainingLabel.Text = "Line color:";
            // 
            // circleButton
            // 
            this.circleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.circleButton.FlatAppearance.BorderSize = 2;
            this.circleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circleButton.Location = new System.Drawing.Point(3, 62);
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(117, 36);
            this.circleButton.TabIndex = 7;
            this.circleButton.TabStop = false;
            this.circleButton.Tag = "5";
            this.circleButton.Text = "Circle";
            this.circleButton.UseVisualStyleBackColor = false;
            // 
            // segmentLabel
            // 
            this.segmentLabel.Location = new System.Drawing.Point(126, 20);
            this.segmentLabel.Name = "segmentLabel";
            this.segmentLabel.Size = new System.Drawing.Size(156, 36);
            this.segmentLabel.TabIndex = 2;
            this.segmentLabel.Tag = "1";
            this.segmentLabel.Text = "Click 2 more points to draw a segment.";
            this.segmentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.segmentLabel.Visible = false;
            // 
            // quadrangleButton
            // 
            this.quadrangleButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.quadrangleButton.FlatAppearance.BorderSize = 2;
            this.quadrangleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quadrangleButton.Location = new System.Drawing.Point(3, 144);
            this.quadrangleButton.Name = "quadrangleButton";
            this.quadrangleButton.Size = new System.Drawing.Size(117, 36);
            this.quadrangleButton.TabIndex = 0;
            this.quadrangleButton.TabStop = false;
            this.quadrangleButton.Tag = "3";
            this.quadrangleButton.Text = "Convex";
            this.quadrangleButton.UseVisualStyleBackColor = false;
            // 
            // lineColorPickerButton
            // 
            this.lineColorPickerButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lineColorPickerButton.BackColor = System.Drawing.Color.Black;
            this.lineColorPickerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineColorPickerButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lineColorPickerButton.Location = new System.Drawing.Point(13, 224);
            this.lineColorPickerButton.Name = "lineColorPickerButton";
            this.lineColorPickerButton.Size = new System.Drawing.Size(107, 20);
            this.lineColorPickerButton.TabIndex = 1;
            this.lineColorPickerButton.TabStop = false;
            this.lineColorPickerButton.Tag = "color picker";
            this.lineColorPickerButton.UseVisualStyleBackColor = false;
            this.lineColorPickerButton.Click += new System.EventHandler(this.lineColorPickerButton_Click);
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
            this.canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(470, 437);
            this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // cleanClipButton
            // 
            this.cleanClipButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cleanClipButton.Location = new System.Drawing.Point(637, 470);
            this.cleanClipButton.Name = "cleanClipButton";
            this.cleanClipButton.Size = new System.Drawing.Size(134, 22);
            this.cleanClipButton.TabIndex = 15;
            this.cleanClipButton.Text = "Clean clipping";
            this.cleanClipButton.UseVisualStyleBackColor = true;
            this.cleanClipButton.Click += new System.EventHandler(this.cleanClipButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.refreshButton.Location = new System.Drawing.Point(492, 470);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(139, 24);
            this.refreshButton.TabIndex = 14;
            this.refreshButton.Text = "Refresh canvas";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Choose a folder. ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(783, 504);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.cleanClipButton);
            this.Controls.Add(this.refreshButton);
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
            ((System.ComponentModel.ISupportInitialize)(this.supersamplingUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
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
        private System.Windows.Forms.Label segmentLabel;
        private System.Windows.Forms.Button lineColorPickerButton;
        private System.Windows.Forms.Button quadrangleButton;
        private System.Windows.Forms.Button segmentButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label colorButtonExplainingLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.CheckBox antialiasCheckBox;
        private System.Windows.Forms.Label circleLabel;
        private System.Windows.Forms.Button circleButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown radiusPicker;
        private System.Windows.Forms.NumericUpDown supersamplingUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rectangleButton;
        private System.Windows.Forms.Label rectangleLabel;
        private System.Windows.Forms.CheckBox clippingCheckBox;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button cleanClipButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button fillColorPickerButton;
        private System.Windows.Forms.CheckBox standardFillingCheckbox;
        private System.Windows.Forms.CheckBox floodFillingCheckBox;
    }
}

