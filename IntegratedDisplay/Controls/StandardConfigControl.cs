using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using CommonFileSDK;

namespace IntegratedDisplay
{
    public partial class StandardConfigControl : UserControl
    {

        /// <summary>
        /// 标准值控件列表
        /// </summary>
        private List<TextBox> _txtStandardList = new List<TextBox>();

        /// <summary>
        /// 自定义值控件列表
        /// </summary>
        private List<TextBox> _txtCustomList = new List<TextBox>();

        /// <summary>
        /// 类型字典
        /// </summary>
        private Dictionary<string, string> _dicExcptnType = new Dictionary<string, string>();

        /// <summary>
        /// 标准类型
        /// </summary>
        private int _standerdType = 0;

        /// <summary>
        /// 类别
        /// </summary>
        private int _defectLevel = 1;

        /// <summary>
        /// 速度
        /// </summary>
        private int _speedClass = 120;

        /// <summary>
        /// 标准及自定义数据表
        /// </summary>
        public DataTable dtStandardValue { get; set; }


        public StandardConfigControl()
        {
            InitializeComponent();
            dtStandardValue = new DataTable();
        }

        /// <summary>
        /// 初始化标准值控件列表
        /// </summary>
        private void InitStandardTextboxList()
        {
            if (_txtStandardList.Count > 0)
            {
                _txtStandardList.Clear();
            }
            _txtStandardList.Add(txt_Std_Align_SC);
            _txtStandardList.Add(txt_Std_Align_SC_120);
            _txtStandardList.Add(txt_Std_Align_SC_70);
            _txtStandardList.Add(txt_Std_CrossLevel);
            _txtStandardList.Add(txt_Std_WideGage);
            _txtStandardList.Add(txt_Std_NarrowGage);
            _txtStandardList.Add(txt_Std_Short_Twist);
            _txtStandardList.Add(txt_Std_LACC);
            _txtStandardList.Add(txt_Std_VACC);
            _txtStandardList.Add(txt_Std_Prof_SC);
            _txtStandardList.Add(txt_Std_Prof_SC_120);
            _txtStandardList.Add(txt_Std_Prof_SC_70);
        }

        /// <summary>
        /// 初始化自定义值控件列表
        /// </summary>
        private void InitCustomTextboxList()
        {
            if (_txtCustomList.Count > 0)
            {
                _txtCustomList.Clear();
            }
            _txtCustomList.Add(txt_Diy_Align_SC);
            _txtCustomList.Add(txt_Diy_Align_SC_120);
            _txtCustomList.Add(txt_Diy_Align_SC_70);
            _txtCustomList.Add(txt_Diy_CrossLevel);
            _txtCustomList.Add(txt_Diy_WideGage);
            _txtCustomList.Add(txt_Diy_NarrowGage);
            _txtCustomList.Add(txt_Diy_Short_Twist);
            _txtCustomList.Add(txt_Diy_LACC);
            _txtCustomList.Add(txt_Diy_VACC);
            _txtCustomList.Add(txt_Diy_Prof_SC);
            _txtCustomList.Add(txt_Diy_Prof_SC_120);
            _txtCustomList.Add(txt_Diy_Prof_SC_70);
        }

