namespace IntegratedDisplay.Forms
{
    partial class Geo2CitConvertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Geo2CitConvertForm));
            this.panelIIC = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectIICFile = new System.Windows.Forms.Button();
            this.txtIICFilePath = new System.Windows.Forms.TextBox();
            this.btnConvertToCIT = new System.Windows.Forms.Button();
            this.gpbCitHeadEditer = new System.Windows.Forms.GroupBox();
            this.labLoading = new System.Windows.Forms.Label();
            this.picSelectLineName = new System.Windows.Forms.PictureBox();
            this.txtEndPos = new System.Windows.Forms.TextBox();
            this.txtStartPos = new System.Windows.Forms.TextBox();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labKmInc = new System.Windows.Forms.Label();
            this.labRunDir = new System.Windows.Forms.Label();
            this.labEndPos = new System.Windows.Forms.Label();
            this.labStartPos = new System.Windows.Forms.Label();
            this.labTrainCode = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.labDate = new System.Windows.Forms.Label();
            this.labLineDir = new System.Windows.Forms.Label();
            this.labLineName = new System.Windows.Forms.Label();
            this.cbxKmInc = new System.Windows.Forms.ComboBox();
            this.cbxRunDir = new System.Windows.Forms.ComboBox();
            this.cbxTrainCode = new System.Windows.Forms.ComboBox();
            this.cbxLineDir = new System.Windows.Forms.ComboBox();
            this.cbxLineName = new System.Windows.Forms.ComboBox();
            this.gpbGeoSelect = new System.Windows.Forms.GroupBox();
            this.txtGeoPath = new System.Windows.Forms.TextBox();
            this.btnSelectGeoFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.panelIIC.SuspendLayout();
            this.gpbCitHeadEditer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectLineName)).BeginInit();
            this.gpbGeoSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelIIC
            // 
            this.panelIIC.Controls.Add(this.label1);
            this.panelIIC.Controls.Add(this.btnSelectIICFile);
            this.panelIIC.Controls.Add(this.txtIICFilePath);
            this.panelIIC.Enabled = false;
            this.panelIIC.Location = new System.Drawing.Point(16, 347);
            this.panelIIC.Name = "panelIIC";
            this.panelIIC.Size = new System.Drawing.Size(235, 62);
            this.panelIIC.TabIndex = 16;
            this.panelIIC.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "IIC文件：";
            // 
            // btnSelectIICFile
            // 
            this.btnSelectIICFile.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectIICFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelectIICFile.Location = new System.Drawing.Point(153, 5);
            this.btnSelectIICFile.Name = "btnSelectIICFile";
            this.btnSelectIICFile.Size = new System.Drawing.Size(75, 27);
            this.btnSelectIICFile.TabIndex = 10;
            this.btnSelectIICFile.Text = "浏览";
            this.btnSelectIICFile.UseVisualStyleBackColor = true;
            this.btnSelectIICFile.Click += new System.EventHandler(this.btnSelectIICFile_Click);
            // 
            // txtIICFilePath
            // 
            this.txtIICFilePath.ForeColor = System.Drawing.Color.Blue;
            this.txtIICFilePath.Location = new System.Drawing.Point(9, 32);
            this.txtIICFilePath.Name = "txtIICFilePath";
            this.txtIICFilePath.Size = new System.Drawing.Size(219, 21);
            this.txtIICFilePath.TabIndex = 11;
            // 
            // btnConvertToCIT
            // 
            this.btnConvertToCIT.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConvertToCIT.Location = new System.Drawing.Point(377, 347);
            this.btnConvertToCIT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConvertToCIT.Name = "btnConvertToCIT";
            this.btnConvertToCIT.Size = new System.Drawing.Size(134, 64);
            this.btnConvertToCIT.TabIndex = 15;
            this.btnConvertToCIT.Text = "转换为标准CIT文件";
            this.btnConvertToCIT.UseVisualStyleBackColor = true;
            this.btnConvertToCIT.Click += new System.EventHandler(this.btnConvertToCIT_Click);
            // 
            // gpbCitHeadEditer
            // 
            this.gpbCitHeadEditer.Controls.Add(this.picSelectLineName);
            this.gpbCitHeadEditer.Controls.Add(this.txtEndPos);
            this.gpbCitHeadEditer.Controls.Add(this.txtStartPos);
            this.gpbCitHeadEditer.Controls.Add(this.dateTimePickerTime);
            this.gpbCitHeadEditer.Controls.Add(this.dateTimePickerDate);
            this.gpbCitHeadEditer.Controls.Add(this.labKmInc);
            this.gpbCitHeadEditer.Controls.Add(this.labRunDir);
            this.gpbCitHeadEditer.Controls.Add(this.labEndPos);
            this.gpbCitHeadEditer.Controls.Add(this.labStartPos);
            this.gpbCitHeadEditer.Controls.Add(this.labTrainCode);
            this.gpbCitHeadEditer.Controls.Add(this.labTime);
            this.gpbCitHeadEditer.Controls.Add(this.labDate);
            this.gpbCitHeadEditer.Controls.Add(this.labLineDir);
            this.gpbCitHeadEditer.Controls.Add(this.labLineName);
            this.gpbCitHeadEditer.Controls.Add(this.cbxKmInc);
            this.gpbCitHeadEditer.Controls.Add(this.cbxRunDir);
            this.gpbCitHeadEditer.Controls.Add(this.cbxTrainCode);
            this.gpbCitHeadEditer.Controls.Add(this.cbxLineDir);
            this.gpbCitHeadEditer.Controls.Add(this.cbxLineName);
            this.gpbCitHeadEditer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbCitHeadEditer.Location = new System.Drawing.Point(17, 125);
            this.gpbCitHeadEditer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpbCitHeadEditer.Name = "gpbCitHeadEditer";
            this.gpbCitHeadEditer.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpbCitHeadEditer.Size = new System.Drawing.Size(495, 213);
            this.gpbCitHeadEditer.TabIndex = 14;
            this.gpbCitHeadEditer.TabStop = false;
            this.gpbCitHeadEditer.Text = "文件头编辑";
            // 
            // labLoading
            // 
            this.labLoading.Image = ((System.Drawing.Image)(resources.GetObject("labLoading.Image")));
            this.labLoading.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labLoading.Location = new System.Drawing.Point(281, 358);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(89, 42);
            this.labLoading.TabIndex = 22;
            this.labLoading.Text = "正在导出...";
            this.labLoading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labLoading.Visible = false;
            // 
            // picSelectLineName
            // 
            this.picSelectLineName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picSelectLineName.Image = ((System.Drawing.Image)(resources.GetObject("picSelectLineName.Image")));
            this.picSelectLineName.Location = new System.Drawing.Point(242, 31);
            this.picSelectLineName.Name = "picSelectLineName";
            this.picSelectLineName.Size = new System.Drawing.Size(16, 16);
            this.picSelectLineName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picSelectLineName.TabIndex = 9;
            this.picSelectLineName.TabStop = false;
            this.picSelectLineName.Click += new System.EventHandler(this.picSelectLineName_Click);
            // 
            // txtEndPos
            // 
            this.txtEndPos.Location = new System.Drawing.Point(349, 100);
            this.txtEndPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEndPos.Name = "txtEndPos";
            this.txtEndPos.Size = new System.Drawing.Size(122, 23);
            this.txtEndPos.TabIndex = 6;
            this.txtEndPos.Text = "0";
            this.txtEndPos.Leave += new System.EventHandler(this.txtMileage_Leave);
            // 
            // txtStartPos
            // 
            this.txtStartPos.Location = new System.Drawing.Point(349, 64);
            this.txtStartPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStartPos.Name = "txtStartPos";
            this.txtStartPos.Size = new System.Drawing.Size(122, 23);
            this.txtStartPos.TabIndex = 5;
            this.txtStartPos.Text = "0";
            this.txtStartPos.Leave += new System.EventHandler(this.txtMileage_Leave);
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.CustomFormat = "HH:mm:ss";
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTime.Location = new System.Drawing.Point(112, 138);
            this.dateTimePickerTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(122, 23);
            this.dateTimePickerTime.TabIndex = 3;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(112, 102);
            this.dateTimePickerDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.ShowUpDown = true;
            this.dateTimePickerDate.Size = new System.Drawing.Size(122, 23);
            this.dateTimePickerDate.TabIndex = 2;
            // 
            // labKmInc
            // 
            this.labKmInc.AutoSize = true;
            this.labKmInc.Location = new System.Drawing.Point(273, 177);
            this.labKmInc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labKmInc.Name = "labKmInc";
            this.labKmInc.Size = new System.Drawing.Size(56, 17);
            this.labKmInc.TabIndex = 1;
            this.labKmInc.Text = "增减里程";
            // 
            // labRunDir
            // 
            this.labRunDir.AutoSize = true;
            this.labRunDir.Location = new System.Drawing.Point(273, 139);
            this.labRunDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labRunDir.Name = "labRunDir";
            this.labRunDir.Size = new System.Drawing.Size(56, 17);
            this.labRunDir.TabIndex = 1;
            this.labRunDir.Text = "检测方向";
            // 
            // labEndPos
            // 
            this.labEndPos.AutoSize = true;
            this.labEndPos.Location = new System.Drawing.Point(273, 103);
            this.labEndPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labEndPos.Name = "labEndPos";
            this.labEndPos.Size = new System.Drawing.Size(56, 17);
            this.labEndPos.TabIndex = 1;
            this.labEndPos.Text = "结束里程";
            // 
            // labStartPos
            // 
            this.labStartPos.AutoSize = true;
            this.labStartPos.Location = new System.Drawing.Point(273, 67);
            this.labStartPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labStartPos.Name = "labStartPos";
            this.labStartPos.Size = new System.Drawing.Size(56, 17);
            this.labStartPos.TabIndex = 1;
            this.labStartPos.Text = "开始里程";
            // 
            // labTrainCode
            // 
            this.labTrainCode.AutoSize = true;
            this.labTrainCode.Location = new System.Drawing.Point(275, 29);
            this.labTrainCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTrainCode.Name = "labTrainCode";
            this.labTrainCode.Size = new System.Drawing.Size(56, 17);
            this.labTrainCode.TabIndex = 1;
            this.labTrainCode.Text = "检测车号";
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(36, 143);
            this.labTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(56, 17);
            this.labTime.TabIndex = 1;
            this.labTime.Text = "检测时间";
            // 
            // labDate
            // 
            this.labDate.AutoSize = true;
            this.labDate.Location = new System.Drawing.Point(36, 107);
            this.labDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(56, 17);
            this.labDate.TabIndex = 1;
            this.labDate.Text = "检测日期";
            // 
            // labLineDir
            // 
            this.labLineDir.AutoSize = true;
            this.labLineDir.Location = new System.Drawing.Point(64, 67);
            this.labLineDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labLineDir.Name = "labLineDir";
            this.labLineDir.Size = new System.Drawing.Size(32, 17);
            this.labLineDir.TabIndex = 1;
            this.labLineDir.Text = "行别";
            // 
            // labLineName
            // 
            this.labLineName.AutoSize = true;
            this.labLineName.Location = new System.Drawing.Point(36, 29);
            this.labLineName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labLineName.Name = "labLineName";
            this.labLineName.Size = new System.Drawing.Size(56, 17);
            this.labLineName.TabIndex = 1;
            this.labLineName.Text = "线路名称";
            // 
            // cbxKmInc
            // 
            this.cbxKmInc.FormattingEnabled = true;
            this.cbxKmInc.Items.AddRange(new object[] {
            "增",
            "减",
            "减变增"});
            this.cbxKmInc.Location = new System.Drawing.Point(349, 174);
            this.cbxKmInc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxKmInc.Name = "cbxKmInc";
            this.cbxKmInc.Size = new System.Drawing.Size(122, 25);
            this.cbxKmInc.TabIndex = 8;
            this.cbxKmInc.SelectedIndexChanged += new System.EventHandler(this.cbxKmInc_SelectedIndexChanged);
            // 
            // cbxRunDir
            // 
            this.cbxRunDir.FormattingEnabled = true;
            this.cbxRunDir.Items.AddRange(new object[] {
            "正",
            "反"});
            this.cbxRunDir.Location = new System.Drawing.Point(349, 136);
            this.cbxRunDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxRunDir.Name = "cbxRunDir";
            this.cbxRunDir.Size = new System.Drawing.Size(122, 25);
            this.cbxRunDir.TabIndex = 7;
            this.cbxRunDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxRunDir_SelectedIndexChanged);
            // 
            // cbxTrainCode
            // 
            this.cbxTrainCode.FormattingEnabled = true;
            this.cbxTrainCode.Location = new System.Drawing.Point(349, 26);
            this.cbxTrainCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxTrainCode.Name = "cbxTrainCode";
            this.cbxTrainCode.Size = new System.Drawing.Size(122, 25);
            this.cbxTrainCode.TabIndex = 4;
            // 
            // cbxLineDir
            // 
            this.cbxLineDir.FormattingEnabled = true;
            this.cbxLineDir.Items.AddRange(new object[] {
            "上",
            "下",
            "单"});
            this.cbxLineDir.Location = new System.Drawing.Point(112, 64);
            this.cbxLineDir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxLineDir.Name = "cbxLineDir";
            this.cbxLineDir.Size = new System.Drawing.Size(122, 25);
            this.cbxLineDir.TabIndex = 1;
            this.cbxLineDir.SelectedIndexChanged += new System.EventHandler(this.comboBoxRunDir_SelectedIndexChanged);
            // 
            // cbxLineName
            // 
            this.cbxLineName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxLineName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxLineName.FormattingEnabled = true;
            this.cbxLineName.Location = new System.Drawing.Point(112, 26);
            this.cbxLineName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxLineName.Name = "cbxLineName";
            this.cbxLineName.Size = new System.Drawing.Size(122, 25);
            this.cbxLineName.Sorted = true;
            this.cbxLineName.TabIndex = 0;
            this.cbxLineName.SelectedIndexChanged += new System.EventHandler(this.cbxLineName_SelectedIndexChanged);
            // 
            // gpbGeoSelect
            // 
            this.gpbGeoSelect.Controls.Add(this.txtGeoPath);
            this.gpbGeoSelect.Controls.Add(this.btnSelectGeoFile);
            this.gpbGeoSelect.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbGeoSelect.Location = new System.Drawing.Point(17, 10);
            this.gpbGeoSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpbGeoSelect.Name = "gpbGeoSelect";
            this.gpbGeoSelect.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpbGeoSelect.Size = new System.Drawing.Size(495, 104);
            this.gpbGeoSelect.TabIndex = 13;
            this.gpbGeoSelect.TabStop = false;
            this.gpbGeoSelect.Text = "选择GEO波形";
            // 
            // txtGeoPath
            // 
            this.txtGeoPath.Location = new System.Drawing.Point(8, 26);
            this.txtGeoPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGeoPath.Multiline = true;
            this.txtGeoPath.Name = "txtGeoPath";
            this.txtGeoPath.ReadOnly = true;
            this.txtGeoPath.Size = new System.Drawing.Size(376, 68);
            this.txtGeoPath.TabIndex = 1;
            // 
            // btnSelectGeoFile
            // 
            this.btnSelectGeoFile.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectGeoFile.Location = new System.Drawing.Point(404, 26);
            this.btnSelectGeoFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectGeoFile.Name = "btnSelectGeoFile";
            this.btnSelectGeoFile.Size = new System.Drawing.Size(78, 68);
            this.btnSelectGeoFile.TabIndex = 0;
            this.btnSelectGeoFile.Text = "打开波形";
            this.btnSelectGeoFile.UseVisualStyleBackColor = true;
            this.btnSelectGeoFile.Click += new System.EventHandler(this.btnSelectGeoFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(15, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "提示：如果未找到检测车号，请尝试GJ-6。";
            // 
            // Geo2CitConvertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 429);
            this.Controls.Add(this.labLoading);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelIIC);
            this.Controls.Add(this.btnConvertToCIT);
            this.Controls.Add(this.gpbCitHeadEditer);
            this.Controls.Add(this.gpbGeoSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Geo2CitConvertForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GEO转CIT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Geo2CitConvertForm_FormClosing);
            this.Load += new System.EventHandler(this.Geo2CitConvertForm_Load);
            this.panelIIC.ResumeLayout(false);
            this.panelIIC.PerformLayout();
            this.gpbCitHeadEditer.ResumeLayout(false);
            this.gpbCitHeadEditer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectLineName)).EndInit();
            this.gpbGeoSelect.ResumeLayout(false);
            this.gpbGeoSelect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelIIC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectIICFile;
        private System.Windows.Forms.TextBox txtIICFilePath;
        private System.Windows.Forms.Button btnConvertToCIT;
        private System.Windows.Forms.GroupBox gpbCitHeadEditer;
        private System.Windows.Forms.PictureBox picSelectLineName;
        private System.Windows.Forms.TextBox txtEndPos;
        private System.Windows.Forms.TextBox txtStartPos;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labKmInc;
        private System.Windows.Forms.Label labRunDir;
        private System.Windows.Forms.Label labEndPos;
        private System.Windows.Forms.Label labStartPos;
        private System.Windows.Forms.Label labTrainCode;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.Label labLineDir;
        private System.Windows.Forms.Label labLineName;
        private System.Windows.Forms.ComboBox cbxKmInc;
        private System.Windows.Forms.ComboBox cbxRunDir;
        private System.Windows.Forms.ComboBox cbxTrainCode;
        private System.Windows.Forms.ComboBox cbxLineDir;
        private System.Windows.Forms.ComboBox cbxLineName;
        private System.Windows.Forms.GroupBox gpbGeoSelect;
        private System.Windows.Forms.TextBox txtGeoPath;
        private System.Windows.Forms.Button btnSelectGeoFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labLoading;
        private System.Windows.Forms.Label label2;
    }
}