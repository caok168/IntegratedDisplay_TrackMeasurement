using CommonFileSDK;
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
    public partial class SelectLineNameForm : Form
    {
        private Geo2CitConvertForm _convertForm;

        private List<string> _removeList = new List<string>();

        public SelectLineNameForm(Geo2CitConvertForm from)
        {
            InitializeComponent();
            _convertForm = from;
        }

        private void SelectLineNameForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                lstallLineName.Items.Clear();
                //liyang: 这个form一定是在waveconvertform加载完毕后，才会出现，所以有些数据直接从waveconvertform拿即可，不用再从idf读。
                if (_convertForm.DtLineCodeAndName != null && _convertForm.DtLineCodeAndName.Rows.Count > 0)
                {
                    foreach (var row in _convertForm.DtLineCodeAndName.AsEnumerable())
                    {
                        lstallLineName.Items.Add(row["线路名"].ToString());
                    }
                }



                // 从自选线路代码表中填充： selectedLineNameListBox
                LoadUserSelectionTable();
            }
            catch(Exception ex)
            {
                MyLogger.LogError("加载线路出错", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUserSelectionTable()
        {
            try
            {
                string sqlText = "select 线路名 from 自选线路代码表 ";
                DataTable dt = null;
                try
                {
                    dt = InnerFileOperator.Query(sqlText);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("查询自选线路失败：" + ex.Message);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (var row in dt.AsEnumerable())
                    {
                        lstselectedLineName.Items.Add(row["线路名"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            var filter = from row in _convertForm.DtLineCodeAndName.AsEnumerable() where row.Field<string>("线路名").Contains(txtKey.Text) select row;
            if (filter != null && filter.Count() > 0)
            {
                lstallLineName.Items.Clear();
                foreach (var r in filter)
                {
                    lstallLineName.Items.Add(r.Field<string>("线路名"));
                }
            }
            
        }

        private void btnAddLineName_Click(object sender, EventArgs e)
        {
            //string selectstr = "0000";
            for (int i = 0; i < lstallLineName.CheckedItems.Count; ++i)
            {
                if(!lstselectedLineName.Items.Contains(lstallLineName.CheckedItems[i].ToString()))
                {
                    lstselectedLineName.Items.Add(lstallLineName.CheckedItems[i].ToString());
                }
            }
        }

        private void btnRemoveLineName_Click(object sender, EventArgs e)
        {
            _removeList.Clear();
            foreach (string str in lstselectedLineName.SelectedItems)
            {
                _removeList.Add(str);
            }
            foreach (string str in _removeList)
            {
                lstselectedLineName.Items.Remove(str);
            }
        }

        private void SelectLineNameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> names = new List<string>();
            foreach (string str in lstselectedLineName.Items)
            {
                names.Add(str);
            }
            _convertForm.FillComboboxLineName(names);
            UpdateUserSelectionTable();
        }

        private void UpdateUserSelectionTable()
        {
            // delete all the data in 自选线路代码表
            // add the items in list box into 自选线路代码表
            try
            {
               string sqlText="delete from 自选线路代码表 ";
               InnerFileOperator.ExcuteSql(sqlText);
                foreach (string str in lstselectedLineName.Items)
                {
                    sqlText = "insert into 自选线路代码表 (线路名) values('" + str + "')";
                    InnerFileOperator.ExcuteSql(sqlText);
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
