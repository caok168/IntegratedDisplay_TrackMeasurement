/// -------------------------------------------------------------------------------------------
/// FileName：AutoIndexForm.cs
/// 说    明：里程快速校正窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CitFileSDK;
using IntegratedDisplay.Models;
using CitIndexFileSDK.MileageFix;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 里程快速校正窗体
    /// </summary>
    public partial class AutoIndexForm : Form
    {
        private String citFilePath = "";
        private List<AutoIndex> autoIndexClsList = new List<AutoIndex>();

        CITFileProcess citHelper = new CITFileProcess();

        WaveformMaker waveformMaker = null;
        List<WavefromData> waveFormDataList = new List<WavefromData>();

        public AutoIndexForm()
        {
            InitializeComponent();
        }

        public AutoIndexForm(WaveformMaker maker)
        {
            waveformMaker = maker;

            waveFormDataList = waveformMaker.WaveformDataList;

            citFilePath = waveFormDataList[0].CitFilePath;

            InitializeComponent();
        }

        /// <summary>
        /// 选择CIT文件 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectCIT_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                citFilePath = openFileDialog1.FileName;

                toolStripStatusLabel2.Text = citFilePath;
                toolStripStatusLabel2.Width = statusStrip1.Width - toolStripStatusLabel1.Width;

                buttonReadCIT.Enabled = true;
                buttonWriteInf.Enabled = true;
                buttonExportCsv.Enabled = true;
            }
            else
            {
                citFilePath = "";
            }
        }

        /// <summary>
        /// 读取CIT文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReadCIT_Click(object sender, EventArgs e)
        {
            ReadCIT(citFilePath);
            Display(autoIndexClsList);
        }

        /// <summary>
        /// 写入索引点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWriteInf_Click(object sender, EventArgs e)
        {
            String idfFileName = Path.GetFileNameWithoutExtension(citFilePath) + ".idf";

            String idfFilePath = Path.Combine(Path.GetDirectoryName(citFilePath), idfFileName);

            if (!File.Exists(idfFilePath))
            {
                MessageBox.Show("找不到波形索引文件！");
                return;
            }
            WriteIdf(idfFilePath);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 导出CSV事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportCsv_Click(object sender, EventArgs e)
        {
            String excelPath = null;
            String excelName = null;

            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            try
            {
                excelPath = Path.GetDirectoryName(citFilePath);
                excelName = Path.GetFileNameWithoutExtension(citFilePath);

                excelName = excelName + "_里程跳变点" + ".csv";

                excelPath = Path.Combine(excelPath, excelName);

                StreamWriter sw = new StreamWriter(excelPath, false, Encoding.Default);

                StringBuilder sbtmp = new StringBuilder();
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    sbtmp.Append(dataGridView1.Columns[i].HeaderText + ",");
                }
                sbtmp.Remove(sbtmp.Length - 1, 1);
                sw.WriteLine(sbtmp.ToString());

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Rows[i].Cells.Count; j++)
                    {
                        sw.Write(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        if ((j + 1) != dataGridView1.Rows[i].Cells.Count)
                        {
                            sw.Write(",");
                        }
                        else
                        {
                            sw.Write("\n");
                        }
                    }
                }

                sw.Close();

                MessageBox.Show("导出成功！");
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("导出csv文件失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("导出失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        /// <summary>
        /// 读取CIT文件
        /// </summary>
        /// <param name="citFilePath"></param>
        private void ReadCIT(String citFilePath)
        {
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("容许跳变值为 0");
                return;
            }

            try
            {
                autoIndexClsList.Clear();
                dataGridView1.Rows.Clear();

                FileInformation fileInfomation = citHelper.GetFileInformation(citFilePath);
                List<ChannelDefinition> channelList = citHelper.GetChannelDefinitionList(citFilePath);

                FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                br.ReadBytes(120);


                br.ReadBytes(65 * fileInfomation.iChannelNumber);
                br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));
                int iChannelNumberSize = fileInfomation.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];

                long milePos = 0;
                int km_pre = 0;
                int meter_pre = 0;
                int km_currrent = 0;
                int meter_current = 0;
                int meter_between = 0;
                int km_index = 0;
                int meter_index = 2;

                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

                for (int i = 0; i < iArray; i++)
                {
                    milePos = br.BaseStream.Position;

                    b = br.ReadBytes(iChannelNumberSize);

                    if (Encryption.IsEncryption(fileInfomation.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    if (i == 0)
                    {
                        km_pre = (int)(BitConverter.ToInt16(b, km_index));
                        meter_pre = (int)(BitConverter.ToInt16(b, meter_index));
                    }
                    else
                    {
                        km_currrent = (int)(BitConverter.ToInt16(b, km_index));
                        meter_current = (int)(BitConverter.ToInt16(b, meter_index));
                        //第二个通道为采样点，换算为米就要除以4
                        meter_between = (km_currrent - km_pre) * 1000 + (meter_current - meter_pre) / 4;

                        if (Math.Abs(meter_between) > numericUpDown1.Value)
                        {
                            AutoIndex autoIndexCls = new AutoIndex();
                            autoIndexCls.milePos = milePos;
                            autoIndexCls.km_current = km_currrent;
                            autoIndexCls.meter_current = meter_current;
                            autoIndexCls.km_pre = km_pre;
                            autoIndexCls.meter_pre = meter_pre;
                            autoIndexCls.meter_between = meter_between;

                            autoIndexClsList.Add(autoIndexCls);
                        }

                        km_pre = km_currrent;
                        meter_pre = meter_current;

                    }

                }

                br.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("读取CIT文件跳变点失败:" + ex.Message + ",堆栈：" + ex.StackTrace);

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 将结果展示到窗体的表格上面
        /// </summary>
        /// <param name="autoIndexClsList">需要修正的里程点集合</param>
        private void Display(List<AutoIndex> autoIndexClsList)
        {
            if (autoIndexClsList == null || autoIndexClsList.Count == 0)
            {
                return;
            }

            int i = 0;
            dataGridView1.Rows.Clear();
            foreach (AutoIndex autoIndexCls in autoIndexClsList)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(dataGridView1);
                dgvr.Cells[0].Value = ++i;
                dgvr.Cells[1].Value = autoIndexCls.milePos;
                dgvr.Cells[2].Value = autoIndexCls.km_current;
                dgvr.Cells[3].Value = autoIndexCls.meter_current;
                dgvr.Cells[4].Value = autoIndexCls.km_pre;
                dgvr.Cells[5].Value = autoIndexCls.meter_pre;
                dgvr.Cells[6].Value = autoIndexCls.meter_between;

                dataGridView1.Rows.Add(dgvr);
            }

            Boolean isKmInc = true; // 是否是增里程
            if (autoIndexClsList.Count < 2)
            {
                return;
            }
            if ((int)(dataGridView1.Rows[0].Cells[2].Value) > (int)(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value))
            {
                isKmInc = false;
            }

            for (int j = 1; j < dataGridView1.Rows.Count; j++)
            {
                int km_pre = (int)(dataGridView1.Rows[j - 1].Cells[2].Value);
                int meter_pre = (int)(dataGridView1.Rows[j - 1].Cells[3].Value);
                int km_current = (int)(dataGridView1.Rows[j].Cells[2].Value);
                int meter_current = (int)(dataGridView1.Rows[j].Cells[3].Value);

                int point_between = (km_current - km_pre) * 4000 + (meter_current - meter_pre);

                //增里程时，里程突然变小
                if (point_between <= 0 && isKmInc == true)
                {
                    dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.White;
                }
                //减里程时，里程突然变大
                if (point_between >= 0 && isKmInc == false)
                {
                    dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 向idf文件中写入
        /// </summary>
        /// <param name="idfFilePath"></param>
        private void WriteIdf(String idfFilePath)
        {
            bool writeResult = false;
            bool CacluteResult = false;
            try
            {
                UserFixedTable fixedTable = new UserFixedTable(waveformMaker.WaveformDataList[0].IndexOperator, waveformMaker.WaveformDataList[0].CitFile.iKmInc);
                fixedTable.Clear();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    float mile = (int)dataGridView1.Rows[i].Cells[2].Value + (int)(dataGridView1.Rows[i].Cells[3].Value);
                    UserMarkedPoint markedPoint = new UserMarkedPoint();
                    markedPoint.ID = (i + 1).ToString();
                    markedPoint.FilePointer = long.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    markedPoint.UserSetMileage = mile;
                    fixedTable.MarkedPoints.Add(markedPoint);
                }
                fixedTable.Save();
                writeResult = true;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("读取里程修正表错误:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("错误：" + ex.Message);
            }
           
            try
            {
                MilestoneFix fix = new MilestoneFix(waveformMaker.WaveformDataList[0].CitFilePath, waveformMaker.WaveformDataList[0].IndexOperator);
                fix.RunFixingAlgorithm();
                fix.SaveMilestoneFixTable();
                CacluteResult = true;
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("手动里程修正失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("错误：" + ex.Message);
            }
            if (writeResult && CacluteResult)
            {
                MessageBox.Show("创建并且写入成功！");
            }

        }
    }
}
