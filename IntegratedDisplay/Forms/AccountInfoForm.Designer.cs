namespace IntegratedDisplay.Forms
{
    partial class AccountInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountInfoForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.tsmiStartMileage = new System.Windows.Forms.ToolStripMenuItem();
            this.tstxtStartMileage = new System.Windows.Forms.ToolStripTextBox();
            this.tsmiEndMileage = new System.Windows.Forms.ToolStripMenuItem();
            this.tstxtEndMileage = new System.Windows.Forms.ToolStripTextBox();
            this.tsmiType = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbxType = new System.Windows.Forms.ToolStripComboBox();
            this.tsmiLinkage = new System.Windows.Forms.ToolStripMenuItem();
            this.tscbxLinkage = new System.Windows.Forms.ToolStripComboBox();
            this.tsmiFind = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvAccountInfo = new System.Windows.Forms.DataGridView();
            this.menuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.AutoSize = false;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStartMileage,
            this.tstxtStartMileage,
            this.tsmiEndMileage,
            this.tstxtEndMileage,
            this.tsmiType,
            this.tscbxType,
            this.tsmiLinkage,
            this.tscbxLinkage,
            this.tsmiFind});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(851, 33);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "menuStrip1";
            // 
            // tsmiStartMileage
            // 
            this.tsmiStartMileage.Name = "tsmiStartMileage";
            this.tsmiStartMileage.Size = new System.Drawing.Size(68, 29);
            this.tsmiStartMileage.Text = "起始里程";
            // 
            // tstxtStartMileage
            // 
            this.tstxtStartMileage.Name = "tstxtStartMileage";
            this.tstxtStartMileage.Size = new System.Drawing.Size(100, 29);
            this.tstxtStartMileage.Text = "0";
            this.tstxtStartMileage.Leave += new System.EventHandler(this.tstxtMileageCheck_Leave);
            // 
            // tsmiEndMileage
            // 
            this.tsmiEndMileage.Name = "tsmiEndMileage";
            this.tsmiEndMileage.Size = new System.Drawing.Size(68, 29);
            this.tsmiEndMileage.Text = "终止里程";
            // 
            // tstxtEndMileage
            // 
            this.tstxtEndMileage.Name = "tstxtEndMileage";
            this.tstxtEndMileage.Size = new System.Drawing.Size(100, 29);
            this.tstxtEndMileage.Text = "3000";
            this.tstxtEndMileage.Leave += new System.EventHandler(this.tstxtMileageCheck_Leave);
            // 
            // tsmiType
            // 
            this.tsmiType.Name = "tsmiType";
            this.tsmiType.Size = new System.Drawing.Size(68, 29);
            this.tsmiType.Text = "数据类型";
            // 
            // tscbxType
            // 
            this.tscbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbxType.Name = "tscbxType";
            this.tscbxType.Size = new System.Drawing.Size(121, 29);
            this.tscbxType.SelectedIndexChanged += new System.EventHandler(this.tscbxType_SelectedIndexChanged);
            // 
            // tsmiLinkage
            // 
            this.tsmiLinkage.Name = "tsmiLinkage";
            this.tsmiLinkage.Size = new System.Drawing.Size(128, 29);
            this.tsmiLinkage.Text = "是否跟随主窗口联动";
            // 
            // tscbxLinkage
            // 
            this.tscbxLinkage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbxLinkage.Items.AddRange(new object[] {
            "是",
            "否"});
            this.tscbxLinkage.Name = "tscbxLinkage";
            this.tscbxLinkage.Size = new System.Drawing.Size(121, 29);
            // 
            // tsmiFind
            // 
            this.tsmiFind.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tsmiFind.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFind.Image")));
            this.tsmiFind.Name = "tsmiFind";
            this.tsmiFind.Size = new System.Drawing.Size(60, 29);
            this.tsmiFind.Text = "查找";
            this.tsmiFind.Click += new System.EventHandler(this.tsmiFind_Click);
            // 
            // dgvAccountInfo
            // 
            this.dgvAccountInfo.AllowUserToAddRows = false;
            this.dgvAccountInfo.AllowUserToDeleteRows = false;
            this.dgvAccountInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccountInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccountInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccountInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccountInfo.Location = new System.Drawing.Point(0, 33);
            this.dgvAccountInfo.MultiSelect = false;
            this.dgvAccountInfo.Name = "dgvAccountInfo";
            this.dgvAccountInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccountInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAccountInfo.RowTemplate.Height = 23;
            this.dgvAccountInfo.ShowEditingIcon = false;
            this.dgvAccountInfo.Size = new System.Drawing.Size(851, 299);
            this.dgvAccountInfo.TabIndex = 1;
            // 
            // AccountInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 332);
            this.Controls.Add(this.dgvAccountInfo);
            this.Controls.Add(this.menuBar);
            this.MainMenuStrip = this.menuBar;
            this.Name = "AccountInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "台帐信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountInfoForm_FormClosing);
            this.Load += new System.EventHandler(this.AccountInfoForm_Load);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem tsmiStartMileage;
        private System.Windows.Forms.ToolStripMenuItem tsmiEndMileage;
        private System.Windows.Forms.ToolStripMenuItem tsmiType;
        private System.Windows.Forms.ToolStripComboBox tscbxType;
        private System.Windows.Forms.ToolStripMenuItem tsmiLinkage;
        private System.Windows.Forms.ToolStripMenuItem tsmiFind;
        protected internal System.Windows.Forms.DataGridView dgvAccountInfo;
        protected internal System.Windows.Forms.ToolStripComboBox tscbxLinkage;
        protected internal System.Windows.Forms.ToolStripTextBox tstxtStartMileage;
        protected internal System.Windows.Forms.ToolStripTextBox tstxtEndMileage;
    }
}