using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.Models;

namespace IntegratedDisplay
{
    public partial class CommonConfigControl : UserControl
    {
        /// <summary>
        /// 导出路径
        /// </summary>
        public string ExportPath { get; set; }

        /// <summary>
        /// 测量半径
        /// </summary>
        public int MeterageRadius { get; set; }

        /// <summary>
        /// 自动滚动速度
        /// </summary>
        public int AutoScrollVelocity { get; set; }

        /// <summary>
        /// 标注半径
        /// </summary>
        public int SignRadius { get; set; }

        /// <summary>
        /// 媒体库目录
        /// </summary>
        public string MediaPath { get; set; }
        
        public CommonConfigControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 通用配置控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonConfigControl_Load(object sender, EventArgs e)
        {
            if(SignRadius>trackBarSignSize.Maximum)
            {
                SignRadius = trackBarSignSize.Maximum;
            }
            else if(SignRadius<trackBarSignSize.Minimum)
            {
                SignRadius = trackBarSignSize.Minimum;
            }
            trackBarSignSize.Value = SignRadius;
            //txtExportPath.Text = ExportPath;
            if(AutoScrollVelocity>trackBarAutoScrool.Maximum)
            {
                AutoScrollVelocity = trackBarAutoScrool.Maximum;
            }
            else if(AutoScrollVelocity <trackBarAutoScrool.Minimum)
            {
                AutoScrollVelocity = trackBarAutoScrool.Minimum;
            }
            trackBarAutoScrool.Value = AutoScrollVelocity;
            txtMeterageRadius.Text = MeterageRadius.ToString();
            txtMidiaPath.Text = MediaPath;
        }

        ///// <summary>
        ///// 选择导出目录事件
        ///// </summary>
        ///// <param name="sender">触发对象(导出按钮)</param>
        ///// <param name="e">触发参数</param>
        //private void btnSelectExportPath_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog dialog = new FolderBrowserDialog();
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        txtExportPath.Text = dialog.SelectedPath;
        //        ExportPath = txtExportPath.Text;
        //    }
        //}

        /// <summary>
        /// 编辑内容后触发事件
        /// </summary>
        /// <param name="sender">触发对象(测量半径和滚动速度输入框)</param>
        /// <param name="e">触发参数</param>
        private void txtEdit_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                int num = 0;
                if (int.TryParse(textBox.Text.Trim(), out num))
                {
                    if (num > 0 && num <= 500)
                    {
                        MeterageRadius = num;
                    }
                    else
                    {
                        MessageBox.Show("请输入大于0并且小于等于500的整数！");
                        textBox.Text = string.Empty;
                        textBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("请输入一个大于0整数！");
                    textBox.Text = string.Empty;
                    textBox.Focus();
                }
            }
            else
            {
                textBox.Text = MeterageRadius.ToString();
            }
        }

        private void btnSelectMediaPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtMidiaPath.Text = dialog.SelectedPath;
                MediaPath = txtMidiaPath.Text;
            }
        }

        private void trackBarAutoScrool_Scroll(object sender, EventArgs e)
        {
            
            AutoScrollVelocity = trackBarAutoScrool.Value;
            //float rate = (trackBarAutoScrool.Value-trackBarAutoScrool.Minimum) * 1.0f / ((trackBarAutoScrool.Maximum - trackBarAutoScrool.Minimum) * 1.0f);
            //int lineWidth = (int)(trackBarAutoScrool.Width * rate);
            int width = (trackBarAutoScrool.Width-22) * (trackBarAutoScrool.Value-trackBarAutoScrool.Minimum) / (trackBarAutoScrool.Maximum - trackBarAutoScrool.Minimum);
            labScrollShow.Text = AutoScrollVelocity.ToString();
            labScrollShow.Location = new Point(trackBarAutoScrool.Location.X+width, trackBarAutoScrool.Location.Y+30);
        }

        private void trackBarSignSize_Scroll(object sender, EventArgs e)
        {
            SignRadius = trackBarSignSize.Value;
        }

        private void trackBarAutoScrool_MouseDown(object sender, MouseEventArgs e)
        {
            labScrollShow.Visible = true;
            labScrollShow.Text = trackBarAutoScrool.Value.ToString();
        }

        private void trackBarAutoScrool_MouseUp(object sender, MouseEventArgs e)
        {
            labScrollShow.Visible = false;
        }
    }
}