        /// <summary>
        /// 初始化下拉菜单
        /// </summary>
        private void InitCombobox()
        {
            cbxStanderdType.SelectedIndex = 0;
            cbxDefectLevel.SelectedIndex = 0;
            cbxSpeedClass.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化类型字典
        /// </summary>
        private void InitExcptnTypeDic()
        {
            _dicExcptnType.Clear();

            _dicExcptnType.Add("Prof_SC", "高低_中波");
            _dicExcptnType.Add("Prof_SC_70", "高低_70长波");
            _dicExcptnType.Add("Prof_SC_120", "高低_120长波");
            _dicExcptnType.Add("Align_SC", "轨向_中波");
            _dicExcptnType.Add("Align_SC_70", "轨向_70长波");
            _dicExcptnType.Add("Align_SC_120", "轨向_120长波");
            _dicExcptnType.Add("WideGage", "大轨距");
            _dicExcptnType.Add("NarrowGage", "小轨距");
            _dicExcptnType.Add("CrossLevel", "水平");
            _dicExcptnType.Add("Short_Twist", "三角坑");
            _dicExcptnType.Add("LACC", "车体横加");
            _dicExcptnType.Add("VACC", "车体垂加");
        }

        /// <summary>
        /// 查询标准表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQurey_Click(object sender, EventArgs e)
        {

            //DataTable d1 = getTable();
            //DataTable d2 = ConvertToTable(d1);

            SetTextBoxEnable(false);
            string cmd = String.Format("select *  from 大值国家标准表 where CLASS = {0} and SPEED = {1} and STANDARDTYPE = {2}", _defectLevel, _speedClass, _standerdType);
            //string cmd = String.Format("select speed,class,type,VALUESTANDARD from 大值国家标准表 where STANDARDTYPE = {0}", _standerdType);
            try
            {
                dtStandardValue = InnerFileOperator.Query(cmd);
                //DataTable dt = ConvertToTable(dtStandardValue);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                dtStandardValue = null;
            }
            //0,ID
            //1,速度值
            //2,等级
            //3,类型
            //4,标准值
            //5，自定义值
            if(dtStandardValue!=null&&dtStandardValue.Rows.Count>0)
            { for (int i = 0; i < dtStandardValue.Rows.Count; i++)
                {
                    for (int j = 0; j < _txtStandardList.Count; j++)
                    {
                        if (_txtStandardList[j].Name.Substring(8).Equals(dtStandardValue.Rows[i][3].ToString()))
                        {
                            double standardValue = double.Parse(string.IsNullOrEmpty(dtStandardValue.Rows[i][4].ToString()) == true ? "0" : dtStandardValue.Rows[i][4].ToString());
                            double customValue = double.Parse(string.IsNullOrEmpty(dtStandardValue.Rows[i][5].ToString()) == true ? "0" : dtStandardValue.Rows[i][5].ToString());
                            _txtStandardList[j].Text = standardValue.ToString();
                            if (customValue == 0)
                            {
                                //把textbox设为enable
                                if (standardValue >= 0)
                                {
                                    _txtCustomList[j].Text = (standardValue - 1).ToString();
                                }
                                else
                                {
                                    _txtCustomList[j].Text = (standardValue + 1).ToString();
                                }
                            }
                            else
                            {
                                _txtCustomList[j].Text = customValue.ToString();
                            }

                            if (ckbSenior.Checked && cbxStanderdType.SelectedIndex == 1)
                            {
                                _txtCustomList[j].Enabled = true;
                            }
                            _txtCustomList[j].Enabled = true;
                        }
                    }
                }
            }

        }


        private DataTable ConvertToTable(DataTable source)
        {

            DataTable dt = new DataTable();
            //前两列是固定的加上
            dt.Columns.Add("speed");
            dt.Columns.Add("class");
            //dt.Columns.Add("speed");
            //dt.Columns.Add("class");
            //以staff_TiCheng 字段为筛选条件  列转为行  下面有图
            var speed= (from x in source.Rows.Cast<DataRow>() select x["speed"].ToString()).Distinct();
           
                var columns = (from x in source.Rows.Cast<DataRow>() select x["type"].ToString()).Distinct();
                //把 staff_TiCheng 字段 做为新字段添加进去
                foreach (var item in columns) dt.Columns.Add(item).DefaultValue = 0;
                //   x[1] 是字段 staff_Name 按  staff_Name分组 g 是分组后的信息   g.Key 就是名字  如果不懂就去查一个linq group子句进行分组
                var data = from x in source.AsEnumerable()
                           group x by new {spd= x[0],cls=x[1] } into g
                           select new { Key = g.Key.ToString(), Items = g };
            
                data.ToList().ForEach(x =>
                {
                    //这里用的是一个string 数组 也可以用DataRow根据个人需要用
                    string[] array = new string[dt.Columns.Count];
                    //array[1]就是存名字的
                    array[1] = x.Items.Key.cls.ToString();
                    //从第二列开始遍历
                    for (int i = 2; i < dt.Columns.Count; i++)
                    {
                        // array[0]  就是 staff_id
                        if (array[0] == null)
                            array[0] = x.Items.ToList<DataRow>()[0]["speed"].ToString();
                        //array[0] = (from y in x.Items
                        //            where y[2].ToString() == dt.Columns[i].ToString()
                        //            select y[0].ToString()).SingleOrDefault();
                        //array[i]就是 各种提成

                        var t = from y in x.Items
                                where y[2].ToString() == dt.Columns[i].ToString()//   y[2] 各种提成名字等于table中列的名字
                                 && y[0].ToString() == x.Items.Key.spd.ToString()
                                select y[3].ToString();

                        var text = (from y in x.Items
                                    where y[2].ToString() == dt.Columns[i].ToString()//   y[2] 各种提成名字等于table中列的名字
                                    && y[0].ToString() == x.Items.Key.spd.ToString()
                                    select y[3].ToString()                            //  y[3] 就是我们要找的  staff_TiChengAmount 各种提成 的钱数
                               ).SingleOrDefault();
                        array[i] = text;
                    }

                    dt.Rows.Add(array);   //添加到table中
                });
            
            return dt;

        }

        /// <summary>
        /// 类型选择触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxStanderdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _standerdType = cbxStanderdType.SelectedIndex;
        }

