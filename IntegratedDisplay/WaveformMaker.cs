/// -------------------------------------------------------------------------------------------
/// FileName：WaveformMaker.cs
/// 说    明：波形生成类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：jinxl
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegratedDisplay.Models;
using CitFileSDK;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;

namespace IntegratedDisplay
{
    public class WaveformMaker
    {
        /// <summary>
        /// 通道拖动方式
        /// </summary>
        public enum DragMode
        {
            /// <summary>
            /// 单通道拖动
            /// </summary>
            SingleDarg,
            /// <summary>
            /// 同名通道拖动
            /// </summary>
            SameNameDarg,
            /// <summary>
            /// 同基线拖动
            /// </summary>
            SameBaselineDarg,
        }

        #region 所有的私有变量



        /// <summary>
        /// 波形数据的集合，用于所有的波形显示
        /// </summary>
        private List<WavefromData> _waveformDataList;

        /// <summary>
        /// 台帐信息集合
        /// </summary>
        private List<AccountDatabase> _accountDatabaseList;

        /// <summary>
        /// 拖动时所选择的通道
        /// </summary>
        private List<WaveformDragData> _selectedDragItems = new List<WaveformDragData>();

        /// <summary>
        /// 控件的宽度
        /// </summary>
        private int _controlWidth = 800;
        /// <summary>
        /// 控件的高度
        /// </summary>
        private int _controlHeight = 600;
        /// <summary>
        /// 单个波形信息展示的宽度(波形右侧)
        /// </summary>
        private int _singleChannelInfoWidth = 160;
        /// <summary>
        /// 单个波形信息展示的高度(波形右侧)
        /// </summary>
        private int _singleChannelInfoHeight = 36;

        /// <summary>
        /// 台帐的高度
        /// </summary>
        private int _accountInfoHeight = 0;

        /// <summary>
        /// 单个波形通道的Y坐标
        /// </summary>
        private int _waveformCoordinatesY = 0;

        /// <summary>
        /// 里程显示宽度（波形上面显示的里程信息宽）
        /// </summary>
        private int _mileageInfoWidth = 0;

        /// <summary>
        /// 里程显示高度（波形上面显示的里程信息高）
        /// </summary>
        private int _mileageInfoHeight = 30;

        /// <summary>
        /// X轴显示的数据点数
        /// </summary>
        private int _zoomInSize = 0;

        /// <summary>
        /// Y的放大比例
        /// </summary>
        private int _zoomInY = 0;
        /// <summary>
        /// 通道信息字体
        /// </summary>
        private Font _coordiantesFont = new Font("宋体", 10, FontStyle.Regular);

        /// <summary>
        /// 字体名称
        /// </summary>
        private string _fontName = "宋体";

        /// <summary>
        /// 波形是否处于放大模式
        /// </summary>
        private bool _isZoomInView = false;
       
        /// <summary>
        /// 游标的位置
        /// </summary>
        private int _caliperX = 0;
        /// <summary>
        /// 游标的所在位置的索引
        /// </summary>
        private int _caliperIndex = 0;

        //绘制虚线的笔
        public Pen DashedPen = new Pen(Color.FromArgb(192, 192, 192));

        /// <summary>
        /// 放大时绘制的虚框
        /// </summary>
        private RectangleF _zoomInRect = RectangleF.Empty;

        /// <summary>
        /// 游标尺位置
        /// </summary>
        private Rectangle _caliperRect = Rectangle.Empty;

        /// <summary>
        /// 显示波形的点集合
        /// </summary>
        PointF[] waveformPoints = null;
        /// <summary>
        /// 当前显示的所有波形集合，波形索引，通道索引，点集合
        /// </summary>
        private Dictionary<int, Dictionary<int, PointF[]>> _currentChannelAndPoint = new Dictionary<int, Dictionary<int, PointF[]>>();

        /// <summary>
        /// 所有的标记数据
        /// </summary>
        //private List<LabelInfo> _labelInfoList = new List<LabelInfo>();
        #endregion

        /// <summary>
        /// 整个控件的宽度
        /// </summary>
        public int ControlWidth
        {
            get
            {
                return _controlWidth;
            }

            set
            {
                _controlWidth = value;
            }
        }

        /// <summary>
        /// 整个控件的高度
        /// </summary>
        public int ControlHeight
        {
            get
            {
                return _controlHeight;
            }

            set
            {
                _controlHeight = value;
            }
        }

        /// <summary>
        /// 波形信息显示的宽度
        /// </summary>
        public int SingleChannelInfoWidth
        {
            
            get
            {
                return _singleChannelInfoWidth;
            }

            set
            {
                _singleChannelInfoWidth = value;
            }
        }

        /// <summary>
        /// 波形信息显示的高度
        /// </summary>
        public int SingleChannelInfoHeight
        {
            
            get
            {
                return _singleChannelInfoHeight;
            }

            set
            {
                _singleChannelInfoHeight = value;
            }
        }

        /// <summary>
        /// 波形数据列表，只读
        /// </summary>
        public List<WavefromData> WaveformDataList
        {
            get
            {
                return _waveformDataList;
            }
        }

        /// <summary>
        /// 里程信息显示框的宽度
        /// </summary>
        public int MileageInfoWidth
        {
            get
            {
                return _mileageInfoWidth;
            }

            set
            {
                _mileageInfoWidth = value;
            }
        }

        /// <summary>
        /// 里程信息显示框的高度
        /// </summary>
        public int MileageInfoHeight
        {
            get
            {
                return _mileageInfoHeight;
            }

            set
            {
                _mileageInfoHeight = value;
            }
        }

        /// <summary>
        /// Y轴的放大比例
        /// </summary>
        public int ZoomInY
        {
            get
            {
                return _zoomInY;
            }

            set
            {
                _zoomInY = value;
            }
        }

        /// <summary>
        /// 波形通道坐标的Y
        /// </summary>
        public int WaveformCoordinatesY
        {
            get
            {
                return _waveformCoordinatesY;
            }

            set
            {
                _waveformCoordinatesY = value;
            }
        }

        /// <summary>
        /// X轴显示的数据点数
        /// </summary>
        public int ZoomInSize
        {
            get
            {
                return _zoomInSize;
            }

            set
            {
                _zoomInSize = value;
                foreach (var item in WaveformDataList)
                {
                    item.ZoomInSize = _zoomInSize;
                }
            }
        }

        /// <summary>
        /// 是否处于放大显示
        /// </summary>
        public bool IsZoomInView
        {
            get
            {
                return _isZoomInView;
            }

            set
            {
                _isZoomInView = value;
            }
        }

        /// <summary>
        /// 通道的拖动方式
        /// </summary>
        public DragMode ChannelDargMode { get; set; }

        /// <summary>
        /// 所有的台帐信息
        /// </summary>
        public List<AccountDatabase> AccountDatabaseList
        {
            get
            {
                return _accountDatabaseList;
            }
        }

        /// <summary>
        /// 台帐的高度
        /// </summary>
        public int AccountInfoHeight
        {
            get
            {
                return _accountInfoHeight;
            }

            set
            {
                _accountInfoHeight = value;
            }
        }

        /// <summary>
        /// 显示文字的字体名称
        /// </summary>
        public string FontName
        {
            get
            {
                return _fontName;
            }

            set
            {
                _fontName = value;
            }
        }
        /// <summary>
        /// 选中时是否高亮
        /// </summary>
        public bool IsSelectedHighlight { get; set; }
        
        /// <summary>
        /// 是否已选择了通道
        /// </summary>
        public bool IsSelectedChannel;
        /// <summary>
        /// 是否选中了游标
        /// </summary>
        public bool IsSelectedCaliper;

        /// <summary>
        /// 绘制单个里程线的颜色
        /// </summary>
        public Pen meterPen = new Pen(Color.Black);

        /// <summary>
        /// 绘制多个里程线的颜色
        /// </summary>
        public Pen meterLinePen = new Pen(Color.Silver);

        /// <summary>
        /// 通道字符串格式
        /// </summary>
        public StringFormat ChannelstringFormat = new StringFormat();
        /// <summary>
        /// 高度因子，纠正算通道Y值坐标时高度过大
        /// </summary>
        private const int HighFactor = 80;

        /// <summary>
        /// 放大时X轴左边的值
        /// </summary>
        private int _zoomInLeftX = 0;

        /// <summary>
        /// 放大时X轴右边的值
        /// </summary>
        private int _zoomInRightX = 0;

        /// <summary>
        /// 最后选择的波形索引
        /// </summary>
        private WaveformDragData _lastDragData = new WaveformDragData();

        /// <summary>
        /// 波形线宽
        /// </summary>
        private Dictionary<int,List<float>> _waveLineWidthDic = new Dictionary<int, List<float>>();

        /// <summary>
        /// 里程显示颜色
        /// </summary>
        public List<int> MileageColor { get; private set; }

        public int CaliperX
        {
            get
            {
                return _caliperX;
            }

            set
            {
                _caliperX = value;
                _caliperIndex = GetLocationIndex(_caliperX);
            }
        }

