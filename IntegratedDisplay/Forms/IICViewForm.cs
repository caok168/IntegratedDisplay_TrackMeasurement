﻿
using IntegratedDisplay.Models;
/// -------------------------------------------------------------------------------------------
/// FileName：IICViewForm.cs
/// 说    明：IIC 数据查看窗体
/// Version ：1.0
/// Date    ：2017/6/2
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using IntegratedDisplay.DataGridViewAutoFilter;
//using Steema.TeeChart.Styles;

namespace IntegratedDisplay.Forms
{
    /// <summary>
    /// IIC 数据查看窗体
    /// </summary>
    public partial class IICViewForm : Form
    {
        WaveformMaker waveformMaker = null;
        List<WavefromData> waveFormDataList = new List<WavefromData>();

        IICDataManager iicDataManager = new IICDataManager();
        InvalidDataManager invalidManager = new InvalidDataManager();

        List<InvalidData> invalidDataList = new List<InvalidData>();

        ChartConfig chartAccumulateCurveData = new ChartConfig();
        ChartConfig chartMilDistributeData = new ChartConfig();
        ChartConfig chartSpeedCurveData = new ChartConfig();

        string sKmInc = "";

        /// <summary>
        /// idf文件路径
        /// </summary>
        string idfFilePath = "";
        /// <summary>
        /// cit文件路径
        /// </summary>
        string citFilePath = "";
        /// <summary>
        /// iic文件路径
        /// </summary>
        string iicFilePath = "";
        /// <summary>
        /// iic文件名（不含后缀）
        /// </summary>
        string iicFileName = "";

       public  string innerDBPath = "";

        
        /// <summary>
        /// 存放fix_tqi表的valid值
        /// </summary>
        private Dictionary<int, String> dicValid = new Dictionary<int, String>();//存放fix_tqi表的valid值

        object oMissing = System.Reflection.Missing.Value;
        object oFalse = false;
        object oTrue = true;
        public int iHeight = 287;
        public int iWidth = 590;
        public List<int> listETId;
        public List<string> listETName;//IIC文件中大值列表中的大值英文名称
        public List<string> listETChName;//IIC文件中大值列表中的大值中文名称
        public List<string> listDTChName;//大值类型中文名
        public List<string> listDTEnName;//大值类型英文名
        public List<string> listETChannelENName;//大值对应的cit文件中的通道英文名
        private string sFileTitle;
        private string sFilePath = "";
        private string sTQIName = "fix_tqi";
        private string sDefectsName = "fix_defects";
        private Boolean isIICFixed = true;

        /// <summary>
        /// 内存表，绑定到DataGridView
        /// </summary>
        private DataTable dt_Defects;


        public IICViewForm()
        {
            InitializeComponent();
            InitDicValid();
        }

