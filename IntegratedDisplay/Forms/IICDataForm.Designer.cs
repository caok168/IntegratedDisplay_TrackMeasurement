namespace IntegratedDisplay.Forms
{
    partial class IICDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IICDataForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBoxIICFile = new System.Windows.Forms.ListBox();
            this.btnAddIICFile = new System.Windows.Forms.Button();
            this.btnDeleteIICFile = new System.Windows.Forms.Button();
            this.btnClearIICFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnModify = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "波形文件名";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(466, 21);
            this.textBox1.TabIndex = 1;
            // 
            // listBoxIICFile
            // 
            this.listBoxIICFile.FormattingEnabled = true;
            this.listBoxIICFile.HorizontalScrollbar = true;
            this.listBoxIICFile.ItemHeight = 12;
            this.listBoxIICFile.Location = new System.Drawing.Point(6, 20);
            this.listBoxIICFile.Name = "listBoxIICFile";
            this.listBoxIICFile.ScrollAlwaysVisible = true;
            this.listBoxIICFile.Size = new System.Drawing.Size(527, 124);
            this.listBoxIICFile.TabIndex = 3;
            // 
            // btnAddIICFile
            // 
            this.btnAddIICFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddIICFile.Image")));
            this.btnAddIICFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddIICFile.Location = new System.Drawing.Point(545, 19);
            this.btnAddIICFile.Name = "btnAddIICFile";
            this.btnAddIICFile.Size = new System.Drawing.Size(83, 35);
            this.btnAddIICFile.TabIndex = 4;
            this.btnAddIICFile.Text = "添加文件";
            this.btnAddIICFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddIICFile.UseVisualStyleBackColor = true;
            this.btnAddIICFile.Click += new System.EventHandler(this.btnAddIICFile_Click);
            // 
            // btnDeleteIICFile
            // 
            this.btnDeleteIICFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteIICFile.Image")));
            this.btnDeleteIICFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteIICFile.Location = new System.Drawing.Point(545, 60);
            this.btnDeleteIICFile.Name = "btnDeleteIICFile";
            this.btnDeleteIICFile.Size = new System.Drawing.Size(83, 33);
            this.btnDeleteIICFile.TabIndex = 5;
            this.btnDeleteIICFile.Text = "删除文件";
            this.btnDeleteIICFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteIICFile.UseVisualStyleBackColor = true;
            this.btnDeleteIICFile.Click += new System.EventHandler(this.btnDeleteIICFile_Click);
            // 
            // btnClearIICFile
            // 
            this.btnClearIICFile.Image = ((System.Drawing.Image)(resources.GetObject("btnClearIICFile.Image")));
            this.btnClearIICFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearIICFile.Location = new System.Drawing.Point(542, 111);
            this.btnClearIICFile.Name = "btnClearIICFile";
            this.btnClearIICFile.Size = new System.Drawing.Size(86, 33);
            this.btnClearIICFile.TabIndex = 6;
            this.btnClearIICFile.Text = "清空所有";
            this.btnClearIICFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearIICFile.UseVisualStyleBackColor = true;
            this.btnClearIICFile.Click += new System.EventHandler(this.btnClearIICFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "IIC文件|*.iic";
            // 
            // btnModify
            // 
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModify.Location = new System.Drawing.Point(565, 7);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(83, 35);
            this.btnModify.TabIndex = 7;
            this.btnModify.Text = "开始修正";
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(573, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 16);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "重置IIC";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(210, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(221, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.listBoxIICFile);
            this.groupBox1.Controls.Add(this.btnDeleteIICFile);
            this.groupBox1.Controls.Add(this.btnAddIICFile);
            this.groupBox1.Controls.Add(this.btnClearIICFile);
            this.groupBox1.Location = new System.Drawing.Point(14, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 153);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对应IIC";
            // 
            // IICDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 235);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IICDataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IIC数据修正";
            this.Load += new System.EventHandler(this.IICDataForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBoxIICFile;
        private System.Windows.Forms.Button btnAddIICFile;
        private System.Windows.Forms.Button btnDeleteIICFile;
        private System.Windows.Forms.Button btnClearIICFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}