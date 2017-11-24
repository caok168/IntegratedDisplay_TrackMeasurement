namespace IntegratedDisplay
{
    partial class WaveDisplayConfigControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvWaveConfig = new System.Windows.Forms.DataGridView();
            this.clnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelectConfigFile = new System.Windows.Forms.Button();
            this.openSelectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaveConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWaveConfig
            // 
            this.dgvWaveConfig.AllowUserToAddRows = false;
            this.dgvWaveConfig.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWaveConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWaveConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaveConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnID,
            this.clnPath});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWaveConfig.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvWaveConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvWaveConfig.Location = new System.Drawing.Point(0, 0);
            this.dgvWaveConfig.MultiSelect = false;
            this.dgvWaveConfig.Name = "dgvWaveConfig";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWaveConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWaveConfig.RowTemplate.Height = 23;
            this.dgvWaveConfig.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaveConfig.Size = new System.Drawing.Size(549, 233);
            this.dgvWaveConfig.TabIndex = 2;
            // 
            // clnID
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.clnID.DefaultCellStyle = dataGridViewCellStyle2;
            this.clnID.HeaderText = "波形序号";
            this.clnID.MinimumWidth = 60;
            this.clnID.Name = "clnID";
            this.clnID.ReadOnly = true;
            this.clnID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.clnID.Width = 80;
            // 
            // clnPath
            // 
            this.clnPath.HeaderText = "路径";
            this.clnPath.MinimumWidth = 800;
            this.clnPath.Name = "clnPath";
            this.clnPath.Width = 800;
            // 
            // btnSelectConfigFile
            // 
            this.btnSelectConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectConfigFile.AutoSize = true;
            this.btnSelectConfigFile.Location = new System.Drawing.Point(17, 245);
            this.btnSelectConfigFile.Name = "btnSelectConfigFile";
            this.btnSelectConfigFile.Size = new System.Drawing.Size(99, 30);
            this.btnSelectConfigFile.TabIndex = 14;
            this.btnSelectConfigFile.Text = "选择文件(&C)...";
            this.btnSelectConfigFile.UseVisualStyleBackColor = true;
            this.btnSelectConfigFile.Click += new System.EventHandler(this.btnSelectConfigFile_Click);
            // 
            // openSelectFileDialog
            // 
            this.openSelectFileDialog.Filter = " 波形显示配置文件|*.xml";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(143, 245);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(99, 30);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // WaveDisplayConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSelectConfigFile);
            this.Controls.Add(this.dgvWaveConfig);
            this.Name = "WaveDisplayConfigControl";
            this.Size = new System.Drawing.Size(549, 310);
            this.Load += new System.EventHandler(this.WaveDisplayConfigControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaveConfig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWaveConfig;
        private System.Windows.Forms.Button btnSelectConfigFile;
        private System.Windows.Forms.OpenFileDialog openSelectFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnPath;
        private System.Windows.Forms.Button btnDelete;
    }
}
