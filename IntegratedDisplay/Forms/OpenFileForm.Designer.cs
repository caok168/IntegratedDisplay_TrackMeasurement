namespace IntegratedDisplay
{
    partial class OpenFileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFileForm));
            this.slidePanel1 = new DevComponents.DotNetBar.Controls.SlidePanel();
            this.DockManager = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite9 = new DevComponents.DotNetBar.DockSite();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.clnLineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnLineCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnUpDown = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnMlieageIOD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnStartMileage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnEndMileage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnTotalMileage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnTIme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnCarNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnOriginalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnOriginalPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clnClose = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.labLoading = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOpenFiles = new System.Windows.Forms.Button();
            this.ckbLoadIndex = new System.Windows.Forms.CheckBox();
            this.dockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.barMeida = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.treePathAndMedia = new DevComponents.AdvTree.AdvTree();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dockSite9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.panelDockContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.dockSite1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barMeida)).BeginInit();
            this.barMeida.SuspendLayout();
            this.panelDockContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treePathAndMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // slidePanel1
            // 
            this.slidePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slidePanel1.Location = new System.Drawing.Point(0, 0);
            this.slidePanel1.Name = "slidePanel1";
            this.slidePanel1.Size = new System.Drawing.Size(900, 441);
            this.slidePanel1.TabIndex = 48;
            this.slidePanel1.Text = "slidePanel1";
            this.slidePanel1.UsesBlockingAnimation = false;
            // 
            // DockManager
            // 
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.DockManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.DockManager.BottomDockSite = this.dockSite4;
            this.DockManager.EnableFullSizeDock = false;
            this.DockManager.FillDockSite = this.dockSite9;
            this.DockManager.LeftDockSite = this.dockSite1;
            this.DockManager.ParentForm = this;
            this.DockManager.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.Fade;
            this.DockManager.RightDockSite = this.dockSite2;
            this.DockManager.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DockManager.ToolbarBottomDockSite = this.dockSite8;
            this.DockManager.ToolbarLeftDockSite = this.dockSite5;
            this.DockManager.ToolbarRightDockSite = this.dockSite6;
            this.DockManager.ToolbarTopDockSite = this.dockSite7;
            this.DockManager.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(0, 441);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(900, 0);
            this.dockSite4.TabIndex = 52;
            this.dockSite4.TabStop = false;
            // 
            // dockSite9
            // 
            this.dockSite9.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite9.Controls.Add(this.bar2);
            this.dockSite9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockSite9.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar2, 900, 141)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite9.Location = new System.Drawing.Point(0, 300);
            this.dockSite9.Name = "dockSite9";
            this.dockSite9.Size = new System.Drawing.Size(900, 141);
            this.dockSite9.TabIndex = 57;
            this.dockSite9.TabStop = false;
            // 
            // bar2
            // 
            this.bar2.AccessibleDescription = "DotNetBar Bar (bar2)";
            this.bar2.AccessibleName = "DotNetBar Bar";
            this.bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.bar2.BackColor = System.Drawing.SystemColors.Control;
            this.bar2.CanDockBottom = false;
            this.bar2.CanDockLeft = false;
            this.bar2.CanDockRight = false;
            this.bar2.CanDockTab = false;
            this.bar2.CanDockTop = false;
            this.bar2.CanMove = false;
            this.bar2.CanReorderTabs = false;
            this.bar2.CanUndock = false;
            this.bar2.Controls.Add(this.panelDockContainer2);
            this.bar2.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.IsMaximized = false;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem2});
            this.bar2.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(900, 141);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 0;
            this.bar2.TabStop = false;
            // 
            // panelDockContainer2
            // 
            this.panelDockContainer2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockContainer2.Controls.Add(this.panel1);
            this.panelDockContainer2.Controls.Add(this.panel2);
            this.panelDockContainer2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer2.Location = new System.Drawing.Point(3, 3);
            this.panelDockContainer2.Name = "panelDockContainer2";
            this.panelDockContainer2.Size = new System.Drawing.Size(894, 135);
            this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer2.Style.GradientAngle = 90;
            this.panelDockContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewFiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 83);
            this.panel1.TabIndex = 42;
            // 
            // listViewFiles
            // 
            this.listViewFiles.CheckBoxes = true;
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clnLineName,
            this.clnLineCode,
            this.clnUpDown,
            this.clnDirection,
            this.clnMlieageIOD,
            this.clnStartMileage,
            this.clnEndMileage,
            this.clnTotalMileage,
            this.clnDate,
            this.clnTIme,
            this.clnCarNo,
            this.clnOriginalName,
            this.clnSize,
            this.clnOriginalPath,
            this.clnClose});
            this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.Location = new System.Drawing.Point(0, 0);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.ShowItemToolTips = true;
            this.listViewFiles.Size = new System.Drawing.Size(894, 83);
            this.listViewFiles.TabIndex = 37;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SizeChanged += new System.EventHandler(this.listViewFiles_SizeChanged);
            this.listViewFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewFiles_MouseClick);
            this.listViewFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFiles_MouseDoubleClick);
            this.listViewFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewFiles_MouseDown);
            // 
            // clnLineName
            // 
            this.clnLineName.Text = "线路名";
            this.clnLineName.Width = 78;
            // 
            // clnLineCode
            // 
            this.clnLineCode.Text = "线路编号";
            // 
            // clnUpDown
            // 
            this.clnUpDown.Text = "行别";
            this.clnUpDown.Width = 50;
            // 
            // clnDirection
            // 
            this.clnDirection.Text = "方向";
            this.clnDirection.Width = 50;
            // 
            // clnMlieageIOD
            // 
            this.clnMlieageIOD.Text = "增减里程";
            // 
            // clnStartMileage
            // 
            this.clnStartMileage.Text = "起始里程(Km)";
            this.clnStartMileage.Width = 90;
            // 
            // clnEndMileage
            // 
            this.clnEndMileage.Text = "终止里程(Km)";
            this.clnEndMileage.Width = 90;
            // 
            // clnTotalMileage
            // 
            this.clnTotalMileage.Text = "总里程(Km)";
            this.clnTotalMileage.Width = 78;
            // 
            // clnDate
            // 
            this.clnDate.Text = "检测日期";
            this.clnDate.Width = 75;
            // 
            // clnTIme
            // 
            this.clnTIme.Text = "检测时间";
            this.clnTIme.Width = 75;
            // 
            // clnCarNo
            // 
            this.clnCarNo.Text = "检测车号";
            this.clnCarNo.Width = 78;
            // 
            // clnOriginalName
            // 
            this.clnOriginalName.Text = "原始文件名";
            this.clnOriginalName.Width = 100;
            // 
            // clnSize
            // 
            this.clnSize.Text = "大小(B)";
            this.clnSize.Width = 100;
            // 
            // clnOriginalPath
            // 
            this.clnOriginalPath.Text = "原始路径";
            this.clnOriginalPath.Width = 100;
            // 
            // clnClose
            // 
            this.clnClose.Text = "从列表中移除";
            this.clnClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clnClose.Width = 100;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labLoading);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOpenFiles);
            this.panel2.Controls.Add(this.ckbLoadIndex);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 52);
            this.panel2.TabIndex = 41;
            // 
            // labLoading
            // 
            this.labLoading.Image = ((System.Drawing.Image)(resources.GetObject("labLoading.Image")));
            this.labLoading.Location = new System.Drawing.Point(302, 8);
            this.labLoading.Name = "labLoading";
            this.labLoading.Size = new System.Drawing.Size(33, 24);
            this.labLoading.TabIndex = 42;
            this.labLoading.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(801, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOpenFiles
            // 
            this.btnOpenFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFiles.Location = new System.Drawing.Point(701, 8);
            this.btnOpenFiles.Name = "btnOpenFiles";
            this.btnOpenFiles.Size = new System.Drawing.Size(75, 30);
            this.btnOpenFiles.TabIndex = 39;
            this.btnOpenFiles.Text = "打开(&O)";
            this.btnOpenFiles.UseVisualStyleBackColor = true;
            this.btnOpenFiles.Click += new System.EventHandler(this.btnOpenFiles_Click);
            // 
            // ckbLoadIndex
            // 
            this.ckbLoadIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbLoadIndex.AutoSize = true;
            this.ckbLoadIndex.Location = new System.Drawing.Point(557, 16);
            this.ckbLoadIndex.Name = "ckbLoadIndex";
            this.ckbLoadIndex.Size = new System.Drawing.Size(120, 16);
            this.ckbLoadIndex.TabIndex = 40;
            this.ckbLoadIndex.Text = "加载里程校正数据";
            this.ckbLoadIndex.UseVisualStyleBackColor = true;
            // 
            // dockContainerItem2
            // 
            this.dockContainerItem2.Control = this.panelDockContainer2;
            this.dockContainerItem2.Name = "dockContainerItem2";
            this.dockContainerItem2.Text = "选择文件";
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Controls.Add(this.barMeida);
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.barMeida, 900, 297)))}, DevComponents.DotNetBar.eOrientation.Horizontal);
            this.dockSite1.Location = new System.Drawing.Point(0, 0);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(900, 300);
            this.dockSite1.TabIndex = 49;
            this.dockSite1.TabStop = false;
            // 
            // barMeida
            // 
            this.barMeida.AccessibleDescription = "DotNetBar Bar (barMeida)";
            this.barMeida.AccessibleName = "DotNetBar Bar";
            this.barMeida.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.barMeida.CanDockBottom = false;
            this.barMeida.CanDockRight = false;
            this.barMeida.CanDockTab = false;
            this.barMeida.CanDockTop = false;
            this.barMeida.CanMove = false;
            this.barMeida.CanUndock = false;
            this.barMeida.CloseSingleTab = true;
            this.barMeida.Controls.Add(this.panelDockContainer1);
            this.barMeida.Dock = System.Windows.Forms.DockStyle.Top;
            this.barMeida.DockedBorderStyle = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.barMeida.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.barMeida.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.barMeida.IsMaximized = false;
            this.barMeida.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem1});
            this.barMeida.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.barMeida.Location = new System.Drawing.Point(0, 0);
            this.barMeida.Name = "barMeida";
            this.barMeida.Size = new System.Drawing.Size(900, 297);
            this.barMeida.Stretch = true;
            this.barMeida.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barMeida.TabIndex = 0;
            this.barMeida.TabStop = false;
            this.barMeida.Text = "波形库及文件夹";
            // 
            // panelDockContainer1
            // 
            this.panelDockContainer1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelDockContainer1.Controls.Add(this.treePathAndMedia);
            this.panelDockContainer1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelDockContainer1.Location = new System.Drawing.Point(6, 26);
            this.panelDockContainer1.Name = "panelDockContainer1";
            this.panelDockContainer1.Size = new System.Drawing.Size(888, 265);
            this.panelDockContainer1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer1.Style.GradientAngle = 90;
            this.panelDockContainer1.TabIndex = 0;
            // 
            // treePathAndMedia
            // 
            this.treePathAndMedia.AllowDrop = false;
            this.treePathAndMedia.AllowExternalDrop = false;
            this.treePathAndMedia.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treePathAndMedia.BackgroundStyle.Class = "TreeBorderKey";
            this.treePathAndMedia.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treePathAndMedia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePathAndMedia.ExpandButtonType = DevComponents.AdvTree.eExpandButtonType.Triangle;
            this.treePathAndMedia.ExpandWidth = 14;
            this.treePathAndMedia.HotTracking = true;
            this.treePathAndMedia.Location = new System.Drawing.Point(0, 0);
            this.treePathAndMedia.Margin = new System.Windows.Forms.Padding(8);
            this.treePathAndMedia.Name = "treePathAndMedia";
            this.treePathAndMedia.NodeSpacing = 10;
            this.treePathAndMedia.NodeStyle = this.elementStyle1;
            this.treePathAndMedia.PathSeparator = ";";
            this.treePathAndMedia.Size = new System.Drawing.Size(888, 265);
            this.treePathAndMedia.Styles.Add(this.elementStyle1);
            this.treePathAndMedia.TabIndex = 5;
            this.treePathAndMedia.Text = "advTree1";
            this.treePathAndMedia.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.treePathAndMedia_NodeClick);
            this.treePathAndMedia.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.treePathAndMedia_PreviewKeyDown);
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Control = this.panelDockContainer1;
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "dockContainerItem1";
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Location = new System.Drawing.Point(900, 300);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 141);
            this.dockSite2.TabIndex = 50;
            this.dockSite2.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(0, 441);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(900, 0);
            this.dockSite8.TabIndex = 56;
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(0, 0);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 441);
            this.dockSite5.TabIndex = 53;
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(900, 0);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 441);
            this.dockSite6.TabIndex = 54;
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(0, 0);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(900, 0);
            this.dockSite7.TabIndex = 55;
            this.dockSite7.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(0, 0);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(900, 0);
            this.dockSite3.TabIndex = 51;
            this.dockSite3.TabStop = false;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "blue-loading.gif");
            // 
            // OpenFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 441);
            this.Controls.Add(this.dockSite9);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.slidePanel1);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(910, 480);
            this.Name = "OpenFileForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打开文件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenFileForm_FormClosing);
            this.Load += new System.EventHandler(this.OpenFileForm_Load);
            this.VisibleChanged += new System.EventHandler(this.OpenFileForm_VisibleChanged);
            this.dockSite9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.panelDockContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.dockSite1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barMeida)).EndInit();
            this.barMeida.ResumeLayout(false);
            this.panelDockContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treePathAndMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SlidePanel slidePanel1;
        private DevComponents.DotNetBar.DotNetBarManager DockManager;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockSite dockSite9;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.Bar barMeida;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer1;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader clnLineName;
        private System.Windows.Forms.ColumnHeader clnLineCode;
        private System.Windows.Forms.ColumnHeader clnUpDown;
        private System.Windows.Forms.ColumnHeader clnDirection;
        private System.Windows.Forms.ColumnHeader clnMlieageIOD;
        private System.Windows.Forms.ColumnHeader clnDate;
        private System.Windows.Forms.ColumnHeader clnTIme;
        private System.Windows.Forms.ColumnHeader clnCarNo;
        private System.Windows.Forms.ColumnHeader clnOriginalName;
        private System.Windows.Forms.ColumnHeader clnSize;
        private System.Windows.Forms.ColumnHeader clnOriginalPath;
        private System.Windows.Forms.CheckBox ckbLoadIndex;
        private System.Windows.Forms.Button btnOpenFiles;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.AdvTree.AdvTree treePathAndMedia;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader clnStartMileage;
        private System.Windows.Forms.ColumnHeader clnEndMileage;
        private System.Windows.Forms.ColumnHeader clnTotalMileage;
        private System.Windows.Forms.ColumnHeader clnClose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labLoading;
    }
}