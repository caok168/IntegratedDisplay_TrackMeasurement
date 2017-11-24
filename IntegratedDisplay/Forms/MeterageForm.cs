/// -------------------------------------------------------------------------------------------
/// FileName：MeterageForm.cs
/// 说    明：测量窗体
/// Version ：1.0
/// Date    ：2017/6/2
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 测量窗体
    /// </summary>
    public partial class MeterageForm : Form
    {
        WaveformMaker waveformMaker = null;

        List<WavefromData> waveformDataList = new List<WavefromData>();

        private string _fileName = "";

        public MeterageForm()
        {
            InitializeComponent();
        }

        public MeterageForm(WaveformMaker maker)
        {
            waveformMaker = maker;

            waveformDataList = waveformMaker.WaveformDataList;

            InitializeComponent();
        }

        /// <summary>
        /// 导出Excel按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            String citPath = Path.GetDirectoryName(waveformDataList[0].CitFilePath);
            String meterageDataFilePath = Path.Combine(citPath, DateTime.Now.ToString("yyyyMMddHHmmss") +"-"+_fileName+ "-测量结果.csv");

            ExportDataFromDataGridView(MeterageDataGridView1, meterageDataFilePath);
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeterageForm_Load(object sender, EventArgs e)
        {
            MeterageDataGridView1.Rows.Clear();

            try
            {
                string sStr = this.Tag.ToString();
                string[] sSplit = sStr.Split(new char[] { ',' });
                Point p = new Point();
                p.X = int.Parse(sSplit[1]);
                int iDPointX = p.X;
                p.Y = int.Parse(sSplit[2]);
                int iWidth = int.Parse(sSplit[3]);
                int iChecked = int.Parse(sSplit[0]);
                if (p.X < 0)
                {
                    p.X = 0;
                }
                switch (iChecked)
                {
                    //同通道名测量
                    case 1:
                        {
                            GetSameChannel(p, iWidth);
                            _fileName = "同通道测量";
                            break;
                        }
                    //同基线测量
                    case 2:
                        {
                            _fileName = "同基线测量";
                            GetSameScale(p, iWidth);
                            break;
                        }
                    //同一层测量
                    case 3:
                        {
                            _fileName = "同图层测量";
                            GetSameLayer(p, iWidth);
                            break;
                        }
                    //同里程测量
                    case 4:
                        {
                            _fileName = "同里程测量";
                            GetSameMile(p, iWidth);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("里程不存在,请重新生成波形通道配置文件!\n错误描述" + ex.Message);
                this.Close();
            }
        }

        /// <summary>
        /// 同通道名测量
        /// </summary>
        /// <param name="p"></param>
        /// <param name="iWidth"></param>
        private void GetSameChannel(Point p,int iWidth)
        {
            int iDPointX = p.X;

            for (int i = 0; i < waveformDataList.Count; i++)
            {
                for (int j = 0; j < waveformDataList[i].ChannelList.Count; j++)
                {
                    var channel = waveformDataList[i].ChannelList[j];
                    var mileList = waveformDataList[i].MileList.milestoneList;

                    if (channel.IsVisible && channel.DisplayRect.Contains(channel.DisplayRect.X + 1, p.Y - waveformMaker.MileageInfoHeight))
                    {
                        iDPointX = ((int)(p.X / (iWidth / 1.0 / mileList.Count)));

                        if (iDPointX >= mileList.Count)
                        {
                            iDPointX = mileList.Count - 1;
                        }
                        iDPointX = GetMeterArea(iDPointX, i, j);

                        object[] o = new object[6];
                        o[0] = i + 1;
                        o[1] = waveformDataList[i].CitFile.sDate;
                        string sDir = "";
                        if (waveformDataList[i].CitFile.iDir == 1)
                        {
                            sDir = "上行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 2)
                        {
                            sDir = "下行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 3)
                        {
                            sDir = "单线";
                        }

                        o[2] = sDir;
                        o[3] = channel.ChineseName;
                        o[4] = "K" + mileList[iDPointX].mKm.ToString() + "+" + mileList[iDPointX].mMeter.ToString();

                        if (channel.IsMeaOffset)
                        {
                            o[5] = channel.Data[iDPointX]  + channel.Offset;/// channel.Scale
                        }
                        else
                        {
                            o[5] = channel.Data[iDPointX] ;/// channel.Scale
                        }

                        MeterageDataGridView1.Rows.Add(o);

                    }
                }
            }
            
        }

        /// <summary>
        /// 同基线测量
        /// </summary>
        /// <param name="p"></param>
        /// <param name="iWidth"></param>
        private void GetSameScale(Point p, int iWidth)
        {
            int iDPointX = p.X;
            int pointY = p.Y;
            for (int i = 0; i < waveformDataList.Count; i++)
            {
                for (int j = 0; j < waveformDataList[i].ChannelList.Count; j++)
                {

                    var channel = waveformDataList[i].ChannelList[j];
                    var mileList = waveformDataList[i].MileList.milestoneList;

                    if (channel.IsVisible && channel.DisplayRect.Contains(channel.DragRect.X + 1, p.Y - waveformMaker.MileageInfoHeight))
                    {
                        iDPointX = ((int)(p.X / (iWidth / 1.0 / mileList.Count)));
                        if (iDPointX >= mileList.Count)
                        {
                            iDPointX = mileList.Count - 1;
                        }
                        iDPointX = GetMeterArea(iDPointX, i, j);

                        object[] o = new object[6];
                        o[0] = i + 1;
                        o[1] = waveformDataList[i].CitFile.sDate;
                        string sDir = "";
                        if (waveformDataList[i].CitFile.iDir == 1)
                        {
                            sDir = "上行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 2)
                        {
                            sDir = "下行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 3)
                        {
                            sDir = "单线";
                        }

                        o[2] = sDir;

                        o[3] = channel.ChineseName;

                        o[4] = "K" + mileList[iDPointX].mKm.ToString() + "+" + mileList[iDPointX].mMeter.ToString();

                        if (channel.IsMeaOffset)
                        {
                            o[5] = channel.Data[iDPointX] + channel.Offset;// / channel.Scale
                        }
                        else
                        {
                            o[5] = channel.Data[iDPointX] ;/// channel.Scale
                        }


                        MeterageDataGridView1.Rows.Add(o);
                    }
                }
            }
        }

        /// <summary>
        /// 同一层测量
        /// </summary>
        /// <param name="p"></param>
        /// <param name="iWidth"></param>
        private void GetSameLayer(Point p, int iWidth)
        {
            int iDPointX = p.X;

            for (int i = 0; i < waveformDataList.Count; i++)
            {
                for (int k = 0; k < waveformDataList[i].ChannelList.Count; k++)
                {
                    var channel = waveformDataList[i].ChannelList[k];
                    var mileList = waveformDataList[i].MileList.milestoneList;

                    if (channel.IsVisible)
                    {
                        iDPointX = ((int)(p.X / (iWidth / 1.0 / mileList.Count)));
                        if (iDPointX >= mileList.Count)
                        {
                            iDPointX = mileList.Count - 1;
                        }
                        iDPointX = GetMeterArea(iDPointX, i, k);

                        object[] o = new object[6];
                        o[0] = i + 1;
                        o[1] = waveformDataList[i].CitFile.sDate;
                        string sDir = "";
                        if (waveformDataList[i].CitFile.iDir == 1)
                        {
                            sDir = "上行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 2)
                        {
                            sDir = "下行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 3)
                        {
                            sDir = "单线";
                        }
                        o[2] = sDir;

                        o[3] = channel.ChineseName;

                        o[4] = "K" + mileList[iDPointX].mKm.ToString() + "+" + mileList[iDPointX].mMeter.ToString();

                        if (channel.IsMeaOffset)
                        {
                            o[5] = channel.Data[iDPointX]  + channel.Offset;/// channel.Scale
                        }
                        else
                        {
                            o[5] = channel.Data[iDPointX] ;/// channel.Scale
                        }

                        MeterageDataGridView1.Rows.Add(o);
                    }
                }
            }
        }

        /// <summary>
        /// 同里程测量
        /// </summary>
        /// <param name="p"></param>
        /// <param name="iWidth"></param>
        private void GetSameMile(Point p, int iWidth)
        {
            int iDPointX = p.X;

            for (int i = 0; i < waveformDataList.Count; i++)
            {
                for (int k = 0; k < waveformDataList[i].ChannelList.Count; k++)
                {
                    var channel = waveformDataList[i].ChannelList[k];
                    var mileList = waveformDataList[i].MileList.milestoneList;

                    if (channel.IsVisible)
                    {
                        iDPointX = ((int)(p.X / (iWidth / 1.0 / (mileList.Count - 1))));
                        if (iDPointX >= mileList.Count)
                        {
                            iDPointX = mileList.Count - 1;
                        }

                        object[] o = new object[6];
                        o[0] = i + 1;
                        o[1] = waveformDataList[i].CitFile.sDate;
                        string sDir = "";
                        if (waveformDataList[i].CitFile.iDir == 1)
                        {
                            sDir = "上行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 2)
                        {
                            sDir = "下行";
                        }
                        if (waveformDataList[i].CitFile.iDir == 3)
                        {
                            sDir = "单线";
                        }

                        o[2] = sDir;
                        o[3] = channel.ChineseName;

                        o[4] = "K" + mileList[iDPointX].mKm.ToString() + "+" + mileList[iDPointX].mMeter.ToString();

                        if (channel.IsMeaOffset)
                        {
                            o[5] = channel.Data[iDPointX]  + channel.Offset;/// channel.Scale
                        }
                        else
                        {
                            o[5] = channel.Data[iDPointX] ;/// channel.Scale
                        }


                        MeterageDataGridView1.Rows.Add(o);

                    }
                }
            }
        }


        private int GetMeterArea(int iDPointX, int k, int g)
        {
            int i = iDPointX - MainForm.WaveformConfigData.MeterageRadius / 2;//* 4
            int j = iDPointX + MainForm.WaveformConfigData.MeterageRadius / 2;// * 4
            if (i < 0)
            {
                i = 0;
            }
            if (j >= waveformDataList[k].MileList.milestoneList.Count)
            {
                j = waveformDataList[k].MileList.milestoneList.Count - 1;
            }
            float iValue = 0f;
            for (; i < j; i++)
            {
                double value = waveformDataList[k].ChannelList[g].Data[i];
                if (Math.Abs(value) > iValue)
                {
                    iValue = Convert.ToSingle(Math.Abs(waveformDataList[k].ChannelList[g].Data[i]));
                    iDPointX = i;
                }
            }

            return iDPointX;
        }


        /// <summary>
        /// 导出excel文件
        /// </summary>
        /// <param name="dgv">表格</param>
        /// <param name="csvFilePath">导出文件路径</param>
        private void ExportDataFromDataGridView(DataGridView dgv, String csvFilePath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.UTF8);
                StringBuilder sb = new StringBuilder();
                sb.Append("序号");
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    sb.Append("," + dgv.Columns[i].HeaderText);
                }
                sw.WriteLine(sb.ToString());
                sw.AutoFlush = true;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb = new StringBuilder();
                    sb.Append((i + 1).ToString());
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        sb.Append("," + dgv.Rows[i].Cells[j].Value.ToString());
                    }
                    sw.WriteLine(sb.ToString());
                }

                sw.Close();
                MessageBox.Show("导出成功,位置：" + csvFilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
