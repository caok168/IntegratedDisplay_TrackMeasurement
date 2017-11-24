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
    public partial class LayerTranslationForm : Form
    {
        public delegate void ReviseValueChangedDelegage();

        public event ReviseValueChangedDelegage ReviseValueChangedEvent;

        WaveformMaker _maker;
        public LayerTranslationForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void LayerTranslationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            MainForm.sMainform.Activate();
        }

        private void LayerTranslationForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            try
            {
                dgvLayerInfo.Rows.Clear();
                for (int i = 0; i < _maker.WaveformDataList.Count; i++)
                {
                    object[] cell = new object[7];
                    cell[0] = (i + 1).ToString();
                    cell[1] = _maker.WaveformDataList[i].CitFile.sTrackName + _maker.WaveformDataList[i].CitFile.iDir;
                    cell[2] = _maker.WaveformDataList[i].CitFile.sDate;
                    string dir = _maker.WaveformDataList[i].CitFile.iRunDir == 0 ? "正" : "反";
                    cell[3] = dir;
                    cell[4] = _maker.WaveformDataList[i].MileList.milestoneList[0].GetMeterString();
                    string inc = _maker.WaveformDataList[i].CitFile.iKmInc == 0 ? "增" : "减";
                    cell[5] = inc;
                    cell[6] = _maker.WaveformDataList[i].CitFile.sTime;
                    dgvLayerInfo.Rows.Add(cell);
                }
            }
            catch(Exception ex)
            {
                MyLogger.LogError("加载图层数据时出错", ex);
                MessageBox.Show("加载图层数据时出错：" + ex.Message);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            if (dgvLayerInfo.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvLayerInfo.SelectedRows.Count; i++)
                {
                    int index = int.Parse(dgvLayerInfo.SelectedRows[i].Cells[0].Value.ToString());
                    long times = long.Parse(((Button)sender).Tag.ToString());
                    if (index - 1 > 0)
                    {
                        if (times != -1 && times != 1)
                        {
                            _maker.WaveformDataList[index - 1].ReviseValue += times;
                        }
                        else
                        {
                            _maker.WaveformDataList[index - 1].ReviseValue += (times * (long)nupReviseValue.Value);
                        }
                    }
                }
                ReviseValueChangedEvent();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Tag = timer.Tag;
            btnAll_Click(btn, e);
        }

        private void btnAll_MouseDown(object sender, MouseEventArgs e)
        {
            timer.Enabled = true;
            timer.Tag = (sender as Button).Tag;
        }

        private void btnAll_MouseUp(object sender,MouseEventArgs e)
        {
            timer.Enabled = false;
        }

        private void dgvLayerInfo_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvLayerInfo.Rows.Count>0)
            {
                if (dgvLayerInfo.SelectedRows != null && dgvLayerInfo.SelectedRows.Count > 0 && dgvLayerInfo.SelectedRows[0].Index == 0)
                {
                    tsbtnMileageAlignment.Enabled = false;
                    tsbtnWaveAlignment.Enabled = false;
                    tsbtnResetOffset.Enabled = false;
                    btnLeft50X.Enabled = false;
                    btnLeft10X.Enabled = false;
                    btnLeft1X.Enabled = false;
                    btnRight50X.Enabled = false;
                    btnRight10X.Enabled = false;
                    btnRight1X.Enabled = false;
                }
                else
                {
                    tsbtnMileageAlignment.Enabled = true;
                    tsbtnWaveAlignment.Enabled = true;
                    tsbtnResetOffset.Enabled = true;
                    btnLeft50X.Enabled = true;
                    btnLeft10X.Enabled = true;
                    btnLeft1X.Enabled = true;
                    btnRight50X.Enabled = true;
                    btnRight10X.Enabled = true;
                    btnRight1X.Enabled = true;
                }
            }
            
        }

        private void tsbtnResetOffset_Click(object sender, EventArgs e)
        {
            if (dgvLayerInfo.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvLayerInfo.SelectedRows.Count; i++)
                {
                    int index = int.Parse(dgvLayerInfo.SelectedRows[i].Cells[0].Value.ToString());
                    if (index - 1 > 0)
                    {
                        _maker.WaveformDataList[index - 1].ReviseValue = 0;
                    }
                }
                ReviseValueChangedEvent();
                MessageBox.Show("偏移重置成功!");
            }
        }

        private void tsbtnMileageAlignment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLayerInfo.SelectedRows.Count > 0)
                {
                    float mileage = Convert.ToSingle(dgvLayerInfo.Rows[0].Cells[4].Value);
                    for (int i = 0; i < dgvLayerInfo.SelectedRows.Count; i++)
                    {
                        int index = int.Parse(dgvLayerInfo.SelectedRows[i].Cells[0].Value.ToString());
                        if (index - 1 > 0)
                        {
                            _maker.AutoMileageAlignmenTranslation(index - 1, mileage);
                        }
                    }
                    ReviseValueChangedEvent();
                    MessageBox.Show("里程对齐完成!");
                }
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("里程对齐时出错：" + ex.Message + "堆栈：" + ex.StackTrace);
                MessageBox.Show("遇到错误：" + ex.Message);
            }
        }

        private void tsbtnWaveAlignment_Click(object sender, EventArgs e)
        {
            if (dgvLayerInfo.SelectedRows.Count > 0)
            {
                try
                {
                    _maker.AutoWaveAlignmentTranslation();
                    ReviseValueChangedEvent();
                    MessageBox.Show("波形对齐完成!");
                }
                catch(Exception ex)
                {
                    MyLogger.logger.Error("波形对齐时出错：" + ex.Message + "堆栈：" + ex.StackTrace);
                    MessageBox.Show("遇到错误：" + ex.Message);
                }

            }
            
           
        }
    }
}
