using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.CustomControl;

namespace IntegratedDisplay.Forms
{
    public partial class MileageLocationForm : Form
    {
        private WaveformMaker _maker = null;

        public MileageLocationForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void txtMileage_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtMileage.Text.Trim()))
            {
                float location = 0f;
                if(float.TryParse(txtMileage.Text.Trim(),out location))
                {
                    if (location <= 0)
                    {
                        errorProvider.SetError(txtMileage, "里程数必须大于零");
                        txtMileage.Text = "";
                        txtMileage.Focus();
                    }
                    else
                    {
                        this.Tag = location * 1000;
                    }
                }
                else
                {
                    errorProvider.SetError(txtMileage, "请输入一个大于零的数字");
                    txtMileage.Text = "";
                    txtMileage.Focus();
                }

            }
        }

        /// <summary>
        /// 定位按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocation_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtMileage.Text.Trim()))
            {
                errorProvider.SetError(txtMileage, "请输入一个大于零的数字");
                txtMileage.Text = "";
                txtMileage.Focus();
                return;
            }
            float mileage = float.Parse(txtMileage.Text.Trim()) * 1000;
            long locationPostion = 0;
            
            int offset = _maker.GetLocationScrollSize(mileage, ref locationPostion);
            if (offset < 0)
            {
                MessageBox.Show("未找到当前里程！");
            }
            else
            {
                //将定位条放到中间
                if (offset > 10)
                {
                    offset -= 5;
                }
                else
                {
                    offset -= offset / 2;
                }
                MainForm.sMainform.IsMileageLocation = true;
                MainForm.sMainform.LocationPostion = locationPostion;
                MainForm.sMainform.InvokeScroolBar_Scroll(sender, offset);
            }
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 文本变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMileage_TextChanged(object sender, EventArgs e)
        {
            errorProvider.Clear();
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMileage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLocation_Click(sender, e);
            }
        }
    }
}
