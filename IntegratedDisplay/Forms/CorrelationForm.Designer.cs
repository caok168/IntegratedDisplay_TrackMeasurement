namespace IntegratedDisplay.Forms
{
    partial class CorrelationForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnWaveFix = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnFileDelete = new System.Windows.Forms.Button();
            this.btnFileAdd = new System.Windows.Forms.Button();
            this.lstCitFiles = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTargetPointCount = new System.Windows.Forms.TextBox();
            this.txtOriginalPointCount = new System.Windows.Forms.TextBox();
            this.txtR_Prof = new System.Windows.Forms.TextBox();
            this.txtL_Prof = new System.Windows.Forms.TextBox();
            this.txtGage = new System.Windows.Forms.TextBox();
            this.txtSuperelevation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(274, 165);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 21;
            this.btnBack.Text = "返回(&B)";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnWaveFix
            // 
            this.btnWaveFix.Location = new System.Drawing.Point(60, 165);
            this.btnWaveFix.Name = "btnWaveFix";
            this.btnWaveFix.Size = new System.Drawing.Size(75, 23);
            this.btnWaveFix.TabIndex = 20;
            this.btnWaveFix.Text = "修正(&P)";
            this.btnWaveFix.UseVisualStyleBackColor = true;
            this.btnWaveFix.Click += new System.EventHandler(this.btnWaveFix_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(395, 129);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 19;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnFileDelete
            // 
            this.btnFileDelete.Location = new System.Drawing.Point(395, 78);
            this.btnFileDelete.Name = "btnFileDelete";
            this.btnFileDelete.Size = new System.Drawing.Size(75, 23);
            this.btnFileDelete.TabIndex = 18;
            this.btnFileDelete.Text = "删除文件";
            this.btnFileDelete.UseVisualStyleBackColor = true;
            this.btnFileDelete.Click += new System.EventHandler(this.btnFileDelete_Click);
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.Location = new System.Drawing.Point(395, 28);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Size = new System.Drawing.Size(75, 23);
            this.btnFileAdd.TabIndex = 17;
            this.btnFileAdd.Text = "添加文件";
            this.btnFileAdd.UseVisualStyleBackColor = true;
            this.btnFileAdd.Click += new System.EventHandler(this.btnFileAdd_Click);
            // 
            // lstCitFiles
            // 
            this.lstCitFiles.FormattingEnabled = true;
            this.lstCitFiles.ItemHeight = 12;
            this.lstCitFiles.Location = new System.Drawing.Point(12, 28);
            this.lstCitFiles.Name = "lstCitFiles";
            this.lstCitFiles.ScrollAlwaysVisible = true;
            this.lstCitFiles.Size = new System.Drawing.Size(377, 124);
            this.lstCitFiles.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "要修正的波形文件列表";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTargetPointCount);
            this.groupBox1.Controls.Add(this.txtOriginalPointCount);
            this.groupBox1.Controls.Add(this.txtR_Prof);
            this.groupBox1.Controls.Add(this.txtL_Prof);
            this.groupBox1.Controls.Add(this.txtGage);
            this.groupBox1.Controls.Add(this.txtSuperelevation);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(493, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 188);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "参数配置";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(178, 107);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "0.6-1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(177, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "0.6-1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 23;
            this.label9.Text = "0.6-1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "0.6-1";
            // 
            // txtTargetPointCount
            // 
            this.txtTargetPointCount.Location = new System.Drawing.Point(108, 158);
            this.txtTargetPointCount.Name = "txtTargetPointCount";
            this.txtTargetPointCount.Size = new System.Drawing.Size(64, 21);
            this.txtTargetPointCount.TabIndex = 15;
            this.txtTargetPointCount.Text = "4000";
            // 
            // txtOriginalPointCount
            // 
            this.txtOriginalPointCount.Location = new System.Drawing.Point(107, 130);
            this.txtOriginalPointCount.Name = "txtOriginalPointCount";
            this.txtOriginalPointCount.Size = new System.Drawing.Size(64, 21);
            this.txtOriginalPointCount.TabIndex = 14;
            this.txtOriginalPointCount.Text = "200";
            // 
            // txtR_Prof
            // 
            this.txtR_Prof.Location = new System.Drawing.Point(108, 103);
            this.txtR_Prof.Name = "txtR_Prof";
            this.txtR_Prof.Size = new System.Drawing.Size(64, 21);
            this.txtR_Prof.TabIndex = 13;
            this.txtR_Prof.Text = "0.8";
            // 
            // txtL_Prof
            // 
            this.txtL_Prof.Location = new System.Drawing.Point(107, 75);
            this.txtL_Prof.Name = "txtL_Prof";
            this.txtL_Prof.Size = new System.Drawing.Size(64, 21);
            this.txtL_Prof.TabIndex = 12;
            this.txtL_Prof.Text = "0.8";
            // 
            // txtGage
            // 
            this.txtGage.Location = new System.Drawing.Point(108, 48);
            this.txtGage.Name = "txtGage";
            this.txtGage.Size = new System.Drawing.Size(64, 21);
            this.txtGage.TabIndex = 11;
            this.txtGage.Text = "0.8";
            // 
            // txtSuperelevation
            // 
            this.txtSuperelevation.Location = new System.Drawing.Point(108, 17);
            this.txtSuperelevation.Name = "txtSuperelevation";
            this.txtSuperelevation.Size = new System.Drawing.Size(64, 21);
            this.txtSuperelevation.TabIndex = 10;
            this.txtSuperelevation.Text = "0.8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "原始数据点：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "目标数据点：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "右高低门阚值：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "左高低门阚值：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "超高门阚值：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "轨距门阚值：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Cit文件|*.cit";
            // 
            // CorrelationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 212);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnWaveFix);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnFileDelete);
            this.Controls.Add(this.btnFileAdd);
            this.Controls.Add(this.lstCitFiles);
            this.Controls.Add(this.label2);
            this.Name = "CorrelationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "里程智能校正";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnWaveFix;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFileDelete;
        private System.Windows.Forms.Button btnFileAdd;
        private System.Windows.Forms.ListBox lstCitFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTargetPointCount;
        private System.Windows.Forms.TextBox txtOriginalPointCount;
        private System.Windows.Forms.TextBox txtR_Prof;
        private System.Windows.Forms.TextBox txtL_Prof;
        private System.Windows.Forms.TextBox txtGage;
        private System.Windows.Forms.TextBox txtSuperelevation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}