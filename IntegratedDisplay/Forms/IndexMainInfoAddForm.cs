/// -------------------------------------------------------------------------------------------
/// FileName：IndexManager.cs
/// 说    明：索引标定 窗体
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// 索引标定
    /// </summary>
    public partial class IndexMainInfoAddForm : Form
    {
        public IndexMainInfoAddForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化索引标定显示框中的文件指针记录号和文件原始里程文本框
        /// </summary>
        /// <param name="lPostion">文件指针记录号</param>
        /// <param name="iKM">公里标(单位为公里)</param>
        /// <param name="iMeter">偏移量(单位为米)</param>
        public IndexMainInfoAddForm(long lPostion, int iKM, float iMeter)
        {
            InitializeComponent();
            txtPosition.Text = lPostion.ToString();
            txtOrigialMile.Text = (iKM + (iMeter / 1000.0f)).ToString();
            //txtIndexMile.Text = CommonClass.sLastSelectText;
            
            txtIndexMile.Text = "";
            label4.Text = "KM" + iKM.ToString() + "+" + iMeter.ToString();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Boolean retVal = Textbox_Check(txtIndexMile);
            if (!retVal)
            {
                return;
            }

            if (txtIndexMile.Text.Length < 1)
            {
                btn_Cancel_Click(sender, e);
            }
            this.Tag = txtIndexMile.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 返回按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 验证文本框格式是否正确
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private Boolean Textbox_Check(TextBox tb)
        {
            String tbStr = tb.Text;

            if (String.IsNullOrEmpty(tbStr))
            {
                MessageBox.Show("里程不能为空！");
                return false;
            }
            else
            {
                try
                {
                    float mile = float.Parse(tbStr);
                    if (mile < 0)
                    {
                        MessageBox.Show("里程数必须大于或等于零！");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入数字！");
                    return false;
                }
            }
            return true;
        }
    }
}
