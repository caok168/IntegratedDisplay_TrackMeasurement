namespace IntegratedDisplay.Forms
{
    partial class ToolstipConfigForm
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.listBoxAll = new DevComponents.DotNetBar.ListBoxAdv();
            this.listBoxCustom = new DevComponents.DotNetBar.ListBoxAdv();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(174, 95);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加>>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(174, 156);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "<<移除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // listBoxAll
            // 
            this.listBoxAll.AutoScroll = true;
            // 
            // 
            // 
            this.listBoxAll.BackgroundStyle.Class = "ListBoxAdv";
            this.listBoxAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listBoxAll.CheckStateMember = null;
            this.listBoxAll.ContainerControlProcessDialogKey = true;
            this.listBoxAll.DragDropSupport = true;
            this.listBoxAll.Location = new System.Drawing.Point(12, 12);
            this.listBoxAll.Name = "listBoxAll";
            this.listBoxAll.SelectionMode = DevComponents.DotNetBar.eSelectionMode.MultiExtended;
            this.listBoxAll.Size = new System.Drawing.Size(150, 312);
            this.listBoxAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.listBoxAll.TabIndex = 7;
            // 
            // listBoxCustom
            // 
            this.listBoxCustom.AutoScroll = true;
            // 
            // 
            // 
            this.listBoxCustom.BackgroundStyle.Class = "ListBoxAdv";
            this.listBoxCustom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listBoxCustom.CheckStateMember = null;
            this.listBoxCustom.ContainerControlProcessDialogKey = true;
            this.listBoxCustom.DragDropSupport = true;
            this.listBoxCustom.EnableDragDrop = true;
            this.listBoxCustom.Location = new System.Drawing.Point(255, 12);
            this.listBoxCustom.Name = "listBoxCustom";
            this.listBoxCustom.SelectionMode = DevComponents.DotNetBar.eSelectionMode.MultiExtended;
            this.listBoxCustom.Size = new System.Drawing.Size(150, 312);
            this.listBoxCustom.TabIndex = 8;
            // 
            // ToolstipConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 343);
            this.Controls.Add(this.listBoxCustom);
            this.Controls.Add(this.listBoxAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolstipConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义工具栏";
            this.Load += new System.EventHandler(this.ToolstipConfigForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private DevComponents.DotNetBar.ListBoxAdv listBoxAll;
        private DevComponents.DotNetBar.ListBoxAdv listBoxCustom;
    }
}