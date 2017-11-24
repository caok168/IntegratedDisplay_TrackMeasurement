using CitFileSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    public partial class ExportDataByMileageForm : Form
    {

        private WaveformMaker _maker = null;

        private CITFileProcess citProcess = new CITFileProcess();

        private double startMileage = 0;
        private double endMileage = 0;

        private string exportFilePath = string.Empty;

        public ExportDataByMileageForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void textAll_Leave(object sender,EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if(!string.IsNullOrEmpty(txt.Text.Trim()))
            {
                float number = 0f;
                if(float.TryParse(txt.Text.Trim(),out number))
                {
                    if(number<0)
                    {
                        MessageBox.Show("里程数必须大于或等于零！");
                        txt.Text = string.Empty;
                        txt.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("请输入一个大于零的数字！");
                    txt.Text = string.Empty;
                    txt.Focus();
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            startMileage = double.Parse(txtMileStart.Text.Trim()) * 1000;
            endMileage = (double.Parse(txtMileEnd.Text.Trim()) * 1000);
            if (startMileage == endMileage)
            {
                MessageBox.Show("起始公里标和终点公里标相同，请重新填写！");
                return;
            }

            string exportFilePath = string.Empty;
            if (!string.IsNullOrEmpty(txtExportPath.Text))
            {
                exportFilePath = txtExportPath.Text;
            }
            else
            {
                exportFilePath = "D://";
            }
            long startPostion = -1;
            long endPostion = -1;
            //long[] postions = _maker.WaveformDataList[0].CitFileProcess.GetPositons(_maker.WaveformDataList[0].CitFilePath);
            Milestone start= _maker.WaveformDataList[0].CitFileProcess.GetStartMilestone(_maker.WaveformDataList[0].CitFilePath);
            Milestone end= _maker.WaveformDataList[0].CitFileProcess.GetEndMilestone(_maker.WaveformDataList[0].CitFilePath);

            if (ckbIsStart.Checked)
            {
                startPostion = start.mFilePosition;
                startMileage = start.GetMeter();
            }
            else
            {
                startPostion  = _maker.WaveformDataList[0].CitFileProcess.GetCurrentPositionByMilestone(_maker.WaveformDataList[0].CitFilePath, (float)startMileage, true);
            }
            if (ckbIsEnd.Checked)
            {
                endPostion = end.mFilePosition;
                endMileage = end.GetMeter();
            }
            else
            {
                endPostion = _maker.WaveformDataList[0].CitFileProcess.GetCurrentPositionByMilestone(_maker.WaveformDataList[0].CitFilePath, (float)endMileage, true);
            }
            
            if (startPostion == -1)
            {
                MessageBox.Show("文件中不存在里程为:" + txtMileStart.Text.Trim() + " 的数据,请确认！");
                return;
            }
            
            if (endPostion == -1)
            {
                MessageBox.Show("文件中不存在里程为:" + txtMileEnd.Text.Trim() + " 的数据,请确认！");
                return;
            }

            btnExport.Enabled = false;
            labLoading.Visible = true;


            Task<string> task = Task.Factory.StartNew(
                () => _maker.WaveformDataList[0].ExportOnlyCITFile(exportFilePath, startMileage, endMileage)
                );
            task.ContinueWith((t) =>
            {
                if (t.Exception == null && t.IsCompleted)
                {
                    if (this != null && this.IsHandleCreated && btnExport.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            btnExport.Enabled = true;
                            labLoading.Visible = false;
                        }));

                    }
                    string exprotFile = t.Result;
                    if (!string.IsNullOrEmpty(exprotFile))
                    {
                        MessageBox.Show("导出成功，路径为：" + exprotFile);
                    }
                    else
                    {
                        MessageBox.Show("导出完成，但没有数据导出");
                    }

                }
                else if (t.Exception != null)
                {
                    MyLogger.LogError("导出失败", t.Exception.InnerException);
                    MessageBox.Show("导出失败，错误：" + t.Exception.InnerException.Message);
                }
            });

        }

        private void ExportDataByMileageForm_Load(object sender, EventArgs e)
        {

            txtExportPath.Text = Path.Combine(Path.GetDirectoryName(_maker.WaveformDataList[0].CitFilePath), DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + _maker.WaveformDataList[0].CitFile.sTrackName + ".cit");

            saveFileDialog.InitialDirectory = Path.GetDirectoryName(_maker.WaveformDataList[0].CitFilePath);
            //if (_maker.WaveformDataList.Count > 0 && _maker.WaveformDataList[0].MileageFix.FixData.Count > 0 && _maker.WaveformDataList[0].IsLoadIndex)
            //{
            //    startMileage = (_maker.WaveformDataList[0].MileageFix.FixData[0].MarkedStartPoint.UserSetMileage/1000 );
            //    endMileage = (_maker.WaveformDataList[0].MileageFix.FixData[_maker.WaveformDataList[0].MileageFix.FixData.Count-1].MarkedEndPoint.UserSetMileage/1000);
            //    txtMileStart.Text = startMileage.ToString("F3");
            //    txtMileEnd.Text = endMileage.ToString("F3");
            //}
            //else
            //{
                txtMileStart.Text = (citProcess.GetStartMilestone(_maker.WaveformDataList[0].CitFilePath).GetMeter() / 1000).ToString();
                txtMileEnd.Text = (citProcess.GetEndMilestone(_maker.WaveformDataList[0].CitFilePath).GetMeter() / 1000).ToString();

            //}

        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            
            saveFileDialog.Filter = "cit 文件|*.cit";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExportPath.Text = saveFileDialog.FileName;
            }
        }

        private void ckbIsStart_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbIsStart.Checked)
            {
                txtMileStart.Enabled = false;
            }
            else
            {
                txtMileStart.Enabled = true;
            }
        }

        private void ckbIsEnd_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbIsEnd.Checked)
            {
                txtMileEnd.Enabled = false;
            }
            else
            {
                txtMileEnd.Enabled = true;
            }
        }
    }

    
}
