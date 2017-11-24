using CitFileSDK;
using GeoFileProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonFileSDK;

namespace IntegratedDisplay.Forms
{
    public partial class Geo2CitConvertForm : Form
    {
        /// <summary>
        /// cit文件处理类
        /// </summary>
        private CITFileProcess _citProcess;

        /// <summary>
        /// geo文件处理类
        /// </summary>
        private GeoFileHelper _geoHelper;

        /// <summary>
        /// 线路编码和名称集合
        /// </summary>
        private DataTable _dtLineCodeAndName;

        /// <summary>
        /// geo文件路径
        /// </summary>
        private string _geoFilePath = string.Empty;

        /// <summary>
        /// iic文件路径
        /// </summary>
        private string _iicFilePath = string.Empty;

        /// <summary>
        /// iic导出路径
        /// </summary>
        private string _destIccFilePath = string.Empty;

        /// <summary>
        /// 线路编码
        /// </summary>
        private string _lineCode = string.Empty;

        /// <summary>
        /// iic数据管理类
        /// </summary>
        private IICDataManager _iicManager = new IICDataManager();

        /// <summary>
        /// 里程是否为增
        /// </summary>
        private bool _isKmInc = false;

        /// <summary>
        /// 上下行及名称集合
        /// </summary>
        private Dictionary<string, string> _dicDirAndDesc = new Dictionary<string, string>();

        /// <summary>
        /// 是否前后反转
        /// </summary>
        private bool _isReverseToForward;

        //此变量为307时，geo转换有特殊情况。
        private int _type = 1;

        /// <summary>
        /// 线路代码和配置文件集合
        /// </summary>
        private Dictionary<string, string> dicTrainCodeAndConfigPath = new Dictionary<string, string>();

        /// <summary>
        /// 是否进行iic修正
        /// </summary>
        private bool _isIICRevise = false;

        /// <summary>
        /// 正在转换
        /// </summary>
        private bool isConverting = false;

        /// <summary>
        /// 线路编码和名称集合
        /// </summary>
        public DataTable DtLineCodeAndName
        {
            get
            {
                return _dtLineCodeAndName;
            }
        }

        public Geo2CitConvertForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载上下行
        /// </summary>
        private void LoadLineDir()
        {
            _dicDirAndDesc.Clear();
            _dicDirAndDesc.Add("S", "上");
            _dicDirAndDesc.Add("X", "下");
            _dicDirAndDesc.Add("D", "单");
        }

