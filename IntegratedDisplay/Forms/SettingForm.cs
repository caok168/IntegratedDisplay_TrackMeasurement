using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DevComponents.AdvTree;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using CommonFileSDK;
using IntegratedDisplay.Controls;

namespace IntegratedDisplay
{
    public partial class SettingForm : Form
    {
        /// <summary>
        /// 波形显示控件
        /// </summary>
        private WaveDisplayConfigControl _waveDisplayControl = null;
        
        /// <summary>
        /// 标准设置控件
        /// </summary>
        private StandardConfigControl _standardControl = null;

        private StandardConfig _standard = null;

        /// <summary>
        /// 通用设置控件
        /// </summary>
        private CommonConfigControl _commonControl = null;

        /// <summary>
        /// 网络设置控件
        /// </summary>
        private NetworkConfigControl _networkControl = null;

        /// <summary>
        /// 配置文件数据
        /// </summary>
        private ConfigData _configData = null;

        public SettingForm(ConfigData configData)
        {
            InitializeComponent();
            _configData = configData;
        }

        /// <summary>
        /// 设置窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            _waveDisplayControl = new WaveDisplayConfigControl();
            //_standardControl = new StandardConfigControl();
            _commonControl = new CommonConfigControl();
            _networkControl = new NetworkConfigControl();
            _standard = new StandardConfig();

            _waveDisplayControl.WaveConfigs = _configData.WaveConfigs;
            _commonControl.AutoScrollVelocity = _configData.AutoScrollVelocity;
            _commonControl.MeterageRadius = _configData.MeterageRadius;
            _commonControl.SignRadius = _configData.SignRadius;
            _commonControl.MediaPath = _configData.MediaPath;

            //添加空格只是让下面的节点靠下一点
            List<string> configItems = new List<string>();
            configItems.Add("");
            configItems.Add("通用设置");
            configItems.Add("多波形显示设置");
            configItems.Add("标准设置");
            //configItems.Add("网络设置");
            foreach (var key in configItems)
            {
                Node rootNode = new Node(key);
                if (string.IsNullOrEmpty(key))
                {
                    rootNode.Selectable = false;
                    rootNode.StyleMouseOver = null;
                }
                treeSetting.Nodes.Add(rootNode);
            }
            LoadControls(treeSetting.Nodes[1]);
        }

        /// <summary>
        /// 设置节点触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeSetting_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            LoadControls(e.Node);
        }

        private void LoadControls(Node node)
        {
            controlPanel.Controls.Clear();
            switch (node.Text)
            {
                case "多波形显示设置":
                    {
                       
                        _waveDisplayControl.Width = controlPanel.Width;
                        controlPanel.Controls.Add(_waveDisplayControl);
                        break;
                    }
                case "标准设置":
                    {
                        controlPanel.Controls.Add(_standard);
                        //controlPanel.Controls.Add(_standardControl);
                        break;
                    }
                case "通用设置":
                    {
                       
                        controlPanel.Controls.Add(_commonControl);
                        break;
                    }
                case "网络设置":
                    {
                        _networkControl.ServerConfig = _configData.ServerConfig;
                        controlPanel.Controls.Add(_networkControl);
                        break;
                    }
            }
        }

        /// <summary>
        /// 点击确定按钮，保存数据
        /// </summary>
        /// <param name="sender">确定按钮</param>
        /// <param name="e">触发参数</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _configData.ServerConfig = _networkControl.ServerConfig;
                _configData.MeterageRadius = _commonControl.MeterageRadius;
                _configData.MediaPath = _commonControl.MediaPath;
                _configData.SignRadius = _commonControl.SignRadius;
                _configData.AutoScrollVelocity = _commonControl.AutoScrollVelocity;
                _configData.WaveConfigs = _waveDisplayControl.WaveConfigs;
                if (_standard.StandardChangedList != null && _standard.StandardChangedList.Count > 0)
                {
                    foreach(var item in _standard.StandardChangedList)
                    {
                        string cmd = String.Format("update 大值国家标准表 set VALUEDIY={0} where speed = {1} and class = {2} and type = '{3}' and STANDARDTYPE = {4}",
                            item.StdValue, item.Speed, item.StdClass, item.StdParam, item.stdType);
                        InnerFileOperator.ExcuteSql(cmd);
                    }
                }
                //for (int i = 0; i < _standardControl.dtStandardValue.Rows.Count; i++)
                //{
                //    string cmd = String.Format("update 大值国家标准表 set VALUEDIY={0},VALUESTANDARD={1} where ID = {2}",
                //        _standardControl.dtStandardValue.Rows[i][5].ToString(),
                //        _standardControl.dtStandardValue.Rows[i][4].ToString(),
                //        _standardControl.dtStandardValue.Rows[i][0].ToString());

                //    InnerFileOperator.ExcuteSql(cmd);
                //}
                ConfigManger.SaveConfigData(_configData);
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("保存配置失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("错误：" + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
