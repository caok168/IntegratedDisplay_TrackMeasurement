/// -------------------------------------------------------------------------------------------
/// FileName：LabelInfoAddForm.cs
/// 说    明：标注添加窗体
/// Version ：1.0
/// Date    ：2017/6/5
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CitFileSDK;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 标注添加窗体
    /// </summary>
    public partial class LabelInfoAddForm : Form
    {

        CITFileProcess citHelper = new CITFileProcess();

        WaveformMaker waveformMaker = null;
        List<WavefromData> waveFormDataList = new List<WavefromData>();

        /// <summary>
        /// cit文件路径
        /// </summary>
        string citFilePath = "";
        /// <summary>
        /// idf文件路径
        /// </summary>
        string idfFilePath = "";

        LabelInfoManager lableManager = new LabelInfoManager();
        List<LabelInfo> labelInfoList = new List<LabelInfo>();

        private long _filePostion = 0;
        private int _signY = 0;


        public LabelInfoAddForm(WaveformMaker maker,long postion,int Y)
        {
            InitializeComponent();

            waveformMaker = maker;
            citFilePath = waveformMaker.WaveformDataList[0].CitFilePath;
            idfFilePath = waveformMaker.WaveformDataList[0].WaveIndexFilePath;
            _filePostion = postion;
            _signY = Y;
        }

        private void LabelInfoAddForm_Load(object sender, EventArgs e)
        {
            long endPosition = 0;

            List<Milestone> MilestoneList = citHelper.GetMileStoneByRange(citFilePath, _filePostion, 1, ref endPosition);

            if (MilestoneList.Count > 0)
            {
                //当前点的公里
                double mil = (MilestoneList[0].mKm) + (MilestoneList[0].mMeter / 1000);
                txtMeter.Text = mil.ToString();
                txtMeterIndex.Text = _filePostion.ToString();
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            lableManager.Insert(idfFilePath, this.txtMeterIndex.Text.Trim(), this.txtMeter.Text.Trim(), this.txtMemoText.Text.Trim(), DateTime.Now.ToShortDateString(), _signY);

            labelInfoList = lableManager.GetDataLabelInfo(idfFilePath);
            waveformMaker.WaveformDataList[0].LabelInfoList = labelInfoList;
            btnClose_Click(sender, e);
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
