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
    public partial class LayerControlForm : Form
    {
        public delegate void SelectedValueChangedDelegage();

        public event SelectedValueChangedDelegage SelectedValueChangedEvent;
        private WaveformMaker _maker;
        public LayerControlForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void LayerControlForm_Load(object sender, EventArgs e)
        {
            dgvLayerConfig.Rows.Clear();
            for(int i=0;i<_maker.WaveformDataList.Count;i++)
            {
                object[] row = new object[6];
                row[0] = _maker.WaveformDataList[i].LayerConfig.Name;
                row[1] = _maker.WaveformDataList[i].LayerConfig.IsVisible;
                row[2] = _maker.WaveformDataList[i].LayerConfig.IsMileageLabelVisible;
                row[3] = _maker.WaveformDataList[i].LayerConfig.IsChannelLabelVisible;
                row[4] = _maker.WaveformDataList[i].LayerConfig.IsAnnotationVisible;
                row[5] = _maker.WaveformDataList[i].LayerConfig.IsReverse;
                dgvLayerConfig.Rows.Add(row);
            }
            if(dgvLayerConfig.Rows.Count>0)
            {
                dgvLayerConfig.Rows[0].ReadOnly = true;
                dgvLayerConfig.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dgvLayerConfig.Rows.Count<1)
            {
                return;
            }
            for (int i = 1; i < dgvLayerConfig.Rows.Count; i++)
            {
                _maker.WaveformDataList[i].LayerConfig.IsVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[1].Value.ToString());
                _maker.WaveformDataList[i].LayerConfig.IsMileageLabelVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[2].Value.ToString());
                _maker.WaveformDataList[i].LayerConfig.IsChannelLabelVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[3].Value.ToString());
                _maker.WaveformDataList[i].LayerConfig.IsAnnotationVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[4].Value.ToString());
                _maker.WaveformDataList[i].LayerConfig.IsReverse = bool.Parse(dgvLayerConfig.Rows[i].Cells[5].Value.ToString());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dgvLayerConfig.Rows.Count < 1)
            {
                return;
            }
            this.Close();
        }

        private void dgvLayerConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvLayerConfig.Rows.Count < 1)
            //{
            //    return;
            //}
            //for (int i = 1; i < dgvLayerConfig.Rows.Count; i++)
            //{
            //    _maker.WaveformDataList[i].LayerConfig.IsVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[1].Value.ToString());
            //    _maker.WaveformDataList[i].LayerConfig.IsMileageLabelVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[2].Value.ToString());
            //    _maker.WaveformDataList[i].LayerConfig.IsChannelLabelVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[3].Value.ToString());
            //    _maker.WaveformDataList[i].LayerConfig.IsAnnotationVisible = bool.Parse(dgvLayerConfig.Rows[i].Cells[4].Value.ToString());
            //    _maker.WaveformDataList[i].LayerConfig.IsReverse = bool.Parse(dgvLayerConfig.Rows[i].Cells[5].Value.ToString());
            //}
            //if (dgvLayerConfig.Rows.Count < 1 || e.RowIndex <= 0)
            //{
            //    return;
            //}

            ////由于此时控件还没有被check，所以要取反
            //switch (e.ColumnIndex)
            //{
            //    case 1:
            //        {
            //            _maker.WaveformDataList[e.RowIndex].LayerConfig.IsVisible = !bool.Parse(dgvLayerConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //            break;
            //        }
            //    case 2:
            //        {
            //            _maker.WaveformDataList[e.RowIndex].LayerConfig.IsMileageLabelVisible = !bool.Parse(dgvLayerConfig.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //            break;
            //        }
            //    case 3:
            //        {
            //            _maker.WaveformDataList[e.RowIndex].LayerConfig.IsChannelLabelVisible = !bool.Parse(dgvLayerConfig.Rows[e.RowIndex].Cells[3].Value.ToString());
            //            break;
            //        }
            //    case 4:
            //        {
            //            _maker.WaveformDataList[e.RowIndex].LayerConfig.IsAnnotationVisible = !bool.Parse(dgvLayerConfig.Rows[e.RowIndex].Cells[4].Value.ToString());
            //            break;
            //        }
            //    case 5:
            //        {
            //            _maker.WaveformDataList[e.RowIndex].LayerConfig.IsReverse = !bool.Parse(dgvLayerConfig.Rows[e.RowIndex].Cells[5].Value.ToString());
            //            break;
            //        }
            //}
            //SelectedValueChangedEvent?.Invoke();
        }

        private void LayerControlForm_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void LayerControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

    }
}
