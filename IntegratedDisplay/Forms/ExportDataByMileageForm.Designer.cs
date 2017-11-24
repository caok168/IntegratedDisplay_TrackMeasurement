namespace IntegratedDisplay.Forms
{
    partial class ExportDataByMileageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportDataByMileageForm));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtExportPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.labLoading = new System.Windows.Forms.Label();
            this.txtMileEnd = new IntegratedDisplay.CustomControl.CustomTextBox();
            this.txtMileStart = new IntegratedDisplay.CustomControl.CustomTextBox();
            this.ckbIsStart = new System.Windows.Forms.CheckBox();
            this.ckbIsEnd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "终止里程:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "起始里程:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(375, 95);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(79, 30);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtExportPath
            // 
            this.txtExportPath.Location = new System.Drawing.Point(76, 101);
            this.txtExportPath.Name = "txtExportPath";
            this.txtExportPath.ReadOnly = true;
            this.txtExportPath.Size = new System.Drawing.Size(225, 21);
            this.txtExportPath.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "导出路径";
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPath.Location = new System.Drawing.Point(307, 98);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(66, 26);
            this.btnSelectPath.TabIndex = 17;
            this.btnSelectPath.Text = "浏览";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // labLoading
            // 
            this.labLoading.Image = ((System.Drawing.Image)(resources.GetObject("labLoading.Image")));
            this.labLoading.Location = new System.Drawing.Point(461, 101);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(18, 18);
            this.labLoading.TabIndex = 20;
            this.labLoading.Visible = false;
            // 
            // txtMileEnd
            // 
            this.txtMileEnd.Location = new System.Drawing.Point(121, 56);
            this.txtMileEnd.Name = "txtMileEnd";
            this.txtMileEnd.Size = new System.Drawing.Size(130, 21);
            this.txtMileEnd.TabIndex = 19;
            this.txtMileEnd.WaterText = "123.567(Km.m)";
            // 
            // txtMileStart
            // 
            this.txtMileStart.Location = new System.Drawing.Point(121, 12);
            this.txtMileStart.Name = "txtMileStart";
            this.txtMileStart.Size = new System.Drawing.Size(130, 21);
            this.txtMileStart.TabIndex = 18;
            this.txtMileStart.WaterText = "123.567(Km.m)";
            // 
            // ckbIsStart
            // 
            this.ckbIsStart.AutoSize = true;
            this.ckbIsStart.Location = new System.Drawing.Point(284, 16);
            this.ckbIsStart.Name = "ckbIsStart";
            this.ckbIsStart.Size = new System.Drawing.Size(84, 16);
            this.ckbIsStart.TabIndex = 21;
            this.ckbIsStart.Text = "从文件开始";
            this.ckbIsStart.UseVisualStyleBackColor = true;
            this.ckbIsStart.CheckedChanged += new System.EventHandler(this.ckbIsStart_CheckedChanged);
            // 
            // ckbIsEnd
            // 
            this.ckbIsEnd.AutoSize = true;
            this.ckbIsEnd.Location = new System.Drawing.Point(283, 58);
            this.ckbIsEnd.Name = "ckbIsEnd";
            this.ckbIsEnd.Size = new System.Drawing.Size(84, 16);
            this.ckbIsEnd.TabIndex = 22;
            this.ckbIsEnd.Text = "到文件结束";
            this.ckbIsEnd.UseVisualStyleBackColor = true;
            this.ckbIsEnd.CheckedChanged += new System.EventHandler(this.ckbIsEnd_CheckedChanged);
            // 
            // ExportDataByMileageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 156);
            this.Controls.Add(this.ckbIsEnd);
            this.Controls.Add(this.ckbIsStart);
            this.Controls.Add(this.labLoading);
            this.Controls.Add(this.txtMileEnd);
            this.Controls.Add(this.txtMileStart);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txtExportPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportDataByMileageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出文件-按里程";
            this.Load += new System.EventHandler(this.ExportDataByMileageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txtExportPath;
        private CustomControl.CustomTextBox txtMileStart;
        private CustomControl.CustomTextBox txtMileEnd;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label labLoading;
        private System.Windows.Forms.CheckBox ckbIsStart;
        private System.Windows.Forms.CheckBox ckbIsEnd;
    }
}