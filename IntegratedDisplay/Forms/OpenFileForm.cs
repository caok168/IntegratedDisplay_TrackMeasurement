using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.AdvTree;
using CitFileSDK;
using IntegratedDisplay.Models;
using System.Threading.Tasks;
using IntegratedDisplay.ListviewSortHelper;
using System.Threading;

namespace IntegratedDisplay
{
    public partial class OpenFileForm : Form
    {
        /// <summary>
        /// 配置文件数据
        /// </summary>
        public ConfigData ConfigData { get; set; }

        /// <summary>
        /// 文件过滤
        /// </summary>
        private const string _fileFilter = "*.cit";

        /// <summary>
        /// cit文件处理类
        /// </summary>
        private CITFileProcess _citProcess = new CITFileProcess();

        private FormWindowState _formState = FormWindowState.Maximized;

        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="configData"></param>
        public OpenFileForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开时加载配置文件的配置
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void OpenFileForm_Load(object sender, EventArgs e)
        {

            this.listViewFiles.ListViewItemSorter = new ListViewColumnSorter();
            this.listViewFiles.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
            listViewFiles.AutoResizeColumn(listViewFiles.Columns.Count - 1, ColumnHeaderAutoResizeStyle.ColumnContent);

            LoadTreeData();


        }

        /// <summary>
        /// 加载树结构数据
        /// </summary>
        public void LoadTreeData()
        {
            try
            {
                treePathAndMedia.Nodes.Clear();
                List<string> nodes = new List<string>() { "添加更多...", "波形库", "最近打开的文件" };
                dockContainerItem1.Text = "波形库";

                treePathAndMedia.BeginUpdate();
                //初始化根节点
                foreach (var s in nodes)
                {
                    Node node = new Node(s);
                    treePathAndMedia.Nodes.Add(node);
                }

                if (ConfigData != null)
                {
                    //初始化媒体库
                    if (!string.IsNullOrEmpty(ConfigData.MediaPath))
                    {
                        if (Directory.Exists(ConfigData.MediaPath))
                        {
                            //Node mediaFilesNode = new Node();
                            treePathAndMedia.Nodes[1].Nodes.AddRange(GetFiles(ConfigData.MediaPath, _fileFilter));
                            //treePathAndMedia.Nodes[1].Nodes.Add(mediaFilesNode);
                        }
                    }
                    //初始化目录节点
                    if (ConfigData.DirPaths.PathList.Count > 0)
                    {
                        List<string> noExistDir = new List<string>();
                        foreach (string dir in ConfigData.DirPaths.PathList)
                        {
                            if (Directory.Exists(dir))
                            {
                                Node dirFilesNode = new Node();
                                DirectoryInfo folder = new DirectoryInfo(dir);
                                dirFilesNode.Text = folder.Name;
                                dirFilesNode.Tag = folder.FullName;
                                string[] files = Directory.GetFiles(dir, _fileFilter, SearchOption.TopDirectoryOnly);
                                foreach (string file in files)
                                {
                                    Node chldNode = new Node();
                                    string[] splitString = file.Split('\\');
                                    chldNode.Text = splitString[splitString.Length - 1];
                                    chldNode.Tag = file;
                                    dirFilesNode.Nodes.Add(chldNode);
                                }
                                Node loadingNode = new Node();
                                loadingNode.Text = "加载子目录....";
                                //loadingNode.ImageExpanded= Image.FromFile("blue-loading.gif");
                                //loadingNode.Image = Image.FromFile("blue-loading.gif");
                                //loadingNode.Tag = "loading";
                                dirFilesNode.Nodes.Add(loadingNode);
                                treePathAndMedia.Nodes.Insert(2, dirFilesNode);
                            }
                            else
                            {
                                noExistDir.Add(dir);
                            }
                        }
                        if (noExistDir.Count > 0)
                        {
                            MyLogger.logger.Info("有：" + noExistDir.Count + "条目录不存在，准备删除");
                            foreach (var item in noExistDir)
                            {
                                MyLogger.logger.Info("删除目录：" + item);
                                ConfigData.DirPaths.PathList.Remove(item);
                                
                            }
                            ConfigManger.SaveConfigData(ConfigData);
                        }


                    }
                    //初始化最近访问列表
                    if (ConfigData.RecentFiles.Files.Count > 0)
                    {
                        Node dirFilesNode = treePathAndMedia.Nodes[treePathAndMedia.Nodes.Count - 1];
                        //最近访问的文件个数小于配置中的个数
                        if (ConfigData.RecentFiles.Files.Count > ConfigData.RecentFiles.FilesCount)
                        {
                            for (int i = 0; i < ConfigData.RecentFiles.FilesCount; i++)
                            {
                                string[] split = ConfigData.RecentFiles.Files[i].Split('\\');
                                Node fileNode = new Node(split[split.Length - 1]) { Tag = ConfigData.RecentFiles.Files[i] };
                                dirFilesNode.Nodes.Add(fileNode);
                            }
                        }
                        else
                        {
                            foreach (var file in ConfigData.RecentFiles.Files)
                            {
                                string[] split = file.Split('\\');
                                Node fileNode = new Node(split[split.Length - 1]) { Tag = file };
                                dirFilesNode.Nodes.Add(fileNode);
                            }
                        }
                        //if (IsDirectLoad)
                        //{
                        //    CheckNodeAndLoad(dirFilesNode.Nodes[0]);
                        //}
                    }

                }
                treePathAndMedia.Nodes[treePathAndMedia.Nodes.Count - 1].Expand();
                Node addMoreNode = treePathAndMedia.Nodes[0];
                addMoreNode.Nodes.Add(new Node("添加文件"));
                addMoreNode.Nodes.Add(new Node("添加文件夹"));
                addMoreNode.Expanded = false;
                treePathAndMedia.EndUpdate();
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("初始化目录失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("加载目录失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 根据路径和过滤后缀填充Node
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        /// <param name="node">需要填充的节点</param>
        /// <param name="filefilter">过滤后缀名</param>
        private Node[] GetFiles(string filePath, string filefilter)
        {
            try
            {
                List<Node> nodes = new List<Node>();
                DirectoryInfo folder = new DirectoryInfo(filePath);
                //node.Text = folder.Name;
                //node.Tag = folder.FullName;

                FileInfo[] chldFiles = folder.GetFiles(filefilter);//"*.*"
                foreach (FileInfo chlFile in chldFiles)
                {
                    Node chldNode = new Node();
                    chldNode.Text = chlFile.Name;
                    chldNode.Tag = chlFile.FullName;
                    //node.Nodes.Add(chldNode);
                    nodes.Add(chldNode);
                }
                DirectoryInfo[] chldFolders = folder.GetDirectories();
                foreach (DirectoryInfo chldFolder in chldFolders)
                {
                    //Node chldNode = new Node();
                    //chldNode.Text = folder.Name;
                    //chldNode.Tag = folder.FullName;
                    ////node.Nodes.Add(chldNode);
                    //nodes.Add(chldNode);
                    Node[] subNodes = GetFiles(chldFolder.FullName, filefilter);
                    nodes.AddRange(subNodes);
                }
                return nodes.ToArray();
            }
            catch(Exception ex)
            {
                throw new Exception("初始化节点失败："+ ex.Message);
            }

        }

        private Node[] GetSubFiles(string filePath, string filefilter)
        {
            try
            {
                List<Node> node = new List<Node>();
                DirectoryInfo folder = new DirectoryInfo(filePath);
                //node.Text = folder.Name;

                //FileInfo[] chldFiles = folder.GetFiles(filefilter);//"*.cit*"
                //foreach (FileInfo chlFile in chldFiles)
                //{
                //    Node chldNode = new Node();
                //    chldNode.Text = chlFile.Name;
                //    chldNode.Tag = chlFile.FullName;
                //    node.Nodes.Add(chldNode);
                //}
                DirectoryInfo[] chldFolders = folder.GetDirectories();
                foreach (DirectoryInfo chldFolder in chldFolders)
                {
                    //Node chldNode = new Node();
                    //chldNode.Text = folder.Name;
                    //chldNode.Tag = folder.FullName;
                    //node.Add(chldNode);
                    node.AddRange(GetFiles(chldFolder.FullName, filefilter));
                }
                return node.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("初始化节点失败：" + ex.Message);
            }

        }


        /// <summary>
        /// 打开时封装数据并传递到Main窗口
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            List<WavefromData> waveDataList = new List<WavefromData>();
            for (int i = 0; i < listViewFiles.CheckedItems.Count;i++ )
            {
                WavefromData waveData = new WavefromData();
                string citFullPath = "";
                string indexFilePath = string.Empty;
                try
                {
                   
                    int directoryIndex = listViewFiles.Columns.Count - 2;
                    int fileNameIndex = listViewFiles.Columns.Count - 4;
                    ListViewItem item = listViewFiles.CheckedItems[i];
                   
                    if (ckbLoadIndex.Checked)
                    {
                        waveData.IsLoadIndex = true;
                    }
                    if (item.SubItems[directoryIndex].Text.EndsWith("\\"))
                    {
                        citFullPath = item.SubItems[directoryIndex].Text + item.SubItems[fileNameIndex].Text;
                        waveData.CitFilePath = citFullPath;
                        indexFilePath = item.SubItems[directoryIndex].Text + Path.GetFileNameWithoutExtension(item.SubItems[fileNameIndex].Text) + ".idf";
                    }
                    else
                    {
                        citFullPath = item.SubItems[directoryIndex].Text + "\\" + item.SubItems[fileNameIndex].Text;
                        waveData.CitFilePath = citFullPath;
                        indexFilePath = item.SubItems[directoryIndex].Text + "\\" + Path.GetFileNameWithoutExtension(item.SubItems[fileNameIndex].Text) + ".idf";
                    }
                    if (ConfigData.RecentFiles.Files.Contains(citFullPath))
                    {
                        ConfigData.RecentFiles.Files.Remove(citFullPath);
                    }
                    ConfigData.RecentFiles.Files.Insert(0, citFullPath);
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("初始化cit文件时失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                    if (listViewFiles.CheckedItems.Count > 1)
                    {
                        MessageBox.Show("无法解析" + citFullPath + "文件,系统将自动跳过!");
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("无法解析" + citFullPath + ",文件可能已损坏！");
                        return;
                    }
                }
                try
                {
                    waveData.WaveIndexFilePath = indexFilePath;
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("初始化索引文件时失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                    MessageBox.Show(ex.Message);
                }

                if (ConfigData.WaveConfigs.WaveConfigList.Count > 0)
                {
                    WaveConfigDesc waveConfig = ConfigData.WaveConfigs.WaveConfigList.Find(w => w.WaveConfigIndex == (i + 1));
                    if (waveConfig != null)
                    {
                        if (!string.IsNullOrEmpty(waveConfig.WaveConfigFile) && File.Exists(waveConfig.WaveConfigFile))
                        {
                            try
                            {
                                waveData.WaveConfigFilePath = waveConfig.WaveConfigFile;
                            }
                            catch (Exception ex)
                            {
                                
                                MyLogger.LogError("加载波形配置路径错误", ex);
                                MessageBox.Show(ex.Message);
                                waveData.WaveConfigFilePath = string.Empty;
                                waveConfig.WaveConfigFile = string.Empty;
                                
                            }
                            finally
                            {
                               
                            }
                        }
                    }
                }
                waveDataList.Add(waveData);
            }
            if (waveDataList.Count > 0)
            {
                try
                {
                    ConfigManger.SaveConfigData(ConfigData);
                }
                catch(Exception ex)
                {
                    MyLogger.LogError("保存错误", ex);
                }
                this.Visible = false;
                this.ckbLoadIndex.Checked = false;
                this.listViewFiles.Items.Clear();
                MainForm.sMainform.LoadLayerInfo(waveDataList);
                MainForm.sMainform.Activate();
            }
            else
            {
                MessageBox.Show("请先选择一个Cit文件！");
            }
            
        }

        /// <summary>
        /// 检查节点的文件并加载
        /// </summary>
        /// <param name="node">选择的文件节点</param>
        private void CheckNodeAndLoad(Node node)
        {
            if (node.Level < 1)
            {
                return;
            }
            if (node.Tag == null)
            {
                if (node.Text == "添加文件")
                {
                    MainForm.sMainform.tsmiOpenFile_Click(null, null);
                }
                else if (node.Text == "添加文件夹")
                {
                    MainForm.sMainform.tsmiOpenPathOrMedia_Click(null, null);
                }
                else
                {
                    node.Text = "";
                    node.HostedControl = labLoading;
                    labLoading.Visible = true;
                    Task<Node[]> t = Task.Factory.StartNew<Node[]>(new Func<Node[]>(() => { return GetSubFiles(node.Parent.Tag.ToString(), _fileFilter); }));
                    t.ContinueWith(new Action<Task<Node[]>>((task) =>
                    {
                        this.Invoke(new Action(() => 
                        {
                            labLoading.Visible = false;
                            Node parentNode = node.Parent;
                            parentNode.Nodes.Remove(node);
                            parentNode.Nodes.AddRange(t.Result);
                        }));
                    }));
                     
                    
                }
            }
            else if (File.Exists(node.Tag.ToString()))
            {
                for (int i = 0; i < listViewFiles.Items.Count; i++)
                {
                    int directoryIndex = listViewFiles.Columns.Count - 2;
                    int fileNameIndex = listViewFiles.Columns.Count - 4;
                    string existFilePath = string.Empty;
                    if (listViewFiles.Items[i].SubItems[directoryIndex].Text.EndsWith("\\"))
                    {
                        existFilePath = listViewFiles.Items[i].SubItems[directoryIndex].Text + listViewFiles.Items[i].SubItems[fileNameIndex].Text;
                    }
                    else
                    {
                        existFilePath = listViewFiles.Items[i].SubItems[directoryIndex].Text + "\\" + listViewFiles.Items[i].SubItems[fileNameIndex].Text;
                    }
                    if (node.Tag.ToString() == existFilePath)
                    {
                        node.Tooltip = "文件已经添加完毕";
                        return;
                    }
                }
                DisplayInListview(node.Tag.ToString());
            }
            else if (node.Parent.Index > 1)
            {
                if (MessageBox.Show("文件不存在，是否从列表中移除？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    bool isSave = false;
                    if (node.Parent.Index != 0)
                    {
                        //最近访问列表移除该项
                        if (node.Parent.Index == treePathAndMedia.Nodes.Count-1)
                        {
                            int index = node.Parent.Nodes.IndexOf(node);
                            node.Parent.Nodes.Remove(node);
                            ConfigData.RecentFiles.Files.Remove(node.TagString);
                            isSave = true;
                        }
                        else
                        {
                            //检查目录个数，如果就一个文件，移除该目录,需要保存配置
                            if (node.Parent.Nodes.Count > 1)
                            {
                                node.Parent.Nodes.Remove(node);
                            }
                            else
                            {
                                treePathAndMedia.Nodes.Remove(node.Parent);
                                isSave = true;
                            }
                        }
                    }
                    else
                    {
                        //媒体库只是移除该项
                        node.Parent.Nodes.Remove(node);
                    }
                    if (isSave)
                    {
                        try
                        {
                            ConfigManger.SaveConfigData(ConfigData);
                        }
                        catch(Exception ex)
                        {
                            MyLogger.logger.Error("保存配置数据时失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                        }
                    }
                }
            }
        }

        public void DisplayInListview(string citPath)
        {
            FileInformation fileInfo = null;
            double startMileage = 0;
            double endMileage = 0;
            double totalMileage = 0;
            try
            {
                fileInfo = _citProcess.GetFileInformation(citPath);
                long[] startAndEnd = _citProcess.GetPositons(citPath);
                Milestone start= _citProcess.GetStartMilestone(citPath);
                Milestone end = _citProcess.GetEndMilestone(citPath);
                startMileage = start.GetMeter() / 1000;
                endMileage = end.GetMeter() / 1000;
                long sampleCount= _citProcess.GetSampleCountByRange(citPath, startAndEnd[0], startAndEnd[1]);
                totalMileage = (sampleCount * 0.25) / 1000;
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("读取文件头部信息出错:" + ex.Message + ",堆栈：" + ex.StackTrace + ",CIT文件名称：" + citPath);
                MessageBox.Show("读取文件头部信息出错，请检查文件是否有效！");

            }
            if (fileInfo != null)
            {
                //线路名
                ListViewItem item = new ListViewItem(fileInfo.sTrackName);
                //线路编码
                item.SubItems.Add(fileInfo.sTrackCode);
                //行别
                string dir = string.Empty;
                switch (fileInfo.iDir)
                {
                    case 1:
                        {
                            dir = "上行"; break;
                        }
                    case 2:
                        {
                            dir = "下行"; break;
                        }
                    case 3:
                        {
                            dir = "单线"; break;
                        }
                    default:
                        {
                            dir = "上行"; break;
                        }
                }
                item.SubItems.Add(dir);
                if (dir.Contains("下"))
                {
                    item.BackColor = Color.LightCyan;
                }
                else
                {
                    item.BackColor = Color.LightBlue;
                }
                //方向
                item.SubItems.Add(fileInfo.iRunDir == 0 ? "正" : "反");
                //增减里程
                item.SubItems.Add(fileInfo.iKmInc == 0 ? "增" : "减");
                item.SubItems.Add(startMileage.ToString());//起始里程
                item.SubItems.Add(endMileage.ToString());//终止里程
                item.SubItems.Add(totalMileage.ToString());//总里程
                //检测日期
                item.SubItems.Add(fileInfo.sDate);
                //检测时间
                item.SubItems.Add(fileInfo.sTime);
                //检测车号
                item.SubItems.Add(fileInfo.sTrain);
                //原始文件名
                item.SubItems.Add(Path.GetFileName(citPath));
                //大小
                item.SubItems.Add((new FileInfo(citPath)).Length.ToString());
                //原始路径
                item.SubItems.Add(Path.GetDirectoryName(citPath));
                //ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                //subItem.ForeColor = Color.Red;
                //subItem.Text = "点击移除";
                item.SubItems.Add("点击移除");
                item.Tag = fileInfo;
                item.Checked = true;
                
                listViewFiles.Items.Add(item);
                listViewFiles.Columns[listViewFiles.Columns.Count - 1].Width = -2;
            }
        }

        /// <summary>
        /// 按delete键时删除访问列表中的项
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">事件参数</param>
        private void treePathAndMedia_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    if (treePathAndMedia.SelectedNode.Index > 1
                        && treePathAndMedia.SelectedNode.Index < treePathAndMedia.Nodes.Count - 1)
                    {
                        Node node = treePathAndMedia.Nodes[treePathAndMedia.SelectedNode.Index];
                        if (node != null)
                        {
                            string nodePath = node.Tag.ToString();
                            treePathAndMedia.Nodes.Remove(node);
                            ConfigData.DirPaths.PathList.Remove(nodePath);
                            ConfigManger.SaveConfigData(ConfigData);
                        }
                    }
                    else
                    {
                        MessageBox.Show("无法删除目录或媒体库");
                    }
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("删除节点出错:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("删除节点出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 窗口变化时调整最后一列
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">参数</param>
        private void listViewFiles_SizeChanged(object sender, EventArgs e)
        {
            listViewFiles.Columns[listViewFiles.Columns.Count - 1].Width = -2;
        }

        /// <summary>
        /// 单击节点时加载文件头部信息
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">参数</param>
        private void treePathAndMedia_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            try
            {
                CheckNodeAndLoad(e.Node);
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("加载节点文件时失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("加载节点文件时失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 单击listView时选中项
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">参数</param>
        private void listViewFiles_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewItem item = listViewFiles.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                Rectangle labelRect = listViewFiles.GetItemRect(item.Index, ItemBoundsPortion.Label);
                ListViewItem.ListViewSubItem subItem = item.GetSubItemAt(e.X, e.Y);
                if (subItem == item.SubItems[item.SubItems.Count - 1])
                {
                    listViewFiles.Items.Remove(item);
                    return;
                }


                if (item.SubItems[0] == subItem && !labelRect.Contains(e.X, e.Y))
                {
                    if (e.Clicks % 2 == 0)
                    {
                        item.Checked = !item.Checked;
                    }
                }
                else
                {
                    item.Checked = !item.Checked;
                }

            }
        }

        /// <summary>
        /// 关闭时隐藏窗体
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">参数</param>
        private void OpenFileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            MainForm.sMainform.Activate();
        }

        /// <summary>
        /// 隐藏窗体
        /// </summary>
        /// <param name="sender">取消按钮</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            listViewFiles.Items.Clear();
            treePathAndMedia.HideSelection = true;
            this.Close();
            
        }

        private void OpenFileForm_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                LoadTreeData();
                treePathAndMedia.HideSelection = true;
                this.WindowState = _formState;
                if (this.WindowState != FormWindowState.Maximized)
                {
                    Point location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                        (Screen.PrimaryScreen.WorkingArea.Height - this.Height)  / 2);
                    this.Location = location;
                }
            }
            else
            {
                _formState = this.WindowState;
            }
        }

        private void listViewFiles_MouseClick(object sender, MouseEventArgs e)
        {
            //ListViewItem item = listViewFiles.GetItemAt(e.X, e.Y);
            //Rectangle labelRect = listViewFiles.GetItemRect(item.Index, ItemBoundsPortion.Label);
            //if (item != null)
            //{
            //    ListViewItem.ListViewSubItem subItem = item.GetSubItemAt(e.X, e.Y);
            //    if (subItem == item.SubItems[item.SubItems.Count - 1])
            //    {
            //        listViewFiles.Items.Remove(item);
            //        return;
            //    }
                
                
            //    if (item.SubItems[0] == subItem && !labelRect.Contains(e.X, e.Y))
            //    {
            //    }
            //    else
            //    {
            //        item.Checked = !item.Checked;
            //    }
               
            //}
        }

        private void listViewFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOpenFiles_Click(sender, e);
        }
    }
}
