/// -------------------------------------------------------------------------------------------
/// FileName：IICDataForm.cs
/// 说    明：IIC数据修正窗体
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using System.Threading.Tasks;

namespace IntegratedDisplay.Forms
{
    delegate void SetEnabledCallbackThis(bool value);

    /// <summary>
    /// IIC数据修正窗体
    /// </summary>
    public partial class IICDataForm : Form
    {
        WaveformMaker waveformMaker = null;
        List<WavefromData> waveFormDataList = new List<WavefromData>();

        /// <summary>
        /// cit文件路径
        /// </summary>
        string citFilePath = "";
        /// <summary>
        /// idf文件路径
        /// </summary>
        string idfFilePath = "";
        
        List<IndexSta> listIndexSta = new List<IndexSta>();
        List<ExceptionType> listETC = new List<ExceptionType>();
        List<InvalidData> listIDC = new List<InvalidData>();

        IICDataManager iicDataManager = new IICDataManager();

        delegate void SetEnabledCallbackThis(bool value);

        public IICDataForm()
        {
            InitializeComponent();
        }

        public IICDataForm(WaveformMaker maker)
        {
            InitializeComponent();
            waveformMaker = maker;
            waveFormDataList = waveformMaker.WaveformDataList;

            citFilePath = waveFormDataList[0].CitFilePath;
            idfFilePath = waveFormDataList[0].WaveIndexFilePath;
        }

        /// <summary>
        /// 添加IIC文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIICFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                listBoxIICFile.Items.Add(openFileDialog1.FileName);
            }
        }

        /// <summary>
        /// 删除IIC文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteIICFile_Click(object sender, EventArgs e)
        {
            if (listBoxIICFile.SelectedItems.Count == 1)
            {
                listBoxIICFile.Items.Remove(listBoxIICFile.SelectedItem);
                listBoxIICFile.Invalidate(); ;
            }
        }

        /// <summary>
        /// 清空IIC文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearIICFile_Click(object sender, EventArgs e)
        {
            listBoxIICFile.Items.Clear();
        }

        /// <summary>
        /// 修正按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            if(listBoxIICFile.Items.Count<=0)
            {
                MessageBox.Show("没有IIC文件列表，请先添加文件！");
                return;
            }
            this.progressBar1.Visible = true;
            this.Enabled = false;
            try
            {
                Task t = new Task(new Action(() => Process()));
                t.ContinueWith((task) => 
                {
                    if(task.IsCompleted||task.Exception==null)
                    {
                        MessageBox.Show("修正成功！");
                        this.Invoke(new Action(() => { SetEnabledThis(true); }));
                    }
                    else
                    {
                        MessageBox.Show("错误:" + task.Exception.InnerException);
                    }
                    
                });
                t.Start();
            }
            catch(Exception ex)
            {
                MyLogger.LogError("处理IIC修正时出现错误", ex);
                MessageBox.Show("处理IIC修正时出现错误：" + ex.Message);
            }

            //backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// 退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IICDataForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = citFilePath;
        }


        private void SetEnabledThis(bool value)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetEnabledCallbackThis d = new SetEnabledCallbackThis(SetEnabledThis);
                    this.Invoke(d, new object[] { value });
                }
                else
                {
                    this.Enabled = value;
                }
            }
            catch
            {
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Process();
        }

        private void Process()
        {
            //创建所需要的表结构【重置IIC】
            if (checkBox1.Checked)
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        this.progressBar1.Value = 10;
                    }));
                    for (int i = 0; i < listBoxIICFile.Items.Count; i++)
                    {
                        
                        iicDataManager.CreateIICTable(listBoxIICFile.Items[i].ToString());
                    }
                }
                catch
                {

                }
            }
            //处理修正--里程校正下
            int value = 90 / listBoxIICFile.Items.Count;
            for (int i = 0; i < listBoxIICFile.Items.Count; i++)
            {
                this.Invoke(new Action(() =>
                {
                    this.progressBar1.Value = (i + 1) * value;
                }));

                iicDataManager.ExceptionFix(citFilePath, listBoxIICFile.Items[i].ToString(), listIndexSta, listETC);

                iicDataManager.TQIFix(citFilePath, listBoxIICFile.Items[i].ToString(), listIndexSta, listIDC);
            }
            this.Invoke(new Action(() =>
            {
                this.progressBar1.Value =100;
                progressBar1.Visible = false;
            }));
            
        }
    }
}
