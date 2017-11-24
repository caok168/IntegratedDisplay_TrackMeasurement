/// -------------------------------------------------------------------------------------------
/// FileName：InvalidDataForm.cs
/// 说    明：无效区段设置查看窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using CitFileSDK;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 无效区段设置查看窗体
    /// </summary>
    public partial class InvalidDataForm : Form
    {
        WaveformMaker waveformMaker = null;

        List<WavefromData> waveFormDataList = new List<WavefromData>();

        /// <summary>
        /// idf文件路径
        /// </summary>
        public string idfFilePath = "";
        /// <summary>
        /// cit文件路径
        /// </summary>
        public string citFilePath = "";

        FileInformation fi = null;

        //是否索引修正
        bool bIndex = false;
        int iChannelNumber;

        
        //cit文件操作类
        CITFileProcess citHelper = new CITFileProcess();

        InvalidDataManager invalidManager = new InvalidDataManager();

        //List<InvalidData> listIDC = new List<InvalidData>();

        List<IndexSta> listIndexSta = new List<IndexSta>();
        
        public InvalidDataForm()
        {
            InitializeComponent();
            
        }

        public InvalidDataForm(WaveformMaker maker)
        {
            InitializeComponent();
            
            waveformMaker = maker;
            
        }

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvalidDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            MainForm.sMainform.Activate();
        }

        /// <summary>
        /// 窗体显示变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvalidDataForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                GetInvalidData();
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            int index = dataGridView1.SelectedRows[0].Index;//选中的行的索引
            int rowsCount = dataGridView1.Rows.Count;
            int index_row_display_old = dataGridView1.FirstDisplayedScrollingRowIndex;//显示在datagridview中的第一行的索引。
            int index_row_display_new = 0;
            int index_new = 0;//删除单条记录后，光标应停留的行的索引。
            int VerticalScrollVal = dataGridView1.VerticalScrollingOffset;
            if (dataGridView1.SelectedRows.Count == 1)
            {
                invalidManager.InvalidDataDelete(idfFilePath, dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                //waveformMaker.WaveformDataList[0].InvalidDataList = invalidManager.InvalidDataList(idfFilePath);
                //CommonClass.listDIC[0].listIDC = invalidManager.InvalidDataList(CommonClass.listDIC[0].sAddFile);
                GetInvalidData();
            }

            //如果选择删除最后一个，则删除完成后，跳到该条的上一条
            if (rowsCount == 1)
            {
                MainForm.sMainform.picMainGraphics.Invalidate();
                return;
            }
            else
            {
                //删除的是最后一行，删完后还停留在最后一行
                if (index == rowsCount - 1)
                {
                    index_new = dataGridView1.Rows.Count - 1;

                }
                else //其余中间行，删完后删完自动跳到下一行
                {
                    index_new = index;
                }
                //datagridview显示的第一行的索引
                if (index_row_display_old == 0)
                {
                    index_row_display_new = index_row_display_old;
                }
                else
                {
                    index_row_display_new = index_row_display_old - 1;
                }
            }

            dataGridView1.FirstDisplayedScrollingRowIndex = index_row_display_old;
            dataGridView1.Rows[index_new].Selected = true;

            String value = dataGridView1[1, index_new].Value.ToString();

            //根据文件指针来定位波形，更精确
            long pos = long.Parse(value);
            //临时注释
            MainForm.sMainform.picMainGraphics.Invalidate();
        }

        /// <summary>
        /// 更新里程信息按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateMileInfo_Click(object sender, EventArgs e)
        {
            if (waveformMaker.WaveformDataList[0].MileageFix.FixData == null || waveformMaker.WaveformDataList[0].MileageFix.FixData.Count == 0)
            {
                MessageBox.Show("没有进行里程校正，不需要更新里程！");
                return;
            }
            if (waveformMaker.WaveformDataList[0].InvalidDataList.Count == 0)
            {
                return;
            }
            try
            {
                for (int i = 0; i < waveformMaker.WaveformDataList[0].InvalidDataList.Count; i++)
                {
                    InvalidData data = waveformMaker.WaveformDataList[0].InvalidDataList[i];
                    float iStartMile = waveformMaker.WaveformDataList[0].MileageFix.CalcPointMileStone(long.Parse(data.sStartPoint));
                    float iEndMile = waveformMaker.WaveformDataList[0].MileageFix.CalcPointMileStone(long.Parse(data.sEndPoint));
                    invalidManager.InvalidDataUpdate(idfFilePath, data.sStartPoint,
                        data.sEndPoint, (iStartMile / 1000f).ToString(), (iEndMile / 1000f).ToString());
                }
            }
            catch(Exception ex)
            {
                MyLogger.LogError("更新无效数据时失败", ex);
            }
            MessageBox.Show("更新成功！");
            GetInvalidData();
        }

        /// <summary>
        /// 统计按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStatistics_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            int totalCount = dataGridView1.Rows.Count;
            int totalGongli = 0;
            int startMile = 0;
            int endMile = 0;

            if (bIndex)
            {
                startMile = (int)(waveformMaker.WaveformDataList[0].MileageFix.FixData[0].MarkedStartPoint.UserSetMileage * 1000);
                endMile = (int)(waveformMaker.WaveformDataList[0].MileageFix.FixData[waveformMaker.WaveformDataList[0].MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage) * 1000;
            }
            else
            {
                Milestone milestone= citHelper.GetStartMilestone(citFilePath);
                startMile = Convert.ToInt32(milestone.mKm * 1000 + milestone.mMeter);
                milestone = citHelper.GetEndMilestone(citFilePath);
                endMile = Convert.ToInt32(milestone.mKm * 1000 + milestone.mMeter);
            }
            totalGongli = Math.Abs(startMile - endMile);


            List<StatisticsData> dataList = new List<StatisticsData>();

            try
            {
                int typeNum = 0;//无效区段类型的个数
                Dictionary<int, String> dicInvalidType = new Dictionary<int, String>();//无效区段类型

                DataTable dt = invalidManager.GetValidDataType();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dicInvalidType.Add(Convert.ToInt32(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString());
                }

                typeNum = dicInvalidType.Count;

                List<InvalidData> list = invalidManager.InvalidDataList(idfFilePath);

                for (int i = 0; i < typeNum; i++)
                {
                    List<InvalidData> listNew = list.Where(s => s.iType == i).ToList();

                    StatisticsData statisticsDataCls = null;
                    for (int j = 0; j < listNew.Count; j++)
                    {
                        if (statisticsDataCls == null)
                        {
                            statisticsDataCls = new StatisticsData(totalCount, totalGongli);
                        }
                        statisticsDataCls.reasonType = dicInvalidType[listNew[j].iType];//类型
                        statisticsDataCls.sumcount++;
                        startMile = (int)(float.Parse(listNew[j].sStartMile) * 1000);
                        endMile = (int)(float.Parse(listNew[j].sEndMile) * 1000);

                        statisticsDataCls.sumGongli = statisticsDataCls.sumGongli + Math.Abs(endMile - startMile);
                    }

                    if (statisticsDataCls != null)
                    {
                        dataList.Add(statisticsDataCls);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MyLogger.LogError("统计无效数据时失败", ex);
                MessageBox.Show(ex.Message);
            }

            //无效区段统计窗体
            InvalidDataStatisticsForm  statisticsForm = new InvalidDataStatisticsForm(dataList);
            //statisticsForm.TopLevel = true;
            statisticsForm.Show();
        }


        /// <summary>
        /// 表格单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            String value = dataGridView1[1, e.RowIndex].Value.ToString();

            //根据文件指针来定位波形，更精确
            long pos = long.Parse(value);
            //MainForm.sMainform.MeterFind(pos);
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// 加载无效数据
        /// </summary>
        public void GetInvalidData()
        {
            dataGridView1.Rows.Clear();
            try
            {
                DataTable dt = invalidManager.GetValidDataType();
                waveformMaker.WaveformDataList[0].InvalidDataList.Clear();
                idfFilePath = waveformMaker.WaveformDataList[0].WaveIndexFilePath;
                waveformMaker.WaveformDataList[0].InvalidDataList = invalidManager.InvalidDataList(idfFilePath);
                if(waveformMaker!=null&& waveformMaker.WaveformDataList[0].InvalidDataList.Count>0)
                { for (int i = 0; i < waveformMaker.WaveformDataList[0].InvalidDataList.Count; i++)
                    {
                        object[] o = new object[9];
                        o[0] = waveformMaker.WaveformDataList[0].InvalidDataList[i].iId;
                        o[1] = long.Parse(waveformMaker.WaveformDataList[0].InvalidDataList[i].sStartPoint);
                        o[2] = long.Parse(waveformMaker.WaveformDataList[0].InvalidDataList[i].sEndPoint);
                        o[3] = float.Parse(waveformMaker.WaveformDataList[0].InvalidDataList[i].sStartMile);
                        o[4] = float.Parse(waveformMaker.WaveformDataList[0].InvalidDataList[i].sEndMile);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j][0].ToString() == waveformMaker.WaveformDataList[0].InvalidDataList[i].iType.ToString())
                            {
                                o[5] = dt.Rows[j][1].ToString();
                            }
                        }

                        o[6] = waveformMaker.WaveformDataList[0].InvalidDataList[i].sMemoText;
                        o[7] = waveformMaker.WaveformDataList[0].InvalidDataList[i].iIsShow;
                        o[8] = waveformMaker.WaveformDataList[0].InvalidDataList[i].ChannelType;
                        dataGridView1.Rows.Add(o);
                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.LogError("获取无效数据时失败", ex);
                MessageBox.Show("请删除idf文件，错误描述：" + ex.Message);
            }
        }

        /// <summary>
        /// 将String类型转换成Float类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float StringToFloat(String value)
        {
            String tmpStr = value.Replace("K", "");
            tmpStr = tmpStr.Replace(".", "");
            tmpStr = tmpStr.Replace("+", ".");

            float retVal = float.Parse(tmpStr);

            return retVal;
        }

        private void InvalidDataForm_Load(object sender, EventArgs e)
        {
           

            waveFormDataList = waveformMaker.WaveformDataList;
            citFilePath = waveFormDataList[0].CitFilePath;
            idfFilePath = waveFormDataList[0].WaveIndexFilePath;



            //waveformMaker.WaveformDataList[0].InvalidDataList[i] = invalidManager.InvalidDataList(idfFilePath);

            if (fi == null)
            {
                fi = waveFormDataList[0].CitFile;

                //if (fi.iKmInc == 0)
                //{
                //    sKmInc = "增";
                //}
                //else if (fi.iKmInc == 1)
                //{
                //    sKmInc = "减";
                //}

                iChannelNumber = fi.iChannelNumber;
            }
        }
    }
}
