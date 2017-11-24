using CitIndexFileSDK.IntelligentMileageFix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedDisplay.Forms
{
    public partial class CorrelationForm : Form
    {
        private WaveformMaker _maker = null;
        //float superelevation = 0.8f;
        //float gage = 0.8f;
        //float LProf = 0.8f;
        //float RProf = 0.8f;
        //int fixedCount = 200;
        //int targetCount = 2000;

        public CorrelationForm(WaveformMaker maker)
        {
            InitializeComponent();
            _maker = maker;
        }

        private void btnFileAdd_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Multiselect = true;
            
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lstCitFiles.Items.AddRange(openFileDialog.FileNames);
            }
        }

        private void btnFileDelete_Click(object sender, EventArgs e)
        {
            if(lstCitFiles.SelectedItems.Count>0)
            {
                lstCitFiles.Items.Remove(lstCitFiles.SelectedItem);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstCitFiles.Items.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnWaveFix_Click(object sender, EventArgs e)
        {
            btnWaveFix.Enabled = false;
            float superelevation = float.Parse(txtSuperelevation.Text);
            float gage = float.Parse(txtGage.Text);
            float LProf = float.Parse(txtL_Prof.Text);
            float RProf = float.Parse(txtR_Prof.Text);
            int fixedCount = int.Parse(txtOriginalPointCount.Text);
            int targetCount = int.Parse(txtTargetPointCount.Text);
            IntelligentMilestoneFix fix = new IntelligentMilestoneFix();
            fix.FixedSamplingCount = fixedCount;
            fix.TargetSamplingCount = targetCount;
            fix.FixParams.Add(new FixParam() { ChannelName = "Gage", ThreShold = gage, Priority = 1 });
            fix.FixParams.Add(new FixParam() { ChannelName = "Superelevation", ThreShold = superelevation, Priority = 0 });
            fix.FixParams.Add(new FixParam() { ChannelName = "L_Prof_SC", ThreShold = LProf, Priority = 2 });
            fix.FixParams.Add(new FixParam() { ChannelName = "R_Prof_SC", ThreShold = RProf, Priority = 3 });
            fix.InitFixData(_maker.WaveformDataList[0].CitFilePath);
            Task task = Task.Factory.StartNew(() =>
             {
                 foreach (string path in lstCitFiles.Items)
                 {
                     fix.RunMilestoneFix(path);
                 }
             });
            task.ContinueWith((t) =>
            {
                if (t.IsFaulted || t.Exception != null)
                {
                    foreach (var error in t.Exception.InnerExceptions)
                    {
                        MyLogger.logger.Error("运行智能里程失败：" + t.Exception.InnerException.Message);
                    }
                    MessageBox.Show("处理中出现错误：" + t.Exception.InnerExceptions[0].Message);
                }
                else if (t.IsCompleted)
                {
                    MessageBox.Show("处理完成！");

                }
                this.Invoke(new Action(() => { btnWaveFix.Enabled = true; }));
            });
        }

        //private void fixMileage(List<string> pathFiles)
        //{
        //    IntelligentMilestoneFix fix = new IntelligentMilestoneFix();
        //    fix.FixedSamplingCount = fixedCount;
        //    fix.TargetSamplingCount = targetCount;
        //    fix.FixParams.Add(new FixParam() { ChannelName = "Gage", ThreShold = gage, Priority = 1 });
        //    fix.FixParams.Add(new FixParam() { ChannelName = "Superelevation", ThreShold = superelevation, Priority = 0 });
        //    fix.FixParams.Add(new FixParam() { ChannelName = "L_Prof_SC", ThreShold = LProf, Priority = 2 });
        //    fix.FixParams.Add(new FixParam() { ChannelName = "R_Prof_SC", ThreShold = RProf, Priority = 3 });
        //    fix.InitFixData(_maker.WaveformDataList[0].CitFilePath);
        //    foreach (string path in lstCitFiles.Items)
        //    {
        //        fix.RunMilestoneFix(path);
        //    }
        //}

        
    }
}