        /// <summary>
        /// 初始化波形制作类
        /// </summary>
        /// <param name="controlWidth">整个绘图区的宽度</param>
        /// <param name="controlHeight">整个绘图区的高度</param>
        /// <param name="waveformInfoWidth">单个波形通道信息显示的宽度</param>
        /// <param name="waveformInfoHeight">单个波形通道信息显示的高度</param>
        /// <param name="mileageInfoHeight">里程显示区域的高度,宽度与波形显示区域同等</param>
        /// <param name="accountInfoHeight">台帐的高度，宽度与波形同等</param>
        public WaveformMaker(int controlWidth, int controlHeight, int waveformInfoWidth, int waveformInfoHeight, int mileageInfoHeight, int accountInfoHeight)
        {
            _controlWidth = controlWidth;
            _controlHeight = controlHeight;
            _singleChannelInfoHeight = waveformInfoHeight;
            _singleChannelInfoWidth = waveformInfoWidth;
            _waveformDataList = new List<WavefromData>();
            _accountDatabaseList = new List<AccountDatabase>();
            _accountDatabaseList = new List<AccountDatabase>();
            
            _mileageInfoHeight = mileageInfoHeight;
            _mileageInfoWidth = _controlWidth - _singleChannelInfoWidth;
            ReCalculateDrawSize();
            _accountInfoHeight = accountInfoHeight;
            meterPen.Width = 1.0f;
            meterLinePen.Width = 1.0f;
            IsZoomInView = false;
            IsSelectedChannel = false;

            DashedPen.DashStyle = DashStyle.Custom;
            DashedPen.Width = 0.5f;
            //绘制虚线的样式，数组代表短划线和空白区域的长度
            DashedPen.DashPattern = new float[] { 3, 3 };
            IsSelectedHighlight = true;
            MileageColor = new List<int>() { -16776961, -16744448, -16777216, -8355712, -8355712, -8355712, -8355712, -8355712, -8355712, -8355712 };
        }

        /// <summary>
        /// 添加一个波形数据到列表中
        /// </summary>
        /// <param name="waveDisplayData">波形数据类实体</param>
        public void AddWaveformData(WavefromData waveDisplayData)
        {
            waveDisplayData.InitWaveformData();
            for (int i = 0; i < waveDisplayData.ChannelList.Count; i++)
            {
                waveDisplayData.ChannelList[i].DisplayRect = new Rectangle(_controlWidth - _singleChannelInfoWidth,
                    _mileageInfoHeight + 2 + waveDisplayData.ChannelList[i].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                    _singleChannelInfoWidth,
                    _singleChannelInfoHeight);

                waveDisplayData.ChannelList[i].ColorRect = new Rectangle(_controlWidth - _singleChannelInfoWidth + 140,
                    _mileageInfoHeight + 2 + waveDisplayData.ChannelList[i].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                    _singleChannelInfoWidth,
                    _singleChannelInfoHeight / 2);

                waveDisplayData.ChannelList[i].DragRect = new Rectangle(_controlWidth - _singleChannelInfoWidth,
                   _mileageInfoHeight + 2 + waveDisplayData.ChannelList[i].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                   _singleChannelInfoWidth,
                   _singleChannelInfoHeight / 2);


                waveDisplayData.ChannelList[i].IsShowRect = true;
                waveDisplayData.ChannelList[i].IsZoomInView = true;
                //List<float> lineWdith= waveDisplayData.ChannelList.Select(p => p.LineWidth).ToList();
                //_waveLineWidthDic.Add(i, lineWdith);
            }
            //加载图层大于1时，禁用后面图层的通道显示
            if (WaveformDataList.Count > 0)
            {
                waveDisplayData.LayerConfig.IsMileageLabelVisible = false;
            }
            else
            {
                waveDisplayData.LayerConfig.IsMileageLabelVisible = true;
            }
            WaveformDataList.Add(waveDisplayData);
        }

        /// <summary>
        /// 将一个波形数据移除
        /// </summary>
        /// <param name="waveDisplayData">波形数据实体</param>
        public void RemoveWaveformData(WavefromData waveDisplayData)
        {
            WaveformDataList.Remove(waveDisplayData);
        }

        /// <summary>
        /// 移除所有数据
        /// </summary>
        public void RemoveAllData()
        {
            WaveformDataList.Clear();
        }

        /// <summary>
        /// 从列表中移除指定索引
        /// </summary>
        /// <param name="index">指定波形数据索引</param>
        public void RemoveWaveformData(int index)
        {
            if (index < WaveformDataList.Count)
            {
                WaveformDataList.RemoveAt(index);
            }
            foreach (var item in WaveformDataList)
            {
                item.InitWaveformData();
            }
        }

        /// <summary>
        /// 根据滚动值获取指定数据,并刷新通道框
        /// </summary>
        /// <param name="scrollValue">滚动值，不能大于最大滚动值</param>
        public void GetWaveformData(int scrollValue)
        {
            foreach (var waveDisplayData in WaveformDataList)
            {
                int sampleNum = GetScrollValueToSampleNum(scrollValue);
                long postion= waveDisplayData.GetAppointEndPostion(sampleNum);
                
                waveDisplayData.GetWaveformData(postion);
            }
        }

        /// <summary>
        /// 将滚动值转换为采样点，同时乘以屏幕换算比
        /// </summary>
        /// <param name="sccrollVaule"></param>
        /// <returns></returns>
        public int GetScrollValueToSampleNum(int sccrollVaule)
        {
            return sccrollVaule * 4 * (ZoomInSize / 10);
        }

        /// <summary>
        /// 将采样点换算为米，同时除以屏幕换算比
        /// </summary>
        /// <param name="samplNum"></param>
        /// <returns></returns>
        public int GetSampleNumToScrollValue(long samplNum)
        {
            return (int)(samplNum / 4 / (ZoomInSize / 10));
        }

        public int GetLocationScrollSize(float mileage, ref long locationPostion)
        {
            if (WaveformDataList != null && WaveformDataList.Count > 0)
            {
                long sampleCount = _waveformDataList[0].GetLocationSampleCount(mileage, ref locationPostion);
                if (sampleCount < 0)
                {
                    return -1;
                }
                int scorllValue = GetSampleNumToScrollValue(sampleCount);
                return scorllValue;
            }
            return 0;
        }

        /// <summary>
        /// 获取最大滚动值
        /// </summary>
        /// <returns>最大滚动值</returns>
        public int GetMaxScorllSize()
        {
            int size = 0;
            long totalSampleCount = 0;
            if (WaveformDataList != null && WaveformDataList.Count > 0)
            {
                
                totalSampleCount = _waveformDataList[0].GetTotalSampleCount();
                size = (int)totalSampleCount / 4 / (ZoomInSize / 10);

            }
            return size;
        }

        public int GetLocationScrollSize(long locationPostion)
        {
            if (WaveformDataList != null && WaveformDataList.Count > 0)
            {
                long sampleCount= _waveformDataList[0].GetLocationSampleCount(locationPostion);
                int scorllValue = GetSampleNumToScrollValue(sampleCount);               
                return scorllValue;
            }
            return 0;
        }

        /// <summary>
        /// 获取波形图以及里程刻度
        /// </summary>
        /// <returns>包含波形的内存图片</returns>
        public Bitmap DrawWavefrom()
        {
            Bitmap waveformBitmap = new Bitmap(ControlWidth, ControlHeight);
            Graphics grapWaveform = Graphics.FromImage(waveformBitmap);
            _currentChannelAndPoint.Clear();
            #region 画波形
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                WavefromData waveDisplay = WaveformDataList[i];
                Dictionary<int, PointF[]> dicAllPoint = new Dictionary<int, PointF[]>();
                for (int j = 0; j < waveDisplay.ChannelList.Count; j++)
                {

                    ChannelsClass channel = waveDisplay.ChannelList[j];
                    if (channel.IsVisible == false
                        || waveDisplay.LayerConfig.IsVisible == false
                        || channel.IsZoomInView == false
                        || channel.Data == null
                        || channel.Data.Length <= 0)
                    {
                        continue;
                    }
                    waveformPoints = new PointF[waveDisplay.WaveformDataCount];
                    for (int k = 0; k < waveformPoints.LongLength; k++)
                    {
                        waveformPoints[k].Y = channel.DisplayRect.Top + MileageInfoHeight
                            + channel.DisplayRect.Height / 2 - (float)channel.Data[k] * (_waveformCoordinatesY / channel.ZoomIn * (channel.IsReverse ? -1 : 1));

                        waveformPoints[k].X = k * (_controlWidth - _singleChannelInfoWidth) / waveDisplay.WaveformDataCount;


                    }
                    dicAllPoint.Add(j, waveformPoints);
                    float lineWidth = 1;
                    if (_waveLineWidthDic.ContainsKey(i))
                    {
                        if (_waveLineWidthDic[i].Count > j)
                        {
                            lineWidth = _waveLineWidthDic[i][j];
                        }
                    }

                    grapWaveform.DrawLines(new Pen(Color.FromArgb(channel.Color), lineWidth), waveformPoints);

                }
                _currentChannelAndPoint.Add(i, dicAllPoint);
            }
            #endregion