        public IICViewForm(WaveformMaker maker)
        {
            InitializeComponent();
            waveformMaker = maker;
            
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IICViewForm_Load(object sender, EventArgs e)
        {
            InitDicValid();
            InitConfigData();

            int iKmInc = waveformMaker.WaveformDataList[0].CitFile.iKmInc;
            if (iKmInc == 0)
            {
                sKmInc = "增";
            }
            else if (iKmInc == 1)
            {
                sKmInc = "减";
            }

            idfFilePath = waveformMaker.WaveformDataList[0].WaveIndexFilePath;
            citFilePath = waveformMaker.WaveformDataList[0].CitFilePath;
            invalidDataList = invalidManager.InvalidDataList(idfFilePath);
           
            listETId = new List<int>();
            listETChName = new List<string>();
            listETName = new List<string>();
            listDTChName = new List<string>();
            listETChannelENName = new List<string>();
            listDTEnName = new List<string>();
            string iicConfig = Application.StartupPath + "\\EXCEPTIONTYPE.ini";
            if (File.Exists(iicConfig))
            {
                StreamReader sr = new StreamReader(iicConfig, Encoding.Default);
                while (sr.Peek() != -1)
                {
                    string sStr = sr.ReadLine();
                    string[] sSplit = sStr.Split(new char[] { ',' });

                    listETId.Add(int.Parse(sSplit[1]));
                    listETName.Add(sSplit[2]);
                    listETChName.Add(sSplit[3]);
                    listDTChName.Add(sSplit[4]);
                    listETChannelENName.Add(sSplit[6]);
                    listDTEnName.Add(sSplit[5]);
                }
                sr.Close();
            }
            else
            {
                MessageBox.Show("找不到IIC配置文件，路径为：" + iicConfig);
            }

        }

        private void InitConfigData()
        {
            chartAccumulateCurveData = new ChartConfig();
            chartAccumulateCurveData.ChartTitle=(sFileTitle + "TQI累积曲线");
            chartAccumulateCurveData.ChartTitleFont = new Font("Arial", 16, FontStyle.Bold);
            chartAccumulateCurveData.AxesXMin = 0;
            chartAccumulateCurveData.AxesXStep = 1;
            chartAccumulateCurveData.AxesXTitle = "TQI";
            chartAccumulateCurveData.AxesXFont = new Font("Arial", 10, FontStyle.Bold);
            chartAccumulateCurveData.AxesYStep = 10;
            chartAccumulateCurveData.AxesYMin = 0;
            chartAccumulateCurveData.AxesYTitle = "百分比";
            chartAccumulateCurveData.AxesYFont = new Font("Arial", 10, FontStyle.Bold);


            chartMilDistributeData = new ChartConfig();
            chartMilDistributeData.ChartTitle=(sFileTitle + "TQI里程分布图");
            chartMilDistributeData.ChartTitleFont = new Font("Arial", 16, FontStyle.Bold);
            chartMilDistributeData.AxesXMin = 0;
            chartMilDistributeData.AxesXStep = 50;
            chartMilDistributeData.AxesXTitle = "公里(km)";
            chartMilDistributeData.AxesXFont = new Font("Arial", 10, FontStyle.Bold);
            chartMilDistributeData.AxesYStep = 1;
            chartMilDistributeData.AxesYMin = 0;
            chartMilDistributeData.AxesYTitle = "TQI(mm)";
            chartMilDistributeData.AxesYFont = new Font("Arial", 10, FontStyle.Bold);

            chartSpeedCurveData = new ChartConfig();
            chartSpeedCurveData.ChartTitle=(sFileTitle + "速度曲线图");
            chartSpeedCurveData.ChartTitleFont = new Font("Arial", 16, FontStyle.Bold);
            chartSpeedCurveData.AxesXMin = 0;
            chartSpeedCurveData.AxesXStep = 500;
            chartSpeedCurveData.AxesYStep = 20;
            chartSpeedCurveData.AxesYMin = 0;
            chartSpeedCurveData.AxesYTitle = "速度(km/h)";
            chartSpeedCurveData.AxesYFont = new Font("Arial", 10, FontStyle.Bold);


        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IICViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            MainForm.sMainform.Activate();
        }

        /// <summary>
        /// 浏览按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            //第一次打开iic文件时，sIICFilePath==null,之后不等于null,且字典里肯定有
            openFileDialog1.FileName = "";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                iicFilePath = openFileDialog1.FileName;

                textBox1.Text = iicFilePath;

                iicFileName = Path.GetFileNameWithoutExtension(textBox1.Text);
                string[] s1 = iicFileName.Split(new char[] { '_' });
                sFileTitle = s1[0] + s1[1];
                chartAccumulateCurveData.ChartTitle = (sFileTitle + "TQI累积曲线");
                chartMilDistributeData.ChartTitle = (sFileTitle + "TQI里程分布图");
                chartSpeedCurveData.ChartTitle = (sFileTitle + "速度曲线图");
                dataGridView1.Rows.Clear();
                sFilePath = Path.GetDirectoryName(textBox1.Text);
                try
                {
                    chartAccumulateCurve.Series.Clear();
                    chartMileDistribute.Series.Clear();
                    chartSpeedCurVe.Series.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                /*
                 * fix_tqi表有两种来源：
                 * a：执行iic修正功能，重新计算tqi，结合无效区段，把有些tqi值置无效(valid == 0),有效的是(valid == 1)
                 * b：未修正时，把tqi表转置,此时valid == 2，当在界面人工置无效时，把valid==0
                 * 
                 * 因此，从iic文件查看是否已经经过里程修正，应该要分以下情况讨论：
                 * 1: fix_tqi表不存在，则表示此iic文件未执行过iic修正功能（valid == 1），且还没有执行过iic查看功能(valid == 2)  ---未修正
                 * 2: fix_tqi表存在，还要分情况：
                 *                             a  有可能执行过IIC修正,valid ==  1  ---已修正
                 *                             b   有可能被打开查看过， valid == 2   ----未修正
                 */

                #region 路局需要查看未修正的iic
                //iic没有修正
                isIICFixed = iicDataManager.IsIICFixed(iicFilePath);

                if (!isIICFixed)
                {
                    if (!iicDataManager.IsHasFixTable(iicFilePath))
                    {
                        //创建两张fix表，然后在窗口关闭时，删掉;同时需要在窗体标题给予提示：iic未修正
                        iicDataManager.CreateIICTable(iicFilePath);
                        //把原始表里的tqi拷贝到fix表里
                        iicDataManager.TQICopy(iicFilePath, sKmInc, invalidDataList);
                    }


                    buttonReSetTqi.Enabled = true;
                    buttonInvalid.Enabled = true;
                    //修改窗体标题
                    this.Text = "IIC数据查看---未修正";
                    //在窗体关闭时，还需要删掉两种新表：fix

                }
                else
                {

                    buttonReSetTqi.Enabled = false;
                    buttonInvalid.Enabled = false;
                    //需要在窗体标题给予提示：iic已修正
                    this.Text = "IIC数据查看---已修正";
                }
                #endregion

                GetTQIData();
                GetDefectData();

                GetKouFenData();

                panel1.Enabled = true;
                Task t = Task.Factory.StartNew(() =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        CreateAccumulateCurve();
                        CreateMileDistribute();
                        CreateSpeedCurVe();
                    }));
                });
            }
        }

        /// <summary>
        /// 无效滤除按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInvalid_Click(object sender, EventArgs e)
        {
            iicDataManager.TQIInvalid(iicFilePath, sKmInc, invalidDataList);
            GetTQIData();
        }

        /// <summary>
        /// 重新生成TQI按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReSetTqi_Click(object sender, EventArgs e)
        {
            #region 路局需要查看未修正的iic
            //iic没有修正
            isIICFixed = iicDataManager.IsIICFixed(iicFilePath);

            if (!isIICFixed)
            {
                //创建两张fix表，然后在窗口关闭时，删掉;同时需要在窗体标题给予提示：iic未修正
                iicDataManager.CreateIICTable(iicFilePath);
                //把原始表里的tqi拷贝到fix表里
                iicDataManager.TQICopy(iicFilePath, sKmInc, invalidDataList);
                //修改窗体标题

                buttonReSetTqi.Enabled = true;
                buttonInvalid.Enabled = true;

                this.Text = "IIC数据查看---IIC未修正";
                //在窗体关闭时，还需要删掉两种新表：fix

            }
            else
            {
                buttonReSetTqi.Enabled = false;
                buttonInvalid.Enabled = false;

                //需要在窗体标题给予提示：iic已修正
                this.Text = "IIC数据查看---IIC已修正";
            }
            #endregion

            GetTQIData();
            GetDefectData();

            GetKouFenData();
        }

        /// <summary>
        /// 生成Excel按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateExcel_Click(object sender, EventArgs e)
        {
            using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
            {
                OleDbCommand sqlcom = new OleDbCommand("select (FromPost+FromMinor/1000.0),TQISum_Value,valid,L_Prof_Value,R_Prof_Value,L_Align_Value,R_Align_Value,Gage_Value,Crosslevel_TQIValue,ShortTwist_Value,LATACCEL_Value,VERTACCEL_Value,AVERAGE_SPEED from " + sTQIName + " where TQISum_Value>" + txtTQIOverValue.Text, odb);
                odb.Open();
                OleDbDataReader oledbr = sqlcom.ExecuteReader();
                StreamWriter sw = new StreamWriter(iicFilePath + ".table1.csv", false, Encoding.Default);

                StringBuilder sbtmp = new StringBuilder();
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    sbtmp.Append(dataGridView1.Columns[i].HeaderText + ",");
                }
                sbtmp.Remove(sbtmp.Length - 1, 1);
                sw.WriteLine(sbtmp.ToString());

                //sw.WriteLine("里程,TQI,有效性,左高低,右高低,左轨向,右轨向,轨距,水平,三角坑,横加,垂加,平均速度");
                while (oledbr.Read())
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < oledbr.FieldCount; j++)
                    {
                        if (j == 2)
                        {
                            sb.Append(dicValid[int.Parse(oledbr.GetValue(j).ToString())]);
                        }
                        else
                        {
                            sb.Append(Math.Round(double.Parse(oledbr.GetValue(j).ToString()), 2));
                        }
                        sb.Append(",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sw.WriteLine(sb.ToString());

                }

                sw.Close();
                oledbr.Close();
                odb.Close();
            }

            MessageBox.Show("生成成功！");
        }

        /// <summary>
        /// 生成累积曲线按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccumulateCurve_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (CreateAccumulateCurve())
                {
                    MessageBox.Show("生成成功！");
                }
                else
                {
                    MessageBox.Show("生成失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生成TQI累计曲线图
        /// </summary>
        /// <returns>成功：true，失败：false</returns>
        private bool CreateAccumulateCurve()
        {
            try
            {
                String cmdText = string.Format("select TQISum_Value from {0} where valid={1} order by TQISum_Value", sTQIName, "1");
                double[][] d = iicDataManager.QueryLeiJiXuQian(cmdText, iicFilePath);
                chartAccumulateCurve.Titles.Clear();
                chartAccumulateCurve.Series.Clear();
                
                Title tile = new Title(chartAccumulateCurveData.ChartTitle);
                tile.Font = chartAccumulateCurveData.ChartTitleFont;
                chartAccumulateCurve.AntiAliasing = AntiAliasingStyles.Graphics;
                chartAccumulateCurve.Titles.Add(tile);
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Line;
                s1.IsVisibleInLegend = false;
                chartAccumulateCurve.BorderlineColor = Color.Black;
                chartAccumulateCurve.BorderSkin = new BorderSkin();
                chartAccumulateCurve.BorderSkin.BorderWidth = 2;
                chartAccumulateCurve.ChartAreas[0].AxisX.Minimum = chartAccumulateCurveData.AxesXMin;
                chartAccumulateCurve.ChartAreas[0].AxisX.Interval = chartAccumulateCurveData.AxesXStep;
                chartAccumulateCurve.ChartAreas[0].AxisX.Title = chartAccumulateCurveData.AxesXTitle;
                chartAccumulateCurve.ChartAreas[0].AxisX.TitleFont = chartAccumulateCurveData.AxesXFont;

                chartAccumulateCurve.ChartAreas[0].AxisY.Interval = chartAccumulateCurveData.AxesYStep;
                chartAccumulateCurve.ChartAreas[0].AxisY.Minimum = chartAccumulateCurveData.AxesYMin;

                chartAccumulateCurve.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartAccumulateCurve.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartAccumulateCurve.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartAccumulateCurve.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartAccumulateCurve.ChartAreas[0].AxisY.Title = chartAccumulateCurveData.AxesYTitle;
                chartAccumulateCurve.ChartAreas[0].AxisY.TitleFont = chartAccumulateCurveData.AxesYFont;
                for (int i = 0; i < d[0].Length; i++)
                {
                    s1.Points.AddXY(d[0][i], d[1][i]);
                }
                s1.MarkerStyle = new MarkerStyle();
                s1.MarkerSize = 3;
                s1.MarkerColor = Color.Blue;
                chartAccumulateCurve.Series.Add(s1);
                chartAccumulateCurve.SaveImage(sFilePath + "\\累积曲线.jpg", ChartImageFormat.Jpeg);
                return true;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("生成TQI累积曲线图失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                chartAccumulateCurve.Titles.Clear();
                chartAccumulateCurve.Series.Clear();
                return false;
            }
        }

        /// <summary>
        /// 生成里程分布按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateMileDistribute_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateMileDistribute())
                {
                    MessageBox.Show("生成成功！");
                }
                else
                {
                    MessageBox.Show("生成失败！");
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生成TQI里程分布图
        /// </summary>
        /// <returns>成功：true，失败：false</returns>
        private bool CreateMileDistribute()
        {
            try
            {
                String cmdText = string.Format("select (FromPost+FromMinor/1000.0),TQISum_Value from {0} where valid = {1} order by TQISum_Value", sTQIName, 1);
                double[][] d = iicDataManager.QueryLCFB(cmdText, iicFilePath);
                chartMileDistribute.Titles.Clear();
                chartMileDistribute.Series.Clear();
                
                Title tile = new Title(chartMilDistributeData.ChartTitle);
                tile.Font = chartMilDistributeData.ChartTitleFont;
                chartMileDistribute.AntiAliasing = AntiAliasingStyles.Graphics;
                chartMileDistribute.Titles.Add(tile);
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Point;
                s1.IsVisibleInLegend = false;
                chartMileDistribute.BorderlineColor = Color.Black;
                chartMileDistribute.BorderSkin = new BorderSkin();
                chartMileDistribute.BorderSkin.BorderWidth = 2;
                chartMileDistribute.ChartAreas[0].AxisX.Minimum = chartMilDistributeData.AxesXMin;
                chartMileDistribute.ChartAreas[0].AxisX.Interval = chartMilDistributeData.AxesXStep;
                chartMileDistribute.ChartAreas[0].AxisX.Title = chartMilDistributeData.AxesXTitle;
                chartMileDistribute.ChartAreas[0].AxisX.TitleFont = chartMilDistributeData.AxesXFont;

                chartMileDistribute.ChartAreas[0].AxisY.Interval = chartMilDistributeData.AxesYStep;
                chartMileDistribute.ChartAreas[0].AxisY.Minimum = chartMilDistributeData.AxesYMin;

                chartMileDistribute.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartMileDistribute.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartMileDistribute.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartMileDistribute.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartMileDistribute.ChartAreas[0].AxisY.Title = chartMilDistributeData.AxesYTitle;
                chartMileDistribute.ChartAreas[0].AxisY.TitleFont = chartMilDistributeData.AxesYFont;
                for (int i = 0; i < d[0].Length; i++)
                {
                    s1.Points.AddXY(d[0][i], d[1][i]);
                }
                s1.MarkerStyle = new MarkerStyle();
                s1.MarkerSize = 3;
                s1.MarkerColor = Color.Blue;
                chartMileDistribute.Series.Add(s1);
                chartMileDistribute.SaveImage(sFilePath + "\\TQI里程分布图.jpg", ChartImageFormat.Jpeg);
                return true;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("生成TQI里程分布图失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                chartMileDistribute.Titles.Clear();
                chartMileDistribute.Series.Clear();
                return false;
            }
        }

        /// <summary>
        /// 生成速度曲线按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateSpeedCurve_Click(object sender, EventArgs e)
        {
            if (CreateSpeedCurVe())
            {
                MessageBox.Show("生成成功！");
            }
            else
            {
                MessageBox.Show("生成失败！");
            }
        }

        /// <summary>
        /// 生成速度曲线图
        /// </summary>
        /// <returns>成功：true，失败：false</returns>
        private bool CreateSpeedCurVe()
        {
            try
            {
                double[][] d = iicDataManager.QuerySUDU("select 公里,通过速度 from kms order by 公里", iicFilePath);
                
                chartSpeedCurVe.Titles.Clear();
                chartSpeedCurVe.Series.Clear();

                Title tile = new Title(chartSpeedCurveData.ChartTitle);
                tile.Font = chartSpeedCurveData.ChartTitleFont;
                chartSpeedCurVe.AntiAliasing = AntiAliasingStyles.Graphics;
                chartSpeedCurVe.Titles.Add(tile);
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Line;
                s1.IsVisibleInLegend = false;
                chartSpeedCurVe.BorderlineColor = Color.Black;
                chartSpeedCurVe.BorderSkin = new BorderSkin();
                chartSpeedCurVe.BorderSkin.BorderWidth = 2;
                chartSpeedCurVe.ChartAreas[0].AxisX.Minimum = chartSpeedCurveData.AxesXMin;
                chartSpeedCurVe.ChartAreas[0].AxisX.Interval = chartSpeedCurveData.AxesXStep;


                chartSpeedCurVe.ChartAreas[0].AxisY.Interval = chartSpeedCurveData.AxesYStep;
                chartSpeedCurVe.ChartAreas[0].AxisY.Minimum = chartSpeedCurveData.AxesYMin;

                chartSpeedCurVe.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartSpeedCurVe.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
                chartSpeedCurVe.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartSpeedCurVe.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartSpeedCurVe.ChartAreas[0].AxisY.Title = chartSpeedCurveData.AxesYTitle;
                chartSpeedCurVe.ChartAreas[0].AxisY.TitleFont = chartSpeedCurveData.AxesYFont;
                for (int i = 0; i < d[0].Length; i++)
                {
                    s1.Points.AddXY(d[0][i], d[1][i]);
                }
                s1.MarkerStyle = new MarkerStyle();
                s1.MarkerSize = 3;
                s1.MarkerColor = Color.Blue;
                chartSpeedCurVe.Series.Add(s1);
                chartSpeedCurVe.SaveImage(sFilePath + "\\速度.jpg", ChartImageFormat.Jpeg);
                return true;
            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("生成速度曲线图失败：" + ex.Message + ",堆栈：" + ex.StackTrace);
                chartSpeedCurVe.Titles.Clear();
                chartSpeedCurVe.Series.Clear();
                return false;
            }
        }

        /// <summary>
        /// 导出TQI分布 按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportTQIDistribute_Click(object sender, EventArgs e)
        {
            //TQI分布比例
            List<double> listResult = new List<double>();
            try
            {
                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    //
                    String cmdText = string.Format("select TQISum_Value from {0} where valid = {1} order by TQISum_Value desc", sTQIName, 1);
                    OleDbCommand sqlcom = new OleDbCommand(cmdText, odb);
                    odb.Open();
                    OleDbDataReader oledbr = sqlcom.ExecuteReader();
                    while (oledbr.Read())
                    {
                        listResult.Add((float)oledbr.GetValue(0));

                    }

                    oledbr.Close();
                    odb.Close();
                }
                //MessageBox.Show(listResult.Count.ToString());
                int i1 = 0;
                int i2 = 0;
                int i3 = 0;
                int i4 = 0;
                int i5 = 0;
                for (int i = 0; i < listResult.Count; i++)
                {
                    if (listResult[i] >= 1 && listResult[i] < 2)
                    {
                        i1++;
                    }
                    else if (listResult[i] >= 2 && listResult[i] < 3)
                    {
                        i2++;
                    }
                    else if (listResult[i] >= 3 && listResult[i] < 4)
                    {
                        i3++;
                    }
                    else if (listResult[i] >= 4 && listResult[i] < 5)
                    {
                        i4++;
                    }
                    else if (listResult[i] >= 5)
                    {
                        i5++;
                    }
                }
                if (listResult.Count > 0)
                {
                    StreamWriter sw = new StreamWriter(iicFilePath + ".分布比例.csv", false, Encoding.Default);
                    sw.WriteLine("[1-2,[2-3,[3-4,[4-5,>=5");
                    sw.WriteLine((i1 / 1.0 / listResult.Count * 100).ToString("f2") + "%" + ","
                        + (i2 / 1.0 / listResult.Count * 100).ToString("f2") + "%" + ","
                        + (i3 / 1.0 / listResult.Count * 100).ToString("f2") + "%" + ","
                        + (i4 / 1.0 / listResult.Count * 100).ToString("f2") + "%" + ","
                        + (i5 / 1.0 / listResult.Count * 100).ToString("f2") + "%");
                    sw.Close();
                    MessageBox.Show("生成成功！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 导出TQI明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportTQIDetail_Click(object sender, EventArgs e)
        {
            String csvFilePath = iicFilePath + ".tqi.csv";

            ExportDataFromDataGridView(dataGridView1, csvFilePath);
        }

        /// <summary>
        /// 导出偏差明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportDeviation_Click(object sender, EventArgs e)
        {
            String csvFilePath = iicFilePath + ".偏差明细.csv";

            ExportDataFromDataGridView(dataGridViewDefect1, csvFilePath);
        }

        /// <summary>
        /// 导出公里扣分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportMileScore_Click(object sender, EventArgs e)
        {
            String csvFilePath = iicFilePath + ".kms.csv";

            ExportDataFromDataGridView(dataGridViewKF, csvFilePath);
        }

        /// <summary>
        /// 偏差截图按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIICPicture_Click(object sender, EventArgs e)
        {
            try
            {
                InitCheckBoxList();
                InitStr();

                CreateIICImageTable(iicFilePath);

                dt = ReadDataToDataTable();

            }
            catch(Exception ex)
            {
                MyLogger.logger.Error("初始化IIC数据失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }

            if (dt_Defects.Rows.Count == 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(strExcptnLevel))
            {
                return;
            }
            try
            {
                for (int i = 0; i < dt_Defects.Rows.Count; i++)
                {
                    int km = (int)(dt_Defects.Rows[i]["公里"]);
                    int meter = (int)(dt_Defects.Rows[i]["米"]);
                    String channelName = (String)(dt_Defects.Rows[i]["项目"]);
                    float excptnVal = (float)(dt_Defects.Rows[i]["峰值"]);
                    string excptnLevel = (string)(dt_Defects.Rows[i]["超限等级"]);
                    int excptnSpd = (int)(dt_Defects.Rows[i]["检测标准"]);

                    float exctpnLen = (float)(dt_Defects.Rows[i]["长度(m/g)"]);
                    string excptnChannelEnName = GetExceptionChannelEnName(channelName);
                    int defectNum = (int)(dt_Defects.Rows[i]["大值编号"]);

                    float milePos = (float)(km + meter / 1000.0);

                    //如果超限等级没有选中，则跳过，不截图。
                    if (!strExcptnLevel.Contains(excptnLevel.ToString()))
                    {
                        continue;
                    }

                    string excptnDTEnName = GetExceptionDataTypeEnName(channelName);
                    float[] m_excptnStd = QueryDataTable(excptnSpd, excptnLevel.ToString(), excptnDTEnName);
                    float[] m_excptnStd_1 = QueryDataTable(excptnSpd, (4 + 1).ToString(), excptnDTEnName);

                    String excptnLevelStr = null;
                    if (excptnLevel == "自定义")
                    {
                        m_excptnStd = QueryDataTable(excptnSpd, "1", excptnDTEnName);
                        excptnLevelStr = string.Format("[{0},{1}]", m_excptnStd[0], m_excptnStd[1]);
                    }
                    else
                    {
                        m_excptnStd = QueryDataTable(excptnSpd, excptnLevel, excptnDTEnName);
                        m_excptnStd_1 = QueryDataTable(excptnSpd, (int.Parse(excptnLevel) + 1).ToString(), excptnDTEnName);
                        excptnLevelStr = string.Format("[{0},{1}]", m_excptnStd[0], m_excptnStd_1[0]);
                    }

                    Bitmap bmp = CutPictures(milePos, channelName, excptnVal, excptnLevel, excptnSpd, excptnLevelStr, exctpnLen, excptnChannelEnName);
                    Boolean retVal = InsertIntoIICImage(i + 1, defectNum, bmp, "png", iicFilePath);
                }
                MessageBox.Show("IIC截图完成");
            }
            catch (Exception ex)
            {
                MyLogger.logger.Error("截图时出现错误:" + ex.Message + ",堆栈：" + ex.StackTrace);
                MessageBox.Show("截图时出现错误"+ex.Message);
            }
        }

        /// <summary>
        /// 偏差截图预览按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonIICPictureView_Click(object sender, EventArgs e)
        {
            if (dataGridViewDefect1.SelectedRows.Count != 0)
            {
                int defectNum = (int)(dataGridViewDefect1.SelectedRows[0].Cells["大值编号"].Value);

                byte[] imageData = null;

                try
                {
                    using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                    {
                        sqlconn.Open();
                        OleDbCommand com = sqlconn.CreateCommand();
                        com.CommandText = "select * from IICImages where ExceptionId = " + defectNum;

                        OleDbDataReader dr = com.ExecuteReader();
                        while (dr.Read())
                        {
                            imageData = (byte[])(dr.GetValue(2));
                        }

                        com.Dispose();
                        sqlconn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取偏差图片异常:" + ex.Message);
                }

                if (imageData == null)
                {
                    return;
                }

                //处理二进制字节数组
                MemoryStream ms = new MemoryStream(imageData);
                Image img = Image.FromStream(ms);

                Form picture = new Form();
                PictureBox pb = new PictureBox();
                pb.Image = img;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Dock = DockStyle.Fill;

                pb.BackColor = Color.White;

                picture.Controls.Add(pb);
                picture.BackColor = Color.White;

                //屏幕宽度
                int width = SystemInformation.WorkingArea.Width;
                //屏幕高度（不包括任务栏）
                int height = SystemInformation.WorkingArea.Height;
                picture.WindowState = FormWindowState.Maximized;
                picture.Width = width / 3 * 2;
                picture.Height = height / 3 * 2;
                picture.StartPosition = FormStartPosition.CenterScreen;
                picture.TopLevel = true;
                picture.Text = "大值截图";
                picture.Show(this);
            }
        }

        /// <summary>
        /// TQI超标值文本框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTQIOverValue_Leave(object sender, EventArgs e)
        {

        }


        #region 私有方法

        private void InitDicValid()
        {
            dicValid.Clear();

            dicValid.Add(0, "无效");
            dicValid.Add(1, "有效");
            dicValid.Add(2, "有效");
        }

        private void GetTQIData()
        {
            try
            {
                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    //老的fix_tqi表不包含平均速度列，因此加载老的iic文件时会报错。---解决方法是从新进行iic修正。----ygx--20140114
                    OleDbCommand sqlcom = new OleDbCommand("select (FromPost+FromMinor/1000.0),TQISum_Value ,valid,L_Prof_Value,R_Prof_Value,L_Align_Value,R_Align_Value,Gage_Value,Crosslevel_TQIValue,ShortTwist_Value,LATACCEL_Value,VERTACCEL_Value,AVERAGE_SPEED from " + sTQIName + " order by TQISum_Value desc", odb);
                    odb.Open();
                    //dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    OleDbDataReader oledbr = sqlcom.ExecuteReader();
                    while (oledbr.Read())
                    {
                        //object[] o = new object[oledbr.FieldCount];

                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.CreateCells(dataGridView1);

                        for (int j = 0; j < oledbr.FieldCount; j++)
                        {
                            //o[j] = oledbr.GetValue(j);   
                            if (j == 2)
                            {
                                dgvr.Cells[j].Value = dicValid[int.Parse(oledbr.GetValue(j).ToString())];
                            }
                            else
                            {
                                dgvr.Cells[j].Value = double.Parse(oledbr.GetValue(j).ToString());
                            }
                        }

                        if (float.Parse(dgvr.Cells[1].Value.ToString()) > float.Parse(txtTQIOverValue.Text))
                        {
                            dgvr.Cells[1].Style.BackColor = Color.Red;
                        }

                        if (dgvr.Cells[2].Value.ToString() == "无效")
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        dataGridView1.Rows.Add(dgvr);

                    }


                    oledbr.Close();
                    odb.Close();
                }

                if (!isIICFixed)
                {
                    dataGridView1.Columns[12].HeaderText = "最大速度";
                }
                else
                {
                    dataGridView1.Columns[12].HeaderText = "平均速度";
                }


                AddListMenuItem();
                SetTQI();
            }
            catch (System.Data.OleDb.OleDbException)
            {
                MessageBox.Show("请重新进行IIC修正!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDefectData()
        {
            try
            {
                InitDataTableDefects(ref dt_Defects, sDefectsName);

                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    OleDbCommand sqlcom = new OleDbCommand("SELECT maxpost,maxminor,defecttype,maxval1,length,defectclass,tbce,speedatmaxval,postedspd,RecordNumber " +
    " FROM " + sDefectsName + " where valid<>'N' and defectclass in (1,2,3,4,21,22,23,24) order by maxpost,maxminor;", odb);
                    odb.Open();

                    if (dt_Defects.Rows.Count > 0)
                    {
                        dt_Defects.Rows.Clear();
                    }
                    OleDbDataReader oledbr = sqlcom.ExecuteReader();
                    while (oledbr.Read())
                    {
                        object[] o = new object[oledbr.FieldCount + 1];
                        for (int j = 0; j < o.Length; j++)
                        {

                            if (j > 2)
                            {
                                o[j] = oledbr.GetValue(j - 1);
                            }
                            else
                            {
                                o[j] = oledbr.GetValue(j);
                            }

                            if (j == 3)
                            {
                                o[j] = GetExceptionName(o[j].ToString());
                            }
                            else if (j == 7)
                            {
                                o[j] = GetExceptionLineStyle(o[j].ToString());
                            }
                            else if (j == 6)
                            {
                                o[j] = GetExceptionClass(o[j].ToString());
                            }
                            else if (j == 2)
                            {
                                o[j] = GetExceptionType(o[j].ToString());
                            }

                        }

                        dt_Defects.Rows.Add(o);


                    }
                    oledbr.Close();
                    odb.Close();

                    DataSource(dt_Defects, dataGridViewDefect1, "公里,超限类型,项目,超限等级，检测标准,线型 (直/缓/曲)");
                    dataGridViewDefect1.Columns["大值编号"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void tsmi_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    String cmdText = "";
                    OleDbCommand sqlcom = new OleDbCommand(cmdText, odb);

                    odb.Open();

                    foreach (DataGridViewRow dgwr in dataGridView1.SelectedRows)
                    {
                        int valid = 0; //有效无效
                        if (((ToolStripMenuItem)sender).Text == "设置有效")
                        {
                            dgwr.Cells[2].Value = "有效";
                            dgwr.DefaultCellStyle.BackColor = Color.White;
                            valid = 1;
                        }
                        else
                        {
                            dgwr.Cells[2].Value = "无效";
                            dgwr.DefaultCellStyle.BackColor = Color.LightGray;
                            valid = 0;
                        }


                        float tqiSum_Value = float.Parse(dgwr.Cells[1].Value.ToString());
                        float gongli = float.Parse(dgwr.Cells[0].Value.ToString());
                        int km = (int)gongli;
                        int meter = (int)(Math.Round(gongli, 2) * 1000 % 1000);

                        sqlcom.CommandText = String.Format("update {0} set valid={1} where TQISum_Value={2} and FromPost={3} and FromMinor={4}", sTQIName, valid, tqiSum_Value, km, meter);
                        OleDbDataReader odr = sqlcom.ExecuteReader();
                        odr.Close();

                    }

                    odb.Close();
                }

                SetTQI();
                Application.DoEvents();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region GetTQIData

        private void AddListMenuItem()
        {
            contextMenuStrip1.Items.Clear();

            ToolStripMenuItem tsmi_Valid = new ToolStripMenuItem();
            tsmi_Valid.Text = "设置有效";
            tsmi_Valid.Click += new EventHandler(tsmi_Click);
            contextMenuStrip1.Items.Add(tsmi_Valid);

            ToolStripMenuItem tsmi_Invalid = new ToolStripMenuItem();
            tsmi_Invalid.Text = "设置无效";
            tsmi_Invalid.Click += new EventHandler(tsmi_Click);
            contextMenuStrip1.Items.Add(tsmi_Invalid);

        }

        private void SetTQI()
        {
            double sum = 0.0;
            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "有效")
                {
                    sum += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    count++;
                }

            }
            textBox3.Text = (sum / count).ToString("f02");
        }

        #endregion

        #region GetDefectData

        /// <summary>
        /// 初始化内存表DataTable
        /// </summary>
        /// <param name="dt">内存表</param>
        /// <param name="tableName">表名</param>
        private void InitDataTableDefects(ref DataTable dt, String tableName)
        {
            dt = new DataTable(tableName);
            dt.Columns.Clear();
            dt.Columns.Add("公里", System.Type.GetType("System.Int32"));
            dt.Columns.Add("米", System.Type.GetType("System.Int32"));
            dt.Columns.Add("超限类型", System.Type.GetType("System.String"));
            dt.Columns.Add("项目", System.Type.GetType("System.String"));
            dt.Columns.Add("峰值", typeof(float));
            dt.Columns.Add("长度(m/g)", typeof(float));
            dt.Columns.Add("超限等级", System.Type.GetType("System.String"));
            dt.Columns.Add("线型 (直/缓/曲)", System.Type.GetType("System.String"));
            dt.Columns.Add("速度(km/h)", System.Type.GetType("System.Int32"));
            dt.Columns.Add("检测标准", System.Type.GetType("System.Int32"));
            dt.Columns.Add("大值编号", System.Type.GetType("System.Int32"));
        }


        private string GetExceptionName(string sName)
        {
            for (int i = 0; i < listETName.Count; i++)
            {
                if (listETName[i].Equals(sName))
                {
                    //返回通道名称
                    return listETChName[i];
                }
            }

            return "N";
        }

        private string GetExceptionLineStyle(string sName)
        {
            switch (sName)
            {
                case "T":
                    return "直";
                case "B":
                    return "缓";
                case "C":
                    return "圆";
                default:
                    return "N";
            }
        }

        /// <summary>
        /// 根据大值等级，查找大值等级。主要是为了转换自定义偏差
        /// </summary>
        /// <param name="sClass">大值等级</param>
        /// <returns>大值等级</returns>
        private String GetExceptionClass(string sClass)
        {
            switch (sClass)
            {
                case "1":
                    return "1";
                case "2":
                    return "2";
                case "3":
                    return "3";
                case "4":
                    return "4";
                case "21":
                    return "自定义";
                case "22":
                    return "自定义";
                case "23":
                    return "自定义";
                case "24":
                    return "自定义";
                default:
                    return "N";
            }
        }

        /// <summary>
        /// 根据大值的英文名，查找大值的类型。例如：WDGA  -- 轨距
        /// </summary>
        /// <param name="sName">大值的英文名</param>
        /// <returns>大值的类型</returns>
        private string GetExceptionType(string sName)
        {

            for (int i = 0; i < listETName.Count; i++)
            {
                if (listETName[i].Equals(sName))
                {
                    //返回大值类型
                    return listDTChName[i];
                }
            }
            return "N";
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="_DT">内存表</param>
        /// <param name="dgw">绑定的DataGridView</param>
        public void DataSource(DataTable _DT, DataGridView dgw, String sth)
        {
            if (dgw.Rows.Count > 0)
            {
                dgw.DataSource = null;
            }
            if (dgw.Columns.Count > 0)
            {
                dgw.Columns.Clear();
            }

            BindingSource source = new BindingSource();
            source.DataSource = _DT;
            foreach (DataColumn col in _DT.Columns)
            {
                if (IsTextBoxColumn(col, sth))//判断该列是否增加筛选
                {
                    DataGridViewAutoFilterTextBoxColumn commonColumn = new DataGridViewAutoFilterTextBoxColumn();
                    commonColumn.DataPropertyName = col.ColumnName;
                    commonColumn.HeaderText = col.ColumnName;
                    commonColumn.Name = col.ColumnName;
                    commonColumn.Resizable = DataGridViewTriState.True;
                    commonColumn.Width = 80;
                    commonColumn.ReadOnly = true;
                    dgw.Columns.Add(commonColumn);
                }
                else
                {
                    DataGridViewTextBoxColumn DC = new DataGridViewTextBoxColumn();
                    DC.DataPropertyName = col.ColumnName;
                    DC.HeaderText = col.ColumnName;
                    DC.Name = col.ColumnName;
                    DC.Resizable = DataGridViewTriState.True;
                    DC.Width = 54;
                    DC.ReadOnly = true;
                    dgw.Columns.Add(DC);
                }
            }
            dgw.DataSource = source;
        }

        /// <summary>
        /// 判断列名是否筛选
        /// </summary>
        /// <param name="_DataColumn">列对象</param>
        /// <returns></returns>
        public bool IsTextBoxColumn(DataColumn _DataColumn, String sth)
        {
            if (sth.IndexOf(_DataColumn.ColumnName) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region 公里扣分

        private DataTable dt_KouFen;
        private String sKouFenName = "fix_kms";
        private DataTable dt_Tmp;
        private List<Int32> listGongli = new List<Int32>();
        private void InitDataTableKouFen(ref DataTable dt, String tableName)
        {
            if (dt == null)
            {
                dt = new DataTable(tableName);
            }
            dt.Columns.Clear();
            dt.Columns.Add("公里", System.Type.GetType("System.Int32"));
            dt.Columns.Add("通过速度", System.Type.GetType("System.Int32"));
            dt.Columns.Add("公里扣分", System.Type.GetType("System.Int32"));
            dt.Columns.Add("高低", System.Type.GetType("System.Int32"));
            dt.Columns.Add("轨向", System.Type.GetType("System.Int32"));
            dt.Columns.Add("轨距", System.Type.GetType("System.Int32"));
            dt.Columns.Add("水平", System.Type.GetType("System.Int32"));
            dt.Columns.Add("三角坑", System.Type.GetType("System.Int32"));
            dt.Columns.Add("水加", System.Type.GetType("System.Int32"));
            dt.Columns.Add("垂加", System.Type.GetType("System.Int32"));
            dt.Columns.Add("70米高低", System.Type.GetType("System.Int32"));
            dt.Columns.Add("70米轨向", System.Type.GetType("System.Int32"));
            dt.Columns.Add("曲率变化率", System.Type.GetType("System.Int32"));
            dt.Columns.Add("轨距变化率", System.Type.GetType("System.Int32"));
            dt.Columns.Add("横加变化率", System.Type.GetType("System.Int32"));
            dt.Columns.Add("复合不平顺", System.Type.GetType("System.Int32"));
            dt.Columns.Add("横加", System.Type.GetType("System.Int32"));
        }
        private void GetKouFenData()
        {
            try
            {
                InitDataTableKouFen(ref dt_KouFen, sKouFenName);

                dt_Tmp = new DataTable("TempTable");

                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    odb.Open();

                    String sqlText = "SELECT maxpost,maxminor,defecttype,maxval1,length,defectclass,tbce,speedatmaxval,postedspd " +
    " FROM " + sDefectsName + " where valid<>'N' and defectclass in (1,2,3,4) order by maxpost,maxminor;";

                    OleDbDataAdapter oledba = new OleDbDataAdapter(sqlText, odb);
                    oledba.Fill(dt_Tmp);

                    oledba.Dispose();
                    odb.Close();
                }

                using (OleDbConnection odb = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    odb.Open();
                    OleDbCommand oledbCmd = new OleDbCommand("SELECT DISTINCT maxpost " + " FROM " + sDefectsName + " where valid<>'N' and defectclass in (1,2,3,4);", odb);

                    OleDbDataReader oledbDr = oledbCmd.ExecuteReader();

                    listGongli.Clear();

                    while (oledbDr.Read())
                    {
                        listGongli.Add(oledbDr.GetInt32(0));
                    }
                }

                AddDataToDataTableKouFen(ref dt_KouFen, listGongli);

                DataSource(dt_KouFen, dataGridViewKF, "");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private object[] GetKouFenByGongli(Int32 gongli)
        {
            object[] dtRow = new object[17];

            //高低
            dtRow[3] = GetKouFenDetail(gongli, "L SURFACE,R SURFACE");
            //轨向
            dtRow[4] = GetKouFenDetail(gongli, "L ALIGNMENT,R ALIGNMENT");
            //轨距
            dtRow[5] = GetKouFenDetail(gongli, "TGTGA,WDGA");
            //水平
            dtRow[6] = GetKouFenDetail(gongli, "CROSSLEVEL");
            //三角坑
            dtRow[7] = GetKouFenDetail(gongli, "TWIST");
            //水加
            dtRow[8] = GetKouFenDetail(gongli, "LAT ACCEL");
            //垂加
            dtRow[9] = GetKouFenDetail(gongli, "VERT ACCEL");
            //70米高低
            dtRow[10] = GetKouFenDetail(gongli, "L SURFACE 70M,R SURFACE 70M");
            //70米轨向
            dtRow[11] = GetKouFenDetail(gongli, "L ALIGNMENT 70M,R ALIGNMENT 70M");
            //曲率变化率
            dtRow[12] = GetKouFenDetail(gongli, "CURVATURE RATE");
            //轨距变化率
            dtRow[13] = GetKouFenDetail(gongli, "GAUGE RATE");
            //横加变化率
            dtRow[14] = GetKouFenDetail(gongli, "LAT ACCEL RATE");
            //复合不平顺
            dtRow[15] = GetKouFenDetail(gongli, "L_IRREGULAR,R_IRREGULAR");
            //带通车体横加
            dtRow[16] = GetKouFenDetail(gongli, "LATACCEL_NOCUR");

            //公里
            dtRow[0] = gongli;
            //速度
            dtRow[1] = 0;
            //公里扣分
            int sumKF = 0;
            for (int i = 3; i <= 16; i++)
            {
                sumKF += (int)dtRow[i];
            }
            dtRow[2] = sumKF;


            return dtRow;
        }

        private void AddDataToDataTableKouFen(ref DataTable dt, List<Int32> listGongli)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Clear();
            }

            foreach (Int32 gongli in listGongli)
            {
                object[] o = GetKouFenByGongli(gongli);
                dt.Rows.Add(o);
            }

        }

        private int GetKouFenDetail(int gongli, String channelTypeArr)
        {
            int koufen = 0;

            String[] strArr = channelTypeArr.Split(',');

            foreach (string channelType in strArr)
            {
                int defect_1_num = GetDefectNum(gongli, channelType, 1);
                int defect_2_num = GetDefectNum(gongli, channelType, 2);
                int defect_3_num = GetDefectNum(gongli, channelType, 3);
                int defect_4_num = GetDefectNum(gongli, channelType, 4);

                koufen += defect_1_num * 1 + defect_2_num * 5 + defect_3_num * 100 + defect_4_num * 301;
            }

            return koufen;
        }

        private int GetDefectNum(int gongli, String channel, int defectlevel)
        {
            int defectNum = 0;

            DataRow[] drArr = null;
            String filterSth = null;
            try
            {
                filterSth = String.Format("maxpost = '{0}' and defecttype = '{1}' and defectclass = '{2}'", gongli.ToString(), channel, defectlevel.ToString());
                drArr = dt_Tmp.Select(filterSth);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (drArr == null || drArr.Length == 0)
            {
                defectNum = 0;
            }
            else
            {
                defectNum = drArr.Length;
            }

            return defectNum;
        }

        #endregion

        /// <summary>
        /// 导出明细
        /// </summary>
        /// <param name="dgv">表格</param>
        /// <param name="csvFilePath">导出的文件路径</param>
        private void ExportDataFromDataGridView(DataGridView dgv, String csvFilePath)
        {
            try
            {
                StreamWriter sw = new StreamWriter(csvFilePath, false, Encoding.Default);
                StringBuilder sb = new StringBuilder();
                sb.Append("序号");
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    sb.Append("," + dgv.Columns[i].HeaderText);
                }
                sw.WriteLine(sb.ToString());
                sw.AutoFlush = true;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    sb = new StringBuilder();
                    sb.Append((i + 1).ToString());
                    for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    {
                        sb.Append("," + dgv.Rows[i].Cells[j].Value.ToString());
                    }
                    sw.WriteLine(sb.ToString());
                }

                sw.Close();
                MessageBox.Show("生成成功！");

            }
            catch (Exception ex)
            {

                MyLogger.logger.Error("导出数据失败:" + ex.Message + ",堆栈：" + ex.StackTrace + ",路径名：" + csvFilePath);
                MessageBox.Show(ex.Message);
            }
        }

        #region 偏差截图

        private void CreateIICImageTable(String iicFilePath)
        {
            //创建表格存放IIC的截图
            //表格存在，则清空表格内容；不存在，则创建表格
            List<String> tableNames = iicDataManager.GetTableNames(iicFilePath);
            if (!tableNames.Contains("IICImages"))
            {
                try
                {
                    using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                    {
                        string sqlCreate = "CREATE TABLE IICImages (" +
                            "Id integer NULL," +
                            "ExceptionId integer primary key," +
                            "IICImage image NULL," +
                            "ImageFormat varchar(255) NULL);";
                        OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                        sqlconn.Open();
                        sqlcom.ExecuteNonQuery();
                        sqlconn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("创建IIC表失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                    {
                        string sqlCreate = "DELETE * FROM IICImages ";
                        OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                        sqlconn.Open();
                        sqlcom.ExecuteNonQuery();
                        sqlconn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.logger.Error("删除IIC数据失败:" + ex.Message + ",堆栈：" + ex.StackTrace);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private Boolean InsertIntoIICImage(int imageId, int exceptionId, Bitmap bmp, String imageFormat, String iicFilePath)
        {
            Boolean retVal = true;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bmpBytes = memoryStream.ToArray();

            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    sqlconn.Open();
                    OleDbCommand com = sqlconn.CreateCommand();
                    com.CommandText = "insert into IICImages values(@Id,@ExceptionId,@IICImage,@ImageFormat)";

                    com.Parameters.AddWithValue("@Id", imageId);
                    com.Parameters.AddWithValue("@ExceptionId", exceptionId);
                    com.Parameters.AddWithValue("@IICImage", bmpBytes);
                    com.Parameters.AddWithValue("@ImageFormat", imageFormat);

                    com.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("新增偏差图片异常:" + ex.Message);
                throw new Exception("新增偏差图片异常:" + ex.Message);
                //retVal = false;
            }
            return retVal;
        }

        private List<CheckBox> checkBoxList = new List<CheckBox>();
        private String strExcptnLevel = null;
        private void InitCheckBoxList()
        {
            if (checkBoxList.Count > 0)
            {
                checkBoxList.Clear();
            }
            checkBoxList.Add(checkBox_excptnLevel1);
            checkBoxList.Add(checkBox_excptnLevel2);
            checkBoxList.Add(checkBox_excptnLevel3);
            checkBoxList.Add(checkBox_excptnLevel4);
            checkBoxList.Add(checkBox_excptnLevelDIY);
        }
        private void InitStr()
        {
            if (checkBoxList.Count == 0)
            {
                return;
            }
            strExcptnLevel = null;
            foreach (CheckBox cb in checkBoxList)
            {
                if (cb.Checked)
                {
                    strExcptnLevel += (string)(cb.Tag);
                }
            }
        }
        private DataTable dt;
        private DataTable ReadDataToDataTable()
        {
            dt = new DataTable("大值国家标准表");

            using (OleDbConnection sqlconn = new OleDbConnection(innerDBPath))
            {
                sqlconn.Open();

                String cmd = "select *  from 大值国家标准表";
                OleDbCommand sqlcom = new OleDbCommand(cmd, sqlconn);

                OleDbDataAdapter oledbDataAdapter = new OleDbDataAdapter(sqlcom);
                oledbDataAdapter.Fill(dt);
                
                sqlconn.Close();
            }

            return dt;
        }
        private float[] QueryDataTable(int excptnSpd, String excptnLevel, String excptnDTEnName)
        {
            String standardType = "0";
            if (excptnLevel == "自定义")
            {
                standardType = "1";
            }

            float[] excptnVal = new float[2];
            try
            {
                String filterStr = String.Format("SPEED={0} and CLASS={1} and TYPE='{2}' and STANDARDTYPE={3}", excptnSpd, excptnLevel, excptnDTEnName, standardType);
                DataRow[] drArray = dt.Select(filterStr);

                if (drArray.Length == 0)
                {
                    excptnVal[0] = 1000;
                    excptnVal[1] = 1000;
                }
                else
                {
                    excptnVal[0] = (float)(drArray[0]["VALUESTANDARD"]);

                    if (drArray[0]["VALUEDIY"] == (System.DBNull.Value))
                    {
                        excptnVal[1] = 1000;
                    }
                    else
                    {
                        excptnVal[1] = (float)(drArray[0]["VALUEDIY"]);
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return excptnVal;
        }


        /// <summary>
        /// 偏差截图
        /// </summary>
        /// <param name="milePos">公里</param>
        /// <param name="excptnChName">项目名称</param>
        /// <param name="excptnVal">大值</param>
        /// <param name="excptnLevel">超限等级</param>
        /// <param name="excptnSpd">线路等级</param>
        /// <param name="excptnStd">大值标准（与大值等级一一对应）</param>
        /// <param name="excptnLen">峰值宽度（单位：米）</param>
        /// <param name="channelEnName">大值对应的通道英文名</param>
        /// <returns>返回bmp图片</returns>
        private Bitmap CutPictures(float milePos, String excptnChName, float excptnVal, String excptnLevel, int excptnSpd, String excptnStd, float excptnLen, string channelEnName)
        {
            Bitmap bmp = null;
            long locationPostion = 0;
            int scroll = waveformMaker.GetLocationScrollSize(milePos*1000, ref locationPostion);
            if (scroll >= 0)
            {
                MainForm.sMainform.InvokeScroolBar_Scroll(null, scroll);
            }
            Thread.Sleep(50);
            String msg_Format = "位置：{0} 项目：{1} 等级：{2} 标准值：{3} 超限值：{4} 长度：{5} 速度等级：{6}";
            String msg = String.Format(msg_Format, milePos, excptnChName, excptnLevel, excptnStd, excptnVal, excptnLen, excptnSpd);
            bmp = waveformMaker.DrawIICDeviation(milePos, msg, channelEnName, locationPostion);

            return bmp;
        }

        /// <summary>
        /// 根据大值的中文名字，查找大值对应的通道的英文名
        /// </summary>
        /// <param name="sName">大值的中文名字</param>
        /// <returns>大值对应的通道的英文名</returns>
        private string GetExceptionChannelEnName(string sName)
        {

            for (int i = 0; i < listETChName.Count; i++)
            {
                if (listETChName[i].Equals(sName))
                {
                    //返回大值对应的通道英文名
                    return listETChannelENName[i];
                }
            }
            return "N";
        }

        /// <summary>
        /// 根据大值的中文名字，查找大值对应的类型的英文名。例如： 左高低--高低--Prof_SC
        /// </summary>
        /// <param name="sName">大值的中文名字</param>
        /// <returns>大值对应的类型的英文名</returns>
        private string GetExceptionDataTypeEnName(string sName)
        {
            for (int i = 0; i < listETChName.Count; i++)
            {
                if (listETChName[i].Equals(sName))
                {
                    //返回大值对应的类型英文名
                    return listDTEnName[i];
                }
            }
            return "N";
        }

        #endregion

        #endregion


        #region DataGridView自动在行头添加行号
        //DataGridView自动在行头添加行号
        /// <summary>
        /// DataGridView自动在行头添加行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            Color color = dgv.RowHeadersDefaultCellStyle.ForeColor;
            if (dgv.Rows[e.RowIndex].Selected)
            {
                //color = dgv.RowHeadersDefaultCellStyle.SelectionForeColor;
            }
            else
            {
                color = dgv.RowHeadersDefaultCellStyle.ForeColor;
            }

            using (SolidBrush b = new SolidBrush(color))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 6);
            }
        }
        #endregion

        private void btnAccumulateCurveChartSetting_Click(object sender, EventArgs e)
        {
            ChartConfigForm configForm = new ChartConfigForm(chartAccumulateCurveData);
            configForm.ShowDialog();
            if (configForm.Tag != null)
            {
                chartAccumulateCurveData = configForm.Tag as ChartConfig;
                CreateAccumulateCurve();
            }
        }

        private void btnMileDistributeChartSetting_Click(object sender, EventArgs e)
        {
            ChartConfigForm configForm = new ChartConfigForm(chartMilDistributeData);
            configForm.ShowDialog();
            if (configForm.Tag != null)
            {
                chartMilDistributeData = configForm.Tag as ChartConfig;
                CreateMileDistribute();
            }
        }

        private void btnSpeedCurVeChartSetting_Click(object sender, EventArgs e)
        {
            ChartConfigForm configForm = new ChartConfigForm(chartSpeedCurveData);
            configForm.ShowDialog();
            if (configForm.Tag != null)
            {
                chartSpeedCurveData = configForm.Tag as ChartConfig;
                CreateSpeedCurVe();
            }
        }
    }
}
