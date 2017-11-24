using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonFileSDK;
using IntegratedDisplay.Models;

namespace IntegratedDisplay.Controls
{
    public partial class StandardConfig : UserControl
    {
        public StandardConfig()
        {
            InitializeComponent();
        }

        public DataTable dtStandardValue = new DataTable();

        int rowIndex = -1;
        int columnIndex = -1;
        string editValue = "";

        public List<StandardChangeItem> StandardChangedList = new List<StandardChangeItem>();


        private void StandardConfig_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.GreenYellow;
                string cmd = String.Format("select speed,class,type,VALUESTANDARD,VALUEDIY,STANDARDTYPE from 大值国家标准表 where STANDARDTYPE = {0}", 0);
                DataTable dt = InnerFileOperator.Query(cmd);
                dtStandardValue = ConvertToTable(dt);
                dataGridView1.DataSource = dtStandardValue;
                //dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Aqua;
                //DgvRowColor(dataGridView1);
            }
            catch(Exception ex)
            {
                MyLogger.LogError("读取数据错误", ex);
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private DataTable ConvertToTable(DataTable source)
        {

            DataTable dt = new DataTable();
            //前两列是固定的加上
            dt.Columns.Add("speed");
            dt.Columns.Add("class");
            dt.Columns.Add("STANDARDTYPE");
            //dt.Columns.Add("speed");
            //dt.Columns.Add("class");
            //以staff_TiCheng 字段为筛选条件  列转为行  下面有图
            var speed = (from x in source.Rows.Cast<DataRow>() select x["speed"].ToString()).Distinct();

            var columns = (from x in source.Rows.Cast<DataRow>() select x["type"].ToString()).Distinct();
            //把 staff_TiCheng 字段 做为新字段添加进去
            foreach (var item in columns) dt.Columns.Add(item).DefaultValue = "×";
            //   x[1] 是字段 staff_Name 按  staff_Name分组 g 是分组后的信息   g.Key 就是名字  如果不懂就去查一个linq group子句进行分组
            var data = from x in source.AsEnumerable()
                       group x by new { spd = x[0], cls = x[1] } into g
                       select new { Key = g.Key.ToString(), Items = g };

            data.ToList().ForEach(x =>
            {
                //这里用的是一个string 数组 也可以用DataRow根据个人需要用
                string[] array = new string[dt.Columns.Count];
                //array[1]就是存名字的
                array[1] = x.Items.Key.cls.ToString();
                array[2] = "行业标准";
                //从第三列开始遍历
                for (int i = 3; i < dt.Columns.Count; i++)
                {
                    // array[0]  就是 staff_id
                    if (array[0] == null)
                        array[0] = x.Items.ToList<DataRow>()[0]["speed"].ToString();
                    //array[0] = (from y in x.Items
                    //            where y[2].ToString() == dt.Columns[i].ToString()
                    //            select y[0].ToString()).SingleOrDefault();
                    //array[i]就是 各种提成


                    var text = (from y in x.Items
                                where y[2].ToString() == dt.Columns[i].ToString()//   y[2] 各种提成名字等于table中列的名字
                                && y[0].ToString() == x.Items.Key.spd.ToString()
                                //&& y[5].ToString()==stdType
                                select y[3].ToString()                            //  y[3] 就是我们要找的  staff_TiChengAmount 各种提成 的钱数
                           ).SingleOrDefault();
                    array[i] = text;
                }
                dt.Rows.Add(array);   //添加到table中
                
                //这里用的是一个string 数组 也可以用DataRow根据个人需要用
                array = new string[dt.Columns.Count];
                //array[1]就是存名字的
                array[1] = x.Items.Key.cls.ToString();
                array[2] = "自定义标准";
                //从第三列开始遍历
                for (int i = 3; i < dt.Columns.Count; i++)
                {
                    // array[0]  就是 staff_id
                    if (array[0] == null)
                        array[0] = x.Items.ToList<DataRow>()[0]["speed"].ToString();

                    var t1 = (from y in x.Items
                                where y[2].ToString() == dt.Columns[i].ToString()//   y[2] 各种提成名字等于table中列的名字
                                && y[0].ToString() == x.Items.Key.spd.ToString()
                                //&& y[5].ToString() == stdType
                              select y[4].ToString()                            //  y[3] 就是我们要找的  staff_TiChengAmount 各种提成 的钱数
                           ).SingleOrDefault();
                    var t2 = (from y in x.Items
                                where y[2].ToString() == dt.Columns[i].ToString()//   y[2] 各种提成名字等于table中列的名字
                                && y[0].ToString() == x.Items.Key.spd.ToString()
                                //&& y[5].ToString() == stdType
                              select y[3].ToString()                            //  y[3] 就是我们要找的  staff_TiChengAmount 各种提成 的钱数
                           ).SingleOrDefault();
                    if (string.IsNullOrEmpty(t1) || t1 == "0")
                    {
                        if (!string.IsNullOrEmpty(t2))
                        {
                            double sta = double.Parse(t2);
                            if (sta > 0)
                            {
                                t1 = (sta - 1).ToString();
                            }
                            else if (sta < 0)
                            {
                                t1 = (sta + 1).ToString();
                            }
                        }

                    }
                    array[i] = t1;

                }

                dt.Rows.Add(array);   //添加到table中
            });

            return dt;

        }


        public void DgvRowColor(DataGridView dgv)
        {
            if (dgv.Rows.Count != 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Black;
                        dgv.Rows[i].ReadOnly = false;
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 254, 254);
                        dgv.Rows[i].ReadOnly = true;
                    }
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
                {
                    rowIndex = e.RowIndex;
                    columnIndex = e.ColumnIndex;
                    dataGridView1.ShowCellErrors = true;
                    if(e.ColumnIndex<=2)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该值无法修改，因为该值为固定值";
                    }
                    else if (((e.RowIndex + 1) % 2) != 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该值无法修改，因为该值为行业标准";
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "该值无法修改，因为该值不可用";
                    }
                }

            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (((e.RowIndex + 1) % 2) != 0
                    || dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "×"
                    || e.ColumnIndex <= 2)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
            }
            if (rowIndex != -1 || columnIndex != -1)
            {
                dataGridView1.Rows[rowIndex].Cells[columnIndex].ErrorText = "";
                rowIndex = -1;
                columnIndex = -1;
            }
            
        }

        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 2)
            {
                double value = 0;
                if (double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out value))
                {
                    StandardChangeItem item = new StandardChangeItem();
                    item.Speed = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    item.StdClass = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    item.stdType = "0";//兼容已有的标准表，现在只更改部标的自定义值(diyValue)
                    item.StdValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    item.StdParam = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
                    StandardChangedList.Add(item);
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = editValue;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "值必须是数字！";
                    rowIndex = e.RowIndex;
                    columnIndex = e.ColumnIndex;
                }

                
            }

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 2)
            {
                editValue= dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }
    }
}
