using System;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using System.Collections.Generic;


namespace IntegratedDisplay
{
    public partial class WaveDisplayConfigControl : UserControl
    {

        public WaveConfigsDesc WaveConfigs { get; set; }

        public WaveDisplayConfigControl()
        {
            InitializeComponent();
        }

        private void WaveDisplayConfigControl_Load(object sender, EventArgs e)
        {
            LoadConfigs();
        }

        private void LoadConfigs()
        {
            dgvWaveConfig.Rows.Clear();
            for (int i = 0; i < WaveConfigs.WaveConfigCount; i++)
            {
                bool isFind = false;
                object[] obj = new object[2];
                obj[0] = (i + 1).ToString();
                if (WaveConfigs.WaveConfigList.Count > 0)
                {
                    foreach (var wave in WaveConfigs.WaveConfigList)
                    {
                        if (wave.WaveConfigIndex == (i+1))
                        {
                            obj[1] = wave.WaveConfigFile;
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                    {
                        obj[1] = string.Empty;
                    }
                }
                else
                {
                    obj[1] = "";
                }
                dgvWaveConfig.Rows.Add(obj);
            }
        }

        private void btnSelectConfigFile_Click(object sender, EventArgs e)
        {
            openSelectFileDialog.FileName = string.Empty;
            if (openSelectFileDialog.ShowDialog()== DialogResult.OK)
            {
                dgvWaveConfig.SelectedRows[0].Cells[1].Value = openSelectFileDialog.FileName;
                int index = int.Parse(dgvWaveConfig.SelectedRows[0].Cells[0].Value.ToString());
                WaveConfigs.WaveConfigList.Add(new WaveConfigDesc() { 
                    WaveConfigFile = openSelectFileDialog.FileName, 
                    WaveConfigIndex = index });
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvWaveConfig.SelectedRows != null && dgvWaveConfig.SelectedRows.Count > 0)
            {
                int index = int.Parse(dgvWaveConfig.SelectedRows[0].Cells[0].Value.ToString());
                WaveConfigDesc desc = WaveConfigs.WaveConfigList.Find(p => p.WaveConfigIndex == index);
                if (desc != null)
                {
                    WaveConfigs.WaveConfigList.Remove(desc);
                    LoadConfigs();
                }
            }
        }
    }
}
