using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CitFileSDK;
using IntegratedDisplay.Models;
using System.Drawing;
using System.Threading.Tasks;
using IntegratedDisplay.Forms;
using System.Threading;
using System.Data;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using CommonFileSDK;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace IntegratedDisplay
{
    public partial class MainForm : Form
    {

        private WaveformMaker _maker = null;

        /// <summary>
        /// 显示范围
        /// </summary>
        private int _zoomInSize = 200;
        /// <summary>
        /// 是否启用里程定位
        /// </summary>
        public bool IsMileageLocation = false;

        /// <summary>
        /// 里程定位的位置
        /// </summary>
        public long LocationPostion = 0;

        /// <summary>
        /// 是否放大显示
        /// </summary>
        private bool _isZoomView = false;

        /// <summary>
        /// 是否使用索引
        /// </summary>
        public bool wasIndex = false;
        /// <summary>
        /// 索引设置控件类对象
        /// </summary>
        public IndexForm fViewIndexForm;

        /// <summary>
        /// 查看IIC文件窗口
        /// </summary>
        public IICViewForm fIICViewForm;

        /// <summary>
        /// 通道设置窗口
        /// </summary>
        public ChannelsDialog channelDialog;

        /// <summary>
        /// 主窗体事件
        /// </summary>
        public static MainForm sMainform;
        /// <summary>
        /// 距离测量使用
        /// </summary>
        private Point lastMeterageLengthPoint = new Point(0, 0);
        private Point startMeterageLengthPoint = new Point(0, 0);
        private bool wasMeterageLength = false;
        //无效标记使用
        private bool wasUnAreas = false;
        private int iSPointX = 0;
        private long iSPointXPos = 0;
        private int iSPointXKM = 0;
        private float iSPointXMeter = 0;
        //添加无效标记窗体
        InvalidDataAddForm fInvalidDataAddForm = null;
        /// <summary>
        /// 无效信息查看窗体
        /// </summary>
        InvalidDataForm fInvalidDataForm = null;

        /// <summary>
        /// 文件列表窗口
        /// </summary>
        OpenFileForm openFileForm = null;

        /// <summary>
        /// 图层控制窗口
        /// </summary>
        LayerControlForm _LayerControlForm = null;

        /// <summary>
        /// 虚框的起始点
        /// </summary>
        private Point _startPoint = new Point(0, 0);

        /// <summary>
        /// 虚框的终止点
        /// </summary>
        private Point _endPoint = new Point(0, 0);

        /// <summary>
        /// 是否移动中
        /// </summary>
        private bool _isMove = false;

        /// <summary>
        /// 通道拖动模式
        /// </summary>
        private string _dragMode = string.Empty;

        /// <summary>
        /// 放大之前的滚动值
        /// </summary>
        private int _lastScrollValue = -1;

        /// <summary>
        /// 图层平移窗体
        /// </summary>
        private LayerTranslationForm _layerTranslationForm = null;

        /// <summary>
        /// 配置文件全局对象类
        /// </summary>
        public static ConfigData WaveformConfigData = new ConfigData();

        /// <summary>
        /// 台帐信息显示窗口
        /// </summary>
        private AccountInfoForm _accountInfoForm = null;

        /// <summary>
        /// 所有里程信息
        /// </summary>
        private List<Milestone> _waveformAllMileage = null;


        /// <summary>
        /// 单个通道的宽度
        /// </summary>
        private int _channelWidth = 160;

        /// <summary>
        /// 单个通道的高度
        /// </summary>
        private int _channelHeight = 32;

        /// <summary>
        /// 里程显示区的高度
        /// </summary>
        private int _mileageHeight = 30;

        /// <summary>
        /// 测量时绘制线的颜色
        /// </summary>
        Pen pen = new Pen(Color.Silver);

        /// <summary>
        /// 是否显示图形标签
        /// </summary>
        private bool _isShowBagging = false;

        /// <summary>
        /// 快捷方式打开的cit文件
        /// </summary>
        public string QuickCitPath = "";


        public MainForm()
        {
            InitializeComponent();
            this.tsCustom.Renderer = new ToolStripRender();
            sMainform = this;
        }

        public MainForm(string quickCitPath)
        {
            InitializeComponent();
            this.tsCustom.Renderer = new ToolStripRender();
            sMainform = this;
            QuickCitPath = quickCitPath;
        }


        /// <summary>
        /// 打开文件按钮触发事件
        /// </summary>
        /// <param name="sender">打开文件按钮</param>
        /// <param name="e">触发参数</param>
        public void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                fileDialog.Filter = "CIT文件|*.cit";
                fileDialog.FileName = string.Empty;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in fileDialog.FileNames)
                    {
                        openFileForm.DisplayInListview(file);

                    }
                    //ConfigManger.SaveConfigData(WaveformConfigData);
                    //openFileForm.IsDirectLoad = true;
                    openFileForm.ConfigData = WaveformConfigData;
                    if (openFileForm.Visible == false)
                    {
                        openFileForm.Show();
                    }
                    openFileForm.LoadTreeData();
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("打开文件时出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 主窗体加载事件，进行参数初始化
        /// </summary>
        /// <param name="sender">主窗体</param>
        /// <param name="e">触发参数</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            progressBar.Visible = false;
            try
            {
                ConfigManger.ConfigPath = Application.StartupPath + @"\config.xml";
                ConfigManger.LayoutConfigPath = Application.StartupPath + @"\Loayout.config";
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("加载配置文件时出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
            Task.Factory.StartNew(new Action(() => LoadCustomToolscripItem()));
            try
            {
                WaveformConfigData.DefaultChannelConfig = Application.StartupPath + @"\默认配置文件.xml";
                WaveformConfigData = ConfigManger.GetConfigData();
                DBOperator.InnderDBConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\InnerDB.idf;Persist Security Info=True;Mode=Share Exclusive;Jet OLEDB:Database Password=iicdc";
                InnerFileOperator.InnerFilePath = Application.StartupPath + "\\InnerDB.idf";
                InnerFileOperator.InnerConnString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0}; Persist Security Info = True; Mode = Share Exclusive; Jet OLEDB:Database Password = iicdc; ";

                WaveformConfigData.WaveConfigs.WaveConfigCount = 10;
                WaveformConfigData.RecentFiles.FilesCount = 10;
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("设置配置文件时出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }

            this.MouseWheel += MainForm_MouseWheel;
            try
            {
                _maker = new WaveformMaker(picMainGraphics.Width,
                    picMainGraphics.Height,
                    _channelWidth,
                    _channelHeight,
                    _mileageHeight,
                    30);
                //初始化所需要的窗口
                fViewIndexForm = new IndexForm(_maker);
                fViewIndexForm.Owner = this;
                fViewIndexForm.TopLevel = true;
                fViewIndexForm.delegateIndexClosed = indexFormClosed;
                fIICViewForm = new IICViewForm(_maker);
                fIICViewForm.Owner = this;
                fIICViewForm.TopLevel = true;
                fIICViewForm.innerDBPath = InnerFileOperator.InnerConnString;
                fInvalidDataForm = new InvalidDataForm(_maker);
                fInvalidDataForm.Owner = this;
                fInvalidDataForm.TopLevel = true;
                _layerTranslationForm = new LayerTranslationForm(_maker);
                _layerTranslationForm.Owner = this;
                _layerTranslationForm.TopLevel = true;
                channelDialog = new ChannelsDialog(_maker);
                
                _layerTranslationForm.ReviseValueChangedEvent += _layerTranslationForm_ReviseValueChangedEvent;
                _accountInfoForm = new AccountInfoForm(_maker);
                _accountInfoForm.Owner = this;
                _accountInfoForm.TopLevel = true;
                openFileForm = new OpenFileForm();
                openFileForm.Owner = this;
                openFileForm.TopLevel = true;
                //this.TopLevel = true;
            }
            catch(Exception ex)
            {

                MyLogger.logger.Error("初始化窗体时出错：" + ex.Message + ",堆栈:" + ex.StackTrace);
                MessageBox.Show("初始化窗体时出错");
                return;
            }

            try
            {
                LinkQuickOpen();
            }
            catch(Exception ex)
            {
                MyLogger.LogError("建立快捷打开方式出错", ex);
            }
            pen.Width = 2;

            _waveformAllMileage = new List<Milestone>();

             if(!string.IsNullOrEmpty(QuickCitPath))
            {
                QuickLoad();
                return;
            }

            //判断是否有最近访问的文件，有则显示选择文件窗口
            //if (openFileForm != null
            //    && !string.IsNullOrEmpty(WaveformConfigData.MediaPath)
            //    || (WaveformConfigData.RecentFiles.Files != null
            //    && WaveformConfigData.RecentFiles.Files.Count > 0)
            //    || WaveformConfigData.DirPaths.PathList.Count > 0)
            //{
            //    openFileForm.ConfigData = WaveformConfigData;
            //    if (!string.IsNullOrEmpty(QuickCitPath))
            //    {
            //        openFileForm.DisplayInListview(QuickCitPath);
            //        openFileForm.ConfigData = WaveformConfigData;
            //        if (openFileForm.Visible == false)
            //        {
            //            openFileForm.Show();
            //        }
            //    }
            //    else
            //    {
            //        if (openFileForm.Visible == false)
            //        {
            //            openFileForm.Show();
            //            openFileForm.Activate();
            //        }
            //    }
            //}
            
        }

        private void _LayerControlForm_SelectedValueChangedEvent()
        {
            InvokeScroolBar_Scroll(null, scrollBarPic.Value);
        }

        private void indexFormClosed()
        {
            SetManualCorrect(false);
        }

        /// <summary>
        /// 双击文件打开方式
        /// </summary>
        private void LinkQuickOpen()
        {
            string strExtension = ".cit";

            string strProject = "IntegratedDisplay";

            Registry.ClassesRoot.CreateSubKey(strExtension).SetValue("", strProject, RegistryValueKind.String);

            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(strProject))
            {

                string strExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

                strExePath = Path.GetDirectoryName(strExePath);

                strExePath += "\\IntegratedDisplay.exe";

                key.CreateSubKey(@"Shell\Open\Command").SetValue("", strExePath + " \"%1\"", RegistryValueKind.ExpandString);
            }
        }

        /// <summary>
        /// 快速加载
        /// </summary>
        private void QuickLoad()
        {
            if (File.Exists(QuickCitPath))
            {
               
                try
                {
                    WavefromData waveData = new WavefromData();
                    waveData.CitFilePath = QuickCitPath;
                    string indexFilePath = Path.GetFileNameWithoutExtension(QuickCitPath) + ".idf";
                    waveData.WaveIndexFilePath = indexFilePath;
                    LoadLayerInfo(new List<WavefromData>() { waveData });
                }
                catch (Exception ex)
                {

                    MyLogger.logger.Error("加载cit文件失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 加载自定义工具栏
        /// </summary>
        private void LoadCustomToolscripItem()
        {
            try
            {
                FormlayoutConfig config = ConfigManger.GetLayoutConfig();
                this.Invoke(new Action(() =>
                {
                    foreach (string name in config.statusBarList)
                    {
                        ToolStripItem[] item = tsAllKeys.Items.Find(name, false);
                        if (item.Length > 0)
                        {
                            tsCustom.Items.Insert(tsCustom.Items.Count - 1, item[0]);
                        }
                    }
                }));
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("自定义快捷菜单失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
           
        }

        /// <summary>
        /// 图层平移窗口订阅事件，方便进行重绘波形
        /// </summary>
        private void _layerTranslationForm_ReviseValueChangedEvent()
        {
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 主窗体订阅的鼠标滚轮滚动事件，进行波形的滚动处理
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (scrollBarPic.Enabled == false)
            {
                return;
            }
            int scrollChangeSize = scrollBarPic.LargeChange;
            if (_maker != null && _maker.IsZoomInView)
            {
                scrollChangeSize = scrollBarPic.SmallChange;
            }
            ScrollEventArgs eventArgs = null;
            if (e.Delta > 0)
            {
                if(scrollBarPic.Value<= scrollBarPic.Minimum)
                {
                    return;
                }
                if (scrollBarPic.Value - scrollChangeSize < scrollBarPic.Minimum)
                {
                    scrollBarPic.Value = scrollBarPic.Minimum;
                }
                else
                {
                    scrollBarPic.Value -= scrollChangeSize;
                }
            }
            else
            {
               
                if (scrollBarPic.Value + scrollChangeSize > scrollBarPic.Maximum)
                {
                    return;
                }
                else
                {
                    scrollBarPic.Value += scrollChangeSize;

                }
            }
            eventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, scrollBarPic.Value);
            scrollBarPic_Scroll(sender, eventArgs);
        }

        /// <summary>
        /// 加载所有的里程信息，显示里程信息用
        /// </summary>
        private void LoadAllMileage()
        {
            try
            {

                _waveformAllMileage = _maker.WaveformDataList[0].GetAllMileage();
                ShowOperateInfo("数据已加载完成！");

            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("加载里程信息出错：" + ex.Message +"，文件："+_maker.WaveformDataList[0].CitFilePath+ ",堆栈信息：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 通用设置更改窗口
        /// </summary>
        /// <param name="sender">其它参数按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiOtherSetting_Click(object sender, EventArgs e)
        {
            try
            {
                SettingForm form = new SettingForm(WaveformConfigData);
                form.ShowDialog();
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("设置窗口出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 打开文件夹事件
        /// </summary>
        /// <param name="sender">打开文件夹按钮</param>
        /// <param name="e">触发参数</param>
        public void tsmiOpenPathOrMedia_Click(object sender, EventArgs e)
        {
            ////将打开文件窗口的置顶去掉，以便选择选择目录窗口可以显示
            //openFileForm.TopLevel = true;
            
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!WaveformConfigData.DirPaths.PathList.Contains(folderDialog.SelectedPath))
                    {
                        WaveformConfigData.DirPaths.PathList.Add(folderDialog.SelectedPath);
                        ConfigManger.SaveConfigData(WaveformConfigData);
                    }
                    openFileForm.ConfigData = WaveformConfigData;
                    if (openFileForm.Visible == false)
                    {
                        openFileForm.Show();
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("打开目录或媒体库出错：" + ex.Message + ",堆栈信息：" + ex.StackTrace);
                    MessageBox.Show("打开目录或媒体库出错：" + ex.Message);
                }
                openFileForm.LoadTreeData();
                //openFileForm.TopLevel = true;
            }
        }

        /// <summary>
        /// 图像化显示台帐按钮事件
        /// </summary>
        /// <param name="sender">点击按钮</param>
        /// <param name="e">事件</param>
        private void tsmiViewAccountByDrawing_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    tsmiViewAccountByDrawing.Checked = !tsmiViewAccountByDrawing.Checked;
                    if (tsmiViewAccountByDrawing.Checked)
                    {
                        splitPictrueContainer.Panel1Collapsed = false;

                        labRight.Visible = true;
                        panelRight.Width = _channelWidth;
                        MainForm_Resize(sender, e);
                        WaveformConfigData.Accouts.IsCheck = tsmiViewAccountByDrawing.Checked;

                        Task.Factory.StartNew(() => LoadAccountData());
                    }
                    else
                    {
                        WaveformConfigData.Accouts.IsCheck = tsmiViewAccountByDrawing.Checked;
                        splitPictrueContainer.Panel1Collapsed = true;
                        if (_maker.CheckAccountDatabaseListExist())
                        {
                            _maker.AccountDatabaseList.RemoveAt(0);
                        }
                    }
                    MainForm_Resize(sender, e);
                }
                else
                {
                    tsmiViewAccountByDrawing.Checked = false;
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("点击显示图像台账时出错:" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
            picMainGraphics.Invalidate();

        }

        /// <summary>
        /// 加载台账信息，并重绘波形
        /// </summary>
        private void LoadAccountData()
        {
            try
            {
                if (_maker.GetAccountDatabaseList(WaveformConfigData.Accouts.AccountDescList, WaveformConfigData.Accouts.IsCheck))
                {
                    this.Invoke(new Action(() =>
                    {
                        splitPictrueContainer.SplitterDistance = _maker.AccountInfoHeight;
                        panelAccount.AutoScrollMinSize = new Size(picMainGraphics.Width - _channelWidth, _maker.AccountInfoHeight);//160
                                                                                                                                   //panelAccount.AutoScrollMinSize = new Size(picMainGraphics.Width - _channelWidth, _maker.AccountInfoHeight);
                        picSecondGraphics.Invalidate();
                        //ResizeScrollBar();});

                    }));
                }
                else
                {
                    MessageBox.Show("加载台账数据失败，请重试！");
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("加载台账数据出现异常:" + ex.Message + "堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 台帐选择显示方式
        /// </summary>
        /// <param name="sender">点击按钮(包括:曲线,坡度,道岔,速度区段)</param>
        /// <param name="e"></param>
        private void tsmiAccountDisplayMode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = !item.Checked;
            int visibleCount = 0;
            for (int i = 0; i < WaveformConfigData.Accouts.AccountDescList.Count; i++)
            {
                if (WaveformConfigData.Accouts.AccountDescList[i].Name.Contains(item.Text))
                {
                    WaveformConfigData.Accouts.AccountDescList[i].IsCheck = item.Checked;
                }
                if (WaveformConfigData.Accouts.AccountDescList[i].IsCheck)
                {
                    visibleCount++;
                }
            }
            try
            {
                ConfigManger.SaveConfigData(WaveformConfigData);

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置文件失败:" + ex.Message);
            }
            if (tsmiViewAccountByDrawing.Checked)
            {
                tsmiViewAccountByDrawing_Click(sender, e);
                tsmiViewAccountByDrawing_Click(sender, e);
                if (_maker != null && _maker.WaveformDataList.Count > 0)
                {
                    _maker.AccountInfoHeight = visibleCount * 40;
                    panelAccount.Invalidate();
                }
            }

        }

        /// <summary>
        /// 拖动方式点击事件
        /// </summary>
        /// <param name="sender">点击按钮（包括：单通道拖动、同名通道拖动、同基线拖动）</param>
        /// <param name="e">事件参数</param>
        private void tsmiDragMode_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                item.Checked = true;
                foreach (var im in tsmiDragMode.DropDownItems)
                {
                    if (item != im)
                    {
                        ((ToolStripMenuItem)im).Checked = false;
                    }
                }
                _dragMode = item.Tag.ToString();
                switch (_dragMode)
                {
                    case "0":
                        _maker.ChannelDargMode = WaveformMaker.DragMode.SingleDarg;
                        break;
                    case "1":
                        _maker.ChannelDargMode = WaveformMaker.DragMode.SameNameDarg;
                        break;
                    case "2":
                        _maker.ChannelDargMode = WaveformMaker.DragMode.SameBaselineDarg;
                        break;
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("选择拖动方式时出错:" + ex.Message + ",异常：" + ex.StackTrace);
            }

        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender">退出按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加载通道数据信息，并初始化通道配置文件等数据
        /// </summary>
        /// <param name="waveDataList"></param>
        public void LoadLayerInfo(List<WavefromData> waveDataList)
        {
            //ShowOperateInfo("开始加载数据");
            //MyLogger.logger.Trace("开始加载数据");
            if (waveDataList != null && waveDataList.Count > 0)
            {
                ChannelManager.channelTotalHeight = (_maker.ControlHeight - _maker.MileageInfoHeight);
                foreach (var waveData in waveDataList)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(WaveformConfigData.DefaultChannelConfig) || !File.Exists(WaveformConfigData.DefaultChannelConfig))
                        {
                            //创建配置文件
                            string defalutChannelConfig = ChannelManager.CreateWaveXMLConfig((Path.GetFullPath(Application.ExecutablePath)), _maker.SingleChannelInfoHeight, 0, waveData.ChanneDefinitionList);
                            WaveformConfigData.DefaultChannelConfig = defalutChannelConfig;
                            ConfigManger.SaveConfigData(WaveformConfigData);
                        }
                        if (WaveformConfigData.WaveConfigs != null
                            && WaveformConfigData.WaveConfigs.WaveConfigList.Count > 0)
                        {
                            try
                            {
                                var config = WaveformConfigData.WaveConfigs.WaveConfigList.Find(p => p.WaveConfigIndex == _maker.WaveformDataList.Count + 1);
                                if (config != null)
                                {
                                    waveData.WaveConfigFilePath = config.WaveConfigFile;
                                }
                                else
                                {
                                    waveData.WaveConfigFilePath = WaveformConfigData.DefaultChannelConfig;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                waveData.WaveConfigFilePath = WaveformConfigData.DefaultChannelConfig;
                                MyLogger.logger.Error("读取个性化配置文件失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                            }
                        }
                        else
                        {
                            waveData.WaveConfigFilePath = WaveformConfigData.DefaultChannelConfig;
                        }
                    }
                    catch(Exception ex)
                    {
                        MyLogger.logger.Error("配置波形失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                    }
                    try
                    {
                        _maker.AddWaveformData(waveData);

                        _maker.ControlHeight = picMainGraphics.Height;
                        _maker.ZoomInSize = _zoomInSize;
                        //只获取第一层数据
                        if (_maker.WaveformDataList.Count == 1)
                        {
                            //Task.Factory.StartNew(() => LoadAllMileage());
                            scrollBarPic.Maximum = _maker.GetMaxScorllSize();
                        }
                        RefreshCloseItem();
                        //cit类型不一致时需要重新排列一下
                        _maker.AutoArrange();
                        picMainGraphics.Invalidate();
                    }
                    catch(Exception ex)
                    {
                        MyLogger.logger.Error("开始加载波形时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                    }
                }

            }
            else
            {
                MessageBox.Show("加载波形数据信息时未找到数据！");
            }


        }

        /// <summary>
        /// 刷新需要关闭的文件
        /// </summary>
        private void RefreshCloseItem()
        {
            try
            {
                tsDropDownLayerList.DropDownItems.Clear();
                tsmiCloseFile.DropDownItems.Clear();
                for (int i = 0; i < _maker.WaveformDataList.Count; i++)
                {
                    tsDropDownLayerList.DropDownItems.Add(_maker.WaveformDataList[i].LayerConfig.Name);
                    if (_maker.WaveformDataList[i].IsLoadIndex)
                    {
                        tsDropDownLayerList.DropDownItems[i].Text = "(里程已修正) " + _maker.WaveformDataList[i].LayerConfig.Name;
                        tsDropDownLayerList.DropDownItems[i].ForeColor = Color.Black;
                    }
                    else
                    {
                        tsDropDownLayerList.DropDownItems[i].Text = "(里程未修正) " + _maker.WaveformDataList[i].LayerConfig.Name;
                        tsDropDownLayerList.DropDownItems[i].ForeColor = Color.Red;
                    }
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Name = "tsmiLayer" + i.ToString();
                    tsmi.Text = _maker.WaveformDataList[i].LayerConfig.Name;
                    tsmi.Tag = i;
                    tsmi.Click += new EventHandler(tsmiCloseItem_Click);
                    tsmiCloseFile.DropDownItems.Add(tsmi);

                }
                if (_maker.WaveformDataList.Count > 1)
                {
                    ToolStripMenuItem tsmiAll = new ToolStripMenuItem();
                    tsmiAll.Name = "tsmiLayerAll";
                    tsmiAll.Text = "关闭所有";
                    tsmiAll.Tag = 1000;
                    tsmiAll.Click += tsmiCloseItem_Click;
                    tsmiCloseFile.DropDownItems.Add(tsmiAll);
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("刷新关闭选项时出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 点击关闭选中文件后触发事件
        /// </summary>
        /// <param name="sender">关闭指定的文件按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiCloseItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem tsmiCloseItem = (ToolStripMenuItem)sender;
                int index = Convert.ToInt32(tsmiCloseItem.Tag);
                if (index == 1000)
                {
                    _maker.RemoveAllData();
                }
                else
                {
                    _maker.RemoveWaveformData(Convert.ToInt32(tsmiCloseItem.Tag));

                }
                _maker.ClearHighlightItem();
                scrollBarPic.Value = 0;
                if (_maker.WaveformDataList.Count > 0)
                {
                    //_waveformAllMileage.Clear();
                    //Task.Factory.StartNew(() => LoadAllMileage());

                    scrollBarPic.Maximum = _maker.GetMaxScorllSize();
                }
                if(_layerTranslationForm!=null)
                {
                    _layerTranslationForm.LoadData();
                }
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("点击关闭按钮时出错：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
            finally
            {
                picMainGraphics.Cursor = Cursors.Default;
                picMainGraphics.Invalidate();
            }
            RefreshCloseItem();
        }

        /// <summary>
        /// 波形图绘制事件，主要的波形图处理
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void picMainGraphics_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (_maker != null && _maker.WaveformDataList.Count > 0)
            {
                try
                {
                    //获取数据
                    if (scrollBarPic.Value >= 0)
                    {
                        _maker.GetWaveformData(scrollBarPic.Value);
                    }
                    else
                    {
                        MyLogger.logger.Error("获取波形数据时出现负数,当前值:" + scrollBarPic.Value);
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("获取波形数据时出错：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {

                    Bitmap invaildImg = _maker.DrawInvaildData();
                    e.Graphics.DrawImage(invaildImg, 0, 0);

                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("绘制无效数据失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    //绘制通道基线和通道数据
                    Bitmap bitmapLines = _maker.DrawChannelsBaselines();
                    e.Graphics.DrawImage(bitmapLines, 0, _maker.MileageInfoHeight);
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("绘制通道基线失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                //绘制波形图
                try
                {
                    Bitmap bitmap = _maker.DrawWavefrom();
                    e.Graphics.DrawImage(bitmap, 0, 0);
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("绘制波形失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    Bitmap caliperImg = _maker.DrawCaliperLine();
                    e.Graphics.DrawImage(caliperImg, 0, 0);
                    if(_maker.IsSelectedCaliper)
                    {
                        Bitmap channelData = _maker.DrawChannelData();
                        e.Graphics.DrawImage(channelData, 0, 30);
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("绘制游标失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                
                try
                {
                    //进行里程定位
                    if (IsMileageLocation)
                    {
                        MyLogger.logger.Trace("开始里程定位");
                        Bitmap bitmaplocation = _maker.DrawMileageLocation(LocationPostion);
                        e.Graphics.DrawImage(bitmaplocation, 0, 0);
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("绘制里程定位失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    if (_isShowBagging)
                    {
                        Bitmap bitmapTagging = _maker.DrawTagging(WaveformConfigData.SignRadius);
                        if (bitmapTagging != null)
                        {
                            e.Graphics.DrawImage(bitmapTagging, 0, 0);
                        }
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("绘制标记失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    //图层平移时，显示的里程信息
                    if (_layerTranslationForm.Visible)
                    {
                        for (int i = 0; i < _maker.WaveformDataList.Count; i++)
                        {
                            _layerTranslationForm.dgvLayerInfo.Rows[i].Cells["clnMileage"].Value = _maker.WaveformDataList[i].MileList.milestoneList[0].GetMeter().ToString("F5");
                        }
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("波形平移时出现错误：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    //测量时显示的宽度线

                    if ((tsmiGaugeByChannel.Checked || tsmiGaugeByBaseline.Checked || tsmiGaugeByLayer.Checked) && picMainGraphics.Cursor == Cursors.Cross)
                    {

                        int xPostion = Control.MousePosition.X * (_maker.WaveformDataList[0].MileList.milestoneList.Count) / (picMainGraphics.Width - _channelWidth);
                        float scaling = (_maker.WaveformDataList[0].MileList.milestoneList.Count) * 1.0f / (picMainGraphics.Width - _channelWidth);
                        float middle = WaveformConfigData.MeterageRadius / scaling / 2;//


                        e.Graphics.DrawLine(pen, picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X - middle, _mileageHeight, picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X - middle, picMainGraphics.ClientSize.Height);
                        e.Graphics.DrawLine(pen, picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X + middle, _mileageHeight, picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X + middle, picMainGraphics.ClientSize.Height);

                        //e.Graphics.DrawLine(pen,
                        //   picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X-middle, _mileageHeight,
                        // picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X-middle, picMainGraphics.ClientSize.Height);
                        
                        //e.Graphics.DrawLine(pen,
                        //    picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X + (WaveformConfigData.MeterageRadius-middle), _mileageHeight,
                        //  picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X + (WaveformConfigData.MeterageRadius - middle), picMainGraphics.ClientSize.Height);

                        //e.Graphics.DrawLine(pen,)
                        //int scaling = Control.MousePosition.X*(_maker.WaveformDataList[0].MileList.milestoneList.Count)/ (picMainGraphics.Width - _channelWidth);
                        //e.Graphics.DrawLine(pen,
                        //   picMainGraphics.PointToClient(new Point(Control.MousePosition.X - (int)(WaveformConfigData.MeterageRadius * scaling * 4), 0)).X, _mileageHeight,
                        // picMainGraphics.PointToClient(new Point(Control.MousePosition.X - (int)(WaveformConfigData.MeterageRadius * scaling * 4), 0)).X, picMainGraphics.ClientSize.Height);

                        //e.Graphics.DrawLine(pen,
                        //    picMainGraphics.PointToClient(new Point(Control.MousePosition.X + (int)(WaveformConfigData.MeterageRadius * scaling * 4), 0)).X, _mileageHeight,
                        //  picMainGraphics.PointToClient(new Point(Control.MousePosition.X + (int)(WaveformConfigData.MeterageRadius * scaling * 4), 0)).X, picMainGraphics.ClientSize.Height);
                    }//测量距离时显示的里程及红线
                    else if (tsmiGaugeByMileage.Checked && picMainGraphics.Cursor == Cursors.Cross)
                    {
                        e.Graphics.DrawLine(pen,
                            picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X, _mileageHeight,
                            picMainGraphics.PointToClient(new Point(Control.MousePosition.X, 0)).X, picMainGraphics.ClientSize.Height);

                        // 同里程测量时，那一根竖线上面有红色的公里字符串
                        string mileStoneString = _maker.GetMileStoneString(Control.MousePosition.X);
                        Font f = new Font(_maker.FontName, 10, FontStyle.Regular);
                        e.Graphics.DrawString(mileStoneString, f, new SolidBrush(Color.Red), new PointF(Control.MousePosition.X - 10, 1));
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("进行测量时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
                try
                {
                    //台账窗口显示的里程信息
                    if (_accountInfoForm.Visible && _accountInfoForm.tscbxLinkage.SelectedIndex == 0)
                    {
                        if (_maker.WaveformDataList[0].CitFile.iKmInc == 0)
                        {
                            _accountInfoForm.tstxtStartMileage.Text = (_maker.WaveformDataList[0].MileList.milestoneList[0].GetMeter() - 10000) / 1000 + "";
                            _accountInfoForm.tstxtEndMileage.Text = (_maker.WaveformDataList[0].MileList.milestoneList[0].GetMeter() + 10000) / 1000 + "";
                        }
                        else
                        {
                            _accountInfoForm.tstxtStartMileage.Text = (_maker.WaveformDataList[0].MileList.milestoneList[0].GetMeter() + 10000) / 1000 + "";
                            _accountInfoForm.tstxtEndMileage.Text = (_maker.WaveformDataList[0].MileList.milestoneList[0].GetMeter() - 10000) / 1000 + "";
                        }
                        //刷新台账信息
                        if (_accountInfoForm.Visible)
                        {
                            Task t = Task.Factory.StartNew(() => _accountInfoForm.InvokeLoadAccountData());
                            t.ContinueWith((task) =>
                            {
                                MyLogger.logger.Error("加载台账信息时失败：" + task.Exception.InnerException.Message + "，堆栈：" + task.Exception.InnerException.StackTrace);

                            }, TaskContinuationOptions.OnlyOnFaulted);
                        }
                    }
                    //刷新台账信息
                    if (!splitPictrueContainer.Panel1Collapsed)
                    {
                        panelAccount.Invalidate();
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("获取台账时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
               
            }
        }


        /// <summary>
        /// 鼠标在波形图上按下的时候触发事件
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void picMainGraphics_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {//自动滚动情况下停止滚动
                if (tsmiAutoScroll.Checked)
                {
                    tsmiAutoScroll.Checked = false;
                    tsbtnAutoScroll.Checked = false;
                    tsmiAutoScroll_Click(sender, e);
                }
                //右键关闭放大功能
                if (_maker.IsZoomInView && e.Button == MouseButtons.Right)
                {
                    _maker.MakeViewNormal();
                    ShowCloseOperateInfo("区域放大");
                    picMainGraphics.Cursor = Cursors.Default;
                    _maker.ZoomInSize = _zoomInSize;
                    scrollBarPic.Maximum = _maker.GetMaxScorllSize();
                    picMainGraphics.Invalidate();
                    scrollBarPic.Value = _lastScrollValue;

                }
                //启用放大区域时，开始绘制放大虚框
                if (e.Button == MouseButtons.Left && _isZoomView)
                {
                    _lastScrollValue = scrollBarPic.Value;
                    _startPoint.X = e.X;
                    _startPoint.Y = e.Y;
                    _endPoint.X = e.X;
                    _endPoint.Y = e.Y;
                    _isMove = true;
                    picMainGraphics.Capture = true;
                    picMainGraphics.Invalidate();
                    ControlPaint.DrawReversibleFrame(picMainGraphics.RectangleToScreen(GetNormalizedRectangle(_startPoint, _endPoint)), Color.DarkRed, FrameStyle.Dashed);
                }

                //判断是否有选中的通道
                if (e.Button == MouseButtons.Left)
                {
                    _maker.SelectDragItem(e.X, e.Y);
                    if (_maker.IsSelectedChannel)
                    {
                        picMainGraphics.Invalidate();
                    }
                    if (_maker.SelectCaliper(e.X, e.Y))
                    {
                        picMainGraphics.Invalidate();
                    }

                }
                //距离测量
                if (tsmiGaugeDistance.Checked && e.Button == MouseButtons.Left)
                {
                    lastMeterageLengthPoint.X = e.X;
                    lastMeterageLengthPoint.Y = e.Y;
                    startMeterageLengthPoint.X = e.X;
                    startMeterageLengthPoint.Y = e.Y;
                    wasMeterageLength = true;
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("绘图按下时出现错误：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
          

        }

        /// <summary>
        /// 波形下方的滚动条滚动时触发的事件
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void scrollBarPic_Scroll(object sender, ScrollEventArgs e)
        {
            switch (e.Type)
            {
                //移动过程中显示里程信息
                case ScrollEventType.ThumbTrack:
                    {
                        if (_maker != null && _maker.WaveformDataList.Count > 0)
                        {
                            labMileageInfoShow.Visible = true;
                            try
                            {
                                int scrollPostion= _maker.GetScrollValueToSampleNum(scrollBarPic.Value);
                                List<Milestone> test = _maker.WaveformDataList[0].GetRangeMileage(scrollPostion);
                                //if (_waveformAllMileage.Count > 0 && (scrollPostion + _maker.WaveformDataList[0].WaveformDataCount - 1) < _waveformAllMileage.Count)
                                //{

                                //    labMileageInfoShow.Text = (_waveformAllMileage[scrollPostion].GetMeter() / 1000).ToString("F05") + "-"
                                //        + (_waveformAllMileage[(scrollPostion) + _maker.WaveformDataList[0].WaveformDataCount - 1].GetMeter() / 1000).ToString("F05");
                                //}
                                if (test != null && test.Count > 0)
                                {
                                    labMileageInfoShow.Text = (test[0].GetMeter() / 1000).ToString("F05") + "-"
                                        + (test[test.Count-1].GetMeter() / 1000).ToString("F05");
                                }
                            }
                            catch (Exception ex)
                            {
                                MyLogger.logger.Error("显示里程信息块时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                            }
                        }
                        break;
                    }
                //结束滚动时进行波形重绘
                case ScrollEventType.EndScroll:
                    {
                        try
                        {
                            labMileageInfoShow.Visible = false;
                            if (_maker.WaveformDataList.Count > 0)
                            {
                                picMainGraphics.Invalidate();
                                panelAccount.Invalidate();
                            }
                        }
                        catch (Exception ex)
                        {
                            MyLogger.logger.Error("滚动结束时出现错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 更改显示范围，并进行波形重绘
        /// </summary>
        /// <param name="sender">显示范围按钮，包括所有的更改范围按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiZoomSize_Click(object sender, EventArgs e)
        {
            SetZoomSize(sender);
        }

        private void SetZoomSize(object sender)
        {
            try
            {
                ToolStripMenuItem tsmiZoomSize = sender as ToolStripMenuItem;
                if (tsmiZoomSize != null)
                {
                    _zoomInSize = Convert.ToInt32(tsmiZoomSize.Tag);
                }
                else
                {
                    ToolStripButton btn = sender as ToolStripButton;
                    _zoomInSize = Convert.ToInt32(btn.Tag);
                }
                foreach (var item in tsmiDisplayRange.DropDownItems)
                {
                    ToolStripMenuItem tsItem = (ToolStripMenuItem)(item);
                    if ((Convert.ToInt32(tsItem.Tag)) == _zoomInSize)
                    {
                        tsItem.Checked = true;
                    }
                    else
                    {
                        tsItem.Checked = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("获取ZoomSize失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
            tsbtn1X.Checked = false;
            tsbtn2X.Checked = false;
            tsbtn3X.Checked = false;
            tsbtn4X.Checked = false;
            tsbtn5X.Checked = false;
            tsbtn10X.Checked = false;
            switch (_zoomInSize)
            {
                case 200:
                    {
                        tsbtn1X.Checked = true;
                        break;
                    }
                case 400:
                    {
                        tsbtn2X.Checked = true;
                        break;
                    }
                case 600:
                    {
                        tsbtn3X.Checked = true;
                        break;
                    }
                case 800:
                    {
                        tsbtn4X.Checked = true;
                        break;
                    }
                case 1000:
                    {
                        tsbtn5X.Checked = true;
                        break;
                    }
                case 2000:
                    {
                        tsbtn10X.Checked = true;
                        break;
                    }
            }
            try
            {
                _maker.ZoomInSize = _zoomInSize;
                if (_maker.WaveformDataList.Count > 0)
                {
                    scrollBarPic.Maximum = _maker.GetMaxScorllSize();
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("设置放大倍数时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 进行里程定位
        /// </summary>
        /// <param name="sender">里程定位按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiMileageLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    SetMeterageMenuChecked(false);
                    SetOperationMenuItemChecked(false);
                    SetToolsChecked(false);
                    picMainGraphics.Cursor = Cursors.Default;
                    ShowOpenOperateInfo("里程定位");
                    MileageLocationForm form = new MileageLocationForm(_maker);
                    form.ShowDialog();
                    ShowCloseOperateInfo("里程定位");
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("里程定位出现错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }

        }


        /// <summary>
        /// 更改波形下方滚动条的值
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="value">滚动条的值</param>
        public void InvokeScroolBar_Scroll(object sender, int value)
        {
            scrollBarPic.Value = value;
            ScrollEventArgs eventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, value);
            scrollBarPic_Scroll(sender, eventArgs);
        }

        /// <summary>
        /// 自动滚动
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void bgdAutoScrollWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ShowOpenOperateInfo("自动滚动");
            while (!bgdAutoScrollWorker.CancellationPending)
            {
                try
                {
                    if (scrollBarPic.Value < (scrollBarPic.Maximum - 4))
                    {
                        scrollBarPic.Value++;
                        this.Invoke(new Action(() =>
                        {
                            ScrollEventArgs eventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, scrollBarPic.Value);
                            scrollBarPic_Scroll(sender, eventArgs);
                        }));

                        Thread.Sleep(1600 - WaveformConfigData.AutoScrollVelocity);
                    }
                    else
                    {
                        this.Invoke(new Action(() => { SetAutoScroll(false); }));
                    }
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("自动滚动时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                    break;
                }
            }
        }

        /// <summary>
        /// 触发自动滚动事件
        /// </summary>
        /// <param name="sender">点击的自动滚动按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiAutoScroll_Click(object sender, EventArgs e)
        {
            SetAutoScroll(tsmiAutoScroll.Checked);
        }

        private void SetAutoScroll(bool isChecked)
        {
            if (IsOpenCit())
            {
                SetOperationMenuItemChecked(false);
                SetMeterageMenuChecked(false);
                SetToolsChecked(false);
                if (isChecked)
                {
                    
                    
                    scrollBarPic.Enabled = false;
                    
                    tsbtnAutoScroll.Checked = isChecked;
                    tsmiAutoScroll.Checked = isChecked;
                    
                    bgdAutoScrollWorker.RunWorkerAsync();
                }
                else
                {
                    ShowCloseOperateInfo("自动滚动");
                    tsbtnAutoScroll.Checked = isChecked;
                    tsmiAutoScroll.Checked = isChecked;
                    bgdAutoScrollWorker.CancelAsync();
                    scrollBarPic.Enabled = true;
                }
            }
            else
            {
                tsbtnAutoScroll.Checked = isChecked;
                tsmiAutoScroll.Checked = isChecked;
            }
        }

        /// <summary>
        /// 视图放大
        /// </summary>
        /// <param name="sender">试图放大按钮</param>
        /// <param name="e">触发对象</param>
        private void tsmiViewZoom_Click(object sender, EventArgs e)
        {
            if (IsOpenCit() && _maker.IsZoomInView == false)
            {
                BeginZoomInView(tsmiViewZoom.Checked);
            }
            else
            {
                tsmiViewZoom.Checked = false;
                tsbtnZoomInView.Checked = false;
                _isZoomView = false;
               
            }
        }

        private void BeginZoomInView(bool isChecked)
        {
            SetOperationMenuItemChecked(false);
            SetMeterageMenuChecked(false);
            SetToolsChecked(false);
            _isZoomView = isChecked;
            tsmiViewZoom.Checked = isChecked;
            tsbtnZoomInView.Checked = isChecked;
            if (isChecked)
            {

                ShowOpenOperateInfo("区域放大");
            }
            else
            {
                ShowCloseOperateInfo("区域放大");
            }
        }

        /// <summary>
        /// 根据两个点，获取正常的矩形框
        /// </summary>
        /// <param name="p1">起点</param>
        /// <param name="p2">终点</param>
        /// <returns></returns>
        private Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// 根据矩形获取正常的矩形框
        /// </summary>
        /// <param name="r">矩形</param>
        /// <returns></returns>
        private Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }

        /// <summary>
        /// 根据四个点获取矩形
        /// </summary>
        /// <param name="x1">开始点的X</param>
        /// <param name="y1">开始点的Y</param>
        /// <param name="x2">结束点的X</param>
        /// <param name="y2">结束点的Y</param>
        /// <returns></returns>
        private Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                x1 ^= x2;
                x2 ^= x1;
                x1 ^= x2;
            }

            if (y2 < y1)
            {
                y1 ^= y2;
                y2 ^= y1;
                y1 ^= y2;
            }

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// 在波形图上鼠标移动时触发的事件
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void picMainGraphics_MouseMove(object sender, MouseEventArgs e)
        {
            //显示放大区域
            try
            {
                if (_isZoomView && _isMove)
                {
                    Point currentPoint = new Point(e.X, e.Y);
                    Point oldPoint = _endPoint;
                    _endPoint.X = e.X;
                    _endPoint.Y = e.Y;
                    ControlPaint.DrawReversibleFrame(
                            picMainGraphics.RectangleToScreen(GetNormalizedRectangle(_startPoint, oldPoint)),
                             Color.DarkRed, FrameStyle.Dashed);
                    ControlPaint.DrawReversibleFrame(
                            picMainGraphics.RectangleToScreen(GetNormalizedRectangle(_startPoint, currentPoint)),
                             Color.DarkRed, FrameStyle.Dashed);
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("放大时Move失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            try
            {
                //距离测量
                if (tsmiGaugeDistance.Checked && wasMeterageLength)//绘制线
                {
                    Point point = new Point(e.X, e.Y);
                    Point oldPoint = lastMeterageLengthPoint;

                    lastMeterageLengthPoint.X = e.X;
                    lastMeterageLengthPoint.Y = e.Y;
                    ControlPaint.DrawReversibleLine(picMainGraphics.PointToScreen(startMeterageLengthPoint), picMainGraphics.PointToScreen(oldPoint), Color.DarkRed);
                    ControlPaint.DrawReversibleLine(picMainGraphics.PointToScreen(startMeterageLengthPoint), picMainGraphics.PointToScreen(point), Color.DarkRed);
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("距离测量时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            try
            {
                if (_maker.IsSelectedChannel)
                {
                    //拖动通道线
                    int currentY = 0;
                    if (e.Y >= (scrollBarPic.Location.Y - 8))
                    {
                        currentY = (scrollBarPic.Location.Y - 8);
                    }
                    else
                    {
                        currentY = e.Y;
                    }
                    _maker.MoveDragItem(e.X, currentY);
                    picMainGraphics.Invalidate();
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("移动通道时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            try
            {
                if (_maker.IsSelectedCaliper)
                {
                    if (e.X >= 0 && e.X <= (_maker.ControlWidth - _maker.SingleChannelInfoWidth))
                    {
                        _maker.MoveCaliper(e.X);
                        picMainGraphics.Invalidate();
                    }
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("移动游标时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            if (tsmiGaugeByChannel.Checked || tsmiGaugeByBaseline.Checked ||
                tsmiGaugeByLayer.Checked || tsmiGaugeByMileage.Checked)
            {
                picMainGraphics.Invalidate();
            }
        }
    

        /// <summary>
        /// 在波形图上当鼠标抬起后触发的事件
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void picMainGraphics_MouseUp(object sender, MouseEventArgs e)
        {
            //进行放大处理
            if (_isZoomView && _isMove && e.Button == MouseButtons.Left)
            {
                try
                {
                    tsbtnZoomInView.Checked = false;
                    tsmiViewZoom.Checked = false;
                    _isZoomView = false;
                    Rectangle rect = GetNormalizedRectangle(_startPoint, _endPoint);
                    _maker.IsZoomInView = true;
                    long leftPostion = _maker.MakeViewZoomIn(rect);
                    scrollBarPic.Maximum=  _maker.GetMaxScorllSize();
                    int scorllSize = _maker.GetLocationScrollSize(leftPostion);
                    InvokeScroolBar_Scroll(sender, scorllSize);
                    
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("放大时Up失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
            }
            try
            {
                //清除已选中的通道
                _isMove = false;
                _maker.ClearDragItem();
                _maker.IsSelectedCaliper = false;
                picMainGraphics.Invalidate();
                
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("清除选择通道时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
            //设置特征点  里程修正
            if (e.Button == MouseButtons.Left)
            {
                if (wasIndex)
                {
                    SetIndexForm(sender, e);
                }
                try
                {
                    ShowMeterage(sender, e);
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("进行里程测量时失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 图层控制
        /// </summary>
        /// <param name="sender">图层控制按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiLayerControl_Click(object sender, EventArgs e)
        {
            try
            {
                SetOperationMenuItemChecked(false);
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
                //_LayerControlForm.Show();
                //_LayerControlForm.Activate();
                _LayerControlForm = new LayerControlForm(_maker);
                _LayerControlForm.Owner = this;
                if(_LayerControlForm.ShowDialog()== DialogResult.OK)
                {
                    InvokeScroolBar_Scroll(null, scrollBarPic.Value);
                }
                //_LayerControlForm.SelectedValueChangedEvent += _LayerControlForm_SelectedValueChangedEvent;

            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("图层控制错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }

        }

        /// <summary>
        /// 图层平移按钮触发事件
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">触发时间</param>
        private void tsmiLayerMove_Click(object sender, EventArgs e)
        {
            SetOperationMenuItemChecked(false);
            SetMeterageMenuChecked(false);
            picMainGraphics.Cursor = Cursors.Default;
            if (IsOpenCit())
            {
                _layerTranslationForm.Show();
            }
        }

        /// <summary>
        /// 进行截图
        /// </summary>
        /// <param name="sender">截图按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiCaptureScreen_Click(object sender, EventArgs e)
        {
            //SetOperationMenuItemChecked(false);
            //SetMeterageMenuChecked(false);
            //SetToolsChecked(false);

            Task t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(50);
                this.Invoke(new Action(() =>
                {
                    CaptureScreen();
                }));
            });
            t.ContinueWith((task) =>
            {
                if (task.Exception != null)
                {
                    MyLogger.logger.Error("截图错误：" + task.Exception.InnerException.Message + ",堆栈：" + task.Exception.InnerException.StackTrace);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);

        }

        /// <summary>
        /// 截图功能
        /// </summary>
        private void CaptureScreen()
        {
            try
            {
                bool isScorll = false;
                if (tsmiAutoScroll.Checked)
                {
                    isScorll = true;
                }
                Point point = this.PointToScreen(new Point(0, toolStripContainer1.TopToolStripPanel.Height + menuBar.Height));

                Image img = new Bitmap(picMainGraphics.Width - SystemInformation.Border3DSize.Width * 2,
                    picMainGraphics.Height);
                Graphics graph = Graphics.FromImage(img);

                Size size = new Size(img.Width,
                    img.Height);
                graph.CopyFromScreen(point, new Point(0, 0), size);
                if (isScorll == false)
                {
                    CaptureScreenForm form = new CaptureScreenForm(img);
                    form.WindowState = FormWindowState.Maximized;
                    form.ShowDialog(this);
                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.DefaultExt = "png";
                    dialog.Filter = "PNG Image (*.png)|*.png|GIF Image (*.gif)|*.gif|JPEG Image File (*.jpg)|*.jpg|Bitmaps (*.bmp)|*.bmp";
                    dialog.FileName = "波形截图-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        img.Save(dialog.FileName, ImageFormat.Png);
                        graph.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("截图失败：" + ex.Message + "，堆栈：" + ex.StackTrace);
            }
        }


        #region 通道设置

        /// <summary>
        /// 通道设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiWaveSetting_Click(object sender, EventArgs e)
        {
            if (IsOpenCit())
            {
                InvokeChannelDialog(0, -1);
            }
        }

        /// <summary>
        /// 显示通道设置窗口
        /// </summary>
        /// <param name="layerIndex">当前图层索引</param>
        /// <param name="ChannelIndex">当前通道索引，-2，通道配置设置， -1时通道设置，否则只显示匹配的通道</param>
        private void InvokeChannelDialog(int layerIndex,int ChannelIndex)
        {
            //channelDialog.Hide();
            Point p = new Point(layerIndex, ChannelIndex);
            channelDialog.Tag = p;
            channelDialog.TopLevel = true;
            channelDialog.Owner = this;
            channelDialog.Show();

            channelDialog.Activate();
            this.TopLevel = true;
        }

        #endregion

        #region 处理菜单

        #region 里程修正

        /// <summary>
        /// 里程修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetIndexForm(object sender, MouseEventArgs e)
        {
            if (!IsOpenCit())
            {
                return;
            }
            try
            {
                var milestoneList = _maker.WaveformDataList[0].MileList.milestoneList;

                int i = ((int)(e.X / ((picMainGraphics.ClientSize.Width - _maker.SingleChannelInfoWidth) / 1.0 / milestoneList.Count)));
                if (i >= milestoneList.Count)
                {
                    i = milestoneList.Count - 1;
                }

                using (IndexMainInfoAddForm iiaf = new IndexMainInfoAddForm(milestoneList[i].mFilePosition,
                    Convert.ToInt32(milestoneList[i].mKm), milestoneList[i].mMeter))
                {
                    DialogResult dr = iiaf.ShowDialog();
                    if (dr == DialogResult.OK && iiaf.Tag != null)
                    {
                        string sMeter = iiaf.Tag.ToString();
                        if (fViewIndexForm.Visible)
                        {
                            fViewIndexForm.Activate();
                            fViewIndexForm.SetIndexInfo(milestoneList[i].mFilePosition, sMeter);
                        }
                    }
                    else
                    {
                        fViewIndexForm.Activate();
                    }

                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("里程修正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 里程修正 手动校正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCorrectByHand_Click(object sender, EventArgs e)
        {
            SetManualCorrect(tsmiCorrectByHand.Checked);
        }

        private void SetManualCorrect(bool isChecked)
        {
            if (IsOpenCit())
            {
                fViewIndexForm.TopLevel = true;
                if (isChecked)
                {
                    ShowOpenOperateInfo("手动里程校正");
                    SetOperationMenuItemChecked(false);
                    SetMeterageMenuChecked(false);
                    tsmiCorrectByHand.Checked = isChecked;
                    tsbtnManualCorrect.Checked = isChecked;
                    picMainGraphics.Cursor = Cursors.Cross;
                    wasIndex = true;
                    fViewIndexForm.ShowFormModel = FormModel.Hide;
                    fViewIndexForm.Show();
                }
                else
                {
                    ShowCloseOperateInfo("手动里程校正");
                    tsmiCorrectByHand.Checked = isChecked;
                    tsbtnManualCorrect.Checked = isChecked;
                    picMainGraphics.Cursor = Cursors.Default;
                    wasIndex = false;
                    fViewIndexForm.Hide();
                    //SetOperationMenuItemEnable(true);
                }
            }
        }

        /// <summary>
        /// 里程修正 快速校正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCorrectQuickly_Click(object sender, EventArgs e)
        {
            try
            {
                SetOperationMenuItemChecked(false);
                SetMeterageMenuChecked(false);
                ShowOpenOperateInfo("快速里程校正");
                using (AutoIndexForm aif = new AutoIndexForm(_maker))
                {
                    aif.ShowDialog();
                }
                ShowCloseOperateInfo("快速里程校正");

            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("快速里程校正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 里程修正 查看校正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiViewCorrect_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    //显示校正结果
                    fViewIndexForm.ShowFormModel = FormModel.Show;
                    fViewIndexForm.TopLevel = true;
                    fViewIndexForm.Show();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("查看里程校正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        #endregion

        #region 无效标记

        /// <summary>
        /// 无效标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSignByHand_Click(object sender, EventArgs e)
        {
            SetManualInvaildSign(tsmiSignByHand.Checked);
        }

        private void SetManualInvaildSign(bool isChecked)
        {
            if (IsOpenCit())
            {
                if (isChecked)
                {
                    ShowOpenOperateInfo("手动无效标记");
                    SetOperationMenuItemChecked(false);
                    SetMeterageMenuChecked(false);
                    tsmiSignByHand.Checked = true;
                    tsbtnManualInvaildSign.Checked = true;
                    picMainGraphics.Cursor = Cursors.Cross;
                }
                else
                {
                    ShowCloseOperateInfo("手动无效标记");
                    picMainGraphics.Cursor = Cursors.Default;
                    SetOperationMenuItemChecked(false);
                    wasUnAreas = false;
                }
            }
        }

        /// <summary>
        /// 无效标记 查看标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiViewSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    //ShowOpenOperateInfo("查看无效标记");
                    //fInvalidDataForm.TopLevel = true;
                    fInvalidDataForm.Show();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("查看无效标记错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 展示无效标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowInvid(object sender, MouseEventArgs e)
        {
            if (tsmiSignByHand.Checked && e.Button == MouseButtons.Left)
            {
                //无效标注的起始位置
                if (!wasUnAreas)
                {
                    try
                    {
                        var waveFormData = _maker.WaveformDataList[0];
                        var milestoneList = waveFormData.MileList.milestoneList;
                        var channelList = waveFormData.ChannelList;

                        int iLayerAreaWidth = _maker.ControlWidth - _maker.SingleChannelInfoWidth;

                        iSPointX = ((int)(e.X / (iLayerAreaWidth / 1.0 / milestoneList.Count)));

                        if (iSPointX >= milestoneList.Count)
                        {
                            iSPointX = milestoneList.Count-1;
                        }
                        //int sampleNum = _maker.GetScrollValueToSampleNum(scrollBarPic.Value);
                        iSPointXPos = milestoneList[iSPointX].mFilePosition;
                        iSPointXKM = Convert.ToInt32(milestoneList[iSPointX].mKm);
                        iSPointXMeter = milestoneList[iSPointX].mMeter;
                        wasUnAreas = true;
                        ShowOpenOperateInfo("无效标注,操作：请再选择另外一个里程点");
                    }
                    catch (Exception ex)
                    {
                        MyLogger.logger.Error("手动无效标记1错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                        MessageBox.Show("错误：" + ex.Message);
                    }
                }
                else //无效标注的结束位置
                {
                    //无效区域
                    try
                    {
                        if (tsmiSignByHand.Checked && wasUnAreas && e.Button == MouseButtons.Left)
                        {
                            var waveFormData = _maker.WaveformDataList[0];
                            var milestoneList = waveFormData.MileList.milestoneList;
                            var channelList = waveFormData.ChannelList;

                            int iLayerAreaWidth = _maker.ControlWidth - _maker.SingleChannelInfoWidth;

                            int iEPointX = ((int)(e.X / (iLayerAreaWidth / 1.0 / milestoneList.Count)));

                            if (iEPointX >= milestoneList.Count)
                            {
                                iEPointX = milestoneList.Count - 1;
                            }
                            //int sampleNum = _maker.GetScrollValueToSampleNum(scrollBarPic.Value);
                            long iEPointXPos = milestoneList[iEPointX].mFilePosition;
                            fInvalidDataAddForm = new InvalidDataAddForm(_maker);
                            fInvalidDataAddForm.TopLevel = true;

                            fInvalidDataAddForm.textBoxStartPoint1.Text = iSPointXPos.ToString();
                            fInvalidDataAddForm.textBoxStartMile1.Text = (iSPointXKM + iSPointXMeter / 1000).ToString();
                            fInvalidDataAddForm.textBoxEndPoint.Text = iEPointXPos.ToString();

                            int iEPointXKM = Convert.ToInt32(milestoneList[iEPointX].mKm);
                            float iEPointXMeter = milestoneList[iEPointX].mMeter;

                            ShowOpenOperateInfo("无效标注,操作：操作完成");

                            fInvalidDataAddForm.textBoxEndMile.Text = (iEPointXKM + iEPointXMeter / 1000).ToString();
                            fInvalidDataAddForm.textBoxMemo.Text = "无效区段";
                            fInvalidDataAddForm.ShowDialog();

                            fInvalidDataForm.TopLevel = true;
                            
                            fInvalidDataForm.GetInvalidData();
                            
                        }
                        wasUnAreas = false;
                    }
                    catch (Exception ex)
                    {
                        MyLogger.logger.Error("无效标记2错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                        MessageBox.Show("错误：" + ex.Message);
                    }
                }
            }
        }


        #endregion

        #region IIC修正

        /// <summary>
        /// IIC修正 文件修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiReviseByFile_Click(object sender, EventArgs e)
        {
            if(IsOpenCit())
            {
                if (_maker.WaveformDataList.Count > 1)
                {
                    MessageBox.Show("不要在打开多个波形文件的基础上执行此功能！");
                    return;
                }
                SetOperationMenuItemChecked(false);
                SetMeterageMenuChecked(false);
                if (_maker.WaveformDataList.Count == 1)
                {
                    ShowOpenOperateInfo("IIC修正");
                    //处理
                    try
                    {
                        using (IICDataForm iicdf = new IICDataForm(_maker))
                        {
                            iicdf.ShowDialog();
                        }
                        ShowCloseOperateInfo("IIC修正");
                    }
                    catch (Exception ex)
                    {
                        MyLogger.logger.Error("IIC修正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                    }
                }
            }
            else
            {
                tsmiReviseByFile.Checked = false;
                tsbtnIICCorrect.Checked = false;
            }
        }

        /// <summary>
        /// IIC修正 文件查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiViewRevise_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    //ShowOpenOperateInfo("查看IIC修正");
                    fIICViewForm.TopLevel = true;
                    fIICViewForm.Show();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("查看IIC修正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        #endregion

        #region 标注

        /// <summary>
        /// 普通标注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiTaggingByWord_Click(object sender, EventArgs e)
        {
            SetWordTagging(tsmiTaggingByWord.Checked);
        }

        private void SetWordTagging(bool isChecked)
        {
            if (IsOpenCit())
            {
                if (isChecked)
                {
                    ShowOpenOperateInfo("添加标注");
                    SetOperationMenuItemChecked(false);
                    SetMeterageMenuChecked(false);
                    picMainGraphics.Cursor = Cursors.Cross;
                    tsmiTaggingByWord.Checked = true;
                    tsbtnTaggingByWord.Checked = true;
                }
                else
                {
                    ShowCloseOperateInfo("添加标注");
                    SetOperationMenuItemChecked(false);
                    picMainGraphics.Cursor = Cursors.Default;
                }
            }
            else
            {
                tsmiTaggingByWord.Checked = true;
                tsbtnTaggingByWord.Checked = true;
            }
        }

        #endregion

        /// <summary>
        /// 取消处理菜单项的控件选择
        /// </summary>
        /// <param name="b"></param>
        private void SetOperationMenuItemChecked(bool b)
        {
            tsmiSignByHand.Checked = b;
            tsmiTaggingByWord.Checked = b;
            tsmiCorrectByHand.Checked = b;
            tsbtnManualCorrect.Checked = b;
            tsbtnTaggingByWord.Checked = b;
            tsbtnManualInvaildSign.Checked = b;
        }

        /// <summary>
        /// 关闭工具栏
        /// </summary>
        /// <param name="isChecked"></param>
        private void SetToolsChecked(bool isChecked)
        {
            if (!isChecked)
            {
                tsbtnAutoScroll.Checked = isChecked;
                bgdAutoScrollWorker.CancelAsync();
            }
            else
            {
                tsbtnAutoScroll.Checked = isChecked;
            }
            tsbtnZoomInView.Checked = isChecked;
            tsmiAutoScroll.Checked = isChecked;
            tsmiViewZoom.Checked = isChecked;
        }

        #endregion


        #region 测量菜单

        /// <summary>
        ///是否选中同通道测量
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        private void SetGaugeByChannel(bool isChecked)
        {
            if (isChecked)
            {
                ShowOpenOperateInfo("同通道测量");
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                tsmiGaugeByChannel.Checked = true;
                tsbtnSameChannel.Checked = true;
                picMainGraphics.Cursor = Cursors.Cross;
                this.wasIndex = false;
            }
            else
            {
                ShowCloseOperateInfo("同通道测量");
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 是否选中同基线测量
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        private void SetGaugeByBaseline(bool isChecked)
        {
            if (isChecked)
            {
                ShowOpenOperateInfo("同基线测量");
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                tsmiGaugeByBaseline.Checked = true;
                tsbtnSameBaseline.Checked = true;
                picMainGraphics.Cursor = Cursors.Cross;
                this.wasIndex = false;
            }
            else
            {
                ShowCloseOperateInfo("同基线测量");
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 是否选中同图层测量
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        private void SetGaugeByLayer(bool isChecked)
        {
            if (isChecked)
            {
                ShowOpenOperateInfo("同图层测量");
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                tsmiGaugeByLayer.Checked = true;
                tsbtnSameLayer.Checked = true;
                picMainGraphics.Cursor = Cursors.Cross;
                this.wasIndex = false;
            }
            else
            {
                ShowCloseOperateInfo("同图层测量");
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 是否选中同里程测量
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        private void SetGaugeByMileage(bool isChecked)
        {
            if (isChecked)
            {
                ShowOpenOperateInfo("同里程测量");
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                tsmiGaugeByMileage.Checked = true;
                tsbtnSameMileage.Checked = true;
                picMainGraphics.Cursor = Cursors.Cross;
                this.wasIndex = false;
            }
            else
            {
                ShowCloseOperateInfo("同里程测量");
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 是否选中测量距离
        /// </summary>
        /// <param name="isChecked">是否选中</param>
        private void SetGaugeDistance(bool isChecked)
        {
            if (isChecked)
            {
                ShowOpenOperateInfo("距离测量");
                SetMeterageMenuChecked(false);
                SetOperationMenuItemChecked(false);
                tsmiGaugeDistance.Checked = true;
                tsbtnGaugeDistance.Checked = true;
                picMainGraphics.Cursor = Cursors.Cross;
                this.wasIndex = false;
            }
            else
            {
                ShowCloseOperateInfo("距离测量");
                SetMeterageMenuChecked(false);
                picMainGraphics.Cursor = Cursors.Default;
                this.wasIndex = false;
            }
            picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 设置测量菜单是否可用
        /// </summary>
        /// <param name="isChecked">是否可用</param>
        private void SetMeterageMenuChecked(bool isChecked)
        {
            tsmiGaugeByChannel.Checked = isChecked;
            tsmiGaugeByBaseline.Checked = isChecked;
            tsmiGaugeByLayer.Checked = isChecked;
            tsmiGaugeByMileage.Checked = isChecked;
            tsmiGaugeDistance.Checked = isChecked;

            tsbtnSameChannel.Checked = isChecked;
            tsbtnSameBaseline.Checked = isChecked;
            tsbtnSameLayer.Checked = isChecked;
            tsbtnSameMileage.Checked = isChecked;
            tsbtnGaugeDistance.Checked = isChecked;
        }

        /// <summary>
        /// 同通道测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGaugeByChannel_Click(object sender, EventArgs e)
        {
            SetGaugeByChannel(tsmiGaugeByChannel.Checked);
        }

        /// <summary>
        /// 同基线测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGaugeByBaseline_Click(object sender, EventArgs e)
        {
            SetGaugeByBaseline(tsmiGaugeByBaseline.Checked);
        }

        /// <summary>
        /// 同图层测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGaugeByLayer_Click(object sender, EventArgs e)
        {
            SetGaugeByLayer(tsmiGaugeByLayer.Checked);
            
        }

        /// <summary>
        /// 同里程测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGaugeByMileage_Click(object sender, EventArgs e)
        {
            SetGaugeByMileage(tsmiGaugeByMileage.Checked);
            
        }

        /// <summary>
        /// 距离测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiGaugeDistance_Click(object sender, EventArgs e)
        {
            SetGaugeDistance(tsmiGaugeDistance.Checked);
        }

        /// <summary>
        /// 展示测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMeterage(object sender, MouseEventArgs e)
        {
            //picMainGraphics.Invalidate();

            if (tsmiGaugeDistance.Checked && wasMeterageLength && e.Button == MouseButtons.Left)
            {
                if (startMeterageLengthPoint.X > 0 && lastMeterageLengthPoint.X > 0)
                {
                    var waveFormData = _maker.WaveformDataList[0];
                    var milestoneList = waveFormData.MileList.milestoneList;
                    var channelList = waveFormData.ChannelList;

                    int iLayerAreaWidth = _maker.ControlWidth - _maker.SingleChannelInfoWidth;

                    int iSPointX = ((int)(startMeterageLengthPoint.X / (iLayerAreaWidth / 1.0 / milestoneList.Count)));

                    if (iSPointX >= milestoneList.Count)
                    {
                        iSPointX = milestoneList.Count - 1;
                    }
                    int iEPointX = ((int)(lastMeterageLengthPoint.X / (iLayerAreaWidth / 1.0 / milestoneList.Count)));

                    if (iEPointX >= milestoneList.Count)
                    {
                        iEPointX = milestoneList.Count - 1;
                    }
                    MessageBox.Show("从K" + milestoneList[iSPointX].mKm.ToString()
                        + "+" + (milestoneList[iSPointX].mMeter ).ToString() + " \n到K"
                        + milestoneList[iEPointX].mKm.ToString()
                        + "+" + (milestoneList[iEPointX].mMeter).ToString() + "\n长度为:" +
                        Math.Abs((milestoneList[iSPointX].GetMeter() -
                        milestoneList[iEPointX].GetMeter())).ToString("f3") + "米\n包含采样点数:" +
                        (((milestoneList[iEPointX].mFilePosition - milestoneList[iSPointX].mFilePosition) / (waveFormData.CitFile.iChannelNumber * 2))).ToString(""));
                }
                startMeterageLengthPoint.X = -1;
                lastMeterageLengthPoint.X = -1;
                wasMeterageLength = false;
            }

            if ((tsmiGaugeByChannel.Checked || tsmiGaugeByBaseline.Checked 
                || tsmiGaugeByLayer.Checked || tsmiGaugeByMileage.Checked)
                && picMainGraphics.Cursor == Cursors.Cross && e.Button == MouseButtons.Left)//测量位置
            {
                //测量数据
                using (MeterageForm mf = new MeterageForm(_maker))
                {
                    int iWidth = picMainGraphics.ClientSize.Width - _maker.SingleChannelInfoWidth;
                    Point p = new Point();
                    p.X = e.X;
                    p.Y = e.Y;
                    int iChecked = 0;
                    if (tsmiGaugeByChannel.Checked)
                    {
                        //同通道测量
                        iChecked = 1;
                    }
                    else if (tsmiGaugeByBaseline.Checked)
                    {
                        //同基线测量
                        iChecked = 2;
                    }
                    else if (tsmiGaugeByLayer.Checked)
                    {
                        //同图层测量
                        iChecked = 3;
                    }
                    else
                    {
                        //同里程测量
                        iChecked = 4;
                    }
                    mf.Tag = iChecked.ToString() + "," + p.X.ToString() + "," + p.Y.ToString() + "," + iWidth.ToString();
                    mf.ShowDialog();
                }
            }
        }




        #endregion

        /// <summary>
        /// 判断是否打开波形文件
        /// </summary>
        /// <returns>成功:true,失败：false</returns>
        private bool IsOpenCit()
        {
            if (_maker != null && _maker.WaveformDataList.Count < 1)
            {
                MessageBox.Show("请先打开一个波形文件");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 显示台账信息
        /// </summary>
        /// <param name="sender">显示台账信息按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiViewAccountByList_Click(object sender, EventArgs e)
        {
            try
            {
                if (_maker != null && _maker.WaveformDataList.Count > 0)
                {
                    _accountInfoForm.Show();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("显示台账信息错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 台账信息绘制事件，台账主要显示的区域
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void picSecondGraphics_Paint(object sender, PaintEventArgs e)
        {
            if (_maker.AccountDatabaseList.Count > 0)
            {
                try
                {
                    Bitmap accountImage = _maker.DrawAccountDatabase();
                    if (accountImage != null)
                    {
                        e.Graphics.DrawImage(accountImage, 0, 0, splitPictrueContainer.Panel1.Width, _maker.AccountInfoHeight);
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("绘制台账信息错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 重新刷新台账的显示类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAccountDisplay_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                tsmiAccountDisplayByCurve.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByCurve.Text).IsCheck;
                tsmiAccountDisplayBySlope.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayBySlope.Text).IsCheck;
                tsmiAccountDisplayByTumout.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByTumout.Text).IsCheck;
                tsmiAccountDisplayByVelocitySection.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByVelocitySection.Text).IsCheck;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("加载台账信息失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("加载台账配置信息失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 主窗体大小变化时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (_maker != null)
                {
                    if (!_maker.IsZoomInView)
                    {
                        _maker.ControlHeight = picMainGraphics.Height;
                        _maker.ControlWidth = picMainGraphics.Width;
                        _maker.ReCalculateDrawSize();
                        _maker.ReCalculateChannelInfoSize();
                    }
                }
                if (!splitPictrueContainer.Panel1Collapsed)
                {
                    if (panelAccount.VerticalScroll.Visible)
                    {
                        panelRight.Width = _channelWidth - SystemInformation.VerticalScrollBarWidth;
                    }
                    else
                    {
                        panelRight.Width = _channelWidth;
                    }
                    labMileageInfoShow.Left = splitPictrueContainer.Panel2.Width / 2 - labMileageInfoShow.Width / 2;
                    int top = (splitPictrueContainer.Panel2.Height / 2 - labMileageInfoShow.Height / 2) - splitPictrueContainer.Panel1.Height;
                    if (top > splitPictrueContainer.Panel2.Height)
                    {
                        top = splitPictrueContainer.Panel2.Height - labMileageInfoShow.Height / 2 - 30;
                    }
                    labMileageInfoShow.Top = top;
                }
                else
                {
                    labMileageInfoShow.Left = splitPictrueContainer.Panel2.Width / 2 - labMileageInfoShow.Width / 2;
                    labMileageInfoShow.Top = splitPictrueContainer.Panel2.Height / 2 - labMileageInfoShow.Height / 2;
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("主窗体大小错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }

        }

        /// <summary>
        /// 分割条拖动时触发
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">触发参数</param>
        private void splitPictrueContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MainForm_Resize(sender, e);
        }

        /// <summary>
        /// 按里程导出
        /// </summary>
        /// <param name="sender">按里程导出按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiExportByMileage_Click(object sender, EventArgs e)
        {
            if (IsOpenCit())
            {
                try
                {
                    if(_maker.WaveformDataList.Count>1)
                    {
                        if (MessageBox.Show("当前导出只会导出第一层数据，是否继续？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            ExportDataByMileageForm form = new ExportDataByMileageForm(_maker);
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        ExportDataByMileageForm form = new ExportDataByMileageForm(_maker);
                        form.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("按里程导出错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// GEO转CIT
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiGEO2CIT_Click(object sender, EventArgs e)
        {
            try
            {
                Geo2CitConvertForm convertForm = new Geo2CitConvertForm();
                convertForm.Owner = this;
                convertForm.TopLevel = true;
                convertForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("GEO转CIT错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 按管界导出
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiExportByBoundary_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    ExportDataByBoundaryForm form = new ExportDataByBoundaryForm(_maker);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
                MyLogger.logger.Error("按管界导出错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 打开文件列表
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">触发参数</param>
        private void tsmiOpenMediaAndRecent_Click(object sender, EventArgs e)
        {
            try
            {
                openFileForm.ConfigData = WaveformConfigData;
                openFileForm.LoadTreeData();
                if (openFileForm.Visible == false)
                {
                    openFileForm.Show();
                    openFileForm.Activate();
                }
                else
                {
                    openFileForm.Activate();
                }
                
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("打开目录失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 绘制台账信息
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void panelAccount_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.TranslateTransform(panelAccount.AutoScrollPosition.X, panelAccount.AutoScrollPosition.Y);
                e.Graphics.Clear(Color.White);
                if (_maker.AccountDatabaseList.Count > 0)
                {
                    Bitmap accountImage = _maker.DrawAccountDatabase();
                    if (accountImage != null)
                    {
                        e.Graphics.DrawImage(accountImage, 0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("绘制台账错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 滚动滚动条时，刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelAccount_Scroll(object sender, ScrollEventArgs e)
        {
            panelAccount.Invalidate();
        }

        /// <summary>
        /// 查看标记
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">参数</param>
        private void tsmiViewTagging_Click(object sender, EventArgs e)
        {
            try
            {
                LabelInfoForm form = new LabelInfoForm(_maker);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("查看标记错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 快捷键打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnOpenFile_Click(object sender, EventArgs e)
        {
            tsmiOpenFile_Click(sender, e);
        }

        /// <summary>
        /// 快捷键导出按管界
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">事件</param>
        private void tsbtnExportByBoundary_Click(object sender, EventArgs e)
        {
            tsmiExportByBoundary_Click(sender, e);
        }

        /// <summary>
        /// 快捷键截屏
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void tsbtnCaptureScreen_Click(object sender, EventArgs e)
        {
            tsmiCaptureScreen_Click(sender, e);
        }

        /// <summary>
        /// 快捷键通知显示
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void tsmiShow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            notifyIcon.Visible = false;
            this.Activate();
        }

        /// <summary>
        /// 通知推出
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void tsmiExitApp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                notifyIcon.Visible = false;
                Environment.Exit(Environment.ExitCode);
            }
        }

        /// <summary>
        /// 双击通知栏图标
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsmiShow_Click(sender, null);
        }

        /// <summary>
        /// 快捷键打开列表
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void tsbtnOpenFileList_Click(object sender, EventArgs e)
        {
            tsmiOpenMediaAndRecent_Click(sender, e);
        }

        /// <summary>
        /// 快捷键按里程导出
        /// </summary>
        /// <param name="sender">触发按钮</param>
        /// <param name="e">参数</param>
        private void tsbtnExportByMileage_Click(object sender, EventArgs e)
        {
            tsmiExportByMileage_Click(sender, e);
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="text"></param>
        private void ShowOperateInfo(string text)
        {
            tslabelPromptInfo.Text = "提示：" + text;
        }

        /// <summary>
        /// 显示正在进行文本状态
        /// </summary>
        /// <param name="text"></param>
        private void ShowOpenOperateInfo(string text)
        {
            ShowOperateInfo("当前正在进行[" + text + "]");
        }

        /// <summary>
        /// 显示已关闭信息
        /// </summary>
        /// <param name="text"></param>
        private void ShowCloseOperateInfo(string text)
        {
            ShowOperateInfo("已关闭[" + text + "]");
        }

        /// <summary>
        /// 关于对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }


        private void picMainGraphics_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    WaveformDragData dragData = _maker.SelectSingleItem(e.X, e.Y);
                    if (dragData != null)
                    {
                        InvokeChannelDialog(dragData.SelectDragDataIndex, dragData.SelectDragChannel);
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("显示单个通道设置错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                }
            }
            //进行标注
            if (tsmiTaggingByWord.Checked && e.Button == MouseButtons.Left)
            {
                try
                {
                    int index = _maker.GetLocationIndex(e.X);
                    LabelInfoAddForm form = new LabelInfoAddForm(_maker, _maker.WaveformDataList[0].MileList.milestoneList[index].mFilePosition, e.Y);
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("手动标注失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                }
            }

            //无效标记
            ShowInvid(sender, e);
        }

        /// <summary>
        /// 通道自动排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAutoArrange_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    _maker.AutoArrange();
                    picMainGraphics.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("自动排列错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 同通道测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSameChannel_Click(object sender, EventArgs e)
        {
            SetGaugeByChannel(tsbtnSameChannel.Checked);
        }
        /// <summary>
        /// 同基线测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSameBaseline_Click(object sender, EventArgs e)
        {
            SetGaugeByBaseline(tsbtnSameBaseline.Checked);
        }
        /// <summary>
        /// 同图层测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSameLayer_Click(object sender, EventArgs e)
        {
            SetGaugeByLayer(tsbtnSameLayer.Checked);
        }
        /// <summary>
        /// 同里程测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSameMileage_Click(object sender, EventArgs e)
        {
            SetGaugeByMileage(tsbtnSameMileage.Checked);
        }
        /// <summary>
        /// 距离测量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnGaugeDistance_Click(object sender, EventArgs e)
        {
            SetGaugeByMileage(tsbtnGaugeDistance.Checked);
        }
        /// <summary>
        /// 自动滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAutoScroll_Click(object sender, EventArgs e)
        {
            SetAutoScroll(tsbtnAutoScroll.Checked);
        }
        /// <summary>
        /// 自定义工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiConfigToolsrip_Click(object sender, EventArgs e)
        {
            try
            {
                ToolstipConfigForm form = new ToolstipConfigForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("自定义工具栏失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 手动里程修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnManualCorrect_Click(object sender, EventArgs e)
        {
            SetManualCorrect(tsbtnManualCorrect.Checked);
        }
        /// <summary>
        /// 手动无效标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnManualInvaildSign_Click(object sender, EventArgs e)
        {
            bool isCheked = tsbtnManualInvaildSign.Checked;
            SetManualInvaildSign(isCheked);
        }
        /// <summary>
        /// 手动标注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnTaggingByWord_Click(object sender, EventArgs e)
        {
            SetWordTagging(tsbtnTaggingByWord.Checked);
        }
        /// <summary>
        /// 显示图像标注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiShowBaggingByDrawing_Click(object sender, EventArgs e)
        {
            SetShowBagging(tsmiShowBaggingByDrawing.Checked);
        }
        /// <summary>
        /// 是否显示图形标注
        /// </summary>
        /// <param name="isChecked">true：显示，false：不显示</param>
        private void SetShowBagging(bool isChecked)
        {
            if(isChecked)
            {
                _isShowBagging = isChecked;
            }
            else
            {
                _isShowBagging = isChecked;
            }
            picMainGraphics.Invalidate();
        }
        /// <summary>
        /// 主窗体快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Left)
            {
                MoveMainLayer(0, 0);
                return;
            }
            else if (e.Alt && e.KeyCode == Keys.Right)
            {
                MoveMainLayer(1, 0);
                return;
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                MoveMainLayer(0, 1);
                return;
            }
            else if (e.Control && e.KeyCode == Keys.Right)
            {
                MoveMainLayer(1, 1);
                return;
            }
        }

        /// <summary>
        /// 快捷键移动图层
        /// </summary>
        /// <param name="dir">方向</param>
        /// <param name="size">移动步长</param>
        private void MoveMainLayer(int dir, int size)
        {
            try
            {
                if (IsOpenCit())
                {
                    int scrollValue = 0;
                    int stepSize = 0;
                    if (size == 0)
                    {
                        stepSize = scrollBarPic.SmallChange;
                    }
                    else
                    {
                        stepSize = scrollBarPic.LargeChange;
                    }
                    if (dir == 0)
                    {

                        if (scrollBarPic.Value - stepSize < scrollBarPic.Minimum)
                        {
                            return;
                        }
                        scrollValue = scrollBarPic.Value - stepSize;
                    }
                    else
                    {
                        if (scrollBarPic.Value + stepSize > scrollBarPic.Maximum)
                        {
                            return;
                        }
                        scrollValue = scrollBarPic.Value + stepSize;
                    }
                    //object sender = new object();
                    InvokeScroolBar_Scroll(null, scrollValue);
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("快捷键移动波形失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 拦截快捷键，否则快捷键会被其它窗体拦截
        /// </summary>
        /// <param name="keyData">快捷键</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down ||
              keyData == Keys.Left || keyData == Keys.Right)
                return false;
            else
                return base.ProcessDialogKey(keyData);

        }
        /// <summary>
        /// 区域放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnZoomInView_Click(object sender, EventArgs e)
        {
            if (IsOpenCit() && _maker.IsZoomInView == false)
            {
                BeginZoomInView(tsbtnZoomInView.Checked);
            }
            else
            {
                tsmiViewZoom.Checked = false;
                tsbtnZoomInView.Checked = false;
                _isZoomView = false;
            }
        }
        /// <summary>
        /// 关闭窗体事件，必须手动关闭，否则会被其它窗体拦截而关闭不了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
        /// <summary>
        /// 确认关闭窗体，否则会被其它窗体拦截
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            try
            {
                if (WaveformConfigData.IsAutoSaveConfig)
                {
                    if (_maker != null && _maker.WaveformDataList.Count > 0)
                    {
                        for (int i = 0; i < _maker.WaveformDataList.Count; i++)
                        {
                            if (i == 0 || (i != 0 && _maker.WaveformDataList[i].WaveConfigFilePath != _maker.WaveformDataList[0].WaveConfigFilePath))
                            {
                                ChannelManager.SaveChannelsConfig(_maker.WaveformDataList[i].WaveConfigFilePath, _maker.WaveformDataList[i].ChannelList);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("保存通道配置文件失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }
        /// <summary>
        /// 智能里程校正（相关性里程修正）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCorrectByAI_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    if (_maker.WaveformDataList.Count > 0 && !_maker.WaveformDataList[0].IsLoadIndex)
                    {
                        MessageBox.Show("第一层数据未添加索引或索引数据为空，无法修正其它cit文件！");
                        return;
                    }
                    CorrelationForm form = new CorrelationForm(_maker);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("智能里程修正错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("错误：" + ex.Message);
            }

        }
        /// <summary>
        /// 通道参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChannelSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsOpenCit())
                {
                    //InvokeChannelDialog(0, -2);
                }
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("通道配置错误：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 重新刷新台账的显示类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAccountDisplay_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                tsmiAccountDisplayByCurve.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByCurve.Text).IsCheck;
                tsmiAccountDisplayBySlope.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayBySlope.Text).IsCheck;
                tsmiAccountDisplayByTumout.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByTumout.Text).IsCheck;
                tsmiAccountDisplayByVelocitySection.Checked = WaveformConfigData.Accouts.AccountDescList.Find(p => p.Name == tsmiAccountDisplayByVelocitySection.Text).IsCheck;
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("加载台账信息失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("加载台账配置信息失败：" + ex.Message);
            }
        }
    }
}
