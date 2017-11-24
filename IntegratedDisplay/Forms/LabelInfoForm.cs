using CitIndexFileSDK;
using IntegratedDisplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    public partial class LabelInfoForm : Form
    {
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

        public LabelInfoForm()
        {
            InitializeComponent();
        }

        public LabelInfoForm(WaveformMaker maker)
        {
            InitializeComponent();

            waveformMaker = maker;
            citFilePath = waveformMaker.WaveformDataList[0].CitFilePath;
            idfFilePath = waveformMaker.WaveformDataList[0].WaveIndexFilePath;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelInfoForm_Load(object sender, EventArgs e)
        {
            labelInfoList = lableManager.GetDataLabelInfo(idfFilePath);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < labelInfoList.Count; i++)
            {

                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(dataGridView1);

                dgvr.Cells[0].Value = labelInfoList[i].iID;
                dgvr.Cells[1].Value = labelInfoList[i].sMileIndex;

                dgvr.Cells[2].Value = labelInfoList[i].sMile;

                dgvr.Cells[3].Value = labelInfoList[i].sMemoText;
                if (!string.IsNullOrEmpty(labelInfoList[i].logDate))
                {
                    dgvr.Cells[4].Value = Convert.ToDateTime(labelInfoList[i].logDate).ToShortDateString();
                }
                dataGridView1.Rows.Add(dgvr);
            }
            waveformMaker.WaveformDataList[0].LabelInfoList = labelInfoList;
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                string id = dataGridView1.Rows[0].Cells[0].Value.ToString();
                waveformMaker.WaveformDataList[0].IndexOperator.ExcuteSql("delete from LabelInfo where id=" + id);
                LabelInfoForm_Load(sender, e);
            }
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击表格事件
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
    }
}
