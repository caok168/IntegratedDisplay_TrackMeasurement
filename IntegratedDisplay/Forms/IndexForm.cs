/// -------------------------------------------------------------------------------------------
/// FileName：IndexManager.cs
/// 说    明：索引设置 窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using CitFileSDK;
using CitIndexFileSDK;
using CitIndexFileSDK.MileageFix;

namespace IntegratedDisplay.Forms
{
    public enum FormModel
    {
        Show,
        Hide
    }

    /// <summary>
    /// 索引设置
    /// </summary>
    public partial class IndexForm : Form
    {

        public delegate void IndexCloseDelegate();
        public IndexCloseDelegate delegateIndexClosed;

        WaveformMaker waveformMaker = null;
        UserFixedTable _fixedTable = null;
        public FormModel ShowFormModel { get; set; }
        

        public IndexForm()
        {
            InitializeComponent();
        }

        public IndexForm(WaveformMaker maker)
        {
            InitializeComponent();
            waveformMaker = maker;
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IndexForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.sMainform.picMainGraphics.Cursor = Cursors.Default;
            MainForm.sMainform.wasIndex = false;
            if(ShowFormModel== FormModel.Hide)
            {
                delegateIndexClosed?.Invoke();
            }
            e.Cancel = true;
            this.Visible = false;
            MainForm.sMainform.Activate();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //删除
            _fixedTable = new UserFixedTable(waveformMaker.WaveformDataList[0].IndexOperator,waveformMaker.WaveformDataList[0].CitFile.iKmInc);
            _fixedTable.Clear();
            //重新保存索引库
            for (int i = 0; i < dgvMarkedPoints.Rows.Count; i++)
            {
                UserMarkedPoint markedPoint = new UserMarkedPoint();
                markedPoint.ID = dgvMarkedPoints.Rows[i].Cells[0].Value.ToString();
                markedPoint.FilePointer = long.Parse(dgvMarkedPoints.Rows[i].Cells[2].Value.ToString());
                markedPoint.UserSetMileage = float.Parse(dgvMarkedPoints.Rows[i].Cells[3].Value.ToString()) * 1000;
                _fixedTable.MarkedPoints.Add(markedPoint);
            }
            _fixedTable.Save();
            //创建计算后的索引库
            MilestoneFix fix = new MilestoneFix(waveformMaker.WaveformDataList[0].CitFilePath, waveformMaker.WaveformDataList[0].IndexOperator);
            try
            {
                fix.ClearMilestoneFixTable();
                fix.RunFixingAlgorithm();
                fix.SaveMilestoneFixTable();
                MessageBox.Show("保存成功！");
                btnLoad_Click(sender, e);

            }
            catch(Exception ex)
            {
                MyLogger.LogError("手动里程修正失败", ex);
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        /// <summary>
        /// 删除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMarkedPoints.SelectedRows.Count == 1)
            {
                string ID = dgvMarkedPoints.SelectedRows[0].Cells[0].Value.ToString();
                _fixedTable.Delete(ID);
                ShowList();
            }
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 展开或者合闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (btnRight.Text.Equals(">"))
            {
                btnRight.Text = "<";
                this.Width = 955;
            }
            else
            {
                btnRight.Text = ">";
                this.Width = 505;
            }
        }


        /// <summary>
        /// 加载按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {

            MilestoneFix fix = new MilestoneFix(waveformMaker.WaveformDataList[0].CitFilePath,waveformMaker.WaveformDataList[0].IndexOperator);
            fix.ReadMilestoneFixTable();
            dataGridView1.Rows.Clear();
            if (fix.FixData != null && fix.FixData.Count > 0)
            {
                for (int i = 0; i < fix.FixData.Count; i++)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(dataGridView1);

                    dgvr.Cells[0].Value = fix.FixData[i].ID;
                    dgvr.Cells[1].Value = "";
                    dgvr.Cells[2].Value = fix.FixData[i].RealDistance / 1000.0f;
                    dgvr.Cells[3].Value = fix.FixData[i].SamplePointCount;

                    dgvr.Cells[4].Value = fix.FixData[i].SampleRate;
                    dgvr.Cells[5].Value = fix.FixData[i].MarkedStartPoint.FilePointer;
                    dgvr.Cells[6].Value = fix.FixData[i].MarkedEndPoint.FilePointer;
                    dgvr.Cells[7].Value = fix.FixData[i].MarkedStartPoint.UserSetMileage / 1000;
                    dgvr.Cells[8].Value = fix.FixData[i].MarkedEndPoint.UserSetMileage / 1000;

                    if (fix.FixData[i].SampleRate > 0.2510000 || fix.FixData[i].SampleRate < 0.2490000)
                    {
                        if (fix.FixData[i].SampleRate > 0.2510000)
                        {
                            dgvr.Cells[4].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvr.Cells[4].Style.ForeColor = Color.Blue;
                        }
                        //dgvr.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgvr.Cells[4].Style.ForeColor = Color.Black;
                    }
                    
                    dataGridView1.Rows.Add(dgvr);
                }
            }
        }

