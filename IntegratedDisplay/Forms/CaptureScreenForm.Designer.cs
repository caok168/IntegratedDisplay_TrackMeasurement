namespace IntegratedDisplay.Forms
{
    partial class CaptureScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureScreenForm));
            this.picCaptureImage = new System.Windows.Forms.PictureBox();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbxLineWidth = new System.Windows.Forms.ToolStripComboBox();
            this.tsmiSelectColor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectFont = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReback = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddRect = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddFont = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.txtFontText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureImage)).BeginInit();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCaptureImage
            // 
            this.picCaptureImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCaptureImage.Location = new System.Drawing.Point(0, 47);
            this.picCaptureImage.Name = "picCaptureImage";
            this.picCaptureImage.Size = new System.Drawing.Size(790, 330);
            this.picCaptureImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCaptureImage.TabIndex = 0;
            this.picCaptureImage.TabStop = false;
            this.picCaptureImage.Paint += new System.Windows.Forms.PaintEventHandler(this.picCaptureImage_Paint);
            this.picCaptureImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCaptureImage_MouseDown);
            this.picCaptureImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCaptureImage_MouseMove);
            this.picCaptureImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCaptureImage_MouseUp);
            // 
            // menuBar
            // 
            this.menuBar.AutoSize = false;
            this.menuBar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.tsmiSave,
            this.tsmiClose,
            this.tscbxLineWidth,
            this.tsmiSelectColor,
            this.tsmiSelectFont,
            this.tsmiReback,
            this.tsmiAddArrow,
            this.tsmiAddCircle,
            this.tsmiAddRect,
            this.编辑截图ToolStripMenuItem,
            this.tsmiAddFont});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuBar.Size = new System.Drawing.Size(790, 47);
            this.menuBar.TabIndex = 1;
            this.menuBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.menuBar_MouseDoubleClick);
            this.menuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuBar_MouseDown);
            this.menuBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuBar_MouseMove);
            this.menuBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuBar_MouseUp);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.toolStripMenuItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripMenuItem6.Enabled = false;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(12, 43);
            this.toolStripMenuItem6.Text = "    ";
            this.toolStripMenuItem6.Visible = false;
            // 
            // tsmiSave
            // 
            this.tsmiSave.AutoSize = false;
            this.tsmiSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSave.Image")));
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(43, 43);
            this.tsmiSave.ToolTipText = "保存";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiClose
            // 
            this.tsmiClose.AutoSize = false;
            this.tsmiClose.Image = ((System.Drawing.Image)(resources.GetObject("tsmiClose.Image")));
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(43, 43);
            this.tsmiClose.ToolTipText = "关闭";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tscbxLineWidth
            // 
            this.tscbxLineWidth.AutoSize = false;
            this.tscbxLineWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.tscbxLineWidth.Name = "tscbxLineWidth";
            this.tscbxLineWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tscbxLineWidth.Size = new System.Drawing.Size(121, 25);
            this.tscbxLineWidth.Text = "线条宽度";
            this.tscbxLineWidth.SelectedIndexChanged += new System.EventHandler(this.tscbxLineWidth_SelectedIndexChanged);
            // 
            // tsmiSelectColor
            // 
            this.tsmiSelectColor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSelectColor.Image")));
            this.tsmiSelectColor.Name = "tsmiSelectColor";
            this.tsmiSelectColor.Size = new System.Drawing.Size(68, 43);
            this.tsmiSelectColor.Text = "选择颜色";
            this.tsmiSelectColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiSelectColor.Click += new System.EventHandler(this.tsmiSelectColor_Click);
            // 
            // tsmiSelectFont
            // 
            this.tsmiSelectFont.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSelectFont.Image")));
            this.tsmiSelectFont.Name = "tsmiSelectFont";
            this.tsmiSelectFont.Size = new System.Drawing.Size(68, 43);
            this.tsmiSelectFont.Text = "选择字体";
            this.tsmiSelectFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiSelectFont.Click += new System.EventHandler(this.tsmiSelectFont_Click);
            // 
            // tsmiReback
            // 
            this.tsmiReback.Image = ((System.Drawing.Image)(resources.GetObject("tsmiReback.Image")));
            this.tsmiReback.Name = "tsmiReback";
            this.tsmiReback.Size = new System.Drawing.Size(68, 43);
            this.tsmiReback.Tag = "5";
            this.tsmiReback.Text = "撤销操作";
            this.tsmiReback.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiReback.Click += new System.EventHandler(this.tsmiActionButton_Click);
            // 
            // tsmiAddArrow
            // 
            this.tsmiAddArrow.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddArrow.Image")));
            this.tsmiAddArrow.Name = "tsmiAddArrow";
            this.tsmiAddArrow.Size = new System.Drawing.Size(68, 43);
            this.tsmiAddArrow.Tag = "4";
            this.tsmiAddArrow.Text = "箭头工具";
            this.tsmiAddArrow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiAddArrow.Click += new System.EventHandler(this.tsmiActionButton_Click);
            // 
            // tsmiAddCircle
            // 
            this.tsmiAddCircle.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddCircle.Image")));
            this.tsmiAddCircle.Name = "tsmiAddCircle";
            this.tsmiAddCircle.Size = new System.Drawing.Size(68, 43);
            this.tsmiAddCircle.Tag = "3";
            this.tsmiAddCircle.Text = "圆形工具";
            this.tsmiAddCircle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiAddCircle.Click += new System.EventHandler(this.tsmiActionButton_Click);
            // 
            // tsmiAddRect
            // 
            this.tsmiAddRect.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddRect.Image")));
            this.tsmiAddRect.Name = "tsmiAddRect";
            this.tsmiAddRect.Size = new System.Drawing.Size(68, 43);
            this.tsmiAddRect.Tag = "2";
            this.tsmiAddRect.Text = "矩形工具";
            this.tsmiAddRect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiAddRect.Click += new System.EventHandler(this.tsmiActionButton_Click);
            // 
            // 编辑截图ToolStripMenuItem
            // 
            this.编辑截图ToolStripMenuItem.Enabled = false;
            this.编辑截图ToolStripMenuItem.Name = "编辑截图ToolStripMenuItem";
            this.编辑截图ToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.编辑截图ToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.编辑截图ToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.编辑截图ToolStripMenuItem.Text = "编辑截图";
            // 
            // tsmiAddFont
            // 
            this.tsmiAddFont.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAddFont.Image")));
            this.tsmiAddFont.Name = "tsmiAddFont";
            this.tsmiAddFont.Size = new System.Drawing.Size(68, 43);
            this.tsmiAddFont.Tag = "1";
            this.tsmiAddFont.Text = "文字工具";
            this.tsmiAddFont.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsmiAddFont.Click += new System.EventHandler(this.tsmiActionButton_Click);
            // 
            // txtFontText
            // 
            this.txtFontText.Location = new System.Drawing.Point(0, 50);
            this.txtFontText.Multiline = true;
            this.txtFontText.Name = "txtFontText";
            this.txtFontText.Size = new System.Drawing.Size(143, 51);
            this.txtFontText.TabIndex = 2;
            this.txtFontText.VisibleChanged += new System.EventHandler(this.txtFontText_VisibleChanged);
            this.txtFontText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtFontText_PreviewKeyDown);
            // 
            // CaptureScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 377);
            this.Controls.Add(this.txtFontText);
            this.Controls.Add(this.picCaptureImage);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuBar;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaptureScreenForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑截图";
            this.Load += new System.EventHandler(this.CaptureScreenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureImage)).EndInit();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCaptureImage;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectFont;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddRect;
        private System.Windows.Forms.ToolStripMenuItem tsmiReback;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCircle;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectColor;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.TextBox txtFontText;
        private System.Windows.Forms.ToolStripComboBox tscbxLineWidth;
        private System.Windows.Forms.ToolStripMenuItem 编辑截图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddFont;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddArrow;
    }
}