        /// <summary>
        /// 加载线路代码和名称
        /// </summary>
        private void LoadLineCodeAndName()
        {
            if (_dtLineCodeAndName != null && _dtLineCodeAndName.Rows.Count > 0)
            {
                _dtLineCodeAndName.Rows.Clear();
            }

            //获取线路编号和线路名e
            try
            {
                string cmdText= "select 自定义线路编号, 线路名 from 自定义线路代码表";
                _dtLineCodeAndName = InnerFileOperator.Query(cmdText);
            }
            catch (Exception ex)
            {
                MyLogger.LogError("查询线路数据失败", ex);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 填充线路名下拉框
        /// </summary>
        /// <param name="lineNames"></param>
        public void FillComboboxLineName(List<string> lineNames)
        {
            cbxLineName.Items.Clear();

            foreach (string str in lineNames)
            {
                cbxLineName.Items.Add(str);
            }
        }

        /// <summary>
        /// 选择geo文件
        /// </summary>
        /// <param name="sender">选择按钮</param>
        /// <param name="e">参数</param>
        private void btnSelectGeoFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "GEO波形文件 | *.geo";
            if (openFileDialog.ShowDialog()== DialogResult.OK)
            {
                try
                {
                    txtGeoPath.Text = openFileDialog.FileName;
                    _geoFilePath = txtGeoPath.Text;
                    string[] sDHI = Path.GetFileNameWithoutExtension(_geoFilePath).Split('-');
                    string lineShortName = sDHI[0].ToUpper();
                    _geoHelper.QueryDataChannelInfoHead(_geoFilePath);
                    string mileageRange = _geoHelper.GetExportDataMileageRange(_geoFilePath);
                    mileageRange = mileageRange.Substring(2);
                    float startMileage = float.Parse(mileageRange.Substring(0, mileageRange.IndexOf("-")));
                    float endMileage = float.Parse(mileageRange.Substring(mileageRange.IndexOf("-") + 1));
                    if (startMileage < endMileage)
                    {
                        cbxKmInc.SelectedIndex = 0;
                    }
                    else
                    {
                        cbxKmInc.SelectedIndex = 1;
                    }
                    txtStartPos.Text = startMileage.ToString();
                    txtEndPos.Text = endMileage.ToString();
                    string time = String.Format("{0}:{1}:{2}", sDHI[4].Substring(0, 2), sDHI[4].Substring(2, 2), sDHI[4].Substring(4, 2));
                    string date = String.Format("{0}/{1}/{2}", sDHI[3].Substring(2, 2), sDHI[3].Substring(0, 2), sDHI[3].Substring(4, 4));
                    string lineCode = GetLineCodeByName(lineShortName);
                    if (!string.IsNullOrEmpty(lineCode))
                    {
                        var lineName = _dtLineCodeAndName.AsEnumerable().Where(p => p.Field<string>("自定义线路编号") == lineCode);
                        if (lineName != null)
                        {
                            cbxLineName.SelectedItem = lineName;
                        }
                    }
                    if (_dicDirAndDesc.ContainsKey(lineShortName.Substring(3, 1)))
                    {
                        cbxLineDir.SelectedItem = _dicDirAndDesc[lineShortName.Substring(3, 1)];
                    }
                    else
                    {
                        cbxLineDir.SelectedIndex = 0;
                    }
                    dateTimePickerDate.Value = DateTime.Parse(date);
                    dateTimePickerTime.Value = DateTime.Parse(time);

                    txtIICFilePath.Text = string.Empty;
                }
                catch(Exception ex)
                {
                    MyLogger.LogError("加载Geo文件时失败", ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 通过线路名获取线路编码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetLineCodeByName(string name)
        {
            string cmdStr = string.Format("select 线路编号 from 线路代码表 where 线路代码='{0}'", name);

            object lineCode = InnerFileOperator.ExecuteScalar(cmdStr);
            if (lineCode != null)
            {
                return lineCode.ToString();
            }
            return "";
        }

        /// <summary>
        /// 转换格式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertToCIT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGeoPath.Text))
            {
                MessageBox.Show("请先选择一个波形文件！");
                return;
            }

            if (cbxLineName.SelectedIndex < 0
                || cbxLineDir.SelectedIndex < 0 || cbxTrainCode.SelectedIndex < 0
                || cbxRunDir.SelectedIndex < 0 || cbxKmInc.SelectedIndex < 0 ||
                string.IsNullOrEmpty(txtStartPos.Text) || string.IsNullOrEmpty(txtEndPos.Text))
            {
                MessageBox.Show("请完善基础信息！");
                return;
            }
            btnConvertToCIT.Enabled = false;
            isConverting = true;
            labLoading.Visible = true;
            List<GEOCITChannelMapping> listGeo2Cit = new List<GEOCITChannelMapping>();
            string trainCode = cbxTrainCode.SelectedItem.ToString();
            if (trainCode.Contains("999307"))
            {
                _type = 307;//iType=307时，geo转换有特殊情况
            }
            else
            {
                _type = 1;
            }
            StreamReader sr = new StreamReader(dicTrainCodeAndConfigPath[trainCode], Encoding.Default);
            while (sr.Peek() != -1)
            {
                string[] sSplit = sr.ReadLine().Trim().Split(new char[] { '=' });
                GEOCITChannelMapping channelMapping = new GEOCITChannelMapping();
                channelMapping.sCIT = sSplit[0].Trim();
                channelMapping.sGEO = sSplit[1].Trim();
                channelMapping.sChinese = sSplit[2].Trim();
                listGeo2Cit.Add(channelMapping);
            }
            if(panelIIC.Enabled)
            {
                _isIICRevise = true;
            }
            else
            {
                _isIICRevise = false;
            }
            FileInformation citFileHeader = new FileInformation();
            citFileHeader.iDataType = 1;
            citFileHeader.sDataVersion = "3.0.0";
            citFileHeader.sTrackCode = _lineCode;
            citFileHeader.sTrackName = cbxLineName.Text.ToString();
            citFileHeader.sTrain = cbxTrainCode.Text.ToString();
            citFileHeader.sDate = dateTimePickerDate.Value.ToString("yyyy-MM-dd");
            citFileHeader.sTime = dateTimePickerTime.Value.ToString("HH:mm:ss");
            citFileHeader.iSmaleRate = 4;
            citFileHeader.iRunDir = cbxRunDir.SelectedIndex;
            citFileHeader.iKmInc = 1;
            if (_isKmInc)
            {
                citFileHeader.iKmInc = 0;
            }
            citFileHeader.iDir = cbxLineDir.SelectedIndex + 1;
            Task.Factory.StartNew(() => ConvertData(trainCode,citFileHeader));

        }

        /// <summary>
        /// 根据数据进行转换
        /// </summary>
        /// <param name="listGeo2Cit">geo转cit通道数据</param>
        /// <param name="citFileHeader">cit头部数据</param>
        public void ConvertData(string trainCode, FileInformation citFileHeader)
        {
            try
            {
                string geoFilePath = _geoFilePath;
                string geoNameWithoutExtension = Path.GetFileNameWithoutExtension(geoFilePath).ToUpper();
                string geoDirectory = Path.GetDirectoryName(geoFilePath);
                string destFileName = Path.Combine(geoDirectory, geoNameWithoutExtension + ".cit");

               
                _geoHelper.InitChannelMapping(dicTrainCodeAndConfigPath[trainCode]);
               
                _geoHelper.ConvertData(_geoFilePath, destFileName, citFileHeader);
                if (_isKmInc)
                {
                    _citProcess.ModifyCitMergeKmInc(destFileName);
                }
                if (_isReverseToForward)
                {
                    _citProcess.ModifyCitReverseToForward(destFileName);
                }

                if (_isIICRevise)
                {
                    ConvertIIC();
                }
                isConverting = false;
                MessageBox.Show("导出成功,导出路径为:" + destFileName);
            }
            catch(Exception ex)
            {
                MyLogger.LogError("导出Cit文件失败", ex);
                MessageBox.Show("导出失败："+ex.Message);
                isConverting = false;
            }
            if (this != null && this.IsHandleCreated && btnConvertToCIT.IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                {
                    btnConvertToCIT.Enabled = true;
                    labLoading.Visible = false;
                }));
            }
            
            
        }

        /// <summary>
        /// iic修正
        /// </summary>
        private void ConvertIIC()
        {
            if (_iicFilePath == null)
            {
                return;
            }

            File.Copy(_iicFilePath, _destIccFilePath, true);
            if (_isReverseToForward)
            {
                //左右高低对调
               _iicManager.IICChannelSwap(_destIccFilePath, "defects", "defecttype", "L SURFACE", "defecttype", "R SURFACE");
                //左右轨向对调
                _iicManager.IICChannelSwap(_destIccFilePath, "defects", "defecttype", "L ALIGNMENT", "defecttype", "R ALIGNMENT");
                //幅值*(-1)
                _iicManager.IICChannelFlip(_destIccFilePath, "defects", "maxval1", "maxval1", "defecttype", "L ALIGNMENT");//左轨距
                _iicManager.IICChannelFlip(_destIccFilePath, "defects", "maxval1", "maxval1", "defecttype", "R ALIGNMENT");//右轨距
                _iicManager.IICChannelFlip(_destIccFilePath, "defects", "maxval1", "maxval1", "defecttype", "CROSSLEVEL");//水平
                                                                                                                     //CommonClass.wdp.IICChannelFlip(iicFilePath, "defects", "maxval1", "maxval1", "defecttype", "R ALIGNMENT");//超高--没有这项大值
                _iicManager.IICChannelFlip(_destIccFilePath, "defects", "maxval1", "maxval1", "defecttype", "TWIST");//三角坑
                _iicManager.IICChannelFlip(_destIccFilePath, "defects", "maxval1", "maxval1", "defecttype", "CURVATURE RATE");//曲率变化率
            }

            #region TQI处理
            if (_isReverseToForward)
            {
                //左右高低对调
                _iicManager.IICChannelSwap(_destIccFilePath, "tqi", "TQIMetricName", "L_STDSURF", "TQIMetricName", "R_STDSURF");
                //左右轨向对调
                _iicManager.IICChannelSwap(_destIccFilePath, "tqi", "TQIMetricName", "L_STDALIGN", "TQIMetricName", "R_STDALIGN");
            }
            if (_isKmInc)
            {
                //减里程调整为增里程时，tqi里程要减去0.2
                _iicManager.IICTqi(_destIccFilePath, "tqi", "BasePost", "BasePost");
            }
            #endregion
            //删除已经创建的fix表
            _iicManager.DropFixTalbe(_destIccFilePath);
        }

        private void Geo2CitConvertForm_Load(object sender, EventArgs e)
        {
            try
            {
                _geoHelper = new GeoFileHelper();
                _citProcess = new CITFileProcess();
                InitDicTrainCodeAndConfigPath();
                LoadLineDir();
                LoadLineName();
                LoadTrainCode();
                LoadLineCodeAndName();
            }
            catch(Exception ex)
            {
                MyLogger.LogError("初始化Geo转cit时失败", ex);
                MessageBox.Show(ex.Message);
            }
            
        }

        private void LoadLineName()
        {
            cbxLineName.Items.Clear();
            InnerFileOperator.CheckAndCreateTable("自选线路代码表", "CREATE TABLE 自选线路代码表(线路名 TEXT(255))");
            DataTable dt = null;
            try
            {
                dt = InnerFileOperator.Query("select 线路名 from 自选线路代码表");
            }
            catch(Exception ex)
            {
                MessageBox.Show("查询线路失败：" + ex.Message);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbxLineName.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }

        private void LoadTrainCode()
        {
            cbxTrainCode.Items.Clear();
            foreach (var keyValuePair in dicTrainCodeAndConfigPath)
            {
                cbxTrainCode.Items.Add(keyValuePair.Key);
            }
        }

        private void InitDicTrainCodeAndConfigPath()
        {
            dicTrainCodeAndConfigPath.Clear();
            string configPath = Path.Combine(Application.StartupPath, "GEOConfig");
            string[] configFiles = Directory.GetFiles(configPath, "*.csv", SearchOption.AllDirectories);
            foreach (string configFile in configFiles)
            {
                dicTrainCodeAndConfigPath.Add(Path.GetFileNameWithoutExtension(configFile), configFile);
            }
        }

        private void btnSelectIICFile_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "IIC文件|*.iic";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _iicFilePath = openFileDialog.FileName;
                txtIICFilePath.Text = openFileDialog.FileName;
                string iicDiretory = Path.GetDirectoryName(_iicFilePath);
                string iicFileName = Path.GetFileNameWithoutExtension(_iicFilePath);
                _destIccFilePath = Path.Combine(iicDiretory, iicFileName + "_new" + ".iic");
            }
        }

        private void cbxLineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLineName.SelectedItem != null)
            {
                string cmdStr = string.Format("select 线路编号 from 线路代码表 where PWMIS原始线路名='{0}'", cbxLineName.SelectedItem.ToString());
                object lineCode = InnerFileOperator.ExecuteScalar(cmdStr);
                if (lineCode != null)
                {
                    _lineCode = lineCode.ToString();
                }
            }
        }

