/// ---------------------------------------------------------------------------------------------------------------------------------------
/// FileName:WaveDispalyData.cs
/// 说    明：主窗口波形显示数据
/// Version:  1.0 
/// Date:     2017/5/27  
/// Author:   Jinxl
//// --------------------------------------------------------------------------------------------------------------------------------------
using CitFileSDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CitIndexFileSDK;
using CitIndexFileSDK.MileageFix;
using CommonFileSDK;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 波形显示数据
    /// </summary>
    public class WavefromData
    {

        public delegate void ExportCitPageChanged(int totalCount, int currentIndex);

        public event ExportCitPageChanged PageChanged;

        #region 私有文件处理类

        /// <summary>
        /// cit文件处理类实例
        /// </summary>
        public CITFileProcess CitFileProcess { get; private set; }

        /// <summary>
        /// 索引操作类
        /// </summary>
        public IOperator IndexOperator { get; private set; }

        /// <summary>
        /// 无效数据处理类
        /// </summary>
        private InvalidDataManager _invaildDataManager;

        /// <summary>
        /// 标注数据处理类
        /// </summary>
        private LabelInfoManager _labelInfoDataManager;

        #endregion

        #region 私有文件属性
        /// <summary>
        /// cit文件信息
        /// </summary>
        private FileInformation _citFile;

        /// <summary>
        /// 里程信息列表
        /// </summary>
        private MilestoneList _mileList;

        /// <summary>
        /// 索引列表
        /// </summary>
        private MilestoneFix _mileageFix;

        /// <summary>
        /// 无效数据列表
        /// </summary>
        private List<InvalidData> _invalidDataList;

        /// <summary>
        /// 标注信息列表
        /// </summary>
        private List<LabelInfo> _labelInfoList;

        /// <summary>
        /// 当前显示的范围
        /// </summary>
        private int _zoomInSize = 200;

        /// <summary>
        /// 线路编码
        /// </summary>
        private string _lineCode;

        #endregion

        #region 私有文件路径信息

        /// <summary>
        /// cit文件路径
        /// </summary>
        private string _citFilePath;

        /// <summary>
        /// 波形配置文件
        /// </summary>
        private string _waveConfigFilePath;

        /// <summary>
        /// 波形索引文件
        /// </summary>
        private string _waveIndexFilePath;

        #endregion

        #region 公共文件路径属性

        /// <summary>
        /// cit文件路径
        /// </summary>
        public string CitFilePath
        {
            get
            {
                return _citFilePath;
            }

            set
            {
                _citFilePath = value;
                if (File.Exists(_citFilePath))
                {
                    _citFile = CitFileProcess.GetFileInformation(_citFilePath);
                    ChanneDefinitionList = CitFileProcess.GetChannelDefinitionList(_citFilePath);
                }

            }
        }

        /// <summary>
        /// 波形索引文件路径
        /// </summary>
        public string WaveIndexFilePath
        {
            get
            {
                return _waveIndexFilePath;
            }

            set
            {
                _waveIndexFilePath = value;
                if (!string.IsNullOrEmpty(WaveIndexFilePath))
                {
                    IndexOperator.IndexFilePath = WaveIndexFilePath;
                    //_mileageFix = new MilestoneFix(_citFilePath);
                    //_mileageFix.ReadMilestoneFixTable();
                    //if (_mileageFix.FixData == null || _mileageFix.FixData.Count <= 0)
                    //{
                    //    IsLoadIndex = false;
                    //}
                    //_invalidDataList = _invaildDataManager.InvalidDataList(_waveIndexFilePath);
                    //_labelInfoList = _labelInfoDataManager.GetDataLabelInfo(_waveIndexFilePath);
                    CitFile.sTrackName = CitFile.sTrackName.Replace("\0", "");
                   
                }
            }
        }

        /// <summary>
        /// 波形通道配置文件
        /// </summary>
        public string WaveConfigFilePath
        {
            get
            {
                return _waveConfigFilePath;
            }

            set
            {
                _waveConfigFilePath = value;
                if (!string.IsNullOrEmpty(_waveConfigFilePath) && File.Exists(_waveConfigFilePath))
                {
                    ChannelList = ChannelManager.LoadChannelsConfig(_waveConfigFilePath, ChanneDefinitionList);
                }
                else
                {
                    throw new FileNotFoundException("找不到波形配置文件,请检查!");
                }
            }
        }

        #endregion

        #region 公共文件属性

        /// <summary>
        /// 是否加载索引文件
        /// </summary>
        public bool IsLoadIndex { get; set; }

        /// <summary>
        /// 通道信息列表
        /// </summary>
        public List<ChannelsClass> ChannelList { get; set; }

        /// <summary>
        /// 原始通道信息列表，生成配置文件需要
        /// </summary>
        public List<ChannelDefinition> ChanneDefinitionList { get; set; }

        public long CurrentPostion { get; private set; }

        /// <summary>
        /// 当前显示的总点数
        /// </summary>
        public int WaveformDataCount { get; set; }

        /// <summary>
        /// 图层显示配置信息
        /// </summary>
        public LayerConfigData LayerConfig { get; set; }


        /// <summary>
        /// 当前显示的范围
        /// </summary>
        public int ZoomInSize
        {
            get
            {
                return _zoomInSize;
            }

            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                _zoomInSize = value;
                if (!string.IsNullOrEmpty(_citFilePath))
                {
                    int rate = 0;
                    if (_citFile.iSmaleRate < 0)
                    {
                        rate = Math.Abs(CitFile.iSmaleRate) / 100;
                    }
                    else
                    {
                        rate = CitFile.iSmaleRate;
                    }
                    WaveformDataCount = _zoomInSize * rate;
                }
            }
        }

        /// <summary>
        /// cit文件信息
        /// </summary>
        public FileInformation CitFile
        {
            get
            {
                return _citFile;
            }
        }

        /// <summary>
        /// 偏移值
        /// </summary>
        public long ReviseValue { get; set; }

        /// <summary>
        /// 线路编码
        /// </summary>
        public string LineCode
        {
            get
            {
                if(string.IsNullOrEmpty(_lineCode))
                {
                    string cmdText = "select 自定义线路编号 from 自定义线路代码表 where 自定义线路编号='"
                          + CitFile.sTrackCode
                          + "' and 线路名='" + CitFile.sTrackName + "'";
                    object obj = InnerFileOperator.ExecuteScalar(cmdText);
                    if (obj != null)
                    {
                        _lineCode = obj.ToString();
                    }
                }
                return _lineCode;
            }
        }

        /// <summary>
        /// 里程列表
        /// </summary>
        public MilestoneList MileList
        {
            get
            {
                return _mileList;
            }
        }

        /// <summary>
        /// 索引信息列表
        /// </summary>
        public MilestoneFix MileageFix
        {
            get
            {
                if (_mileageFix == null)
                {
                    _mileageFix = new MilestoneFix(CitFilePath,IndexOperator);
                    _mileageFix.ReadMilestoneFixTable();
                    if (_mileageFix.FixData == null || _mileageFix.FixData.Count <= 0)
                    {
                        IsLoadIndex = false;
                    }
                }
                return _mileageFix;
            }
        }

        /// <summary>
        /// 无效数据列表
        /// </summary>
        public List<InvalidData> InvalidDataList
        {
            get
            {
                if (_invalidDataList == null)
                {
                    _invalidDataList = _invaildDataManager.InvalidDataList(_waveIndexFilePath);
                }
                return _invalidDataList;
            }
            set
            {
                _invalidDataList = value;
            }
        }

        /// <summary>
        /// 标记列表
        /// </summary>
        public List<LabelInfo> LabelInfoList
        {
            set
            {
                _labelInfoList = value;
            }
            get
            {
                if (_labelInfoList == null)
                {
                    _labelInfoList = _labelInfoDataManager.GetDataLabelInfo(_waveIndexFilePath);
                }
                return _labelInfoList;
            }
        }

        /// <summary>
        /// 所有的里程
        /// </summary>
        //private List<Milestone> _allStone = new List<Milestone>();
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        public WavefromData()
        {
            CitFileProcess = new CITFileProcess();
            _invaildDataManager = new InvalidDataManager();
            _labelInfoDataManager = new LabelInfoManager();
            _mileList = new MilestoneList();
            ChannelList = new List<ChannelsClass>();
            LayerConfig = new LayerConfigData();
            ChanneDefinitionList = new List<ChannelDefinition>();
            IndexOperator = new IndexOperator();
        }


        /// <summary>
        /// 根据起始位置加载波形数据
        /// </summary>
        /// <param name="startPostion">起始点</param>
        public void GetWaveformData(long startPostion)
        {
            if (File.Exists(_citFilePath))
            {
                long endPos = 0;
                long startPos = 0;
                long realStartPos = 0;
                long[] postions = CitFileProcess.GetPositons(_citFilePath);
                startPos = startPostion;
                if (LayerConfig.IsReverse)
                {
                    startPos -= ReviseValue * _citFile.iChannelNumber * 2;
                }
                else
                {
                    startPos += ReviseValue * _citFile.iChannelNumber * 2;
                }
                realStartPos = startPos;
                int pointCount = 0;
                if (startPos < postions[0])
                {
                    while (startPos < postions[0] && pointCount < WaveformDataCount)
                    {
                        startPos += _citFile.iChannelNumber * 2;
                        pointCount++;
                    }
                    pointCount = WaveformDataCount - pointCount;
                }
                else if(startPos>postions[1])
                {
                    while (startPos > postions[1] && pointCount < WaveformDataCount)
                    {
                        startPos -= _citFile.iChannelNumber * 2;
                        pointCount++;
                    }
                    pointCount = WaveformDataCount - pointCount;
                }
                else
                {
                    pointCount = WaveformDataCount;
                }
                if(LayerConfig.IsReverse)
                {
                    pointCount = pointCount * -1;
                }

                
                CurrentPostion = startPos;
                endPos = CitFileProcess.GetAppointEndPostion(_citFilePath, startPos, pointCount);
                List<double[]> data = null;
              
                if (LayerConfig.IsReverse)
                {
                    data = CitFileProcess.GetAllChannelDataInRange(_citFilePath, endPos, startPos);
                    MileList.milestoneList = CitFileProcess.GetMileStoneByRange(_citFilePath, endPos, startPos);
                }
                else
                {
                    data = CitFileProcess.GetAllChannelDataInRange(_citFilePath, startPos, endPos);
                    MileList.milestoneList = CitFileProcess.GetMileStoneByRange(_citFilePath, startPos, endPos);
                }
                
                if (IsLoadIndex)
                {
                    MileList.milestoneList = MileageFix.GetMileageReviseData(MileList.milestoneList);
                }
                data.RemoveAt(0);
                data.RemoveAt(0);
                if (ChannelList.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)

                    {
                        if (ChannelList.Count > i)
                        {
                            ChannelList[i].Data = new double[WaveformDataCount];
                            if (data[i].Length < WaveformDataCount)
                            {
                                double[] temp = new double[data[i].Length];
                                Array.Copy(data[i], temp, data[i].Length);
                                data[i] = new double[WaveformDataCount];
                                if (realStartPos < postions[0])
                                {
                                    Array.Copy(temp, 0, data[i], WaveformDataCount - temp.Length, temp.Length);
                                }
                                else
                                {
                                    Array.Copy(temp, data[i], temp.Length);
                                }
                            }
                            if (LayerConfig.IsReverse)
                            {
                                Array.Reverse(data[i]);
                            }
                            Array.Copy(data[i], ChannelList[i].Data, WaveformDataCount);
                           
                        }
                        else
                        { break; }
                    }
                    
                    if (MileList.milestoneList.Count < WaveformDataCount)
                    {
                        int count = WaveformDataCount - MileList.milestoneList.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (realStartPos < postions[0])
                            {
                                MileList.milestoneList.Insert(0,new Milestone() { mKm = 0, mMeter = 0 });
                            }
                            else
                            {
                                MileList.milestoneList.Add(new Milestone() { mKm = 0, mMeter = 0 });
                            }
                        }
                    }
                    if (LayerConfig.IsReverse)
                    {
                        MileList.milestoneList.Reverse();
                    }
                }
            }
            else
            {
                throw new InvalidDataException("CIT文件路径无效或为空！");
            }
        }

        /// <summary>
        /// 初始化波形数据
        /// </summary>
        public void InitWaveformData()
        {
            if (File.Exists(_citFilePath))
            {
                LayerConfig = new LayerConfigData();
                string dir = string.Empty;
                switch (CitFile.iDir)
                {
                    case 1: dir = "上行"; break;
                    case 2: dir = "下行"; break;
                    case 3: dir = "单线"; break;
                }
                LayerConfig.Name = CitFile.sTrackName + dir + " " + CitFile.sTrain 
                + " " + (CitFile.iKmInc == 0 ? "增" : "减") + " " + (CitFile.iRunDir == 0 ? "正" : "反") + " " + CitFile.sDate;//+ " " + Path.GetFileNameWithoutExtension(CitFilePath);
                List<ChannelDefinition> channelDefinitions = CitFileProcess.GetChannelDefinitionList(_citFilePath);
                if (channelDefinitions.Count > 2)
                {
                    channelDefinitions.RemoveAt(0);
                    channelDefinitions.RemoveAt(0);
                    for (int i = 0; i < channelDefinitions.Count; i++)
                    {
                        if (ChannelList.Count > 0)
                        {
                            ChannelsClass channel = ChannelList.Find(p => p.Id == channelDefinitions[i].sID);
                            if (channel != null)
                            {
                                channel.Offset = channelDefinitions[i].fOffset;
                                channel.Scale = channelDefinitions[i].fScale;
                            }
                        }
                    }
                }
                
                
            }
        }

        /// <summary>
        /// 获取采样点总数
        /// </summary>
        /// <returns>采样点总数</returns>
        public long GetTotalSampleCount()
        {
            long sampleCount = 0;
            if (IsLoadIndex)
            {
                if (MileageFix.FixData.Count > 0)
                {
                    sampleCount = CitFileProcess.GetSampleCountByRange(_citFilePath, MileageFix.FixData[0].MarkedStartPoint.FilePointer, MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.FilePointer);
                }
                else
                {
                    IsLoadIndex = false;
                    sampleCount = CitFileProcess.GetTotalSampleCount(_citFilePath);
                }
            }
            else
            {
                sampleCount = CitFileProcess.GetTotalSampleCount(_citFilePath);
            }
            return sampleCount;
        }

        /// <summary>
        /// 根据里程信息获取采样值，换算为采样点个数，同时获取坐标位置
        /// </summary>
        /// <param name="mileage">里程，单位为米</param>
        /// <param name="locationPostion"></param>
        /// <returns>采样点个数</returns>
        public long GetLocationSampleCount(float mileage, ref long locationPostion)
        {
            if (IsLoadIndex)
            {
                //if (CitFile.iKmInc == 0)
                //{

                //    Milestone stone= MileageFix.CalcMilestoneByFixedMilestone(mileage);
                //    //Milestone stone = _allStone.FindLast(p => p.GetMeter() <= mileage);
                //    if (stone != null)
                //    {
                //        locationPostion = stone.mFilePosition;
                //    }
                //    else
                //    {
                //        locationPostion = -1;
                //    }
                //}
                //else
                //{
                //    //Milestone stone = _allStone.FindLast(p => p.GetMeter() >= mileage);
                //    long postion = CitFileProcess.GetCurrentPositionByMilestone(_citFilePath, mileage, true);
                //    if (postion != -1)
                //    {
                //        locationPostion = postion;
                //    }
                //    else
                //    {
                //        locationPostion = -1;
                //    }
                //}
                Milestone stone = MileageFix.CalcMilestoneByFixedMilestone(mileage);
                if (stone != null)
                {
                    locationPostion = stone.mFilePosition;
                }
                else
                {
                    locationPostion = -1;
                }

            }
            else
            {
                locationPostion = CitFileProcess.GetCurrentPositionByMilestone(_citFilePath, mileage, true);
            }
            if (locationPostion != -1)
            {
                return GetLocationSampleCount(locationPostion);
            }
            return -1;
        }

        /// <summary>
        /// 根据坐标值获取采样点个数
        /// </summary>
        /// <param name="locationPostion">坐标值</param>
        /// <returns>居中后的采样点个数</returns>
        public long GetLocationSampleCount(long locationPostion)
        {
            long sampleCount = CitFileProcess.GetAppointSampleCount(_citFilePath, locationPostion);
          
            return sampleCount;
        }

        

        /// <summary>
        /// 根据采样点获取目标点
        /// </summary>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        public long GetPostionBySamapleCount(int sampleCount)
        {
           long[] postion= CitFileProcess.GetPositons(_citFilePath);
           return CitFileProcess.GetAppointEndPostion(_citFilePath, postion[0], sampleCount);
        }

        /// <summary>
        /// 获取所有的里程信息
        /// </summary>
        /// <returns></returns>
        public List<Milestone> GetAllMileage()
        {
            List<Milestone> _allStone = new List<Milestone>();
            _allStone = CitFileProcess.GetAllMileStone(_citFilePath);
            if (IsLoadIndex)
            {
                if (MileageFix.FixData.Count > 0)
                {
                    _allStone = MileageFix.GetMileageReviseData(_allStone);
                }
                else
                {
                    IsLoadIndex = false;
                }
            }
            return _allStone;
        }


        public List<Milestone> GetRangeMileage(int totalSmapleCount)
        {
            List<Milestone> rangeMileage = new List<Milestone>(WaveformDataCount);
            long endPostion = -1;
            long[] postions = CitFileProcess.GetPositons(_citFilePath);
            long postion= CitFileProcess.GetAppointEndPostion(CitFilePath, postions[0], totalSmapleCount);
            rangeMileage = CitFileProcess.GetMileStoneByRange(CitFilePath, postion, WaveformDataCount, ref endPostion);
            if (IsLoadIndex)
            {
                if (MileageFix.FixData.Count > 0)
                {
                    rangeMileage = MileageFix.GetMileageReviseData(rangeMileage);
                }
                else
                {
                    IsLoadIndex = false;
                }
            }
            return rangeMileage;
        }
        /// <summary>
        /// 根据采样个数获取对应位置
        /// </summary>
        /// <param name="sampleCount">采样点个数</param>
        /// <param name="isReviseValue">是否进行偏移</param>
        /// <returns>采样点的位置</returns>
        public long GetAppointEndPostion(int sampleCount)
        {
            long endPos = 0;
            if (File.Exists(_citFilePath))
            {
                //int sampleCount = GetScrollValueToSampleNum(scrollValue);
                long[] postions = CitFileProcess.GetPositons(_citFilePath);
                //int sampleNum = GetScrollValueToSampleNum(scrollValue);
                if(LayerConfig.IsReverse)
                {
                    if (sampleCount == 0)
                    {
                        endPos = postions[1];
                    }
                    else
                    {
                        endPos = CitFileProcess.GetAppointEndPostion(_citFilePath, postions[1], sampleCount * -1);
                    }
                }
                else
                {
                    if (sampleCount == 0)
                    {
                        endPos = postions[0];
                    }
                    else
                    {
                        endPos = CitFileProcess.GetAppointEndPostion(_citFilePath, postions[0], sampleCount);
                    }
                }
               
                //if (isReviseValue)
                //{
                //    if (LayerConfig.IsReverse)
                //    {
                //        endPos -= ReviseValue * _citFile.iChannelNumber * 2;
                //    }
                //    else
                //    {
                //        endPos += ReviseValue * _citFile.iChannelNumber * 2;
                //    }
                //}

            }
            return endPos;
        }

        /// <summary>
        /// 根据索引导出CIT文件
        /// </summary>
        /// <param name="filePath">cit文件路径</param>
        /// <param name="startMileage">开始里程(公里)</param>
        /// <param name="endMileage">结束里程(公里)</param>
        /// <returns>成功：返回导出路径，失败：空字符串</returns>
        public string ExportCITFileAndIndexData(string filePath, double startMileage, double endMileage)
        {
            string destFile = filePath + "\\" +
                       Path.GetFileNameWithoutExtension(CitFilePath) + "_" +
                       startMileage.ToString() + "-" + endMileage.ToString() + ".cit";
            if (_citFile.iKmInc == 1 && startMileage < endMileage)
            {
                double change = startMileage;
                startMileage = endMileage;
                endMileage = change;
            }
            if (_citFile != null && MileageFix.FixData.Count > 0)
            {
                if (_citFile.iKmInc == 0)
                {
                    if (startMileage >= MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000
                        || endMileage <= MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000)
                    {
                        return "";
                    }
                    if (startMileage< ( MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000))
                    {
                        startMileage = (MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000);
                    }
                    if(endMileage> MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000)
                    {
                        endMileage = MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000;
                    }
                }
                else if (_citFile.iKmInc == 1)
                {
                    if (endMileage >= MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000
                        || startMileage <= MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000)
                    {
                        return "";
                    }
                    if(startMileage> MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000)
                    {
                        startMileage = MileageFix.FixData[0].MarkedStartPoint.UserSetMileage / 1000;
                    }
                    if(endMileage< MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000)
                    {
                        endMileage = MileageFix.FixData[MileageFix.FixData.Count - 1].MarkedEndPoint.UserSetMileage / 1000;
                    }
                }
                Milestone startStone = new Milestone();
                Milestone endStone = new Milestone();
                startStone = MileageFix.CalcMilestoneByFixedMilestone((float)startMileage * 1000);
                endStone = MileageFix.CalcMilestoneByFixedMilestone((float)endMileage * 1000);
                if (startStone.mFilePosition != -1 && endStone.mFileEndPostion != -1)
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    List<double[]> channelData = CitFileProcess.GetAllChannelDataInRange(_citFilePath, startStone.mFilePosition, endStone.mFileEndPostion);
                    List<ChannelDefinition> channelDefinitionList = CitFileProcess.GetChannelDefinitionList(_citFilePath);
                    if (CitFileProcess.WriteCitFile(destFile, CitFile, channelDefinitionList, "", channelData))
                    {
                        CitFileProcess.SetKmFrom(destFile, (float)startStone.GetMeter() / 1000);
                        CitFileProcess.SetKmTo(destFile, (float)endStone.GetMeter() / 1000);
                        string indexFile = destFile.Replace(".cit", ".idf");
                        IOperator newOperator = new IndexOperator();
                        newOperator.IndexFilePath = indexFile;
                        //IndexOperator.CreateDB(indexFile);
                        long fileHead = FileDataOffset.GetSamplePointStartOffset(channelData.Count, 4);
                        AddNewIndexData(destFile, newOperator, fileHead, startStone.mFilePosition, endStone.mFileEndPostion, channelData.Count);
                        AddInvaildData(newOperator, fileHead, startStone.mFilePosition, endStone.mFileEndPostion, channelData.Count);
                        return destFile;
                    }
                    return "";
                }
                return "";


            }
            else
            {
               return ExportOnlyCITFile(destFile, startMileage, endMileage);
            }
        }

        /// <summary>
        /// 添加新索引数据
        /// </summary>
        /// <param name="citHeadEndPostion">文件头部结束为止</param>
        /// <param name="fileStartPostion">数据开始位置</param>
        /// <param name="fileEndPostion">数据结束位置</param>
        /// <param name="channelNumber">通道个数</param>
        /// <returns></returns>
        private bool AddNewIndexData(string destCitPath,IOperator NewIndexOperator,long citHeadEndPostion, long fileStartPostion,long fileEndPostion,int channelNumber)
        {
            string sqlCmd = "delete from IndexSta";
            NewIndexOperator.ExcuteSql(sqlCmd);
            long fileHead = citHeadEndPostion;
            if (MileageFix.FixData != null && MileageFix.FixData.Count > 0)
            {
                for (int i = 0; i < MileageFix.FixData.Count; i++)
                {
                    double startMeter = MileageFix.FixData[i].MarkedStartPoint.UserSetMileage;
                    double endMeter = MileageFix.FixData[i].MarkedEndPoint.UserSetMileage;
                    long startPoint = MileageFix.FixData[i].MarkedStartPoint.FilePointer;
                    long endPoint = MileageFix.FixData[i].MarkedEndPoint.FilePointer;
                    double containsMeter = MileageFix.FixData[i].RealDistance;
                    long containsPoint = MileageFix.FixData[i].SamplePointCount;
                    double meterPerPoint = containsMeter / (containsPoint-1);

                    int id = 0;
                    int indexIdValue = 0;
                    long startPositionValue = 0;
                    double startMeterValue = 0;
                    long endPositionValue = 0;
                    double endMeterValue = 0;
                    long containsPointValue = 0;
                    double containsMeterValue = 0;
                    string indexTypeValue = "";

                    string sqlFormat = "insert into IndexSta values({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
                    string sqlText = string.Format(sqlFormat,id, indexIdValue.ToString(), startPositionValue.ToString(), startMeterValue.ToString(), endPositionValue.ToString(), endMeterValue.ToString(), containsPointValue.ToString(), containsMeterValue.ToString(), indexTypeValue);
                    id = i;
                    indexIdValue = 1;
                    //第一段索引
                    if (startPoint <= fileStartPostion && fileStartPostion < endPoint)
                    {
                        startPositionValue = citHeadEndPostion;
                        startMeterValue = Math.Round(startMeter + ((endMeter - startMeter) / containsPoint) * ((fileStartPostion - startPoint) / (channelNumber * 2)), 3);
                        //如果分割的波形都在同一段索引内
                        if (startPoint < fileEndPostion && fileEndPostion <= endPoint)
                        {
                            endPositionValue = fileEndPostion - fileStartPostion + startPositionValue;
                            endMeter = Math.Round(startMeter + ((endMeter - startMeter) / containsPoint) * ((fileEndPostion - startPoint) / (channelNumber * 2)), 3);

                            containsPoint = (endPositionValue - startPositionValue) / (channelNumber * 2);
                            containsMeterValue = Math.Abs(startMeterValue - endMeterValue);
                            indexTypeValue = "正常";

                            sqlText = string.Format(sqlFormat,id, indexIdValue.ToString(), startPositionValue.ToString(), startMeterValue.ToString(), endPositionValue.ToString(), endMeterValue.ToString(), containsPointValue.ToString(), containsMeterValue.ToString(), indexTypeValue);
                            //DBOperator.ExcuteSqlInCommonDB(sqlText);
                            NewIndexOperator.ExcuteSql(sqlText);
                            fileHead = endPositionValue;

                            break;
                        }

                        endPositionValue = endPoint - fileStartPostion + startPositionValue;
                        endMeterValue = endMeter;

                        containsPointValue = (endPositionValue - startPositionValue) / (channelNumber * 2);
                        containsMeterValue = Math.Abs(startMeterValue - endMeterValue);
                        indexTypeValue = "正常";

                        sqlText = string.Format(sqlFormat, id, indexIdValue.ToString(), startPositionValue.ToString(), startMeterValue.ToString(), endPositionValue.ToString(), endMeterValue.ToString(), containsPointValue.ToString(), containsMeterValue.ToString(), indexTypeValue);
                        NewIndexOperator.ExcuteSql(sqlText);
                        fileHead = endPositionValue;

                    }
                    //中间段索引
                    if ((fileStartPostion < startPoint && fileStartPostion < endPoint) && (fileEndPostion > startPoint && fileEndPostion > endPoint))
                    {
                        startPositionValue = fileHead; ;
                        startMeterValue = startMeter;
                        endPositionValue = endPoint - startPoint + startPositionValue;
                        endMeterValue = endMeter;

                        containsPointValue = (endPositionValue - startPositionValue) / (channelNumber * 2);
                        containsMeterValue = Math.Abs(startMeterValue - endMeterValue);
                        indexTypeValue = "正常";

                        sqlText = string.Format(sqlFormat,id, indexIdValue.ToString(), startPositionValue.ToString(), startMeterValue.ToString(), endPositionValue.ToString(), endMeterValue.ToString(), containsPointValue.ToString(), containsMeterValue.ToString(), indexTypeValue);
                        NewIndexOperator.ExcuteSql(sqlText);
                        fileHead = endPositionValue;
                    }
                    //最后一段索引
                    if (startPoint < fileEndPostion && fileEndPostion <= endPoint)
                    {
                        startPositionValue = fileHead;
                        startMeterValue = startMeter;
                        endPositionValue = fileEndPostion - startPoint + startPositionValue;
                        endMeterValue = Math.Round(startMeter + ((endMeter - startMeter) / containsPoint) * ((fileEndPostion - startPoint) / (channelNumber * 2)), 3);

                        containsPointValue = (endPositionValue - startPositionValue) / (channelNumber * 2);
                        containsMeterValue = Math.Abs(startMeterValue - endMeterValue);
                        indexTypeValue = "正常";

                        sqlText = string.Format(sqlFormat,id, indexIdValue.ToString(), startPositionValue.ToString(), startMeterValue.ToString(), endPositionValue.ToString(), endMeterValue.ToString(), containsPointValue.ToString(), containsMeterValue.ToString(), indexTypeValue);
                        NewIndexOperator.ExcuteSql(sqlText);
                        fileHead = endPositionValue;
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 添加无效数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="citHeadEndPostion">文件头部结束位置</param>
        /// <param name="fileStartPostion">数据开始位置</param>
        /// <param name="fileEndPostion">数据结束位置</param>
        /// <param name="channelNumber">通道个数</param>
        /// <returns></returns>
        private bool AddInvaildData(IOperator newIndexOperaotr,long citHeadEndPostion, long fileStartPostion, long fileEndPostion, int channelNumber)
        {
            
            //DBOperator.CommonDbConnectString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Jet OLEDB:Engine Type=5";
            string sqlCmd = "delete from InvalidData";
            newIndexOperaotr.ExcuteSql(sqlCmd);
            long fileDataHead = citHeadEndPostion;
            if (InvalidDataList != null&& InvalidDataList.Count > 0)
            {
                for (int i = 0; i < InvalidDataList.Count; i++)
                {
                    long startPoint = long.Parse(InvalidDataList[i].sStartPoint);
                    long endPoint = long.Parse(InvalidDataList[i].sEndPoint);
                    double startMeter = double.Parse(InvalidDataList[i].sStartMile);
                    double endMeter = double.Parse(InvalidDataList[i].sEndMile);
                    int invalidType = InvalidDataList[i].iType;
                    string memoText = InvalidDataList[i].sMemoText;
                    int isShow = InvalidDataList[i].iIsShow;
                    string channelType = InvalidDataList[i].ChannelType;

                    int idValue = 0;
                    long startPositionValue = 0;
                    long endPositionValue = 0;
                    double startMeterValue = 0;
                    double endMeterValue = 0;
                    int invalidTypeValue = invalidType;
                    string memoTextValue = memoText;
                    int isShowValue = isShow;
                    string channelTypeValue = channelType;

                    string sqlFormat = "insert into InvalidData values({0},'{1}','{2}','{3}','{4}',{5},'{6}',{7},'{8}')";
                    string sqlText = string.Format(sqlFormat, idValue.ToString(), startPositionValue.ToString(), endPositionValue.ToString(), startMeterValue.ToString(), endMeterValue.ToString(), invalidTypeValue.ToString(), memoTextValue, isShowValue.ToString(), channelTypeValue);

                    idValue = i;


                    //第一种情形：无效区段完全在分割的波形内
                    if (startPoint >= fileStartPostion && endPoint <= fileEndPostion)
                    {

                        startPositionValue = fileDataHead + startPoint - fileStartPostion;
                        startMeterValue = startMeter;
                        endPositionValue = endPoint - startPoint + startPositionValue;
                        endMeterValue = endMeter;

                        sqlText = String.Format(sqlFormat, idValue.ToString(), startPositionValue.ToString(), endPositionValue.ToString(), startMeterValue.ToString(), endMeterValue.ToString(), invalidTypeValue.ToString(), memoTextValue, isShowValue.ToString(), channelTypeValue);
                        newIndexOperaotr.ExcuteSql(sqlText);
                    }
                    //第二种情形：无效区段的跨越分割波形的起始部分
                    if (startPoint < fileStartPostion && fileStartPostion < endPoint)
                    {
                        startPositionValue = fileDataHead; ;
                        startMeterValue = startMeter + (0.25 * (fileStartPostion - startPoint) / (channelNumber * 2)) / 1000;
                        endPositionValue = endPoint - fileStartPostion + startPositionValue;
                        endMeterValue = endMeter;

                        sqlText = String.Format(sqlFormat, idValue.ToString(), startPositionValue.ToString(), endPositionValue.ToString(), startMeterValue.ToString(), endMeterValue.ToString(), invalidTypeValue.ToString(), memoTextValue, isShowValue.ToString(), channelTypeValue);
                        newIndexOperaotr.ExcuteSql(sqlText);
                    }
                    //第三种情形：无效区段的跨越分割波形的结束部分
                    if (startPoint < fileEndPostion && fileEndPostion < endPoint)
                    {
                        startPositionValue = fileDataHead + startPoint - fileEndPostion;
                        startMeterValue = startMeter;
                        endPositionValue = endPoint - startPoint + startPositionValue;
                        endMeterValue = startMeter + (0.25 * (fileEndPostion - startPoint) / (channelNumber * 2)) / 1000;

                        sqlText = String.Format(sqlFormat, idValue.ToString(), startPositionValue.ToString(), endPositionValue.ToString(), startMeterValue.ToString(), endMeterValue.ToString(), invalidTypeValue.ToString(), memoTextValue, isShowValue.ToString(), channelTypeValue);
                        newIndexOperaotr.ExcuteSql(sqlText);
                    }
                    
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 导出CIT文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="filePath">导出路径</param>
        /// <param name="startMileage">起始里程(单位：米)</param>
        /// <param name="endMileage">结束里程(单位：米)</param>
        /// <returns></returns>
        public string ExportOnlyCITFile(string filePath, double startMileage, double endMileage)
        {
            try
            {
                if (_citFile != null)
                {
                   
                    long startPostion = -1;
                    long endPostion = -1;

                    startPostion = CitFileProcess.GetCurrentPositionByMilestone(_citFilePath, (float)startMileage, true);
                    endPostion = CitFileProcess.GetCurrentPositionByMilestone(_citFilePath, (float)endMileage, true);

                    if (startPostion == -1 || endPostion == -1)
                    {
                        return "";
                    }

                    if (CitFileProcess.WriteCitFile(filePath, _citFile, CitFileProcess.GetChannelDefinitionList(_citFilePath), ""))
                    {
                        if (startPostion > endPostion)
                        {
                            long temp = startPostion;
                            startPostion = endPostion;
                            endPostion = temp;
                        }
                        long sampleCount = CitFileProcess.GetSampleCountByRange(_citFilePath, startPostion, endPostion);
                        int sampleNumber = 5000;

                        if (sampleCount > sampleNumber)
                        {
                            long pageCount = (sampleCount / sampleNumber);

                            long pageEndPostion = 0;
                            for (int i = 0; i < pageCount; i++)
                            {
                                List<double[]> channelData = CitFileProcess.GetAllChannelDataInRange(_citFilePath, startPostion, sampleNumber, ref pageEndPostion);
                                startPostion = pageEndPostion;
                                CitFileProcess.WriteCitChannelData(filePath, channelData);
                            }

                            if (pageEndPostion < endPostion)
                            {
                                List<double[]> channelData = CitFileProcess.GetAllChannelDataInRange(_citFilePath, startPostion, endPostion);
                                CitFileProcess.WriteCitChannelData(filePath, channelData);
                            }

                        }
                        else
                        {
                            List<double[]> channelData = CitFileProcess.GetAllChannelDataInRange(_citFilePath, startPostion, endPostion);
                            CitFileProcess.WriteCitChannelData(filePath, channelData);
                        }
                        if (CitFileProcess.SetKmFrom(filePath, (float)startMileage / 1000)
                            && CitFileProcess.SetKmTo(filePath, (float)endMileage / 1000))
                        {
                            return filePath;
                        }

                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
