namespace IntegratedDisplay
{
    partial class CommonConfigControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMeterageRadius = new IntegratedDisplay.CustomControl.CustomTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labScrollShow = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarAutoScrool = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSelectMediaPath = new System.Windows.Forms.Button();
            this.txtMidiaPath = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarSignSize = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAutoScrool)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSignSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "请输入测量半径：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMeterageRadius);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(28, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(247, 74);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测量设置";
            // 
            // txtMeterageRadius
            // 
            this.txtMeterageRadius.Location = new System.Drawing.Point(109, 31);
            this.txtMeterageRadius.Name = "txtMeterageRadius";
            this.txtMeterageRadius.Size = new System.Drawing.Size(120, 21);
            this.txtMeterageRadius.TabIndex = 29;
            this.txtMeterageRadius.WaterText = "采样点个数(1-500)";
            this.txtMeterageRadius.Leave += new System.EventHandler(this.txtEdit_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labScrollShow);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.trackBarAutoScrool);
            this.groupBox1.Location = new System.Drawing.Point(289, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 74);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "滚动速度设置";
            // 
            // labScrollShow
            // 
            this.labScrollShow.AutoSize = true;
            this.labScrollShow.Location = new System.Drawing.Point(38, 49);
            this.labScrollShow.Name = "labScrollShow";
            this.labScrollShow.Size = new System.Drawing.Size(11, 12);
            this.labScrollShow.TabIndex = 29;
            this.labScrollShow.Text = "1";
            this.labScrollShow.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "快";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "慢";
            // 
            // trackBarAutoScrool
            // 
            this.trackBarAutoScrool.LargeChange = 1;
            this.trackBarAutoScrool.Location = new System.Drawing.Point(29, 20);
            this.trackBarAutoScrool.Maximum = 1500;
            this.trackBarAutoScrool.Minimum = 600;
            this.trackBarAutoScrool.Name = "trackBarAutoScrool";
            this.trackBarAutoScrool.Size = new System.Drawing.Size(180, 45);
            this.trackBarAutoScrool.TabIndex = 29;
            this.trackBarAutoScrool.TickFrequency = 100;
            this.trackBarAutoScrool.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarAutoScrool.Value = 600;
            this.trackBarAutoScrool.Scroll += new System.EventHandler(this.trackBarAutoScrool_Scroll);
            this.trackBarAutoScrool.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBarAutoScrool_MouseDown);
            this.trackBarAutoScrool.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarAutoScrool_MouseUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSelectMediaPath);
            this.groupBox3.Controls.Add(this.txtMidiaPath);
            this.groupBox3.Location = new System.Drawing.Point(28, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(503, 81);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "波形库所在目录";
            // 
            // btnSelectMediaPath
            // 
            this.btnSelectMediaPath.Location = new System.Drawing.Point(398, 48);
            this.btnSelectMediaPath.Name = "btnSelectMediaPath";
            this.btnSelectMediaPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectMediaPath.TabIndex = 1;
            this.btnSelectMediaPath.Text = "浏览";
            this.btnSelectMediaPath.UseVisualStyleBackColor = true;
            this.btnSelectMediaPath.Click += new System.EventHandler(this.btnSelectMediaPath_Click);
            // 
            // txtMidiaPath
            // 
            this.txtMidiaPath.Location = new System.Drawing.Point(21, 20);
            this.txtMidiaPath.Name = "txtMidiaPath";
            this.txtMidiaPath.Size = new System.Drawing.Size(452, 21);
            this.txtMidiaPath.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.trackBarSignSize);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(28, 204);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(247, 74);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图形标注半径";
            this.groupBox4.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "小";
            // 
            // trackBarSignSize
            // 
            this.trackBarSignSize.Location = new System.Drawing.Point(35, 21);
            this.trackBarSignSize.Maximum = 50;
            this.trackBarSignSize.Minimum = 5;
            this.trackBarSignSize.Name = "trackBarSignSize";
            this.trackBarSignSize.Size = new System.Drawing.Size(182, 45);
            this.trackBarSignSize.TabIndex = 32;
            this.trackBarSignSize.TickFrequency = 5;
            this.trackBarSignSize.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarSignSize.Value = 20;
            this.trackBarSignSize.Scroll += new System.EventHandler(this.trackBarSignSize_Scroll);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(223, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "大";
            // 
            // CommonConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "CommonConfigControl";
            this.Size = new System.Drawing.Size(549, 310);
            this.Load += new System.EventHandler(this.CommonConfigControl_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAutoScrool)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSignSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSelectMediaPath;
        private System.Windows.Forms.TextBox txtMidiaPath;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarAutoScrool;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarSignSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labScrollShow;
        private CustomControl.CustomTextBox txtMeterageRadius;
    }
}
