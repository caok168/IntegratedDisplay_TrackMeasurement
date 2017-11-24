using CommonFileSDK;
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
    public partial class ExportDataByBoundaryForm : Form
    {
        private WaveformMaker _maker;

        private static object lockObj = new object();

        public ExportDataByBoundaryForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void ExportDataByBoundaryForm_Load(object sender, EventArgs e)
        {
            cbxSplitUnit.SelectedIndex = 0;
            txtExportPath.Text = Path.GetDirectoryName(_maker.WaveformDataList[0].CitFilePath);
            folderBrowserDialog.SelectedPath= txtExportPath.Text = Path.GetDirectoryName(_maker.WaveformDataList[0].CitFilePath);
            if (string.IsNullOrEmpty(_maker.WaveformDataList[0].LineCode))
            {
                MessageBox.Show("找不到线路编号为：" + _maker.WaveformDataList[0].CitFile.sTrackCode + "对应的线路编码,无法导出！");
                btnExport.Enabled = false;
            }
        }

        private void cbxSplitUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvBoundary.Rows.Clear();
            int length = 2;
            switch (cbxSplitUnit.SelectedIndex)
            {
                case 0:
                    length = 2;
                    break;
                case 1:
                    length = 5;
                    break;
                case 2:
                    length = 7;
                    break;
                case 3:
                    length = 9;
                    break;
            }
            string dir = "上";
            switch(_maker.WaveformDataList[0].CitFile.iDir)
            {
                case 1:
                    {
                        dir = "上";
                        break;
                    }
                case 2:
                    {
                        dir = "下";
                        break;
                    }
                case 3:
                    {
                        dir = "单";
                        break;
                    }
            }
            string sql = "select IIf(" + length.ToString() +
                           "=2,(select distinct bureauname from dw b where b.bureaucode=a.sunitid)," +
                           "(select distinct unitname from dw b where b.unitid=a.sunitid)) as unitname,a.sstartmile,a.sendmile from " +
                           "(SELECT left(unitid," + length.ToString() + ")  as sunitid,min(startmile) as sstartmile,max(endmile) as sendmile " +
                           "FROM Gj where lineid='" + _maker.WaveformDataList[0].LineCode + "' and linedirname='" + dir + "' " +
                           "group by left(unitid," + length.ToString() + ")) a";
            DataTable dt = null;
            try
            {
                dt = InnerFileOperator.Query(sql);
            }
            catch(Exception ex)
            {
                MyLogger.LogError("查询IIF数据失败", ex);
                MessageBox.Show("查询数据失败：" + ex.Message);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvBoundary.Rows.Add(dt.Rows[i].ItemArray);
                }
            }

            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            bool isIndex = false;
            if (dgvBoundary.Rows.Count <= 0)
            {
                MessageBox.Show("找不到管界数据，无法导出！");
                return;
            }

            if (!string.IsNullOrEmpty(txtExportPath.Text))
            {
                string selectedPath = txtExportPath.Text;
                List<string> exportPath = new List<string>();
                if (_maker.WaveformDataList[0].MileageFix.FixData.Count > 0)
                {
                    isIndex = true;
                }
               
                int count = dgvBoundary.Rows.Count;
                if (_maker.WaveformDataList[0].IsLoadIndex)
                {
                    ExportCitAndIndex(selectedPath, exportPath, count);
                }
                else if (isIndex == true)
                {
                    DialogResult dr = MessageBox.Show("里程数据未修正，是否导出修正后的数据？\n<是>：按修正数据导出,\n<否>：按原里程数据导出,\n<取消>：取消导出", "提示", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        ExportCitAndIndex(selectedPath, exportPath, count);
                    }
                    else if(dr== DialogResult.No)
                    {
                        ExportOnlyCit(selectedPath, exportPath, count);
                    }
                    
                }
                else if (MessageBox.Show("里程数据未修正，是否继续导出(按原始数据)？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    ExportOnlyCit(selectedPath, exportPath, count);
                }
                else
                {
                    btnExport.Enabled = true;
                    labLoading.Enabled = false;
                    return;
                }
                
            }


        }

        private void ExportCitAndIndex(string selectedPath, List<string> exportPath, int count)
        {
            btnExport.Enabled = false;
            labLoading.Visible = true;
            for (int i = 0; i < dgvBoundary.Rows.Count; i++)
            {
                double startMile = double.Parse(dgvBoundary.Rows[i].Cells[1].Value.ToString());
                double endMile = double.Parse(dgvBoundary.Rows[i].Cells[2].Value.ToString());
                try
                {
                    Task task = new Task(() =>
                    {
                        string path = _maker.WaveformDataList[0].ExportCITFileAndIndexData(selectedPath, startMile, endMile);

                        exportPath.Add(path);

                    });
                    task.ContinueWith((t) =>
                    {
                        if (t.IsFaulted || t.Exception != null)
                        {
                            MessageBox.Show("导出过程中出现错误：" + t.Exception.InnerException.Message);
                            exportPath.Add("");
                        }
                        if (exportPath.Count == count)
                        {
                            if (exportPath.Count > 0)
                            {
                                int success = 0;
                                string allPath = "";
                                foreach (string path in exportPath)
                                {
                                    if (!string.IsNullOrEmpty(path))
                                    {
                                        allPath += "\n" + path;
                                        success++;
                                    }
                                }
                                MessageBox.Show("成功导出" + success + "条数据，导出路径为：" + allPath);
                            }
                            else
                            {
                                MessageBox.Show("任务完成，但没有数据导出，可能由于里程不处于管界范围内！");
                            }
                            this.Invoke(new Action(() =>
                            {
                                btnExport.Enabled = true;
                                labLoading.Visible = false;
                            }));

                        }
                    });
                    task.Start();
                }
                catch (Exception ex)
                {
                    MyLogger.LogError("导出修正数据过程中出现错误", ex);
                    MessageBox.Show("导出过程中出现错误：" + ex.Message);
                }
            }
        }

        private void ExportOnlyCit(string selectedPath, List<string> exportPath, int count)
        {
            btnExport.Enabled = false;
            labLoading.Visible = true;
            
            for (int i = 0; i < dgvBoundary.Rows.Count; i++)
            {
                lock (lockObj)
                {
                    double startMile = double.Parse(dgvBoundary.Rows[i].Cells[1].Value.ToString()) * 1000;
                    double endMile = double.Parse(dgvBoundary.Rows[i].Cells[2].Value.ToString()) * 1000;
                    try
                    {
                        Task task = new Task(() =>
                        {
                            string filePath = Path.Combine(selectedPath, Path.GetFileNameWithoutExtension(_maker.WaveformDataList[0].CitFilePath) + "_" + startMile + '-' + endMile + ".cit");
                            string path = _maker.WaveformDataList[0].ExportOnlyCITFile(filePath, startMile, endMile);

                            exportPath.Add(path);

                        });
                        task.ContinueWith((t) =>
                        {
                            if (t.IsFaulted || t.Exception != null)
                            {
                                MessageBox.Show("导出过程中出现错误：" + t.Exception.InnerException.Message);
                            }
                            else if (exportPath.Count == count)
                            {
                                if (exportPath.Count > 0)
                                {
                                    string allPath = "";
                                    int success = 0;
                                    foreach (string path in exportPath)
                                    {
                                        if (!string.IsNullOrEmpty(path))
                                        {
                                            allPath += "\n" + path;
                                            success++;
                                        }
                                    }
                                    MessageBox.Show("成功导出" + success + "条数据，导出路径为：" + allPath);
                                }
                                else
                                {
                                    MessageBox.Show("任务完成，但没有数据导出，可能由于里程不处于管界范围内！");
                                }
                                this.Invoke(new Action(() =>
                                {
                                    btnExport.Enabled = true;
                                    labLoading.Visible = false;
                                }));

                            }
                        });
                        task.Start();

                    }
                    catch (Exception ex)
                    {
                        MyLogger.LogError("导出未修正数据过程中出现错误", ex);
                        MessageBox.Show("导出过程中出现错误：" + ex.Message);
                    }
                }
            }
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtExportPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
