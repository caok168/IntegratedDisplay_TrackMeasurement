namespace IntegratedDisplay.Forms
{
    partial class MileageLocationForm
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
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLocation = new System.Windows.Forms.Button();
            this.labMileage = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtMileage = new IntegratedDisplay.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnBack.Location = new System.Drawing.Point(169, 58);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "返回(&R)";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLocation
            // 
            this.btnLocation.Location = new System.Drawing.Point(44, 58);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(75, 23);
            this.btnLocation.TabIndex = 6;
            this.btnLocation.Text = "定位(&P)";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // labMileage
            // 
            this.labMileage.AutoSize = true;
            this.labMileage.Location = new System.Drawing.Point(16, 17);
            this.labMileage.Name = "labMileage";
            this.labMileage.Size = new System.Drawing.Size(77, 12);
            this.labMileage.TabIndex = 4;
            this.labMileage.Text = "请输入里程：";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtMileage
            // 
            this.txtMileage.Location = new System.Drawing.Point(99, 12);
            this.txtMileage.Name = "txtMileage";
            this.txtMileage.Size = new System.Drawing.Size(156, 21);
            this.txtMileage.TabIndex = 5;
            this.txtMileage.WaterText = "请输入一个里程(单位:Km)";
            this.txtMileage.TextChanged += new System.EventHandler(this.txtMileage_TextChanged);
            this.txtMileage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMileage_KeyDown);
            this.txtMileage.Leave += new System.EventHandler(this.txtMileage_Leave);
            // 
            // MileageLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 92);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.txtMileage);
            this.Controls.Add(this.labMileage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MileageLocationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "里程定位";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnLocation;
        private CustomControl.CustomTextBox txtMileage;
        private System.Windows.Forms.Label labMileage;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}