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
    public partial class AccountInfoForm : Form
    {
        private WaveformMaker _maker = null;

        private List<Models.AccountDesc> _accountDescList = null;

        public AccountInfoForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
            _accountDescList = MainForm.WaveformConfigData.Accouts.AccountDescList;
        }

        private void AccountInfoForm_Load(object sender, EventArgs e)
        {
            tscbxType.Items.Clear();
            for(int i = 0;i< _accountDescList.Count;i++)
            {
                tscbxType.Items.Add(_accountDescList[i].Name);
            }
            if(tscbxType.Items.Count>0)
            {
                tscbxType.SelectedIndex = 0;
            }
            tscbxLinkage.SelectedIndex = 0;
        }

        private void LoadAccountData(string tableName,string sqlWhereText,string displayFiled)
        {
            dgvAccountInfo.DataSource = null;
            dgvAccountInfo.Rows.Clear();
            dgvAccountInfo.Columns.Clear();
            string dir = string.Empty;
            switch (_maker.WaveformDataList[0].CitFile.iDir)
            {
                case 1:
                    dir = "上";
                    break;
                case 2:
                    dir = "下";
                    break;
                case 3:
                    dir = "单";
                    break;
                default:
                    dir = "上";
                    break;
            }
            if(string.IsNullOrEmpty(_maker.WaveformDataList[0].LineCode))
            {
                throw new ArgumentException("找不到线路编号为：" + _maker.WaveformDataList[0].CitFile.sTrackCode + "的对应编号");
            }
            string sqlText = "select " + displayFiled + " from " + tableName + " where 线编号='" +
                         _maker.WaveformDataList[0].LineCode + "' and 行别='" 
                         + dir+ "' " + sqlWhereText;
            try
            {
                
                DataTable dt = InnerFileOperator.Query(sqlText);
                dgvAccountInfo.DataSource = dt;
            }
            catch(Exception)
            {
                dgvAccountInfo.DataSource = null;
            }
        }

        private string GetSqlWhereText()
        {
            string sSql = "";
            try
            {
                if (_accountDescList[tscbxType.SelectedIndex].StartMileage.Length > 0)
                {
                    sSql += (" and " + _accountDescList[tscbxType.SelectedIndex].StartMileage + " >= " + tstxtStartMileage.Text);
                }
                if (_accountDescList[tscbxType.SelectedIndex].EndMileage.Length > 0)
                {
                    sSql += (" and " + _accountDescList[tscbxType.SelectedIndex].EndMileage + " <= " + tstxtEndMileage.Text);
                }
                else
                {
                    sSql += (" and " + _accountDescList[tscbxType.SelectedIndex].StartMileage + " <= " + tstxtEndMileage.Text);
                }
                sSql += " order by " + _accountDescList[tscbxType.SelectedIndex].StartMileage;
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("加载台账出错:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            return sSql;
        }

        private void tsmiFind_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAccountData(_accountDescList[tscbxType.SelectedIndex].TableName, GetSqlWhereText(), _accountDescList[tscbxType.SelectedIndex].DisplayText);
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("加载台账出错:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void tscbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAccountData(_accountDescList[tscbxType.SelectedIndex].TableName, GetSqlWhereText(), _accountDescList[tscbxType.SelectedIndex].DisplayText);
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("加载台账出错:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        public void InvokeLoadAccountData()
        {
            this.Invoke(new Action(() => LoadAccountData(_accountDescList[tscbxType.SelectedIndex].TableName, GetSqlWhereText(), _accountDescList[tscbxType.SelectedIndex].DisplayText)));
            
        }

        private void AccountInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            MainForm.sMainform.Activate();
        }
        private void tstxtMileageCheck_Leave(object sender, EventArgs e)
        {
            ToolStripTextBox txt = sender as ToolStripTextBox;
            if(string.IsNullOrEmpty(txt.Text))
            {
                txt.Text = "0";
            }
            else
            {
                double number = 0;
                if(!double.TryParse(txt.Text.Trim(),out number))
                {
                    txt.Text = string.Empty;
                    txt.Focus();
                }
            }
        }
    }
}
