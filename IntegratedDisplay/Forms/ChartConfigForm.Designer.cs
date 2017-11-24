namespace IntegratedDisplay.Forms
{
    partial class ChartConfigForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudAxesXMin = new System.Windows.Forms.NumericUpDown();
            this.nudAxesYMin = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAxesXFont = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.btnAxesYFont = new System.Windows.Forms.Button();
            this.labFontAxesX = new System.Windows.Forms.Label();
            this.btnTiltleFont = new System.Windows.Forms.Button();
            this.labFontAxesY = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labFontTitle = new System.Windows.Forms.Label();
            this.txtAxesX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAxesY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudAxesYStep = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudAxesXStep = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesXMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesYMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesYStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesXStep)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudAxesXStep);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.nudAxesYStep);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.nudAxesXMin);
            this.panel1.Controls.Add(this.nudAxesYMin);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnAxesXFont);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.btnAxesYFont);
            this.panel1.Controls.Add(this.labFontAxesX);
            this.panel1.Controls.Add(this.btnTiltleFont);
            this.panel1.Controls.Add(this.labFontAxesY);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.labFontTitle);
            this.panel1.Controls.Add(this.txtAxesX);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtAxesY);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 368);
            this.panel1.TabIndex = 1;
            // 
            // nudAxesXMin
            // 
            this.nudAxesXMin.Location = new System.Drawing.Point(101, 284);
            this.nudAxesXMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAxesXMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudAxesXMin.Name = "nudAxesXMin";
            this.nudAxesXMin.Size = new System.Drawing.Size(65, 21);
            this.nudAxesXMin.TabIndex = 9;
            // 
            // nudAxesYMin
            // 
            this.nudAxesYMin.Location = new System.Drawing.Point(101, 178);
            this.nudAxesYMin.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAxesYMin.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudAxesYMin.Name = "nudAxesYMin";
            this.nudAxesYMin.Size = new System.Drawing.Size(65, 21);
            this.nudAxesYMin.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(42, 289);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "最小值：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "最小值：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "横坐标：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "纵坐标：";
            // 
            // btnAxesXFont
            // 
            this.btnAxesXFont.Location = new System.Drawing.Point(338, 254);
            this.btnAxesXFont.Name = "btnAxesXFont";
            this.btnAxesXFont.Size = new System.Drawing.Size(75, 23);
            this.btnAxesXFont.TabIndex = 5;
            this.btnAxesXFont.Text = "选择字体";
            this.btnAxesXFont.UseVisualStyleBackColor = true;
            this.btnAxesXFont.Click += new System.EventHandler(this.btnAxesXFont_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(338, 333);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "应用";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // btnAxesYFont
            // 
            this.btnAxesYFont.Location = new System.Drawing.Point(338, 143);
            this.btnAxesYFont.Name = "btnAxesYFont";
            this.btnAxesYFont.Size = new System.Drawing.Size(75, 23);
            this.btnAxesYFont.TabIndex = 5;
            this.btnAxesYFont.Text = "选择字体";
            this.btnAxesYFont.UseVisualStyleBackColor = true;
            this.btnAxesYFont.Click += new System.EventHandler(this.btnAxesYFont_Click);
            // 
            // labFontAxesX
            // 
            this.labFontAxesX.AutoSize = true;
            this.labFontAxesX.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFontAxesX.Location = new System.Drawing.Point(84, 256);
            this.labFontAxesX.Name = "labFontAxesX";
            this.labFontAxesX.Size = new System.Drawing.Size(131, 12);
            this.labFontAxesX.TabIndex = 4;
            this.labFontAxesX.Text = "                     ";
            // 
            // btnTiltleFont
            // 
            this.btnTiltleFont.Location = new System.Drawing.Point(338, 72);
            this.btnTiltleFont.Name = "btnTiltleFont";
            this.btnTiltleFont.Size = new System.Drawing.Size(75, 23);
            this.btnTiltleFont.TabIndex = 5;
            this.btnTiltleFont.Text = "选择字体";
            this.btnTiltleFont.UseVisualStyleBackColor = true;
            this.btnTiltleFont.Click += new System.EventHandler(this.btnTiltleFont_Click);
            // 
            // labFontAxesY
            // 
            this.labFontAxesY.AutoSize = true;
            this.labFontAxesY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFontAxesY.Location = new System.Drawing.Point(84, 150);
            this.labFontAxesY.Name = "labFontAxesY";
            this.labFontAxesY.Size = new System.Drawing.Size(131, 12);
            this.labFontAxesY.TabIndex = 4;
            this.labFontAxesY.Text = "                     ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 256);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "字体：";
            // 
            // labFontTitle
            // 
            this.labFontTitle.AutoSize = true;
            this.labFontTitle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFontTitle.Location = new System.Drawing.Point(84, 72);
            this.labFontTitle.Name = "labFontTitle";
            this.labFontTitle.Size = new System.Drawing.Size(131, 12);
            this.labFontTitle.TabIndex = 4;
            this.labFontTitle.Text = "                     ";
            // 
            // txtAxesX
            // 
            this.txtAxesX.Location = new System.Drawing.Point(84, 222);
            this.txtAxesX.Name = "txtAxesX";
            this.txtAxesX.Size = new System.Drawing.Size(329, 21);
            this.txtAxesX.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "字体：";
            // 
            // txtAxesY
            // 
            this.txtAxesY.Location = new System.Drawing.Point(84, 116);
            this.txtAxesY.Name = "txtAxesY";
            this.txtAxesY.Size = new System.Drawing.Size(329, 21);
            this.txtAxesY.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "字体：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "名称：";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(84, 38);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(329, 21);
            this.txtTitle.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题：";
            // 
            // nudAxesYStep
            // 
            this.nudAxesYStep.Location = new System.Drawing.Point(225, 178);
            this.nudAxesYStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAxesYStep.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudAxesYStep.Name = "nudAxesYStep";
            this.nudAxesYStep.Size = new System.Drawing.Size(65, 21);
            this.nudAxesYStep.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(178, 182);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "步长：";
            // 
            // nudAxesXStep
            // 
            this.nudAxesXStep.Location = new System.Drawing.Point(225, 284);
            this.nudAxesXStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAxesXStep.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.nudAxesXStep.Name = "nudAxesXStep";
            this.nudAxesXStep.Size = new System.Drawing.Size(65, 21);
            this.nudAxesXStep.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(178, 288);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "步长：";
            // 
            // ChartConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 368);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "绘图选项";
            this.Load += new System.EventHandler(this.ChartConfigForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesXMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesYMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesYStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAxesXStep)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudAxesXStep;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudAxesYStep;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudAxesXMin;
        private System.Windows.Forms.NumericUpDown nudAxesYMin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAxesXFont;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button btnAxesYFont;
        private System.Windows.Forms.Label labFontAxesX;
        private System.Windows.Forms.Button btnTiltleFont;
        private System.Windows.Forms.Label labFontAxesY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labFontTitle;
        private System.Windows.Forms.TextBox txtAxesX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAxesY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FontDialog fontDialog;
    }
}