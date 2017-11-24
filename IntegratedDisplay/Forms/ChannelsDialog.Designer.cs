namespace IntegratedDisplay.Forms
{
    partial class ChannelsDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelsDialog));
            this.ColorChoiceDialog = new System.Windows.Forms.ColorDialog();
            this.ChannelsConfigDataGridView1 = new System.Windows.Forms.DataGridView();
            this.clnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnChName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnNoChName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnIsVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnLineWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnBaselinePostion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnIs = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnFlip = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LayerComboBox1 = new System.Windows.Forms.ComboBox();
            this.btnConfigSaveAs = new System.Windows.Forms.Button();
            this.SaveAsFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConfigLabel = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.buttonChannelAutoArange = new System.Windows.Forms.Button();
            this.btnSaveAsdefault = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusShow = new System.Windows.Forms.ToolStripStatusLabel();
            this.ckbAutoSave = new System.Windows.Forms.CheckBox();
            this.ckbIsShowHighlight = new System.Windows.Forms.CheckBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelsConfigDataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ColorChoiceDialog
            // 
            this.ColorChoiceDialog.AnyColor = true;
            this.ColorChoiceDialog.FullOpen = true;
            // 
            // ChannelsConfigDataGridView1
            // 
            this.ChannelsConfigDataGridView1.AllowUserToAddRows = false;
            this.ChannelsConfigDataGridView1.AllowUserToDeleteRows = false;
            this.ChannelsConfigDataGridView1.AllowUserToResizeRows = false;
            this.ChannelsConfigDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelsConfigDataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ChannelsConfigDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ChannelsConfigDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChannelsConfigDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnID,
            this.clnChannelName,
            this.clnUnit,
            this.clnChName,
            this.clnNoChName,
            this.clnColor,
            this.clnIsVisible,
            this.clnScale,
            this.clnLineWidth,
            this.clnBaselinePostion,
            this.clnIs,
            this.clnFlip});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ChannelsConfigDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.ChannelsConfigDataGridView1.Location = new System.Drawing.Point(3, 58);
            this.ChannelsConfigDataGridView1.Name = "ChannelsConfigDataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ChannelsConfigDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.ChannelsConfigDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.ChannelsConfigDataGridView1.RowTemplate.Height = 23;
            this.ChannelsConfigDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ChannelsConfigDataGridView1.Size = new System.Drawing.Size(909, 464);
            this.ChannelsConfigDataGridView1.TabIndex = 30;
            this.ChannelsConfigDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChannelsConfigDataGridView1_CellContentClick);
            this.ChannelsConfigDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChannelsConfigDataGridView1_CellDoubleClick);
            this.ChannelsConfigDataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChannelsConfigDataGridView1_CellEndEdit);
            this.ChannelsConfigDataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ChannelsConfigDataGridView1_CellMouseClick);
            // 
            // clnID
            // 
            this.clnID.HeaderText = "通道ID";
            this.clnID.Name = "clnID";
            this.clnID.ReadOnly = true;
            this.clnID.Width = 80;
            // 
            // clnChannelName
            // 
            this.clnChannelName.HeaderText = "原始通道名";
            this.clnChannelName.Name = "clnChannelName";
            this.clnChannelName.ReadOnly = true;
            this.clnChannelName.Visible = false;
            this.clnChannelName.Width = 110;
            // 
            // clnUnit
            // 
            this.clnUnit.HeaderText = "单位";
            this.clnUnit.Name = "clnUnit";
            this.clnUnit.ReadOnly = true;
            this.clnUnit.Visible = false;
            this.clnUnit.Width = 80;
            // 
            // clnChName
            // 
            this.clnChName.HeaderText = "通道名称";
            this.clnChName.Name = "clnChName";
            this.clnChName.Width = 120;
            // 
            // clnNoChName
            // 
            this.clnNoChName.HeaderText = "显示非中文通道名";
            this.clnNoChName.Name = "clnNoChName";
            this.clnNoChName.Visible = false;
            this.clnNoChName.Width = 130;
            // 
            // clnColor
            // 
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            this.clnColor.DefaultCellStyle = dataGridViewCellStyle2;
            this.clnColor.HeaderText = "颜色";
            this.clnColor.Name = "clnColor";
            this.clnColor.ReadOnly = true;
            this.clnColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clnColor.Width = 60;
            // 
            // clnIsVisible
            // 
            this.clnIsVisible.HeaderText = "是否显示";
            this.clnIsVisible.Name = "clnIsVisible";
            this.clnIsVisible.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clnIsVisible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clnIsVisible.Width = 80;
            // 
            // clnScale
            // 
            this.clnScale.HeaderText = "比例";
            this.clnScale.Name = "clnScale";
            this.clnScale.Width = 60;
            // 
            // clnLineWidth
            // 
            this.clnLineWidth.HeaderText = "线宽";
            this.clnLineWidth.Name = "clnLineWidth";
            this.clnLineWidth.Width = 60;
            // 
            // clnBaselinePostion
            // 
            this.clnBaselinePostion.HeaderText = "基线位置";
            this.clnBaselinePostion.Name = "clnBaselinePostion";
            this.clnBaselinePostion.Width = 80;
            // 
            // clnIs
            // 
            this.clnIs.HeaderText = "包含偏移值";
            this.clnIs.Name = "clnIs";
            this.clnIs.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clnIs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // clnFlip
            // 
            this.clnFlip.HeaderText = "波形上下反转";
            this.clnFlip.Name = "clnFlip";
            // 
            // LayerComboBox1
            // 
            this.LayerComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LayerComboBox1.ItemHeight = 12;
            this.LayerComboBox1.Location = new System.Drawing.Point(65, 11);
            this.LayerComboBox1.Name = "LayerComboBox1";
            this.LayerComboBox1.Size = new System.Drawing.Size(841, 20);
            this.LayerComboBox1.TabIndex = 31;
            this.LayerComboBox1.SelectedIndexChanged += new System.EventHandler(this.LayerComboBox1_SelectedIndexChanged);
            // 
            // btnConfigSaveAs
            // 
            this.btnConfigSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfigSaveAs.Enabled = false;
            this.btnConfigSaveAs.Location = new System.Drawing.Point(148, 531);
            this.btnConfigSaveAs.Name = "btnConfigSaveAs";
            this.btnConfigSaveAs.Size = new System.Drawing.Size(110, 23);
            this.btnConfigSaveAs.TabIndex = 34;
            this.btnConfigSaveAs.Text = "配置另存为(&A)...";
            this.btnConfigSaveAs.UseVisualStyleBackColor = true;
            this.btnConfigSaveAs.Click += new System.EventHandler(this.SaveAsButton1_Click);
            // 
            // SaveAsFileDialog1
            // 
            this.SaveAsFileDialog1.DefaultExt = "xml";
            this.SaveAsFileDialog1.Filter = "通道配置文件|*.xml";
            this.SaveAsFileDialog1.InitialDirectory = "Desktop";
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadConfig.Enabled = false;
            this.btnLoadConfig.Location = new System.Drawing.Point(12, 531);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(110, 23);
            this.btnLoadConfig.TabIndex = 35;
            this.btnLoadConfig.Text = "加载配置(&O)...";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.OpenButton1_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.DefaultExt = "xml";
            this.OpenFileDialog1.Filter = "XML文件|*.xml";
            this.OpenFileDialog1.InitialDirectory = "Desktop";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 23);
            this.label1.TabIndex = 37;
            this.label1.Text = "波形名称";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 23);
            this.label2.TabIndex = 38;
            this.label2.Text = "配置文件";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ConfigLabel
            // 
            this.ConfigLabel.Location = new System.Drawing.Point(63, 34);
            this.ConfigLabel.Name = "ConfigLabel";
            this.ConfigLabel.Size = new System.Drawing.Size(513, 18);
            this.ConfigLabel.TabIndex = 39;
            this.ConfigLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveConfig.Location = new System.Drawing.Point(530, 534);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(125, 23);
            this.btnSaveConfig.TabIndex = 40;
            this.btnSaveConfig.Text = "保存当前配置(&D)...";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Visible = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.buttonSaveAsDefault_Click);
            // 
            // buttonChannelAutoArange
            // 
            this.buttonChannelAutoArange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonChannelAutoArange.Location = new System.Drawing.Point(811, 534);
            this.buttonChannelAutoArange.Name = "buttonChannelAutoArange";
            this.buttonChannelAutoArange.Size = new System.Drawing.Size(88, 23);
            this.buttonChannelAutoArange.TabIndex = 41;
            this.buttonChannelAutoArange.Text = "自动排列";
            this.buttonChannelAutoArange.UseVisualStyleBackColor = true;
            this.buttonChannelAutoArange.Visible = false;
            this.buttonChannelAutoArange.Click += new System.EventHandler(this.buttonChannelAutoArange_Click);
            // 
            // btnSaveAsdefault
            // 
            this.btnSaveAsdefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAsdefault.Location = new System.Drawing.Point(273, 531);
            this.btnSaveAsdefault.Name = "btnSaveAsdefault";
            this.btnSaveAsdefault.Size = new System.Drawing.Size(116, 23);
            this.btnSaveAsdefault.TabIndex = 42;
            this.btnSaveAsdefault.Text = "设为默认配置(&D)...";
            this.btnSaveAsdefault.UseVisualStyleBackColor = true;
            this.btnSaveAsdefault.Click += new System.EventHandler(this.buttonNewDefaultConfig_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsStatusShow});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 566);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(915, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 43;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "当前状态：";
            // 
            // tsStatusShow
            // 
            this.tsStatusShow.AutoSize = false;
            this.tsStatusShow.ForeColor = System.Drawing.Color.DodgerBlue;
            this.tsStatusShow.LinkVisited = true;
            this.tsStatusShow.Name = "tsStatusShow";
            this.tsStatusShow.Size = new System.Drawing.Size(500, 17);
            this.tsStatusShow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbAutoSave
            // 
            this.ckbAutoSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbAutoSave.AutoSize = true;
            this.ckbAutoSave.Location = new System.Drawing.Point(661, 538);
            this.ckbAutoSave.Name = "ckbAutoSave";
            this.ckbAutoSave.Size = new System.Drawing.Size(144, 16);
            this.ckbAutoSave.TabIndex = 44;
            this.ckbAutoSave.Text = "自动保存当前通道配置";
            this.ckbAutoSave.UseVisualStyleBackColor = true;
            this.ckbAutoSave.Visible = false;
            this.ckbAutoSave.CheckedChanged += new System.EventHandler(this.ckbAutoSave_CheckedChanged);
            // 
            // ckbIsShowHighlight
            // 
            this.ckbIsShowHighlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbIsShowHighlight.AutoSize = true;
            this.ckbIsShowHighlight.Location = new System.Drawing.Point(419, 535);
            this.ckbIsShowHighlight.Name = "ckbIsShowHighlight";
            this.ckbIsShowHighlight.Size = new System.Drawing.Size(72, 16);
            this.ckbIsShowHighlight.TabIndex = 46;
            this.ckbIsShowHighlight.Text = "高亮选中";
            this.ckbIsShowHighlight.UseVisualStyleBackColor = true;
            this.ckbIsShowHighlight.CheckedChanged += new System.EventHandler(this.ckbIsShowHighlight_CheckedChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "原始通道名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "单位";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "显示中文通道名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "显示非中文通道名";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Transparent;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn6.HeaderText = "颜色";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.Width = 60;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "是否显示";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "比例";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "基线位置";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "线宽";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 60;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "包含偏移值";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.HeaderText = "波形上下反转";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            // 
            // ChannelsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(217)))), ((int)(((byte)(222)))));
            this.ClientSize = new System.Drawing.Size(915, 588);
            this.Controls.Add(this.ckbIsShowHighlight);
            this.Controls.Add(this.ckbAutoSave);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSaveAsdefault);
            this.Controls.Add(this.buttonChannelAutoArange);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.ConfigLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.btnConfigSaveAs);
            this.Controls.Add(this.LayerComboBox1);
            this.Controls.Add(this.ChannelsConfigDataGridView1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChannelsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "波形数据通道配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelsDialog_FormClosing);
            this.Load += new System.EventHandler(this.ChannelsDialog_Load);
            this.VisibleChanged += new System.EventHandler(this.ChannelsDialog_VisibleChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChannelsDialog_MouseClick);
            this.Resize += new System.EventHandler(this.ChannelsDialog_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ChannelsConfigDataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog ColorChoiceDialog;
        private System.Windows.Forms.DataGridView ChannelsConfigDataGridView1;
        private System.Windows.Forms.ComboBox LayerComboBox1;
        private System.Windows.Forms.Button btnConfigSaveAs;
        private System.Windows.Forms.SaveFileDialog SaveAsFileDialog1;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ConfigLabel;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button buttonChannelAutoArange;
        private System.Windows.Forms.Button btnSaveAsdefault;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusShow;
        private System.Windows.Forms.CheckBox ckbAutoSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.CheckBox ckbIsShowHighlight;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnChannelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnChName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnNoChName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnColor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIsVisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnLineWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnBaselinePostion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnIs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnFlip;
    }
}