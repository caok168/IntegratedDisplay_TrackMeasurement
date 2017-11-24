
using IntegratedDisplay.Models;
/// -------------------------------------------------------------------------------------------
/// FileName：InvalidDataAddForm.cs
/// 说    明：无效数据添加窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 无效数据添加窗口
    /// </summary>
    public partial class InvalidDataAddForm : Form
    {
        WaveformMaker waveformMaker = null;

        List<WavefromData> waveFormDataList = new List<WavefromData>();

        /// <summary>
        /// idf文件路径
        /// </summary>
        public string idfFilePath = "";

        public string citFilePath = "";

        InvalidDataManager invalidManager = new InvalidDataManager();

        public InvalidDataAddForm()
        {
            InitializeComponent();
        }

        public InvalidDataAddForm(WaveformMaker maker)
        {
            waveformMaker = maker;

            waveFormDataList = waveformMaker.WaveformDataList;

            citFilePath = waveFormDataList[0].CitFilePath;

            idfFilePath = waveFormDataList[0].WaveIndexFilePath;

            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvalidDataAddForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            DataTable dt = invalidManager.GetValidDataType();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvalidDataAddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            invalidManager.InvalidDataInsertInto(idfFilePath,
                textBoxStartPoint1.Text, textBoxEndPoint.Text, 
                textBoxStartMile1.Text, textBoxEndMile.Text, 
                comboBox1.SelectedIndex, textBoxMemo.Text, "手工标识");

            this.Hide();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        

    }
}