        private void IndexForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ShowList();
                btnLoad_Click(sender, e);
                if(ShowFormModel== FormModel.Show)
                {
                    btnRight.Text = "<";
                    this.Width = 955;
                }
                else
                {
                    btnRight.Text = ">";
                    this.Width = 505;
                }
                Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
                this.Location = p;
            }
        }

        /// <summary>
        /// 索引列表显示
        /// </summary>
        private void ShowList()
        {
            _fixedTable = new UserFixedTable(waveformMaker.WaveformDataList[0].IndexOperator,waveformMaker.WaveformDataList[0].CitFile.iKmInc);
            dgvMarkedPoints.Rows.Clear();
            if (_fixedTable != null && _fixedTable.MarkedPoints != null && _fixedTable.MarkedPoints.Count > 0)
            {
                for (int i = 0; i < _fixedTable.MarkedPoints.Count; i++)
                {
                    object[] oPara = new object[4];
                    oPara[0] = _fixedTable.MarkedPoints[i].ID;
                    oPara[1] = "正常";
                    oPara[2] = _fixedTable.MarkedPoints[i].FilePointer;
                    oPara[3] = _fixedTable.MarkedPoints[i].UserSetMileage / 1000;
                    dgvMarkedPoints.Rows.Add(oPara);
                }
            }
            
        }

        /// <summary>
        /// 把一条索引信息保存在 IndexOri表
        /// </summary>
        /// <param name="lPostion">文件指针</param>
        /// <param name="sIndexKm">新的索引里程（单位：公里）</param>
        public void SetIndexInfo(long lPostion, string sIndexKm)
        {
            UserMarkedPoint markedPoint = new UserMarkedPoint();
            markedPoint.FilePointer = lPostion;
            markedPoint.UserSetMileage = float.Parse(sIndexKm) * 1000;
            _fixedTable.Save(markedPoint);
            ShowList();
        }

        /// <summary>
        /// 索引表格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 索引显示表格dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                String value = dgvMarkedPoints[2, e.RowIndex].Value.ToString();

                //根据文件指针来定位波形，更精确
                long pos = long.Parse(value);
                int offset = waveformMaker.GetLocationScrollSize(pos);
                MainForm.sMainform.IsMileageLocation = true;
                MainForm.sMainform.LocationPostion = pos;
                MainForm.sMainform.InvokeScroolBar_Scroll(sender, offset);
            }
        }

        private void IndexForm_Load(object sender, EventArgs e)
        {

            
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[4].Style.ForeColor == Color.Red)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText = "采样点间隔可能过大";
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells[4].Style.ForeColor == Color.Blue)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText = "采样点间隔可能过小";
                }
            }
        }

        private void btnAutoAddPoint_Click(object sender, EventArgs e)
        {
            AutoIndexForm autoIndexForm = new AutoIndexForm(waveformMaker);
            if (autoIndexForm.ShowDialog() == DialogResult.OK)
            {
                ShowList();
                btnLoad_Click(sender, e);
            }
        }
    }
}
