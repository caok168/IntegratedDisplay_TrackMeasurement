namespace IntegratedDisplay.Forms
{
    partial class LayerTranslationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerTranslationForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nupReviseValue = new System.Windows.Forms.NumericUpDown();
            this.btnRight50X = new System.Windows.Forms.Button();
            this.btnRight10X = new System.Windows.Forms.Button();
            this.btnRight1X = new System.Windows.Forms.Button();
            this.btnLeft1X = new System.Windows.Forms.Button();
            this.btnLeft10X = new System.Windows.Forms.Button();
            this.btnLeft50X = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbtnMileageAlignment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnWaveAlignment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnResetOffset = new System.Windows.Forms.ToolStripButton();
            this.dgvLayerInfo = new System.Windows.Forms.DataGridView();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnLineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnDerection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnMileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnMileageInOrDe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupReviseValue)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nupReviseValue);
            this.groupBox1.Controls.Add(this.btnRight50X);
            this.groupBox1.Controls.Add(this.btnRight10X);
            this.groupBox1.Controls.Add(this.btnRight1X);
            this.groupBox1.Controls.Add(this.btnLeft1X);
            this.groupBox1.Controls.Add(this.btnLeft10X);
            this.groupBox1.Controls.Add(this.btnLeft50X);
            this.groupBox1.Location = new System.Drawing.Point(0, 214);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 67);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // nupReviseValue
            // 
            this.nupReviseValue.Font = new System.Drawing.Font("宋体", 12F);
            this.nupReviseValue.Location = new System.Drawing.Point(206, 23);
            this.nupReviseValue.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nupReviseValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupReviseValue.Name = "nupReviseValue";
            this.nupReviseValue.Size = new System.Drawing.Size(79, 26);
            this.nupReviseValue.TabIndex = 6;
            this.nupReviseValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupReviseValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRight50X
            // 
            this.btnRight50X.Location = new System.Drawing.Point(403, 10);
            this.btnRight50X.Name = "btnRight50X";
            this.btnRight50X.Size = new System.Drawing.Size(50, 50);
            this.btnRight50X.TabIndex = 5;
            this.btnRight50X.Tag = "-40";
            this.btnRight50X.Text = ">>10m";
            this.btnRight50X.UseVisualStyleBackColor = true;
            this.btnRight50X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnRight50X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnRight50X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // btnRight10X
            // 
            this.btnRight10X.Location = new System.Drawing.Point(347, 10);
            this.btnRight10X.Name = "btnRight10X";
            this.btnRight10X.Size = new System.Drawing.Size(50, 50);
            this.btnRight10X.TabIndex = 4;
            this.btnRight10X.Tag = "-20";
            this.btnRight10X.Text = ">>5m";
            this.btnRight10X.UseVisualStyleBackColor = true;
            this.btnRight10X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnRight10X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnRight10X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // btnRight1X
            // 
            this.btnRight1X.Location = new System.Drawing.Point(291, 10);
            this.btnRight1X.Name = "btnRight1X";
            this.btnRight1X.Size = new System.Drawing.Size(50, 50);
            this.btnRight1X.TabIndex = 3;
            this.btnRight1X.Tag = "-1";
            this.btnRight1X.Text = "->";
            this.btnRight1X.UseVisualStyleBackColor = true;
            this.btnRight1X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnRight1X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnRight1X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // btnLeft1X
            // 
            this.btnLeft1X.Location = new System.Drawing.Point(150, 10);
            this.btnLeft1X.Name = "btnLeft1X";
            this.btnLeft1X.Size = new System.Drawing.Size(50, 50);
            this.btnLeft1X.TabIndex = 2;
            this.btnLeft1X.Tag = "1";
            this.btnLeft1X.Text = "<-";
            this.btnLeft1X.UseVisualStyleBackColor = true;
            this.btnLeft1X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnLeft1X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnLeft1X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // btnLeft10X
            // 
            this.btnLeft10X.Location = new System.Drawing.Point(94, 10);
            this.btnLeft10X.Name = "btnLeft10X";
            this.btnLeft10X.Size = new System.Drawing.Size(50, 50);
            this.btnLeft10X.TabIndex = 1;
            this.btnLeft10X.Tag = "20";
            this.btnLeft10X.Text = "<<5m";
            this.btnLeft10X.UseVisualStyleBackColor = true;
            this.btnLeft10X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnLeft10X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnLeft10X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // btnLeft50X
            // 
            this.btnLeft50X.Location = new System.Drawing.Point(38, 10);
            this.btnLeft50X.Name = "btnLeft50X";
            this.btnLeft50X.Size = new System.Drawing.Size(50, 50);
            this.btnLeft50X.TabIndex = 0;
            this.btnLeft50X.Tag = "40";
            this.btnLeft50X.Text = "<<10m";
            this.btnLeft50X.UseVisualStyleBackColor = true;
            this.btnLeft50X.Click += new System.EventHandler(this.btnAll_Click);
            this.btnLeft50X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseDown);
            this.btnLeft50X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAll_MouseUp);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnMileageAlignment,
            this.toolStripSeparator1,
            this.tsbtnWaveAlignment,
            this.toolStripSeparator10,
            this.tsbtnResetOffset});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(486, 25);
            this.toolStrip2.TabIndex = 26;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbtnMileageAlignment
            // 
            this.tsbtnMileageAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnMileageAlignment.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMileageAlignment.Image")));
            this.tsbtnMileageAlignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMileageAlignment.Name = "tsbtnMileageAlignment";
            this.tsbtnMileageAlignment.Size = new System.Drawing.Size(60, 22);
            this.tsbtnMileageAlignment.Text = "里程对齐";
            this.tsbtnMileageAlignment.Click += new System.EventHandler(this.tsbtnMileageAlignment_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnWaveAlignment
            // 
            this.tsbtnWaveAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnWaveAlignment.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnWaveAlignment.Image")));
            this.tsbtnWaveAlignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnWaveAlignment.Name = "tsbtnWaveAlignment";
            this.tsbtnWaveAlignment.Size = new System.Drawing.Size(60, 22);
            this.tsbtnWaveAlignment.Text = "波形对齐";
            this.tsbtnWaveAlignment.Click += new System.EventHandler(this.tsbtnWaveAlignment_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnResetOffset
            // 
            this.tsbtnResetOffset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnResetOffset.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnResetOffset.Image")));
            this.tsbtnResetOffset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnResetOffset.Name = "tsbtnResetOffset";
            this.tsbtnResetOffset.Size = new System.Drawing.Size(60, 22);
            this.tsbtnResetOffset.Text = "重置偏移";
            this.tsbtnResetOffset.Click += new System.EventHandler(this.tsbtnResetOffset_Click);
            // 
            // dgvLayerInfo
            // 
            this.dgvLayerInfo.AllowUserToAddRows = false;
            this.dgvLayerInfo.AllowUserToDeleteRows = false;
            this.dgvLayerInfo.AllowUserToResizeRows = false;
            this.dgvLayerInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLayerInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLayerInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayerInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnID,
            this.clnLineName,
            this.clnDate,
            this.clnDerection,
            this.clnMileage,
            this.clnMileageInOrDe,
            this.clnTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLayerInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLayerInfo.Location = new System.Drawing.Point(0, 28);
            this.dgvLayerInfo.Name = "dgvLayerInfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLayerInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLayerInfo.RowTemplate.Height = 23;
            this.dgvLayerInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLayerInfo.ShowEditingIcon = false;
            this.dgvLayerInfo.Size = new System.Drawing.Size(486, 182);
            this.dgvLayerInfo.TabIndex = 25;
            this.dgvLayerInfo.SelectionChanged += new System.EventHandler(this.dgvLayerInfo_SelectionChanged);
            // 
            // timer
            // 
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 60F;
            this.dataGridViewTextBoxColumn1.HeaderText = "层号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "检测线路";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "检测日期";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "运行方向";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "当前里程";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "里程增减";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "检测时间";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // clnID
            // 
            this.clnID.FillWeight = 60F;
            this.clnID.HeaderText = "层号";
            this.clnID.Name = "clnID";
            this.clnID.ReadOnly = true;
            this.clnID.Width = 60;
            // 
            // clnLineName
            // 
            this.clnLineName.HeaderText = "检测线路";
            this.clnLineName.Name = "clnLineName";
            this.clnLineName.ReadOnly = true;
            // 
            // clnDate
            // 
            this.clnDate.HeaderText = "检测日期";
            this.clnDate.Name = "clnDate";
            this.clnDate.ReadOnly = true;
            // 
            // clnDerection
            // 
            this.clnDerection.HeaderText = "运行方向";
            this.clnDerection.Name = "clnDerection";
            this.clnDerection.ReadOnly = true;
            // 
            // clnMileage
            // 
            this.clnMileage.HeaderText = "当前里程";
            this.clnMileage.Name = "clnMileage";
            this.clnMileage.ReadOnly = true;
            // 
            // clnMileageInOrDe
            // 
            this.clnMileageInOrDe.HeaderText = "里程增减";
            this.clnMileageInOrDe.Name = "clnMileageInOrDe";
            this.clnMileageInOrDe.ReadOnly = true;
            // 
            // clnTime
            // 
            this.clnTime.HeaderText = "检测时间";
            this.clnTime.Name = "clnTime";
            // 
            // LayerTranslationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 288);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.dgvLayerInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayerTranslationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层平移";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LayerTranslationForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.LayerTranslationForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupReviseValue)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nupReviseValue;
        private System.Windows.Forms.Button btnRight50X;
        private System.Windows.Forms.Button btnRight10X;
        private System.Windows.Forms.Button btnRight1X;
        private System.Windows.Forms.Button btnLeft1X;
        private System.Windows.Forms.Button btnLeft10X;
        private System.Windows.Forms.Button btnLeft50X;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnWaveAlignment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbtnResetOffset;
        protected internal System.Windows.Forms.DataGridView dgvLayerInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnDerection;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnMileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnMileageInOrDe;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton tsbtnMileageAlignment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}