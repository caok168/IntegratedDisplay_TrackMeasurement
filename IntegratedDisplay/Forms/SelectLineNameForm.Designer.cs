namespace IntegratedDisplay.Forms
{
    partial class SelectLineNameForm
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
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnRemoveLineName = new System.Windows.Forms.Button();
            this.btnAddLineName = new System.Windows.Forms.Button();
            this.lstselectedLineName = new System.Windows.Forms.ListBox();
            this.lstallLineName = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(142, 27);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 12;
            this.btnFilter.Text = "筛选";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(44, 29);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(92, 21);
            this.txtKey.TabIndex = 11;
            this.txtKey.Text = "输入关键字";
            // 
            // btnRemoveLineName
            // 
            this.btnRemoveLineName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoveLineName.Location = new System.Drawing.Point(239, 258);
            this.btnRemoveLineName.Name = "btnRemoveLineName";
            this.btnRemoveLineName.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveLineName.TabIndex = 10;
            this.btnRemoveLineName.Text = "移除";
            this.btnRemoveLineName.UseVisualStyleBackColor = true;
            this.btnRemoveLineName.Click += new System.EventHandler(this.btnRemoveLineName_Click);
            // 
            // btnAddLineName
            // 
            this.btnAddLineName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddLineName.Location = new System.Drawing.Point(239, 169);
            this.btnAddLineName.Name = "btnAddLineName";
            this.btnAddLineName.Size = new System.Drawing.Size(75, 23);
            this.btnAddLineName.TabIndex = 9;
            this.btnAddLineName.Text = "添加";
            this.btnAddLineName.UseVisualStyleBackColor = true;
            this.btnAddLineName.Click += new System.EventHandler(this.btnAddLineName_Click);
            // 
            // lstselectedLineName
            // 
            this.lstselectedLineName.FormattingEnabled = true;
            this.lstselectedLineName.ItemHeight = 12;
            this.lstselectedLineName.Location = new System.Drawing.Point(334, 82);
            this.lstselectedLineName.Name = "lstselectedLineName";
            this.lstselectedLineName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstselectedLineName.Size = new System.Drawing.Size(173, 292);
            this.lstselectedLineName.TabIndex = 8;
            // 
            // lstallLineName
            // 
            this.lstallLineName.CheckOnClick = true;
            this.lstallLineName.FormattingEnabled = true;
            this.lstallLineName.HorizontalScrollbar = true;
            this.lstallLineName.Location = new System.Drawing.Point(44, 82);
            this.lstallLineName.Name = "lstallLineName";
            this.lstallLineName.Size = new System.Drawing.Size(173, 292);
            this.lstallLineName.Sorted = true;
            this.lstallLineName.TabIndex = 7;
            // 
            // SelectLineNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 420);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnRemoveLineName);
            this.Controls.Add(this.btnAddLineName);
            this.Controls.Add(this.lstselectedLineName);
            this.Controls.Add(this.lstallLineName);
            this.Name = "SelectLineNameForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectLineNameForm_FormClosing);
            this.Load += new System.EventHandler(this.SelectLineNameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnRemoveLineName;
        private System.Windows.Forms.Button btnAddLineName;
        private System.Windows.Forms.ListBox lstselectedLineName;
        private System.Windows.Forms.CheckedListBox lstallLineName;
    }
}