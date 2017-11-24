namespace IntegratedDisplay
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.controlPanel = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.treeSetting = new DevComponents.AdvTree.AdvTree();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            ((System.ComponentModel.ISupportInitialize)(this.treeSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Play_16x16.png");
            this.imageList1.Images.SetKeyName(1, "Clicked.png");
            // 
            // controlPanel
            // 
            this.controlPanel.Location = new System.Drawing.Point(165, 12);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(580, 310);
            this.controlPanel.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(523, 353);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 31);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(634, 353);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 31);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // treeSetting
            // 
            this.treeSetting.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeSetting.BackgroundStyle.Class = "TreeBorderKey";
            this.treeSetting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeSetting.ExpandButtonType = DevComponents.AdvTree.eExpandButtonType.Triangle;
            this.treeSetting.ExpandWidth = 14;
            this.treeSetting.HotTracking = true;
            this.treeSetting.Location = new System.Drawing.Point(12, 12);
            this.treeSetting.Margin = new System.Windows.Forms.Padding(8);
            this.treeSetting.Name = "treeSetting";
            this.treeSetting.NodeSpacing = 10;
            this.treeSetting.NodeStyle = this.elementStyle1;
            this.treeSetting.PathSeparator = ";";
            this.treeSetting.Size = new System.Drawing.Size(142, 310);
            this.treeSetting.Styles.Add(this.elementStyle1);
            this.treeSetting.TabIndex = 4;
            this.treeSetting.Text = "advTree1";
            this.treeSetting.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treeSetting_NodeClick);
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // line1
            // 
            this.line1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.line1.Location = new System.Drawing.Point(165, 321);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(577, 10);
            this.line1.TabIndex = 5;
            this.line1.Text = "line1";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 395);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.treeSetting);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.controlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private DevComponents.AdvTree.AdvTree treeSetting;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.Controls.Line line1;

    }
}