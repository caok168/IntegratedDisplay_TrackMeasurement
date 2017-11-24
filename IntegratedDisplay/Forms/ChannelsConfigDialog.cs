/// -------------------------------------------------------------------------------------------
/// FileName：ChannelsConfigDialog.cs
/// 说    明：批量设置窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 批量设置
    /// </summary>
    public partial class ChannelsConfigDialog : Form
    {
        List<int> Index;
        int iLayerID = -1;

        public ChannelsDialog dialog;

        public List<ChannelsClass> list;

        public ChannelsConfigDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChannelsConfigDialog_Load(object sender, EventArgs e)
        {
            Index = new List<int>();
            string sPar = this.Tag.ToString();
            string[] sSplit = sPar.Split(new char[] { ',' });
            iLayerID = int.Parse(sSplit[0]);

            list = dialog.listChannel;

            for (int i = 1; i < sSplit.Length; i++)
            {
                Index.Add(int.Parse(sSplit[i]));
                comboBox1.Items.Add(list[int.Parse(sSplit[i])].ChineseName);
                comboBox2.Items.Add(list[int.Parse(sSplit[i])].NonChineseName);
                comboBox3.Items.Add(list[int.Parse(sSplit[i])].ZoomIn);
                comboBox4.Items.Add(list[int.Parse(sSplit[i])].Location);
                comboBox6.Items.Add(list[int.Parse(sSplit[i])].LineWidth);
            }
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton1_Click(object sender, EventArgs e)
        {
            //依次批量设置内容
            if (checkBox1.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].ChineseName = comboBox1.Text;
                }
            }
            if (checkBox2.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].NonChineseName = comboBox2.Text;
                }
            }
            if (checkBox4.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].ZoomIn = float.Parse(comboBox3.Text);
                }
            }
            if (checkBox5.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].Location = int.Parse(comboBox4.Text);
                }
            }
            if (checkBox7.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].LineWidth = float.Parse(comboBox6.Text);
                }
            }

            if (checkBox3.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].Color = pictureBox1.BackColor.ToArgb();
                }
            }

            if (checkBox6.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].IsMeaOffset = checkBox9.Checked;
                }
            }

            if (checkBox8.Checked)
            {
                foreach (int i in Index)
                {
                    list[i].IsVisible = checkBox10.Checked;
                }
            }
            CancelButton1_Click(sender, e);
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 显示颜色进行选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// 是否显示选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            GetCheckBoxState();
        }

        /// <summary>
        /// 获取选择框的状态
        /// </summary>
        private void GetCheckBoxState()
        {
            bool b = false;
            for (int i = 1; i < 9; i++)
            {
                if (((CheckBox)this.Controls["checkBox" + i.ToString()]).Checked)
                {
                    b = true;
                    break;
                }
            }
            SaveButton1.Enabled = b;
        }

        
    }
}
