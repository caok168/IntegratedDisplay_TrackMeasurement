/// -------------------------------------------------------------------------------------------
/// FileName：ChannelsDialog.cs
/// 说    明：通道配置窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CitFileSDK;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 通道配置
    /// </summary>
    public partial class ChannelsDialog : Form
    {
        WaveformMaker waveformMaker = null;

        List<WavefromData> waveFormDataList = new List<WavefromData>();

        private int iLayerID = 0;
        private int iChannlesID = 0;

        private string citfilepathGloble = "";
        private string channelConfigPathCurrent = "";

        private List<ChannelDefinition> channelListGloble = new List<ChannelDefinition>();


        List<FileInfoTemp> listTemp = new List<FileInfoTemp>();

        CITFileProcess cithelper = new CITFileProcess();

        public List<ChannelsClass> listChannel = new List<ChannelsClass>();

        public ChannelsDialog()
        {
            InitializeComponent();
        }

        public ChannelsDialog(WaveformMaker maker)
        {
            InitializeComponent();
            waveformMaker = maker;
            waveFormDataList = waveformMaker.WaveformDataList;
            if (waveFormDataList.Count > 0)
            {
                listChannel = new List<ChannelsClass>(waveFormDataList[0].ChannelList);
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsDialog_Load(object sender, EventArgs e)
        {

            ckbAutoSave.Checked = MainForm.WaveformConfigData.IsAutoSaveConfig;
            ckbIsShowHighlight.Checked = waveformMaker.IsSelectedHighlight;
            for (int i = 0; i < ChannelsConfigDataGridView1.Columns.Count; i++)
            {
                ChannelsConfigDataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            ChannelsConfigDataGridView1.ClearSelection();
            //if (ChannelsConfigDataGridView1.Rows.Count > 0)
            //{
            //    ChannelsConfigDataGridView1.Rows[iChannlesID].Selected = true;
            //}
            if (LayerComboBox1.Items.Count > 0)
            {
                btnConfigSaveAs.Enabled = true;
                btnLoadConfig.Enabled = true;
            }
            ChannelsDialog_Resize(sender, e);

        }

        /// <summary>
        /// 切换层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (waveFormDataList.Count > 0)
            {
                citfilepathGloble = waveFormDataList[this.LayerComboBox1.SelectedIndex].CitFilePath;

                //var channelList = cithelper.GetChannelDefinitionList(citfilepathGloble);
                var channelList = waveFormDataList[this.LayerComboBox1.SelectedIndex].ChannelList;
                //channelNames = channelList.Select(s => s.sNameEn).ToArray();

                channelConfigPathCurrent = waveFormDataList[this.LayerComboBox1.SelectedIndex].WaveConfigFilePath;
                
                GetChannelsData(channelConfigPathCurrent, channelList);
            }
        }

        /// <summary>
        /// 另存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAsFileDialog1.FileName = "";
                DialogResult dr = SaveAsFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    File.Copy(channelConfigPathCurrent, SaveAsFileDialog1.FileName, true);
                    //把所选层的配置文件设置为新的
                    channelConfigPathCurrent = SaveAsFileDialog1.FileName;
                    //保存配置到文件
                    SaveChannelSetToConfigFile(channelConfigPathCurrent);

                    //GetChannelsData(channelConfigPathCurrent, channelNames);
                    ConfigLabel.Text = Path.GetFileName(channelConfigPathCurrent);
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                MyLogger.LogError("保存配置文件时出错", ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.InitialDirectory = Application.StartupPath;
                OpenFileDialog1.FileName = "";
                DialogResult dr = OpenFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var channelList = cithelper.GetChannelDefinitionList(waveformMaker.WaveformDataList[0].CitFilePath);
                    GetChannelsData(OpenFileDialog1.FileName, channelList);
                    channelConfigPathCurrent = OpenFileDialog1.FileName;
                    ApplyCurrentConfig();
                }
            }
            catch (Exception ex)
            {
                MyLogger.LogError("加载配置文件时出错", ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存当前配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveAsDefault_Click(object sender, EventArgs e)
        {
            //需后续添加
            SaveChannelSetToConfigFile(channelConfigPathCurrent);
        }

        /// <summary>
        /// 设为默认配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewDefaultConfig_Click(object sender, EventArgs e)
        {
            try
            {
                String configFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "默认配置文件.xml");

                var channelList = cithelper.GetChannelDefinitionList(citfilepathGloble);

                string fileNamePath = ChannelManager.CreateWaveXMLConfig(configFilePath, waveformMaker.SingleChannelInfoHeight, 1, channelList);

                GetChannelsData(fileNamePath, channelList);
            }
            catch(Exception ex)
            {

                MyLogger.LogError("创建通道配置文件时出错", ex);
                MessageBox.Show(ex.Message);
            }
            //QuickSetMFHScrollBar_Scroll();
        }

        /// <summary>
        /// 自动排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChannelAutoArange_Click(object sender, EventArgs e)
        {
            if (ChannelsConfigDataGridView1.Rows.Count < 1)
            {
                return;
            }

            int iIndex = 0;
            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
                if (bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells["Column6"].Value.ToString()))
                {
                    iIndex++;
                }
            }

            if (iIndex < 1)
            {
                return;
            }
            int iSum = (100 / ((iIndex - 2 + 1)));
            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
                if (bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells["Column6"].Value.ToString()))
                {
                    ChannelsConfigDataGridView1.Rows[i].Cells["Column10"].Value = (iSum*(i+1)+(i+1)).ToString();
                }
            }
            ApplyCurrentConfig();
        }

        /// <summary>
        /// 应用保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton1_Click(object sender, EventArgs e)
        {
            SaveChannelSetToConfigFile(channelConfigPathCurrent);
            MainForm.sMainform.picMainGraphics.Invalidate();
        }


        /// <summary>
        /// 调整控件大小响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsDialog_Resize(object sender, EventArgs e)
        {
            //ChannelsConfigDataGridView1.Location = new Point(3, 58);
            //ChannelsConfigDataGridView1.Height = this.ClientSize.Height - 100;
            //ChannelsConfigDataGridView1.Width = this.ClientSize.Width - 3;


            //ConfigLabel.Width = this.ClientSize.Width - ConfigLabel.Left - 20;
            //LayerComboBox1.Width = this.ClientSize.Width - LayerComboBox1.Left - 20;
        }

        
        private void GetChannelsData(string fileName, List<ChannelsClass> channelList)
        {
            ChannelsConfigDataGridView1.Rows.Clear();

            for (int i = 0; i < channelList.Count; i++)
            {
                //if (iChannlesID == -1 || iChannlesID == i)//
                //{
                    object[] o = new object[12];
                    o[0] = channelList[i].Id;
                    o[1] = channelList[i].Name;
                    o[2] = channelList[i].Units;
                    o[3] = channelList[i].ChineseName;
                    o[4] = channelList[i].NonChineseName;
                    o[5] = Color.FromArgb(channelList[i].Color);
                    o[6] = channelList[i].IsVisible;
                    o[7] = channelList[i].ZoomIn;
                    
                    
                    o[8] = channelList[i].LineWidth;
                    o[9] = channelList[i].Location;
                    o[10] = channelList[i].IsMeaOffset;
                    o[11] = channelList[i].IsReverse;
                    ChannelsConfigDataGridView1.Rows.Add(o);
                    ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[0].Style.BackColor = Color.DarkGray;
                    ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[1].Style.BackColor = Color.DarkGray;
                    ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[2].Style.BackColor = Color.DarkGray;
                    ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[5].Style.BackColor =
                        Color.FromArgb(channelList[i].Color);
                    ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[5].Value = "";
                //}
            }
            ConfigLabel.Text = Path.GetFileName(fileName);
            ChannelsConfigDataGridView1.ClearSelection();
        }

        /// <summary>
        /// 获取层数据信息
        /// </summary>
        /// <param name="fileName">配置文件路径</param>
        /// <param name="channelDefinitionList">通道名称数组</param>
        private void GetChannelsData(string fileName, List<ChannelDefinition> channelDefinitionList)
        {
            try
            {
                ChannelsConfigDataGridView1.Rows.Clear();

                List<ChannelsClass> channelList = ChannelManager.LoadChannelsConfig(fileName, channelDefinitionList);

                for (int i = 0; i < channelList.Count; i++)
                {
                    if (iChannlesID == -1)//|| iChannlesID == i
                    {
                        object[] o = new object[12];
                        o[0] = channelList[i].Id;
                        o[1] = channelList[i].Name;
                        o[2] = channelList[i].Units;
                        o[3] = channelList[i].ChineseName;
                        o[4] = channelList[i].NonChineseName;
                        o[5] = Color.FromArgb(channelList[i].Color);
                        o[6] = channelList[i].IsVisible;
                        o[7] = channelList[i].ZoomIn;

                        o[8] = channelList[i].LineWidth;
                        o[9] = channelList[i].Location;
                        o[10] = channelList[i].IsMeaOffset;
                        o[11] = channelList[i].IsReverse;
                        ChannelsConfigDataGridView1.Rows.Add(o);
                        ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[0].Style.BackColor = Color.DarkGray;
                        ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[1].Style.BackColor = Color.DarkGray;
                        ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[2].Style.BackColor = Color.DarkGray;
                        ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[5].Style.BackColor =
                            Color.FromArgb(channelList[i].Color);
                        ChannelsConfigDataGridView1.Rows[ChannelsConfigDataGridView1.Rows.Count - 1].Cells[5].Value = "";
                    }
                }
                ConfigLabel.Text = Path.GetFileName(fileName);
                ChannelsConfigDataGridView1.ClearSelection();
            }
            catch(Exception ex)
            {   
                MyLogger.LogError("加载图层数据时出错", ex);
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 把通道设置保存在系统配置变量写入到所选层的配置文件中
        /// </summary>
        /// <param name="filePath">配置文件路径</param>
        private void SaveChannelSetToConfigFile(string filePath)
        {
            if (ChannelsConfigDataGridView1.Rows.Count < 1)
            {
                return;
            }

            //验证输入的比例，基线位置，线宽等值的合法性
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
                String channelName = ChannelsConfigDataGridView1.Rows[i].Cells[3].Value.ToString();
                try
                {
                    float zoomIn = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[7].Value.ToString());
                    //if (zoomIn <= 0)
                    //{
                    //    sb.AppendLine(String.Format("{0}- 通道比例必须大于0", channelName));
                    //}
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 通道比例格式错误（必须是数字）", channelName));
                }

                try
                {
                    int location = int.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[9].Value.ToString());
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 基线位置格式错误（必须是整数）", channelName));
                }


                try
                {
                    float fLineWidth = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells["clnLineWidth"].Value.ToString());
                    if (fLineWidth <= 0)
                    {
                        sb.AppendLine(String.Format("{0}-  线宽必须大于0", channelName));
                    }
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 线宽格式错误（必须是大于0的数字）", channelName));
                }
            }
            if (!String.IsNullOrEmpty(sb.ToString()))
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            listChannel.Clear();
            listChannel = new List<ChannelsClass>(waveformMaker.WaveformDataList[LayerComboBox1.SelectedIndex].ChannelList);
            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
               
                listChannel[i].Name = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                listChannel[i].ChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[3].Value.ToString();
                listChannel[i].NonChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                listChannel[i].Color = ChannelsConfigDataGridView1.Rows[i].Cells[5].Style.BackColor.ToArgb();
                listChannel[i].IsVisible = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[6].Value.ToString());
                listChannel[i].ZoomIn = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[7].Value.ToString());

                listChannel[i].LineWidth = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[8].Value.ToString());
                listChannel[i].Location = int.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[9].Value.ToString());
                listChannel[i].IsMeaOffset = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[10].Value.ToString());
                listChannel[i].IsReverse = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[11].Value.ToString());
                
            }
            waveformMaker.WaveformDataList[LayerComboBox1.SelectedIndex].ChannelList = new List<ChannelsClass>(listChannel);
            
            //保存配置到文件
            ChannelManager.SaveChannelsConfig(filePath, listChannel);
        }

        /// <summary>
        /// 应用当前配置
        /// </summary>
        private void ApplyCurrentConfig()
        {
            if (ChannelsConfigDataGridView1.Rows.Count < 1)
            {
                return;
            }
            //验证输入的比例，基线位置，线宽等值的合法性
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
                String channelName = ChannelsConfigDataGridView1.Rows[i].Cells[3].Value.ToString();
                try
                {
                    float zoomIn = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[7].Value.ToString());
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 通道比例格式错误（必须是数字）", channelName));
                }

                try
                {
                    int location = int.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[9].Value.ToString());
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 基线位置格式错误（必须是整数）", channelName));
                }
                try
                {
                    float fLineWidth = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[8].Value.ToString());
                    if (fLineWidth <= 0)
                    {
                        sb.AppendLine(String.Format("{0}-  线宽必须大于0", channelName));
                    }
                }
                catch
                {
                    sb.AppendLine(String.Format("{0}- 线宽格式错误（必须是大于0的数字）", channelName));
                }
            }
            if (!String.IsNullOrEmpty(sb.ToString()))
            {
                MessageBox.Show(sb.ToString());
                return;
            }
            listChannel.Clear();
            listChannel = new List<ChannelsClass>(waveformMaker.WaveformDataList[LayerComboBox1.SelectedIndex].ChannelList);

            for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
            {
                //if (iChannlesID == -1)
                //{

                    listChannel[i].Name = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                    listChannel[i].ChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[3].Value.ToString();
                    listChannel[i].NonChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                    listChannel[i].Color = ChannelsConfigDataGridView1.Rows[i].Cells[5].Style.BackColor.ToArgb();
                    listChannel[i].IsVisible = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[6].Value.ToString());
                    listChannel[i].ZoomIn = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[7].Value.ToString());

                    listChannel[i].LineWidth = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[8].Value.ToString());
                    listChannel[i].Location = int.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[9].Value.ToString());
                    listChannel[i].IsMeaOffset = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[10].Value.ToString());
                    listChannel[i].IsReverse = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[11].Value.ToString());
                //}
                //else
                //{
                    
                //    listChannel[i].Name = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                //    listChannel[i].ChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[3].Value.ToString();
                //    listChannel[i].NonChineseName = ChannelsConfigDataGridView1.Rows[i].Cells[4].Value.ToString();
                //    listChannel[i].Color = ChannelsConfigDataGridView1.Rows[i].Cells[5].Style.BackColor.ToArgb();
                //    listChannel[i].IsVisible = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[6].Value.ToString());
                //    listChannel[i].ZoomIn = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[7].Value.ToString());

                //    listChannel[i].LineWidth = float.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[8].Value.ToString());
                //    listChannel[i].Location = int.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[9].Value.ToString());
                //    listChannel[i].IsMeaOffset = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[10].Value.ToString());
                //    listChannel[i].IsReverse = bool.Parse(ChannelsConfigDataGridView1.Rows[i].Cells[11].Value.ToString());
                //    break;
                //}
            }
            waveformMaker.AutoArrange();
            waveformMaker.WaveformDataList[LayerComboBox1.SelectedIndex].ChannelList = new List<ChannelsClass>(listChannel);
            MainForm.sMainform.picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 对通道批量操作
        /// </summary>
        private void BatchModify()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder channelId = new StringBuilder();
            sb.Append(LayerComboBox1.SelectedIndex.ToString());
            sb.Append(",");
            foreach (DataGridViewRow dgvr in ChannelsConfigDataGridView1.SelectedRows)
            {
                sb.Append(dgvr.Index.ToString());
                sb.Append(",");
                channelId.Insert(0, ChannelsConfigDataGridView1.Rows[dgvr.Index].Cells[0].Value.ToString());
                channelId.Insert(0, ",");
            }
            sb.Remove(sb.Length - 1, 1);
            channelId.Remove(0, 1);
            string txt = "编辑的通道ID为：" + channelId.ToString();
            displayStatus(txt);
            List<ChannelsClass> channelConfig = new List<ChannelsClass>();
            listChannel.Clear();
            listChannel = new List<ChannelsClass>(waveformMaker.WaveformDataList[LayerComboBox1.SelectedIndex].ChannelList);
            for (int i = 0; i < ChannelsConfigDataGridView1.SelectedRows.Count; i++)
            {
                int index = ChannelsConfigDataGridView1.SelectedRows[i].Index;
                channelConfig.Add(listChannel[index]);
            }
            using (ChannelsConfigDialog ccd = new ChannelsConfigDialog())
            {
                ccd.Tag = sb.ToString();
                ccd.dialog = this;
                ccd.list = channelConfig;
                DialogResult dr = ccd.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                    //GetChannelsData(channelConfigPathCurrent, channelNames);
                    for (int i = 0; i < ChannelsConfigDataGridView1.SelectedRows.Count; i++)
                    {
                        int ID = (int)ChannelsConfigDataGridView1.SelectedRows[i].Index;

                        ChannelsConfigDataGridView1.Rows[ID].Cells[3].Value = listChannel[ID].ChineseName;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[4].Value = listChannel[ID].NonChineseName;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[5].Style.BackColor = Color.FromArgb(listChannel[ID].Color);
                        ChannelsConfigDataGridView1.Rows[ID].Cells[6].Value = listChannel[ID].IsVisible;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[7].Value = listChannel[ID].ZoomIn;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[8].Value = listChannel[ID].LineWidth;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[9].Value = listChannel[ID].Location;
                        ChannelsConfigDataGridView1.Rows[ID].Cells[10].Value = listChannel[ID].IsMeaOffset;
                    }
                    ApplyCurrentConfig();
                   
                }
            }
        }

        /// <summary>
        /// 显示当前操作状态
        /// </summary>
        /// <param name="txt">操作内容</param>
        private void displayStatus(string txt)
        {
            tsStatusShow.Text = "编辑的波形为：" + LayerComboBox1.Text.Substring(0, LayerComboBox1.Text.IndexOf(":")) + "，" + txt;
        }

        /// <summary>
        /// 关闭窗体时隐藏窗口，不关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Tag = new Point();
            this.Hide();
            MainForm.sMainform.Activate();
        }

        /// <summary>
        /// 窗体显示时，设置通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsDialog_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Point p = (Point)this.Tag;
                ////通道配置
                //if (p.Y == -2)
                //{
                //    btnSaveAsdefault.Visible = true;
                //    btnSaveConfig.Visible = true;
                //    ckbAutoSave.Visible = true;
                //    //this.Height = 300;
                //    this.statusStrip1.Visible = false;
                //    //this.StartPosition = FormStartPosition.CenterScreen;

                //    btnConfigSaveAs.Visible = false;
                //    btnLoadConfig.Visible = false;
                //    ckbIsShowHighlight.Visible = false;
                //    p.Y = -1;
                //}
                //else //波形配置
                //{

                //    btnSaveAsdefault.Visible = false;
                //    btnSaveConfig.Visible = false;
                //    ckbAutoSave.Visible = false;

                //    ckbIsShowHighlight.Visible = true;
                //    this.statusStrip1.Visible = true;
                //    btnConfigSaveAs.Visible = true;
                //    btnLoadConfig.Visible = true;
                //}
                this.StartPosition = FormStartPosition.CenterScreen;
                iLayerID = p.X;
                iChannlesID = p.Y;

                //初始化数据设定
                LayerComboBox1.Items.Clear();

                for (int i = 0; i < waveFormDataList.Count; i++)
                {
                    LayerComboBox1.Items.Add("波形" + (i + 1) + ":" + waveFormDataList[i].LayerConfig.Name);
                }


                if (LayerComboBox1.Items.Count > 0)
                {
                    ChannelsConfigDataGridView1.ClearSelection();
                    LayerComboBox1.SelectedIndex = iLayerID;
                    btnConfigSaveAs.Enabled = true;
                    btnLoadConfig.Enabled = true;
                    var channelList = waveFormDataList[this.LayerComboBox1.SelectedIndex].ChannelList;
                    //channelNames = channelList.Select(s => s.sNameEn).ToArray();

                    channelConfigPathCurrent = waveFormDataList[this.LayerComboBox1.SelectedIndex].WaveConfigFilePath;

                    GetChannelsData(channelConfigPathCurrent, channelList);

                    if (ChannelsConfigDataGridView1.Rows.Count > iChannlesID && iChannlesID != -1)
                    {
                        ChannelsConfigDataGridView1.Rows[iChannlesID].Selected = true;
                    }
                    //{
                    //    for (int i = 0; i < ChannelsConfigDataGridView1.Rows.Count; i++)
                    //    {
                    //        //if (i == iChannlesID)
                    //        //{
                    //        //    ChannelsConfigDataGridView1.Rows[iChannlesID].Visible = true;
                    //        //}
                    //        //else
                    //        //{
                    //        ChannelsConfigDataGridView1.Rows[iChannlesID].Selected = true;
                    //        break;
                    //        //}

                    //    }
                    //}
                }
            }
        }

        #region 表格事件
        
        private void ChannelsConfigDataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ApplyCurrentConfig();
        }

        private void ChannelsConfigDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ChannelsConfigDataGridView1.SelectedRows.Count > 0)
            {
                //是否显示，包含偏移，上下反转
                if (e.ColumnIndex == 6 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    bool isCheck = (bool)ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isCheck;
                    ApplyCurrentConfig();
                    if (e.ColumnIndex == 6)
                    {
                        waveformMaker.AutoArrange();
                    }
                }

            }
        }

        /// <summary>
        /// 事件响应函数---双击单元格中的任意位置时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsConfigDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex > -1)
            {
                ColorChoiceDialog.Color =
                    ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor;
                DialogResult dr = ColorChoiceDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor =
                       ColorChoiceDialog.Color;
                    //Color.FromArgb(CommonClass.listDIC[LayerComboBox1.SelectedIndex].listCC[i].Color);
                    ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    ApplyCurrentConfig();
                }

            }

            if (ChannelsConfigDataGridView1.SelectedRows.Count > 0)
            {
                //是否显示，包含偏移，上下反转
                if (e.ColumnIndex == 6 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                {
                    bool isCheck = (bool)ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isCheck;
                    ApplyCurrentConfig();
                    if (e.ColumnIndex == 6)
                    {
                        waveformMaker.AutoArrange();
                    }
                }

            }
        }

        /// <summary>
        /// 事件响应函数---单击单元格中的任意位置时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsConfigDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ChannelsConfigDataGridView1.SelectedRows.Count > 0)
            {

                BatchModify();
            }
            else if (e.Button == MouseButtons.Left && e.RowIndex > -1)
            {
                string txt = "通道ID为：" + ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()
                    + "，名称为：" + ChannelsConfigDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                displayStatus(txt);
            }
            if (e.RowIndex == -1)
            {
                ChannelsConfigDataGridView1.ClearSelection();
            }
        }

        #endregion

        private void ChannelsDialog_MouseClick(object sender, MouseEventArgs e)
        {
            ChannelsConfigDataGridView1.ClearSelection();
        }

        private void ckbAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MainForm.WaveformConfigData.IsAutoSaveConfig = ckbAutoSave.Checked;
                ConfigManger.SaveConfigData(MainForm.WaveformConfigData);
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("保存配置文件-自动保存失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }

        private void ckbShowInvaildData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ConfigManger.SaveConfigData(MainForm.WaveformConfigData);
                MainForm.sMainform.picMainGraphics.Invalidate();
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("保存配置文件-显示无效数据：" + ex.Message + ",堆栈：" + ex.StackTrace);
            }
        }


        private void ckbIsShowHighlight_CheckedChanged(object sender, EventArgs e)
        {
            waveformMaker.IsSelectedHighlight = ckbIsShowHighlight.Checked;
            MainForm.sMainform.picMainGraphics.Invalidate();
        }
    }

    /// <summary>
    /// 临时使用
    /// </summary>
    public class FileInfoTemp
    {
        public string citFile { get; set; }

        public string xmlFile { get; set; }
    }
}
