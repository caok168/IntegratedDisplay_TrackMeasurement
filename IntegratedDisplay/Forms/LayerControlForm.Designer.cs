namespace IntegratedDisplay.Forms
{
    partial class LayerControlForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLayerConfig = new System.Windows.Forms.DataGridView();
            this.clnLayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnIsLayerVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnIsMileageLabelVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnIsChannelLabelVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnIsTaggingVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnIsReverse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLayerConfig
            // 
            this.dgvLayerConfig.AllowUserToAddRows = false;
            this.dgvLayerConfig.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLayerConfig.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLayerConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayerConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnLayerName,
            this.clnIsLayerVisible,
            this.clnIsMileageLabelVisible,
            this.clnIsChannelLabelVisible,
            this.clnIsTaggingVisible,
            this.clnIsReverse});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLayerConfig.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLayerConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvLayerConfig.Location = new System.Drawing.Point(0, 0);
            this.dgvLayerConfig.MultiSelect = false;
            this.dgvLayerConfig.Name = "dgvLayerConfig";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLayerConfig.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLayerConfig.RowTemplate.Height = 23;
            this.dgvLayerConfig.Size = new System.Drawing.Size(648, 208);
            this.dgvLayerConfig.TabIndex = 32;
            this.dgvLayerConfig.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayerConfig_CellContentClick);
            // 
            // clnLayerName
            // 
            this.clnLayerName.HeaderText = "层名称";
            this.clnLayerName.Name = "clnLayerName";
            this.clnLayerName.ReadOnly = true;
            this.clnLayerName.Width = 200;
            // 
            // clnIsLayerVisible
            // 
            this.clnIsLayerVisible.FillWeight = 60F;
            this.clnIsLayerVisible.HeaderText = "图层";
            this.clnIsLayerVisible.Name = "clnIsLayerVisible";
            this.clnIsLayerVisible.Width = 70;
            // 
            // clnIsMileageLabelVisible
            // 
            this.clnIsMileageLabelVisible.FillWeight = 60F;
            this.clnIsMileageLabelVisible.HeaderText = "里程标";
            this.clnIsMileageLabelVisible.Name = "clnIsMileageLabelVisible";
            this.clnIsMileageLabelVisible.Width = 70;
            // 
            // clnIsChannelLabelVisible
            // 
            this.clnIsChannelLabelVisible.HeaderText = "通道标签";
            this.clnIsChannelLabelVisible.Name = "clnIsChannelLabelVisible";
            this.clnIsChannelLabelVisible.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clnIsChannelLabelVisible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clnIsTaggingVisible
            // 
            this.clnIsTaggingVisible.FillWeight = 60F;
            this.clnIsTaggingVisible.HeaderText = "标注信息";
            this.clnIsTaggingVisible.Name = "clnIsTaggingVisible";
            this.clnIsTaggingVisible.Width = 60;
            // 
            // clnIsReverse
            // 
            this.clnIsReverse.FillWeight = 60F;
            this.clnIsReverse.HeaderText = "波形左右反转";
            this.clnIsReverse.Name = "clnIsReverse";
            this.clnIsReverse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(456, 219);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Location = new System.Drawing.Point(132, 219);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 34;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "层名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.FillWeight = 60F;
            this.dataGridViewCheckBoxColumn1.HeaderText = "图层";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 70;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.FillWeight = 60F;
            this.dataGridViewCheckBoxColumn2.HeaderText = "里程标";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 70;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.HeaderText = "通道标签";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewCheckBoxColumn4
            // 
            this.dataGridViewCheckBoxColumn4.FillWeight = 60F;
            this.dataGridViewCheckBoxColumn4.HeaderText = "标注信息";
            this.dataGridViewCheckBoxColumn4.Name = "dataGridViewCheckBoxColumn4";
            this.dataGridViewCheckBoxColumn4.Width = 60;
            // 
            // dataGridViewCheckBoxColumn5
            // 
            this.dataGridViewCheckBoxColumn5.FillWeight = 60F;
            this.dataGridViewCheckBoxColumn5.HeaderText = "波形左右反转";
            this.dataGridViewCheckBoxColumn5.Name = "dataGridViewCheckBoxColumn5";
            this.dataGridViewCheckBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn5.Visible = false;
            // 
            // LayerControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 259);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvLayerConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayerControlForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层控制";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LayerControlForm_FormClosing);
            this.Load += new System.EventHandler(this.LayerControlForm_Load);
            this.VisibleChanged += new System.EventHandler(this.LayerControlForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerConfig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLayerConfig;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLayerName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsLayerVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsMileageLabelVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsChannelLabelVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsTaggingVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsReverse;
    }
}