            #region 绘制坐标刻度
            Font font = new Font("宋体", 10, FontStyle.Regular);
            Pen silverPen = new Pen(Color.Silver, 1f);
            for (int i = 0; i < 2; i++)
            {
                float v = 1.0f;
                v = v + i * 8 * (ControlWidth - SingleChannelInfoWidth) / 8f - i * 3;
                float dSum = MileageInfoHeight;
                while (dSum < ControlHeight)
                {
                    dSum += WaveformCoordinatesY;
                    grapWaveform.DrawLine(silverPen, new PointF(v - 1, dSum), new PointF(v + 1, dSum));
                }
            }
            #endregion

            #region 绘制边框和里程
            //画分割线
            grapWaveform.DrawLine(new Pen(Color.Black), (ControlWidth - SingleChannelInfoWidth), 0,
                (ControlWidth - _singleChannelInfoWidth), waveformBitmap.Height);
            //画横向分割线
            grapWaveform.DrawLine(new Pen(Color.Black), 0, MileageInfoHeight,
                waveformBitmap.Width, MileageInfoHeight);
            #endregion

            StringFormat meterFormat = new StringFormat();
            meterFormat.Alignment = StringAlignment.Far;
            meterFormat.LineAlignment = StringAlignment.Near;
            if (WaveformDataList.Count > 0 && WaveformDataList[0].MileList.milestoneList.Count > 0)
            {
                string sStartMeter = "";
                string sEndMeter = "";
                sStartMeter = "K" + WaveformDataList[0].MileList.milestoneList[0].mKm + "+" + (WaveformDataList[0].MileList.milestoneList[0].mMeter).ToString("f3");
                sEndMeter = "K" + WaveformDataList[0].MileList.milestoneList[WaveformDataList[0].MileList.milestoneList.Count - 1].mKm + "+" + (WaveformDataList[0].MileList.milestoneList[WaveformDataList[0].MileList.milestoneList.Count - 1].mMeter).ToString("f3");
                //绘制右上角的那两个里程数字
                grapWaveform.DrawString(sStartMeter + "\n" + sEndMeter, font, new SolidBrush(Color.Black), new RectangleF(_mileageInfoWidth, 0, _singleChannelInfoWidth, _mileageInfoHeight), meterFormat);
            }
            DrawMileageData(grapWaveform, font);

            grapWaveform.DrawImage(waveformBitmap, new Point(0, 0));
            return waveformBitmap;
        }