        /// <summary>
        /// 高级选项框选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbSenior_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSenior.Checked && cbxStanderdType.SelectedIndex == 1)
            {
                for (int i = 0; i < _txtCustomList.Count; i++)
                {
                    if (_txtCustomList[i].Enabled)
                    {

                        _txtStandardList[i].Enabled = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _txtCustomList.Count; i++)
                {
                    if (_txtCustomList[i].Enabled)
                    {
                        _txtStandardList[i].Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 设置所有输入框启用状态和文本
        /// </summary>
        /// <param name="b"></param>
        private void SetTextBoxEnable(bool isEnable)
        {
            foreach (TextBox tb in _txtStandardList)
            {
                tb.Text = "";
                tb.Enabled = isEnable;
            }

            foreach (TextBox tb in _txtCustomList)
            {
                tb.Text = "";
                tb.Enabled = isEnable;
            }
        }

        /// <summary>
        /// 标准控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandardConfigControl_Load(object sender, EventArgs e)
        {
            InitStandardTextboxList();
            InitCustomTextboxList();
            InitCombobox();
            InitExcptnTypeDic();
            SetTextBoxEnable(false);
            btnQurey_Click(sender, e);
        }

        /// <summary>
        /// 速度类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxSpeedClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            _speedClass = int.Parse(cbxSpeedClass.SelectedItem.ToString());
        }

        /// <summary>
        /// 标准值控件失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEditStandardTextboxValue_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double value = 0;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (double.TryParse(textBox.Text.Trim(), out value))
                {
                    string type = textBox.Name.Substring(8);
                    for (int i = 0; i < dtStandardValue.Rows.Count; i++)
                    {
                        if (type == dtStandardValue.Rows[i][3].ToString())
                        {
                            dtStandardValue.Rows[i][4] = textBox.Text.Trim();
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请输入一个数字！");
                    textBox.Text = string.Empty;
                    textBox.Focus();

                }
            }
            else
            {
                MessageBox.Show("数据不能为空！");
                textBox.Text = string.Empty;
                textBox.Focus();
            }
        }

        /// <summary>
        /// 自定义值控件失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEditCustomTextboxValue_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string name = textBox.Name.Substring(8);
            bool isFind = false;
            if (textBox.Text.Trim() == "")
            {
                MessageBox.Show("数据不能为空！");
                textBox.Text = string.Empty;
                textBox.Focus();
                return;
            }
            foreach (TextBox txtStandard in _txtStandardList)
            {
                if (txtStandard.Name.Substring(8) == name)
                {
                    double num = double.Parse(txtStandard.Text.Trim());
                    double customNum = 0;
                    if (double.TryParse(textBox.Text.Trim(), out customNum))
                    {
                        if (customNum <= 0 || customNum > num)
                        {
                            MessageBox.Show("输入的值必须大于0并且小于" + num);
                            textBox.Text = string.Empty;
                            textBox.Focus();
                        }
                        else
                        {
                            string type = textBox.Name.Substring(8);
                            for (int i = 0; i < dtStandardValue.Rows.Count; i++)
                            {
                                if (type == dtStandardValue.Rows[i][3].ToString())
                                {
                                    dtStandardValue.Rows[i][5] = textBox.Text.Trim();
                                    isFind = true;
                                    break;
                                }
                            }
                            if (isFind)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入一个大于0并且小于" + num + "的数字！");
                        textBox.Text = string.Empty;
                        textBox.Focus();
                    }
                }
            }
        }
    }
}