        private void comboBoxRunDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbxLineDir.SelectedItem!=null? cbxLineDir.SelectedItem.ToString():"") == "单" 
                && (cbxRunDir.SelectedItem!=null?cbxRunDir.SelectedItem.ToString():"") == "反")
            {
                _isReverseToForward = true;
            }
            else
            {
                _isReverseToForward = false;
            }

            if ((cbxLineDir.SelectedItem!=null?cbxLineDir.SelectedItem.ToString():"") == "单" 
                && ((cbxKmInc.SelectedItem!=null?cbxKmInc.SelectedItem.ToString():"") == "减" 
                || (cbxRunDir.SelectedItem!=null?cbxRunDir.SelectedItem.ToString():"") == "反"))
            {
                panelIIC.Enabled = true;
            }
            else
            {
                panelIIC.Enabled = false;
            }
        }

        private void cbxKmInc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxKmInc.SelectedIndex != 1)
            {
                _isKmInc = true;
            }
        }

        private void txtMileage_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            String tbStr = tb.Text;

            if (String.IsNullOrEmpty(tbStr))
            {
                MessageBox.Show("里程不能为空！");
                return;
            }
            else
            {
                try
                {
                    float mile = float.Parse(tbStr);
                    if (mile < 0)
                    {
                        MessageBox.Show("里程数必须大于或等于零！");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入数字！");
                    return;
                }
            }
        }

        private void picSelectLineName_Click(object sender, EventArgs e)
        {
            SelectLineNameForm form = new SelectLineNameForm(this);
            form.ShowDialog();
        }

        private void Geo2CitConvertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isConverting;
            if(e.Cancel== true)
            {
                MessageBox.Show("当前正在导出，请等待导出完毕!");
            }
        }
    }
}
