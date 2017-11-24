using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CitFileSDK;
using IntegratedDisplay.Models;

namespace IntegratedDisplay
{
    public partial class MainForm : Form
    {

        private List<FileInformation> _fileInfoList = new List<FileInformation>();

        /// <summary>
        /// 配置文件全局对象类
        /// </summary>
        private static ConfigData _configData = new ConfigData();

        public MainForm()
        {
            InitializeComponent();
        }

       

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in fileDialog.FileNames)
                {
                    _configData.RecentFiles.Files.Insert(0, file);
                }
                ConfigManger.SaveConfigData(_configData);
                OpenFileForm form = new OpenFileForm(_configData);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _fileInfoList = form.Tag as List<FileInformation>;
                }
            }

        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            progressBar.Visible = false;
            ConfigManger.ConfigPath = Application.StartupPath + @"\config.xml";
            _configData = ConfigManger.GetConfigData();
            DBOperator.DBConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\InnerDB.idf;Persist Security Info=True;Mode=Share Exclusive;Jet OLEDB:Database Password=iicdc;";
            //_configData.WaveConfigs.WaveConfigCount = 10;
            _configData.RecentFiles.FilesCount = 10;
            if (!string.IsNullOrEmpty(_configData.MediaPath)
                || (_configData.RecentFiles.Files != null
                && _configData.RecentFiles.Files.Count > 0)
                || _configData.DirPaths.PathList.Count > 0)
            {
                OpenFileForm form = new OpenFileForm(_configData);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    progressBar.Visible = true;
                    _fileInfoList = form.Tag as List<FileInformation>;
                }
            }
            
        }

        private void tsmiOtherSetting_Click(object sender, EventArgs e)
        {
            SettingForm form = new SettingForm(_configData);
            form.ShowDialog();
        }

        private void tsmiOpenPathOrMedia_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _configData.DirPaths.PathList.Add(folderDialog.SelectedPath);
                ConfigManger.SaveConfigData(_configData);
                OpenFileForm form = new OpenFileForm(_configData);
                form.ShowDialog();
            }
        }

        /// <summary>
        /// 图像化显示台帐按钮事件
        /// </summary>
        /// <param name="sender">点击按钮</param>
        /// <param name="e">事件</param>
        private void tsmiViewAccountByDrawing_Click(object sender, EventArgs e)
        {
            tsmiViewAccountByDrawing.Checked = !tsmiViewAccountByDrawing.Checked;
            if (tsmiViewAccountByDrawing.Checked)
            {
                splitPictrueContainer.Panel1Collapsed = false;
            }
            else
            {
                splitPictrueContainer.Panel1Collapsed = true;
            }
            _configData.Accouts.IsCheck = tsmiViewAccountByDrawing.Checked;
        }

        /// <summary>
        /// 台帐选择显示方式
        /// </summary>
        /// <param name="sender">点击按钮(包括:曲线,坡度,道岔,速度区段)</param>
        /// <param name="e"></param>
        private void tsmiAccountDisplayMode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = !item.Checked;
            for (int i = 0; i < _configData.Accouts.AcountList.Count; i++)
            {
                if (_configData.Accouts.AcountList[i].Name.Contains(item.Text))
                {
                    _configData.Accouts.AcountList[i].IsCheck = item.Checked;
                    break;
                }
            }
            ConfigManger.SaveConfigData(_configData);
        }

        /// <summary>
        /// 拖动方式点击事件
        /// </summary>
        /// <param name="sender">点击按钮（包括：单通道拖动、同名通道拖动、同基线拖动）</param>
        /// <param name="e">事件</param>
        private void tsmiDragMode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = true;
            foreach (var im in tsmiDragMode.DropDownItems)
            {
                if (item != im)
                {
                    ((ToolStripMenuItem)im).Checked = false;
                }
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