        /// <summary>
        /// 绘制右上角和动态的里程数据
        /// </summary>
        /// <param name="grapWaveform">绘图对象</param>
        /// <param name="font">字体</param>
        private void DrawMileageData(Graphics grapWaveform, Font font)
        {
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                WavefromData waveDisplay = WaveformDataList[i];
                if (!waveDisplay.LayerConfig.IsVisible || !waveDisplay.LayerConfig.IsMileageLabelVisible)
                {
                    continue;
                }
                float lastIntegerMileage = 0;

                int milestoneListCount = waveDisplay.MileList.milestoneList.Count;
                for (int j = 0; j < milestoneListCount; j++)
                {

                    if (waveDisplay.IsLoadIndex)
                    {
                        #region 里程修正下，里程值可能不会有整数的，需要重新计算整数坐标
                        //判断是否里程，如果米数小于1，可以认为整体是一个整数里程
                        if (waveDisplay.MileList.milestoneList[j].mKm != lastIntegerMileage && waveDisplay.MileList.milestoneList[j].mMeter < 1)
                        {
                            lastIntegerMileage = waveDisplay.MileList.milestoneList[j].mKm;
                            float pointX = (float)(j * (_controlWidth - _singleChannelInfoWidth) / 1.0 / waveDisplay.MileList.milestoneList.Count);
                            float pointCalcu = 0;
                            if (j != 0)
                            {
                                float pointLast = (float)((j - 1) * (_controlWidth - _singleChannelInfoWidth) / 1.0 / waveDisplay.MileList.milestoneList.Count);
                                float pointDiff = pointX - pointLast;
                                float mileageDiff = waveDisplay.MileList.milestoneList[j].GetMeter() - lastIntegerMileage * 1000;
                                pointCalcu = pointX - (int)(mileageDiff * pointDiff / (waveDisplay.MileList.milestoneList[j].GetMeter() - waveDisplay.MileList.milestoneList[j - 1].GetMeter()));
                            }
                            else
                            {
                                pointCalcu = pointX;
                            }
                            grapWaveform.DrawLine(meterPen, new PointF(pointCalcu, _mileageInfoHeight), new PointF(pointCalcu, _controlHeight));

                            grapWaveform.DrawString("K" + waveDisplay.MileList.milestoneList[j].mKm + "+0", font, new SolidBrush(Color.FromArgb(MileageColor[i])), new PointF(pointCalcu, 15));
                            j = j + 10;
                        }//大于1小于1.5，开始计算整数线偏移值
                        else if (waveDisplay.MileList.milestoneList[j].mMeter % (waveDisplay.ZoomInSize == 2000 ? 500 : 100) <= 1.5 && waveDisplay.MileList.milestoneList[j].GetMeter() != 0)
                        {
                            bool isRight = true;
                            float pointCalcu = 0;
                            float currentMileage = waveDisplay.MileList.milestoneList[j].mMeter;
                            int intergerMileage = (int)currentMileage;

                            float pointX = (float)(j * (_controlWidth - _singleChannelInfoWidth) / 1.0 / waveDisplay.MileList.milestoneList.Count);
                            if (waveDisplay.MileList.milestoneList[j].mMeter % (waveDisplay.ZoomInSize == 2000 ? 500 : 100) == 0)
                            {
                                pointCalcu = pointX;
                            }
                            else
                            {
                                if (j != 0)
                                {

                                    float lastPointX = (float)((j - 1) * (_controlWidth - _singleChannelInfoWidth) / 1.0 / waveDisplay.MileList.milestoneList.Count);

                                    float lastMileage = waveDisplay.MileList.milestoneList[j - 1].GetMeter();
                                    if (lastMileage == currentMileage)
                                    {
                                        isRight = false;
                                    }
                                    else
                                    {
                                        pointCalcu = lastPointX + ((intergerMileage - lastMileage) * (pointX - lastPointX) / (currentMileage - lastMileage));
                                    }
                                }
                                else
                                {
                                    pointCalcu = pointX;
                                }
                            }
                            if (isRight)
                            {
                                grapWaveform.DrawString("K" + waveDisplay.MileList.milestoneList[j].mKm + "+" + (int)(waveDisplay.MileList.milestoneList[j].mMeter), font, new SolidBrush(Color.FromArgb(MileageColor[i])), new PointF(pointCalcu, 15));
                                grapWaveform.DrawLine(meterLinePen, new PointF(pointCalcu, _mileageInfoHeight), new PointF(pointCalcu, _controlHeight));
                            }
                            j = j + 10;
                        }
                        #endregion
                    }
                    else
                    {
                        if ((waveDisplay.MileList.milestoneList[j].mMeter % (waveDisplay.ZoomInSize == 2000 ? 500 : 100)) == 0 && waveDisplay.MileList.milestoneList[j].GetMeter() != 0)
                        {
                            if (waveDisplay.MileList.milestoneList[j].GetMeter() < 0)
                            {
                                continue;
                            }
                            else
                            {
                                int pointX = (int)Math.Ceiling(j * (_controlWidth - _singleChannelInfoWidth) / 1.0 / waveDisplay.MileList.milestoneList.Count);
                                grapWaveform.DrawString("K" + waveDisplay.MileList.milestoneList[j].mKm + "+" + (waveDisplay.MileList.milestoneList[j].mMeter), font, new SolidBrush(Color.FromArgb(MileageColor[i])), new PointF(pointX, 15));
                                if (waveDisplay.MileList.milestoneList[j].GetMeter() % 1000 == 0 && waveDisplay.MileList.milestoneList[j].GetMeter() != 0)
                                {
                                    grapWaveform.DrawLine(meterPen, new PointF(pointX, _mileageInfoHeight), new PointF(pointX, _controlHeight));
                                }
                                else
                                {
                                    grapWaveform.DrawLine(meterLinePen, new PointF(pointX, _mileageInfoHeight), new PointF(pointX, _controlHeight));
                                }
                                j = j + 10;
                            }
                        }
                    }


                }
            }
        }

        /// <summary>
        /// 获取通道基线图形
        /// </summary>
        /// <returns>包含通道基线的内存图片</returns>
        public Bitmap DrawChannelsBaselines()
        {
            Bitmap channelBaselineBitmap = new Bitmap(_controlWidth, _controlHeight - MileageInfoHeight);
            channelBaselineBitmap.MakeTransparent(Color.White);
            Graphics gChannelBaseline = Graphics.FromImage(channelBaselineBitmap);

            _waveLineWidthDic.Clear();
            
            ChannelstringFormat.LineAlignment = StringAlignment.Center;
            //逆序，为了让第一层波形通道数据显示在最上面
            for (int i = WaveformDataList.Count - 1; i >= 0; i--)
            {
                WavefromData waveDisplay = WaveformDataList[i];
                List<float> lineWdith = waveDisplay.ChannelList.Select(p => p.LineWidth).ToList();
                _waveLineWidthDic.Add(i, lineWdith);
                if (waveDisplay.LayerConfig.IsVisible == false || waveDisplay.LayerConfig.IsChannelLabelVisible == false)
                {
                    continue;
                }
                for (int j = 0; j < waveDisplay.ChannelList.Count; j++)
                {
                    ChannelsClass channel = waveDisplay.ChannelList[j];

                    if (channel.IsVisible == false)
                    {
                        continue;
                    }
                    int iPos = 0;
                    if (channel.Location < ZoomInY)
                    {
                        iPos = -100;
                    }
                    else
                    {
                        iPos = (channel.Location - ZoomInY) * _waveformCoordinatesY - _singleChannelInfoHeight / 2;
                    }

                    channel.DisplayRect = new Rectangle(
                        channel.DisplayRect.X,
                        iPos,
                        channel.DisplayRect.Width,
                        channel.DisplayRect.Height);
                    channel.DragRect = new Rectangle(
                        channel.DragRect.X,
                        iPos + channel.DisplayRect.Height / 4,
                        channel.DragRect.Width,
                        channel.DragRect.Height);
                    channel.ColorRect = new Rectangle(
                        channel.ColorRect.X,
                        iPos + channel.DisplayRect.Height / 4,
                        channel.ColorRect.Width,
                        channel.ColorRect.Height);

                    DrawSingleChannel(gChannelBaseline, channel, Color.WhiteSmoke);

                }

            }
            //绘制高亮底色
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                for (int j = 0; j < WaveformDataList[i].ChannelList.Count; j++)
                {
                    if (_lastDragData.SelectDragDataIndex == i && _lastDragData.SelectDragChannel == j)
                    {
                        DrawSingleChannel(gChannelBaseline, WaveformDataList[i].ChannelList[j], Color.CornflowerBlue);
                        if (IsSelectedHighlight)
                        {
                            float width = _waveformDataList[i].ChannelList[j].LineWidth;
                            if (_waveLineWidthDic.ContainsKey(i))
                            {
                                if (_waveLineWidthDic[i].Count > j)
                                {
                                    _waveLineWidthDic[i][j] = width * 2;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            gChannelBaseline.Dispose();
            gChannelBaseline = null;
            return channelBaselineBitmap;
        }

        /// <summary>
        /// 绘制无效数据
        /// </summary>
        /// <returns></returns>
        public Bitmap DrawInvaildData()
        {
            Bitmap invaildDataBitmap = new Bitmap(_controlWidth, _controlHeight);
            invaildDataBitmap.MakeTransparent(Color.White);
            Graphics gInvaildData = Graphics.FromImage(invaildDataBitmap);
            if (_waveformDataList[0].InvalidDataList!=null
                && _waveformDataList[0].InvalidDataList.Count > 0)
            {
                for (int i = 0; i < _waveformDataList[0].InvalidDataList.Count; i++)
                {
                    int invaildStartIndex = -1;
                    int InvaildEndIndex = -1;
                    bool isFindStart = false;
                    bool isFindEnd = false;
                    InvalidData invaildData = _waveformDataList[0].InvalidDataList[i];

                    if (long.Parse(invaildData.sStartPoint) > _waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].mFilePosition)
                    {
                        continue;
                    }
                    if (long.Parse(invaildData.sEndPoint) < _waveformDataList[0].MileList.milestoneList[0].mFilePosition)
                    {
                        continue;
                    }

                    for (int k = 0; k < _waveformDataList[0].MileList.milestoneList.Count; k++)
                    {
                        if (!isFindStart && invaildData.sStartPoint == _waveformDataList[0].MileList.milestoneList[k].mFilePosition.ToString())
                        {
                            invaildStartIndex = k;
                            isFindStart = true;
                        }
                        if (!isFindEnd && invaildData.sEndPoint == _waveformDataList[0].MileList.milestoneList[k].mFilePosition.ToString())
                        {
                            InvaildEndIndex = k;
                            isFindEnd = true;
                        }
                        if (isFindStart && isFindEnd)
                        {
                            break;
                        }
                    }

                    if (invaildStartIndex != -1 && InvaildEndIndex == -1)
                    {
                        InvaildEndIndex =  _waveformDataList[0].MileList.milestoneList.Count;

                    }
                    if (invaildStartIndex == -1 && InvaildEndIndex != -1)
                    {
                        invaildStartIndex = 0;
                    }
                    if (invaildStartIndex == -1 && InvaildEndIndex == -1)
                    {
                        invaildStartIndex = 0;
                        InvaildEndIndex = _waveformDataList[0].MileList.milestoneList.Count;
                    }
                    int pointCount = _waveformDataList[0].MileList.milestoneList.Count;
                    int invialdStart = invaildStartIndex * (ControlWidth - SingleChannelInfoWidth) / pointCount;
                    int invialdEnd = InvaildEndIndex * (ControlWidth - SingleChannelInfoWidth) / pointCount;
                    gInvaildData.FillRectangle(new SolidBrush(Color.Gainsboro), new Rectangle(invialdStart, MileageInfoHeight, (invialdEnd - invialdStart), (ControlWidth - MileageInfoHeight)));
                   
                }
               
            }
            gInvaildData.Dispose();
            gInvaildData = null;
            return invaildDataBitmap;
        }

        /// <summary>
        /// 绘制单个通道
        /// </summary>
        /// <param name="gChannelBaseline">图布</param>
        /// <param name="channel">要绘制的通道</param>
        /// <param name="SelectChannelColor">绘制指定的背景色</param>
        private void DrawSingleChannel(Graphics gChannelBaseline, ChannelsClass channel, Color SelectChannelColor)
        {
            Color channelColor = Color.FromArgb(channel.Color);
            //绘制拖动框的颜色
            gChannelBaseline.FillRectangle(new SolidBrush(SelectChannelColor), channel.DragRect);

            //绘制同波形颜色相同的通道框
            gChannelBaseline.FillRectangle(new SolidBrush(channelColor), channel.ColorRect);
            //绘制通道上下的边框
            gChannelBaseline.DrawLine(new Pen(Color.Black), channel.DragRect.X, channel.DragRect.Y, channel.DragRect.X + channel.DragRect.Width, channel.DragRect.Y);
            gChannelBaseline.DrawLine(new Pen(Color.Black), channel.DragRect.X, channel.DragRect.Y + channel.DragRect.Height, channel.DragRect.X + channel.DragRect.Width, channel.DragRect.Y + channel.DragRect.Height);

            //画右边区域的通道说明字符
            if (channel.IsShowRect)
            {
                gChannelBaseline.DrawString(channel.ChineseName + "(" + channel.ZoomIn + channel.Units + ")",
                  new Font("宋体", 9.0f, FontStyle.Regular),
                  new SolidBrush(Color.Black),
                  new RectangleF(channel.DisplayRect.Left,
                     channel.DisplayRect.Top + channel.DisplayRect.Height / 4,
                      channel.DisplayRect.Width,
                     channel.DisplayRect.Height / 2),
                  ChannelstringFormat);
            }
            //画虚线
            gChannelBaseline.DrawLine(DashedPen,
                new Point(0, channel.DisplayRect.Top + channel.DisplayRect.Height / 2),
                new Point(ControlWidth - _singleChannelInfoWidth,
               channel.DisplayRect.Top + channel.DisplayRect.Height / 2));
        }

        /// <summary>
        /// 根据位置点进行里程定位
        /// </summary>
        /// <param name="locationPostion">位置点</param>
        /// <returns>包含里程定位的内存图片</returns>
        public Bitmap DrawMileageLocation(long locationPostion)
        {
            Bitmap locationBitmap = new Bitmap(ControlWidth, ControlHeight);
            Graphics grapLocation = Graphics.FromImage(locationBitmap);
            int index = 0;
            int sum = WaveformDataList[0].MileList.milestoneList.Count;
            for (int i = 0; i < sum; i++)
            {
                if (WaveformDataList[0].MileList.milestoneList[i].mFilePosition == locationPostion)
                {
                    index = i;
                    break;
                }
            }
            if (index != 0)
            {
                
                int pointX = (index - sum / 200) * (_controlWidth - _singleChannelInfoWidth) / sum;
                int width = (_controlWidth - _singleChannelInfoWidth) / 100;
                SolidBrush locationBrush = new SolidBrush(Color.FromArgb(100, 255, 125, 255));
                grapLocation.FillRectangle(locationBrush, new Rectangle(pointX,
                  _mileageInfoHeight,
                  width,
                  (_controlHeight - _mileageInfoHeight)));
            }
            grapLocation.Dispose();
            grapLocation = null;
            return locationBitmap;
        }
        /// <summary>
        /// 绘制标签
        /// </summary>
        /// <returns></returns>
        public Bitmap DrawTagging(int size)
        {
            if(_waveformDataList.Count>0)
            {
                if (_waveformDataList[0].LabelInfoList != null
                    && _waveformDataList[0].LabelInfoList.Count > 0)
                {
                    Bitmap taggingBitmap = new Bitmap(ControlWidth, ControlHeight);
                    Graphics grapTagging = Graphics.FromImage(taggingBitmap);
                    List<int> index = new List<int>();
                    int sum = WaveformDataList[0].MileList.milestoneList.Count;
                    int radius = size / 2;
                    foreach (LabelInfo label in _waveformDataList[0].LabelInfoList)
                    {
                        for (int i = 0; i < sum; i++)
                        {
                            long postion = 0;
                            postion = WaveformDataList[0].MileList.milestoneList[i].mFilePosition;
                            if (postion == long.Parse(label.sMileIndex))
                            {
                                int pointX = i * (_controlWidth - _singleChannelInfoWidth) / sum;
                                Pen locationBrush = new Pen(Color.Red, 3);
                                grapTagging.DrawEllipse(locationBrush, new Rectangle(pointX - radius,
                                  label.rectY - radius,
                                  size,
                                  size));
                                break;
                            }
                        }

                    }
                    return taggingBitmap;
                }
            }
            return null;
        }

        /// <summary>
        /// 绘制游标
        /// </summary>
        /// <param name="x">坐标X</param>
        /// <returns>游标</returns>
        public Bitmap DrawCaliperLine()
        {
            Bitmap caliperImage = new Bitmap(ControlWidth, ControlHeight);
            Graphics g = Graphics.FromImage(caliperImage);
            
            Point[] point = new Point[3];
            Font f = new Font(FontName, 10, FontStyle.Regular);
            point[0] = new Point(CaliperX - 15, 0);
            point[1] = new Point(CaliperX + 15, 0);
            point[2] = new Point(CaliperX, 30);
            string mileString = GetMileStoneString(CaliperX);
            if (point[0].X < 0)
            {
                _caliperRect = new Rectangle(0, point[1].Y, 30, 30);
            }
            else if (point[1].X > ControlWidth)
            {
                _caliperRect = new Rectangle(ControlWidth, point[1].Y, 30, 30);
                
            }
            else
            {
                _caliperRect = new Rectangle(point[0].X, point[1].Y, 30, 30);
               
            }
            g.FillPolygon(new SolidBrush(Color.GreenYellow), point);
            g.DrawLine(new Pen(Color.GreenYellow, 1), CaliperX, 30, CaliperX, ControlHeight);
            g.DrawString(mileString, f, new SolidBrush(Color.Red), CaliperX + 15, 1);

            return caliperImage;
        }

        /// <summary>
        /// 绘制通道数据
        /// </summary>
        /// <returns></returns>
        public Bitmap DrawChannelData()
        {
            Bitmap channelDataBitmap = new Bitmap(_controlWidth, _controlHeight - MileageInfoHeight);
            channelDataBitmap.MakeTransparent(Color.White);
            Graphics gChannelData = Graphics.FromImage(channelDataBitmap);
            int offset = 8;
            for (int i = 0; i < _waveformDataList[0].ChannelList.Count; i++)
            {
                var channel = WaveformDataList[0].ChannelList[i];
                if (channel.IsVisible)
                {
                    if(_caliperIndex>(channel.Data.Length/2+50))
                    {
                        offset = -38;
                    }
                    else
                    {
                        offset = 8;
                    }

                    gChannelData.FillRectangle(new SolidBrush(Color.FromArgb(95,Color.Black)), new RectangleF(_caliperX+offset,
                       channel.DisplayRect.Top + channel.DisplayRect.Height / 4,
                        40,
                       channel.DisplayRect.Height / 2));
                    gChannelData.DrawString(channel.Data[_caliperIndex].ToString(),
                    new Font("宋体", 9.0f, FontStyle.Regular),
                    new SolidBrush(Color.White),
                    new RectangleF(_caliperX+offset,
                       channel.DisplayRect.Top + channel.DisplayRect.Height / 4,
                        40,
                       channel.DisplayRect.Height / 2),
                    ChannelstringFormat);
                }
            }
            gChannelData.Dispose();
            gChannelData = null;
            return channelDataBitmap;
        }
       

        /// <summary>
        /// 重新计算单个通道的高度
        /// </summary>
        public void ReCalculateDrawSize()
        {
            int totalHight = _controlHeight - _mileageInfoHeight - HighFactor;
            if (totalHight < 400)
            {
                totalHight = 400;
            }
            _waveformCoordinatesY = totalHight / 100;
           
        }

        /// <summary>
        /// 重新计算通道高度
        /// </summary>
        public void ReCalculateChannelInfoSize()
        {
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                for (int j = 0; j < WaveformDataList[i].ChannelList.Count; j++)
                {
                    WaveformDataList[i].ChannelList[j].DisplayRect = new Rectangle(_controlWidth - _singleChannelInfoWidth,
                      _mileageInfoHeight + 2 + WaveformDataList[i].ChannelList[j].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                      _singleChannelInfoWidth,
                      _singleChannelInfoHeight);
                    WaveformDataList[i].ChannelList[j].DragRect = new Rectangle(_controlWidth - _singleChannelInfoWidth,
                        _mileageInfoHeight + 2 + WaveformDataList[i].ChannelList[i].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                        _singleChannelInfoWidth,
                        _singleChannelInfoHeight / 2);
                    WaveformDataList[i].ChannelList[j].ColorRect = new Rectangle(_controlWidth - _singleChannelInfoWidth + 130,
                        _mileageInfoHeight + 2 + WaveformDataList[i].ChannelList[j].Location * _waveformCoordinatesY - _singleChannelInfoHeight / 2,
                        _singleChannelInfoWidth,
                        _singleChannelInfoHeight / 2);
                }
            }
        }

        /// <summary>
        /// 根据鼠标画的框放大显示
        /// </summary>
        /// <param name="rect">虚线框</param>
        public long MakeViewZoomIn(Rectangle rect)
        {
            if (rect.Height > 0 && rect.Width > 0)
            {
                int picHeight = ControlHeight - MileageInfoHeight;
                if ((rect.Height + rect.Y) > picHeight)
                {
                    rect.Height = picHeight - rect.Y;
                }
                int remainder = 0;
                if (rect.Height <= _waveformCoordinatesY)
                {
                    remainder = 50;
                }
                else
                {
                    remainder = rect.Height / _waveformCoordinatesY;
                }
                
                int currentSize =ZoomInSize * rect.Width / (ControlWidth - SingleChannelInfoWidth);
                //获取缩放比例时会用到，如果小于10会遇到除0异常
                if (currentSize<10)
                {
                    currentSize = 10;
                }
                _zoomInLeftX = waveformPoints.Length * rect.X / (ControlWidth - SingleChannelInfoWidth);
                _zoomInRightX = waveformPoints.Length * (rect.X + rect.Width) / (ControlWidth - SingleChannelInfoWidth);
                if (_zoomInLeftX >= WaveformDataList[0].MileList.milestoneList.Count)
                {
                    _zoomInLeftX = WaveformDataList[0].MileList.milestoneList.Count - 1;
                }
                long postion = WaveformDataList[0].MileList.milestoneList[_zoomInLeftX].mFilePosition;
                //int scorllSize= GetLocationScrollSize(WaveformDataList[0].MileList.milestoneList[_zoomInLeftX].mFilePosition);
                ZoomInSize = currentSize;
                int offset = picHeight / remainder;
                _zoomInY = (rect.Y - _mileageInfoHeight) / _waveformCoordinatesY;
                //ZoomInX = currentSize;
                //处理X轴
                _waveformCoordinatesY = offset;
                _zoomInRect.X = rect.X;
                _zoomInRect.Y = rect.Y;
                _zoomInRect.Width = rect.Width;
                _zoomInRect.Height = rect.Height;

                //判断当前选择的通道，如果选中则显示，否则不显示
                if (_currentChannelAndPoint != null && _currentChannelAndPoint.Count > 0)
                {
                    foreach(var kv in _currentChannelAndPoint)
                    {
                        foreach (var kvp in kv.Value)
                        {
                            bool isFind = false;
                            for (int i = 0; i < kvp.Value.Length; i++)
                            {
                                if (_zoomInRect.Contains(kvp.Value[i]))
                                {
                                    _waveformDataList[kv.Key].ChannelList[kvp.Key].IsZoomInView = true;
                                    isFind = true;
                                    break;
                                }
                            }
                            if (!isFind)
                            {
                                _waveformDataList[kv.Key].ChannelList[kvp.Key].IsZoomInView = false;
                            }
                        }
                    }
                    
                }
                return postion;
            }
            return 0;
        }

        /// <summary>
        /// 恢复正常视图
        /// </summary>
        public void MakeViewNormal()
        {
            ZoomInY = 0;
            IsZoomInView = false;
            ReCalculateDrawSize();
            for (int i = 0; i < _waveformDataList.Count; i++)
            {
                for (int j = 0; j < _waveformDataList[i].ChannelList.Count; j++)
                {
                    _waveformDataList[i].ChannelList[j].IsZoomInView = true;
                }
            }
        }

        /// <summary>
        /// 根据坐标选择通道
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public void SelectDragItem(int x, int y)
        {
            string channelName = string.Empty;
            
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                WavefromData data = WaveformDataList[i];
                if (!WaveformDataList[i].LayerConfig.IsVisible || IsZoomInView)
                {
                    continue;
                }
                bool isFind = false;
                for (int j = 0; j < data.ChannelList.Count; j++)
                {
                    if (data.ChannelList[j].DragRect.Contains(x, y - _mileageInfoHeight)
                        && data.ChannelList[j].IsVisible)
                    {
                        isFind = true;
                        switch (ChannelDargMode)
                        {
                            case DragMode.SameNameDarg:
                                {
                                    string name = data.ChannelList[j].Name;
                                    if (string.IsNullOrEmpty(channelName) || name == channelName)
                                    {
                                        IsSelectedChannel = true;
                                        channelName = name;
                                        WaveformDragData dragData = new WaveformDragData();
                                        dragData.SelectDragDataIndex = i;
                                        dragData.SelectDragChannel = j;
                                        dragData.SelectDragItemY = y - data.ChannelList[j].DisplayRect.Y;
                                        _selectedDragItems.Add(dragData);
                                        _lastDragData.SelectDragDataIndex = i;
                                        _lastDragData.SelectDragChannel = j;
                                        break;
                                    }
                                    break;
                                }
                            case DragMode.SameBaselineDarg:
                            case DragMode.SingleDarg:
                                {
                                    IsSelectedChannel = true;
                                    WaveformDragData dragData = new WaveformDragData();
                                    dragData.SelectDragDataIndex = i;
                                    dragData.SelectDragChannel = j;
                                    dragData.SelectDragItemY = y - data.ChannelList[j].DisplayRect.Y;
                                    _selectedDragItems.Add(dragData);
                                    _lastDragData.SelectDragDataIndex = i;
                                    _lastDragData.SelectDragChannel = j;
                                    break;
                                }
                        }
                        

                    }
                    if (isFind)
                    {
                        if (ChannelDargMode == DragMode.SameNameDarg)
                        {
                            break;
                        }
                        else if (ChannelDargMode == DragMode.SingleDarg)
                        {
                            return;
                        }
                    }

                }
            }
        }


        /// <summary>
        /// 选择单个通道
        /// </summary>
        /// <param name="x">坐标X</param>
        /// <param name="y">坐标Y</param>
        /// <returns>选择的数据</returns>
        public WaveformDragData SelectSingleItem(int x,int y)
        {
            string channelName = string.Empty;
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                WavefromData data = WaveformDataList[i];
                if (!WaveformDataList[i].LayerConfig.IsVisible || IsZoomInView)
                {
                    continue;
                }
                for (int j = 0; j < data.ChannelList.Count; j++)
                {
                    if (data.ChannelList[j].DragRect.Contains(x, y - _mileageInfoHeight)
                        && data.ChannelList[j].IsVisible)
                    {
                        string name = data.ChannelList[j].Name;
                        if (string.IsNullOrEmpty(channelName) || name == channelName)
                        {
                            IsSelectedChannel = true;
                            channelName = name;
                            WaveformDragData dragData = new WaveformDragData();
                            dragData.SelectDragDataIndex = i;
                            dragData.SelectDragChannel = j;
                            dragData.SelectDragItemY = y - data.ChannelList[j].DisplayRect.Y;
                            return dragData;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 是否选中了游标
        /// </summary>
        /// <param name="x">位置X</param>
        /// <param name="y">位置Y</param>
        /// <returns>成功为true，失败为false</returns>
        public bool SelectCaliper(int x, int y)
        {
            IsSelectedCaliper = false;
            if (_caliperRect.Contains(x, y))
            {
                IsSelectedCaliper = true;
            }
            return IsSelectedCaliper;
        }

        /// <summary>
        /// 移动游标
        /// </summary>
        /// <param name="x"></param>
        public void MoveCaliper(int x)
        {
            if (IsSelectedCaliper)
            {
                CaliperX = x;
            }
        }

        /// <summary>
        /// 根据坐标移动通道
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public void MoveDragItem(int x, int y)
        {
            if (_selectedDragItems.Count > 0)
            {
                for (int i = 0; i < _selectedDragItems.Count; i++)
                {
                    int postionY = y - _selectedDragItems[i].SelectDragItemY + _singleChannelInfoHeight / 2;
                    if (postionY > 0 && postionY < _controlHeight - _mileageInfoHeight)
                    {
                        int locationY = postionY / _waveformCoordinatesY;
                        if (locationY < 1)
                        {
                            locationY = 1;
                        }
                        _waveformDataList[_selectedDragItems[i].SelectDragDataIndex].ChannelList[_selectedDragItems[i].SelectDragChannel].Location = locationY; 
                    }
                }
            }
        }

        /// <summary>
        /// 清空已选择的通道
        /// </summary>
        public void ClearDragItem()
        {
            foreach (var item in _selectedDragItems)
            {
                _waveformDataList[item.SelectDragDataIndex].ChannelList[item.SelectDragChannel].LineWidth = 1;
            }
            IsSelectedChannel = false;
            _selectedDragItems.Clear();
        }

        /// <summary>
        /// 清除高亮显示
        /// </summary>
        public void ClearHighlightItem()
        {
            _lastDragData = new WaveformDragData();
        }

        /// <summary>
        /// 获取所有的台帐信息
        /// </summary>
        /// <param name="accountDesc">配置文件中的台帐描述信息</param>
        /// <param name="isAcountDatabaseVisible">配置文件是否显示台帐的值</param>
        /// <returns>是否加载成功，成功为：true，失败为：false</returns>
        public bool GetAccountDatabaseList(List<AccountDesc> accountDesc, bool isAcountDatabaseVisible)
        {
            if (_accountDatabaseList.Count > 0 && _accountDatabaseList[0].Name == "PWMIS台帐")
            {
                return false;
            }
            AccountDatabase accountDatabase = new AccountDatabase();
            List<AccountTotalData> accountTotalDataList = new List<AccountTotalData>();
            int showCount = 0;
            for (int i = 0; i < accountDesc.Count; i++)
            {
                AccountTotalData accountTotaldata = new AccountTotalData();
                string dir = string.Empty;
                switch (WaveformDataList[0].CitFile.iDir)
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
                }
                string sqlText = "select * from " + accountDesc[i].TableName + " where 线编号='" +
                           WaveformDataList[0].LineCode +
                            "' and 行别='" + dir + "' and " + accountDesc[i].StartMileage + " is not null";
                DataTable dt = null;
                try
                {
                    dt = DBOperator.Query(sqlText);
                }
                catch (Exception ex)
                {
                    throw new Exception("读取台账数据失败:" + ex.Message, ex.InnerException);
                }
                List<AccountInfo> accountInfoList = new List<AccountInfo>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.AccountType = accountDesc[i].Name;
                    if (accountInfo.AccountType.Contains("道岔"))
                    {
                        double start = double.Parse(dt.Rows[j][(accountDesc[i].StartMileage)].ToString());
                        double totoalLength = double.Parse(dt.Rows[j]["道岔全长"].ToString()) / 1000000;
                        accountInfo.StartMileage = (long)((start - totoalLength) * 1000);
                        accountInfo.EndMileage = (long)((start + totoalLength) * 1000);
                    }
                    else
                    {
                        accountInfo.StartMileage = (long)(double.Parse(dt.Rows[j][(accountDesc[i].StartMileage)].ToString()) * 1000);
                        if (accountDesc[i].EndMileage.Length > 0)
                        {
                            accountInfo.EndMileage = (long)(double.Parse(dt.Rows[j][(accountDesc[i].EndMileage)].ToString()) * 1000);
                        }
                    }
                    if (accountInfo.AccountType.Contains("曲线"))
                    {
                        accountInfo.AccountKeywords = dt.Rows[j]["曲线方向"].ToString() + " R-" + dt.Rows[j][(accountDesc[i].KeyText)].ToString();
                    }
                    else if (accountInfo.AccountType.Contains("坡度"))
                    {
                        accountInfo.AccountKeywords = "坡度" + Convert.ToDouble(dt.Rows[j][(accountDesc[i].KeyText)]).ToString("F2");
                    }
                    else if (accountInfo.AccountType.Contains("速度区段"))
                    {
                        accountInfo.AccountKeywords = dt.Rows[j][(accountDesc[i].KeyText)].ToString() + "Km/h";
                    }
                    else if (accountInfo.AccountType.Contains("道岔"))
                    {
                        accountInfo.AccountKeywords = "道岔";
                    }

                    accountInfoList.Add(accountInfo);
                }
                accountTotaldata.DispalyRect = new Rectangle(
                    _controlWidth - _singleChannelInfoWidth,
                    3 + (i + 1) * _singleChannelInfoHeight + (i + 1),
                    _singleChannelInfoWidth,
                    _singleChannelInfoHeight);
                accountTotaldata.Name = accountDesc[i].Name;
                accountTotaldata.IsVisible = accountDesc[i].IsCheck;
                if (accountTotaldata.IsVisible)
                {
                    showCount++;
                }
                accountTotaldata.AccountInfoList = accountInfoList;
                accountTotalDataList.Add(accountTotaldata);
            }
            accountDatabase.AccountTotalDataList = accountTotalDataList;
            _accountDatabaseList.Insert(0, accountDatabase);
            _accountDatabaseList[0].Name = "PWMIS台帐";
            _accountDatabaseList[0].IsVisible = isAcountDatabaseVisible;
            _accountInfoHeight = showCount * 40;
            return true;
        }

        /// <summary>
        /// 检查是否存在已加载的台帐信息
        /// </summary>
        /// <returns>是否存在台帐信息，ture：存在，flase：不存在</returns>
        public bool CheckAccountDatabaseListExist()
        {
            if (_accountDatabaseList.Count > 0 && _accountDatabaseList[0].Name == "PWMIS台帐")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 绘制台帐数据
        /// </summary>
        /// <returns>包含台帐的内存图片</returns>
        public Bitmap DrawAccountDatabase()
        {
            if (_controlWidth > 0 && _singleChannelInfoWidth > 0 && _accountInfoHeight > 0)
            {
                Bitmap accountImage = new Bitmap(_controlWidth - _singleChannelInfoWidth, _accountInfoHeight);
                Graphics g = Graphics.FromImage(accountImage);
                int visibleCount = 0;
                int height = 0;
                Pen pen = new Pen(Color.Black, 2);
                Font font = new Font(_fontName, 10, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.Red);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                for (int i = 0; i < _accountDatabaseList.Count; i++)
                {
                    if (!_accountDatabaseList[i].IsVisible)
                    {
                        continue;
                    }
                    visibleCount = 0;
                    for (int j = 0; j < _accountDatabaseList[i].AccountTotalDataList.Count; j++)
                    {
                        AccountTotalData totalData = _accountDatabaseList[i].AccountTotalDataList[j];
                        if (totalData.IsVisible && totalData.AccountInfoList != null)
                        {
                            visibleCount++;
                            foreach (var item in totalData.AccountInfoList)
                            {
                                Point startPoint1 = new Point(-1, -1);
                                Point endPoint1 = new Point(-1, -1);
                                if ((long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() <= item.StartMileage
                                    && item.StartMileage < (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter()
                                    || (long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() >= item.StartMileage
                                     && item.StartMileage >= (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter())
                                {
                                    for (int k = 0; k < _waveformDataList[0].MileList.milestoneList.Count; k++)
                                    {
                                        float mi = _waveformDataList[0].MileList.milestoneList[k].GetMeter();
                                        if ((long)_waveformDataList[0].MileList.milestoneList[k].GetMeter() == item.StartMileage)
                                        {
                                            startPoint1.X = k * (_controlWidth - _singleChannelInfoWidth) / _waveformDataList[0].MileList.milestoneList.Count;
                                            startPoint1.Y = height + (visibleCount - 1) * _singleChannelInfoHeight;

                                            endPoint1.X = k * (_controlWidth - _singleChannelInfoWidth) / _waveformDataList[0].MileList.milestoneList.Count;
                                            endPoint1.Y = startPoint1.Y + _singleChannelInfoHeight;
                                            g.DrawLine(pen, startPoint1.X, startPoint1.Y, endPoint1.X, endPoint1.Y);
                                            k += 10;
                                            string headText = (item.StartMileage / 1000f).ToString("f3");
                                            g.DrawString(headText, font, brush, new PointF(startPoint1.X, startPoint1.Y));
                                            break;
                                        }
                                    }
                                }
                                bool isTail = false;
                                bool isHead = false;
                                Point startPoint2 = new Point(-1, -1);
                                Point endPoint2 = new Point(-1, -1);
                                if ((long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() < item.EndMileage
                                   && item.EndMileage <= (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter()
                                   || (long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() >= item.EndMileage
                                    && item.EndMileage >= (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter())
                                {
                                    for (int k = 0; k < _waveformDataList[0].MileList.milestoneList.Count; k++)
                                    {
                                        if ((long)_waveformDataList[0].MileList.milestoneList[k].GetMeter() == item.EndMileage)
                                        {
                                            startPoint2.X = k * (_controlWidth - _singleChannelInfoWidth) / _waveformDataList[0].MileList.milestoneList.Count;
                                            startPoint2.Y = height + (visibleCount - 1) * _singleChannelInfoHeight;

                                            endPoint2.X = k * (_controlWidth - _singleChannelInfoWidth) / _waveformDataList[0].MileList.milestoneList.Count;
                                            endPoint2.Y = startPoint2.Y + _singleChannelInfoHeight;
                                            g.DrawLine(pen, startPoint2, endPoint2);

                                            string headText = (item.EndMileage / 1000f).ToString("f3");
                                            g.DrawString(headText, font, brush, new PointF(startPoint2.X, startPoint2.Y));
                                            if (startPoint1.X != -1 && endPoint1.X != -1)
                                            {
                                                g.DrawLine(pen, startPoint2.X, startPoint2.Y, startPoint1.X, startPoint1.Y);
                                                g.DrawLine(pen, endPoint2.X, endPoint2.Y, endPoint1.X, endPoint1.Y);
                                                g.DrawString(item.AccountKeywords, font, brush, new RectangleF(startPoint1.X, startPoint1.Y + height, startPoint2.X - startPoint1.X, _singleChannelInfoHeight - height * 4), format);
                                            }
                                            else
                                            {
                                                isHead = true;
                                            }
                                            k += 10;
                                            isTail = true;
                                            break;
                                        }
                                    }
                                }

                                if (!isTail && startPoint1.X != -1 && startPoint1.Y != -1)
                                {
                                    g.DrawLine(pen, startPoint1.X, startPoint1.Y, accountImage.Width, startPoint1.Y);
                                    g.DrawLine(pen, endPoint1.X, endPoint1.Y, accountImage.Width, endPoint1.Y);
                                    g.DrawString(item.AccountKeywords, font, brush, new RectangleF(startPoint1.X, startPoint1.Y, accountImage.Width - startPoint1.X, _singleChannelInfoHeight - height * 4), format);

                                }
                                else if (isHead && startPoint2.X != -1 && startPoint2.Y != -1)
                                {
                                    g.DrawLine(pen, 0, startPoint2.Y, startPoint2.X, startPoint2.Y);
                                    g.DrawLine(pen, 0, endPoint2.Y, endPoint2.X, endPoint2.Y);
                                    g.DrawString(item.AccountKeywords, font, brush, new RectangleF(0, startPoint2.Y, startPoint2.X, _singleChannelInfoHeight - height * 4), format);
                                }
                                else if (!isHead && !isTail)
                                {
                                    float sss = _waveformDataList[0].MileList.milestoneList[0].GetMeter();
                                    float eee = _waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter();
                                    if ((long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() > item.StartMileage
                                   && item.EndMileage > (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter()
                                   || (long)_waveformDataList[0].MileList.milestoneList[0].GetMeter() < item.StartMileage
                                    && item.EndMileage < (long)_waveformDataList[0].MileList.milestoneList[_waveformDataList[0].MileList.milestoneList.Count - 1].GetMeter())
                                    {
                                        g.DrawLine(pen, 0, (visibleCount - 1) * _singleChannelInfoHeight - height, accountImage.Width, (visibleCount - 1) * _singleChannelInfoHeight - height);
                                        g.DrawLine(pen, 0, visibleCount * _singleChannelInfoHeight - height, accountImage.Width, visibleCount * _singleChannelInfoHeight - height);
                                        g.DrawString(item.AccountKeywords, font, brush, new RectangleF(0, (visibleCount - 1) * _singleChannelInfoHeight - height, accountImage.Width, _singleChannelInfoHeight - height * 2), format);
                                    }
                                }
                            }
                        }
                    }
                }
                g.Dispose();
                return accountImage;
            }
            return null;
        }

        /// <summary>
        /// 计算除了第一个波形外的偏移量
        /// </summary>
        /// <returns>除第一个波形外的偏移量</returns>
        public void AutoWaveAlignmentTranslation()
        {
            #region 波形1数据的提取
            //波形1数据的提取
            double[][] data_ww = new double[5][];

            //左高低
            for (int j = 0; j < WaveformDataList[0].ChannelList.Count; j++)
            {
                if (WaveformDataList[0].ChannelList[j].ChineseName == "左高低_中波" || WaveformDataList[0].ChannelList[j].Name == "L_Prof_SC")
                {
                    //float[] floatValue = Array.ConvertAll(WaveformDataList[0].ChannelList[j].Data, d => (float)d);
                    data_ww[0] = WaveformDataList[0].ChannelList[j].Data;
                    break;
                }
            }
            //右高低
            for (int j = 0; j < WaveformDataList[0].ChannelList.Count; j++)
            {
                if (WaveformDataList[0].ChannelList[j].ChineseName == "右高低_中波" || WaveformDataList[0].ChannelList[j].Name == "R_Prof_SC")
                {
                    //float[] floatValue = Array.ConvertAll(WaveformDataList[0].ChannelList[j].Data, d => (float)d);
                    data_ww[1] = WaveformDataList[0].ChannelList[j].Data;
                    break;
                }
            }
            //左轨向
            for (int j = 0; j < WaveformDataList[0].ChannelList.Count; j++)
            {
                if (WaveformDataList[0].ChannelList[j].ChineseName == "左轨向_中波" || WaveformDataList[0].ChannelList[j].Name == "L_Align_SC")
                {
                    //float[] floatValue = Array.ConvertAll(WaveformDataList[0].ChannelList[j].Data, d => (float)d);
                    data_ww[2] = WaveformDataList[0].ChannelList[j].Data;
                    break;
                }
            }
            //右轨向
            for (int j = 0; j < WaveformDataList[0].ChannelList.Count; j++)
            {
                if (WaveformDataList[0].ChannelList[j].ChineseName == "右轨向_中波" || WaveformDataList[0].ChannelList[j].Name == "R_Align_SC")
                {
                    //float[] floatValue = Array.ConvertAll(WaveformDataList[0].ChannelList[j].Data, d => (float)d);
                    data_ww[3] = WaveformDataList[0].ChannelList[j].Data;
                    break;
                }
            }
            //轨距
            for (int j = 0; j < WaveformDataList[0].ChannelList.Count; j++)
            {
                if (WaveformDataList[0].ChannelList[j].ChineseName == "轨距" || WaveformDataList[0].ChannelList[j].Name == "Gage")
                {
                    //float[] floatValue = Array.ConvertAll(WaveformDataList[0].ChannelList[j].Data, d => (float)d);
                    data_ww[4] = WaveformDataList[0].ChannelList[j].Data;
                    break;
                }
            }
            #endregion
            AutoTranslationClass autoTranslationCls = new AutoTranslationClass();
            for (int i = 1; i < WaveformDataList.Count; i++)
            {
                int citIndex = i;

                #region 波形2数据的提取
                //波形2数据的提取
                double[][] data_ww1 = new double[5][];

                //左高低
                for (int j = 0; j < WaveformDataList[citIndex].ChannelList.Count; j++)
                {
                    if (WaveformDataList[citIndex].ChannelList[j].ChineseName == "左高低_中波" || WaveformDataList[citIndex].ChannelList[j].Name == "L_Prof_SC")
                    {
                        //float[] floatValue = Array.ConvertAll(WaveformDataList[citIndex].ChannelList[j].Data, d => (float)d);
                        data_ww1[0] = WaveformDataList[citIndex].ChannelList[j].Data;
                        break;
                    }
                }
                //右高低
                for (int j = 0; j < WaveformDataList[citIndex].ChannelList.Count; j++)
                {
                    if (WaveformDataList[citIndex].ChannelList[j].ChineseName == "右高低_中波" || WaveformDataList[citIndex].ChannelList[j].Name == "R_Prof_SC")
                    {
                        //float[] floatValue = Array.ConvertAll(WaveformDataList[citIndex].ChannelList[j].Data, d => (float)d);
                        data_ww1[1] = WaveformDataList[citIndex].ChannelList[j].Data;
                        break;
                    }
                }
                //左轨向
                for (int j = 0; j < WaveformDataList[citIndex].ChannelList.Count; j++)
                {
                    if (WaveformDataList[citIndex].ChannelList[j].ChineseName == "左轨向_中波" || WaveformDataList[citIndex].ChannelList[j].Name == "L_Align_SC")
                    {
                       // float[] floatValue = Array.ConvertAll(WaveformDataList[citIndex].ChannelList[j].Data, d => (float)d);
                        data_ww1[2] = WaveformDataList[citIndex].ChannelList[j].Data;
                        break;
                    }
                }
                //右轨向
                for (int j = 0; j < WaveformDataList[citIndex].ChannelList.Count; j++)
                {
                    if ((WaveformDataList[citIndex].ChannelList[j].ChineseName == "右轨向_中波") || (WaveformDataList[citIndex].ChannelList[j].Name == "R_Align_SC"))
                    {
                        //float[] floatValue = Array.ConvertAll(WaveformDataList[citIndex].ChannelList[j].Data, d => (float)d);
                        data_ww1[3] = WaveformDataList[citIndex].ChannelList[j].Data;
                        break;
                    }
                }
                //轨距
                for (int j = 0; j < WaveformDataList[citIndex].ChannelList.Count; j++)
                {
                    if (WaveformDataList[citIndex].ChannelList[j].ChineseName == "轨距" || WaveformDataList[citIndex].ChannelList[j].Name == "Gage")
                    {
                        //float[] floatValue = Array.ConvertAll(WaveformDataList[citIndex].ChannelList[j].Data, d => (float)d);
                        data_ww1[4] = WaveformDataList[citIndex].ChannelList[j].Data;
                        break;
                    }
                }
                #endregion

                #region 波形对齐
                try
                {
                    int reviseVal = autoTranslationCls.AutoTranslation(data_ww, data_ww1);
                    WaveformDataList[citIndex].ReviseValue += reviseVal;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }
        }

        /// <summary>
        /// 自动里程对齐
        /// </summary>
        public void AutoMileageAlignmenTranslation(int index,float mileage)
        {
            if (WaveformDataList.Count > 1)
            {
                long revise = 0;
                long location = 0;
                WaveformDataList[index].GetLocationSampleCount(mileage, ref location);
                //WaveformDataList[index].GetLocationScrollSize(mileage, ref location);
                if (location > 0)
                {
                    revise = WaveformDataList[index].CitFileProcess.GetAppointSampleCount(WaveformDataList[index].CitFilePath, location);
                    long scorllSample= _waveformDataList[index].CitFileProcess.GetAppointSampleCount(WaveformDataList[index].CitFilePath, WaveformDataList[index].CurrentPostion);
                    WaveformDataList[index].ReviseValue += (revise-scorllSample);
                }

            }
        }

        /// <summary>
        /// 自动排列
        /// </summary>
        public void AutoArrange()
        {
            if (WaveformDataList.Count > 0)
            {
                for (int i = 0; i < WaveformDataList.Count; i++)
                {
                    List<ChannelsClass> channels = WaveformDataList[i].ChannelList;
                    int channelCount = channels.Where(p => p.IsVisible == true).ToList().Count;
                    int iSum = (100 / ((channelCount + 1)));
                    int index = 0;
                    for (int j = 0; j < WaveformDataList[i].ChannelList.Count; j++)
                    {
                        if (channels[j].IsVisible)
                        {
                            channels[j].Location = (iSum * (index + 1) + (index + 1));
                            index++;
                        }
                       
                    }
                }
            }

        }

        /// <summary>
        /// 根据x位置获取里程标字符串
        /// </summary>
        /// <param name="x">位置x</param>
        /// <returns></returns>
        public string GetMileStoneString(int x)
        {
            int index = GetLocationIndex(x);
            string mileStoneString = "";

            mileStoneString = "K" + WaveformDataList[0].MileList.milestoneList[index].mKm + "+" + (WaveformDataList[0].MileList.milestoneList[index].mMeter).ToString();

            return mileStoneString;
        }

        /// <summary>
        /// 根据屏幕坐标X获取对应的波形索引位置
        /// </summary>
        /// <param name="x">坐标X</param>
        /// <returns></returns>
        public int GetLocationIndex(int x)
        {
            int index = 0;

            index = (int)(x / ((ControlWidth - SingleChannelInfoWidth) / 1.0f / (WaveformDataList[0].MileList.milestoneList.Count)));
            if (index >= WaveformDataList[0].MileList.milestoneList.Count)
            {
                index = WaveformDataList[0].MileList.milestoneList.Count - 1;
            }

            return index;
        }


        public Bitmap DrawIICDeviation(float mileage, String msg, String channelName, long milePostion)
        {
            int publicHeight = 30 + ControlHeight;//信息区高度+坐标区高度
            Bitmap bmp = new Bitmap(ControlWidth, publicHeight);
            Graphics g1 = Graphics.FromImage(bmp);
            //绘制大值信息
            Font f_Except = new Font(FontName, 14, FontStyle.Regular);
            String m_MilePos = "K" + "K" + (int)(mileage) + "+" + Math.Round(mileage * 1000 % 1000, 2);
            g1.DrawString(msg, f_Except, new SolidBrush(Color.Black), new PointF((ControlWidth - SingleChannelInfoWidth) / 10, 10));
            Bitmap baseLine = DrawChannelsBaselines();
            //30给上面的字体，30给里程显示
            g1.DrawImage(baseLine, 0, 60);
            List<Bitmap> images = new List<Bitmap>();
            for (int i = 0; i < WaveformDataList.Count; i++)
            {
                for (int j = 0; j < WaveformDataList[i].ChannelList.Count; j++)
                {
                    if (WaveformDataList[i].ChannelList[j].Name == channelName)
                    {
                        //绘制高亮
                        _lastDragData.SelectDragChannel = j;
                        _lastDragData.SelectDragDataIndex = i;
                        Bitmap m = DrawWavefrom();
                        images.Add(m);
                        break;
                    }
                }
            }
            _lastDragData = new WaveformDragData();//还原
            foreach(var item in images)
            {
                g1.DrawImage(item, 0, 30);
            }
            g1.DrawImage(DrawMileageLocation(milePostion), 0, 30);
            return bmp;
        }
    }
}
