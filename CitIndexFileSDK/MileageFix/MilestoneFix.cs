using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitFileSDK;
using System.Data;

namespace CitIndexFileSDK.MileageFix
{
    public class MilestoneFix
    {
        private LongChainTable _longChainTable;
        private UserFixedTable _userFixedTable;
        private List<MileStoneFixData> _fixData;
        private float _meanSampleRate;

        private CITFileProcess _citProcess;
        private FileInformation _citFileInfo;
        private string _citFilePath;

        private IOperator _indexOperator = null;

        public List<MileStoneFixData> FixData
        {
            get
            {
                return _fixData;
            }
        }

        public MilestoneFix(string citFilePath, IOperator indexOperator)
        {
            _indexOperator = indexOperator;
            _citProcess = new CITFileProcess();
            _citFilePath = citFilePath;
            _citFileInfo = _citProcess.GetFileInformation(citFilePath);
            _longChainTable = new LongChainTable(_citFileInfo.sTrackCode, _citFileInfo.iKmInc, _citFileInfo.iDir);
            _userFixedTable = new UserFixedTable(indexOperator, _citFileInfo.iKmInc);
            _fixData = new List<MileStoneFixData>();
        }

        /// <summary>
        /// 运行里程修正算法
        /// </summary>
        public void RunFixingAlgorithm()
        {
            if (_userFixedTable.MarkedPoints.Count > 0)
            {
                CollectLongChainsBtwMarkedPoints();
                CalcDistanceBtwMarkedPoints();
                CalcMeanSampleDistance();
                CalcCitStartMileStone();
                CalcCitEndMileStone();
            }
        }

        /// <summary>
        /// 读取里程修正表
        /// </summary>
        public void ReadMilestoneFixTable()
        {
            string cmdText = "select * from IndexSta order by id";
            DataTable dt = _indexOperator.Query(cmdText);
            if (dt != null && dt.Rows.Count > 0)
            {
                _fixData = new List<MileStoneFixData>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MileStoneFixData data = new MileStoneFixData();
                    data.ID = dt.Rows[i]["Id"].ToString();
                    data.MarkedStartPoint = new UserMarkedPoint();
                    data.MarkedStartPoint.FilePointer = long.Parse(dt.Rows[i]["StartPoint"].ToString());
                    data.MarkedStartPoint.UserSetMileage = float.Parse(dt.Rows[i]["StartMeter"].ToString());
                    data.MarkedEndPoint = new UserMarkedPoint();
                    data.MarkedEndPoint.FilePointer = long.Parse(dt.Rows[i]["EndPoint"].ToString());
                    data.MarkedEndPoint.UserSetMileage = float.Parse(dt.Rows[i]["EndMeter"].ToString());
                    data.SamplePointCount = long.Parse(dt.Rows[i]["ContainsPoint"].ToString());
                    data.RealDistance = Math.Abs(data.MarkedEndPoint.UserSetMileage - data.MarkedStartPoint.UserSetMileage);
                    data.SampleRate = data.RealDistance / (data.SamplePointCount - 1);
                    if (i == 0 || i == dt.Rows.Count - 1)
                    {
                        data.Chains = new List<LongChain>();
                    }
                    else
                    {
                        data.Chains = _longChainTable.GetChains(data.MarkedStartPoint.UserSetMileage, data.MarkedStartPoint.UserSetMileage);
                    }
                    _fixData.Add(data);
                }
            }
        }
        /// <summary>
        /// 保存里程修正表
        /// </summary>
        public void SaveMilestoneFixTable()
        {
            string cmdText = "delete from IndexSta";
            _indexOperator.ExcuteSql(cmdText);
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    cmdText = "insert into IndexSta values(" + (i + 1) + "," + 1 +
                        ",'" + _fixData[i].MarkedStartPoint.FilePointer.ToString() + "','" + _fixData[i].MarkedStartPoint.UserSetMileage.ToString() +
                        "','" + _fixData[i].MarkedEndPoint.FilePointer.ToString() + "','" + _fixData[i].MarkedEndPoint.UserSetMileage.ToString() + "','" +
                        _fixData[i].SamplePointCount.ToString() + "','" + (_fixData[i].RealDistance).ToString() +
                        "','" + "正常" + "')";
                    _indexOperator.ExcuteSql(cmdText);
                }
            }
        }

        /// <summary>
        /// 清除里程修正表
        /// </summary>
        public void ClearMilestoneFixTable()
        {
            string cmdText = "delete from IndexSta";
            if(_indexOperator.ExcuteSql(cmdText))
            {
                _fixData.Clear();
            }
        }

        /// <summary>
        /// 获取里程修正后的数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="idfFile">索引文件路径</param>
        /// <param name="sourceMilestongList">未修正的里程数据</param>
        /// <returns>修正后的里程数据</returns>
        public List<Milestone> GetMileageReviseData(List<Milestone> sourceMilestongList)
        {
            List<Milestone> reviseMilestongList = new List<Milestone>(sourceMilestongList);
            
            int indexRevise = 0;
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    int mileStoneStartIndex = -1;
                    int mileStoneEndIndex = -1;
                    float sampleDistance = 0;
                    long sampleCount = 0;
                    if (reviseMilestongList[indexRevise].mFilePosition >= _fixData[i].MarkedStartPoint.FilePointer)
                    {
                        mileStoneStartIndex = reviseMilestongList.FindIndex(p => p.mFilePosition == reviseMilestongList[indexRevise].mFilePosition);
                        sampleDistance = _fixData[i].SampleRate;
                        sampleCount = _citProcess.GetSampleCountByRange(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, reviseMilestongList[indexRevise].mFilePosition);
                        if (reviseMilestongList[reviseMilestongList.Count - 1].mFilePosition <= _fixData[i].MarkedEndPoint.FilePointer)
                        {
                            mileStoneEndIndex = reviseMilestongList.FindIndex(m => m.mFilePosition == reviseMilestongList[reviseMilestongList.Count - 1].mFilePosition);
                        }
                        else
                        {
                            mileStoneEndIndex = reviseMilestongList.FindIndex(m => m.mFilePosition == _fixData[i].MarkedEndPoint.FilePointer);
                            if (mileStoneEndIndex == -1)
                            {
                                continue;
                            }
                            else
                            {
                                indexRevise = mileStoneEndIndex;
                            }
                        }
                        int index = 0;
                        bool isChain = false;
                        long currentMileage = 0;

                        double startMileage = _fixData[i].MarkedStartPoint.UserSetMileage + sampleDistance * (sampleCount);
                        
                        for (int j = mileStoneStartIndex; j <= mileStoneEndIndex; j++)
                        {
                            double kMiles = (startMileage + index * sampleDistance) / 1000;
                            int chainIndex = _fixData[i].Chains.FindIndex(m => m.Km == (long)kMiles);
                            if (isChain || chainIndex != -1)
                            {
                                if (isChain == false && chainIndex != -1)
                                {
                                    currentMileage = (long)kMiles;
                                    isChain = true;
                                }
                                reviseMilestongList[j].mKm = currentMileage;
                                reviseMilestongList[j].mMeter = (float)(kMiles - reviseMilestongList[j].mKm) * 1000;
                                if (_fixData[i].Chains[chainIndex].IsLongChain())
                                {
                                    if (reviseMilestongList[j].mMeter >= (1000 + _fixData[i].Chains[chainIndex].ExtraLength))
                                    {
                                        reviseMilestongList[j].mKm = currentMileage + 1;
                                        reviseMilestongList[j].mMeter = reviseMilestongList[j].mMeter - (1000 + _fixData[i].Chains[chainIndex].ExtraLength);
                                        startMileage = reviseMilestongList[j].mKm;
                                        isChain = false;
                                    }
                                }
                                else
                                {
                                    if (reviseMilestongList[j].mMeter >= (1000 + _fixData[i].Chains[chainIndex].ExtraLength))
                                    {
                                        reviseMilestongList[j].mKm = currentMileage + 1;
                                        reviseMilestongList[j].mMeter = reviseMilestongList[j].mMeter - (1000 + _fixData[i].Chains[chainIndex].ExtraLength);
                                        startMileage = reviseMilestongList[j].mKm;
                                        isChain = false;
                                    }
                                }
                            }
                            else
                            {
                                reviseMilestongList[j].mKm = (long)kMiles;
                                reviseMilestongList[j].mMeter = (float)Math.Round((kMiles - reviseMilestongList[j].mKm)*1000, 2);
                            }
                            index++;
                        }
                        if (mileStoneEndIndex == reviseMilestongList.Count - 1)
                        {
                            break;
                        }
                    }
                }

            }
            return reviseMilestongList;
        }

        /// <summary>
        /// 通过文件中的点偏移得到对应修正后的里程
        /// </summary>
        /// <param name="pointPointerInFile">在文件中的偏移位置</param>
        /// <returns>修正后的里程(米)</returns>
        public float CalcPointMileStone(long pointPointerInFile)
        {
            float startMileage = -1;
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    if (pointPointerInFile >= _fixData[i].MarkedStartPoint.FilePointer && pointPointerInFile <= _fixData[i].MarkedEndPoint.FilePointer)
                    {
                        long sampleCount = _citProcess.GetSampleCountByRange(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, pointPointerInFile);
                        startMileage = _fixData[i].MarkedStartPoint.UserSetMileage + _fixData[i].SampleRate * (sampleCount - 1);
                    }
                }
            }
            return startMileage;
        }

        /// <summary>
        /// 根据里程获取修正后的里程
        /// </summary>
        /// <param name="pointMileStone">未修正的里程</param>
        /// <returns>修正后的里程</returns>
        public float CalcPointMileStone(float pointMileStone)
        {
            long postionInFile = _citProcess.GetCurrentPositionByMilestone(_citFilePath, pointMileStone,false);
            if (postionInFile != -1)
            {
                return CalcPointMileStone(postionInFile);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据修正后的里程点得到文件中的原始偏移量
        /// </summary>
        /// <param name="mileStone">修正后的里程(米)</param>
        /// <returns>原始偏移量</returns>
        public long CalcPointFilePointerByFixedMilestone(float mileStone)
        {
            long targetPosion = -1;
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    if (mileStone >= _fixData[i].MarkedStartPoint.UserSetMileage &&
                        mileStone <= FixData[i].MarkedEndPoint.UserSetMileage)
                    {
                        float diff = mileStone - _fixData[i].MarkedStartPoint.UserSetMileage;
                        int sampleCount = (int)(diff / _fixData[i].SampleRate) + 1;
                        targetPosion = _citProcess.GetAppointEndPostion(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, sampleCount);
                        break;
                    }
                }
            }
            return targetPosion;
        }

        /// <summary>
        /// 根据修正后的里程点得到文件中的原始偏移量
        /// </summary>
        /// <param name="mileStone">修正后的里程(米)</param>
        /// <returns>原始偏移量</returns>
        public Milestone CalcMilestoneByFixedMilestone(float mileStone)
        {
            Milestone mstone = new Milestone();
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    if (mileStone >= _fixData[i].MarkedStartPoint.UserSetMileage &&
                        mileStone <= FixData[i].MarkedEndPoint.UserSetMileage)
                    {
                        float diff = mileStone - _fixData[i].MarkedStartPoint.UserSetMileage;
                        int sampleCount = (int)(diff / _fixData[i].SampleRate) + 1;
                        //targetPosion = _citProcess.GetAppointEndPostion(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, sampleCount);
                        mstone= _citProcess.GetAppointMilestone(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, sampleCount);
                        break;
                    }
                }
            }
            return mstone;
        }

        /// <summary>
        /// 根据两个点，获取长短链
        /// </summary>
        private void CollectLongChainsBtwMarkedPoints()
        {
            if(IsVaild())
            {
                for (int i = 0; i < _userFixedTable.MarkedPoints.Count - 1; i++)
                {
                    UserMarkedPoint startPoint = _userFixedTable.MarkedPoints[i];
                    UserMarkedPoint endPoint = _userFixedTable.MarkedPoints[i + 1];
                    List<LongChain> longChains = _longChainTable.GetChains(startPoint.UserSetMileage, endPoint.UserSetMileage);
                    MileStoneFixData data = new MileStoneFixData();
                    data.MarkedStartPoint = startPoint;
                    data.MarkedEndPoint = endPoint;
                    data.Chains = longChains;
                    _fixData.Add(data);
                }
            }
            else
            {
                throw new InvalidOperationException("标记点必须为偶数！");
            }
        }
        /// <summary>
        /// 计算两个标记点的实际距离
        /// </summary>
        private void CalcDistanceBtwMarkedPoints()
        {
            if (_fixData != null && _fixData.Count > 0)
            {
                for (int i = 0; i < _fixData.Count; i++)
                {
                    float longChainNumber = 0;
                    float shortChainNumber = 0;
                    if (_fixData[i].Chains.Count > 0)
                    {
                        foreach (var chain in _fixData[0].Chains)
                        {
                            if (chain.IsLongChain())
                            {
                                longChainNumber += chain.ExtraLength;
                            }
                            else
                            {
                                shortChainNumber += chain.ExtraLength;
                            }
                        }
                    }
                    //因为短链是负的，所以直接相加
                    _fixData[i].RealDistance = _fixData[i].MarkedEndPoint.UserSetMileage - _fixData[i].MarkedStartPoint.UserSetMileage + longChainNumber + shortChainNumber;
                    _fixData[i].SamplePointCount = _citProcess.GetSampleCountByRange(_citFilePath, _fixData[i].MarkedStartPoint.FilePointer, _fixData[i].MarkedEndPoint.FilePointer);
                    
                }
            }
        }

        /// <summary>
        /// 计算标记点的采样间距和平均采样间距
        /// </summary>
        private void CalcMeanSampleDistance()
        {
            if (_fixData != null && _fixData.Count > 0)
            {
                float totalSampleRate = 0;
                for (int i = 0; i < _fixData.Count; i++)
                {
                    _fixData[i].SampleRate = _fixData[i].RealDistance  / (_fixData[i].SamplePointCount - 1);
                    totalSampleRate += _fixData[i].SampleRate;
                }
                _meanSampleRate = totalSampleRate / _fixData.Count;
            }
        }

        /// <summary>
        /// 计算起始里程
        /// </summary>
        private void CalcCitStartMileStone()
        {
            if (_fixData != null && _fixData.Count > 0)
            {
                long markedStartPostion = _fixData[0].MarkedStartPoint.FilePointer;
                long sampleCount = _citProcess.GetAppointSampleCount(_citFilePath, markedStartPostion);
                if (sampleCount > 0)
                {
                    MileStoneFixData data = new MileStoneFixData();
                    data.MarkedEndPoint = _fixData[0].MarkedStartPoint;
                    data.SamplePointCount = sampleCount;
                    data.SampleRate = _meanSampleRate;
                    UserMarkedPoint markedStartPoint = new UserMarkedPoint();
                    long[] points = _citProcess.GetPositons(_citFilePath);
                    markedStartPoint.FilePointer = points[0];
                    //if (_citFileInfo.iKmInc == 1)
                    //{
                    //    markedStartPoint.UserSetMileage = data.MarkedEndPoint.UserSetMileage + (data.SamplePointCount - 1) * data.SampleRate;
                    //}
                    //else
                    //{
                    //    markedStartPoint.UserSetMileage = data.MarkedEndPoint.UserSetMileage - (data.SamplePointCount - 1) * data.SampleRate;
                    //}
                    markedStartPoint.UserSetMileage = data.MarkedEndPoint.UserSetMileage - (data.SamplePointCount - 1) * data.SampleRate;
                    markedStartPoint.FilePointer = points[0];
                   
                    data.MarkedStartPoint = markedStartPoint;
                    data.RealDistance = data.MarkedEndPoint.UserSetMileage - data.MarkedStartPoint.UserSetMileage;
                    _fixData.Insert(0, data);
                }
            }
        }

        /// <summary>
        /// 计算结束里程
        /// </summary>
        private void CalcCitEndMileStone()
        {
            if (_fixData != null && _fixData.Count > 0)
            {
                long markedEndPostion = _fixData[_fixData.Count - 1].MarkedEndPoint.FilePointer;
                long[] points = _citProcess.GetPositons(_citFilePath);
                long sampleCount = _citProcess.GetSampleCountByRange(_citFilePath, markedEndPostion, points[1]);
                MileStoneFixData data = new MileStoneFixData();
                data.MarkedStartPoint = _fixData[_fixData.Count - 1].MarkedEndPoint;
                data.SamplePointCount = sampleCount;
                data.SampleRate = _meanSampleRate;
                UserMarkedPoint markedEndPoint = new UserMarkedPoint();
                markedEndPoint.FilePointer = points[1];
                //if (_citFileInfo.iKmInc == 1)
                //{
                //    markedEndPoint.UserSetMileage = data.MarkedStartPoint.UserSetMileage - (data.SamplePointCount - 1) * data.SampleRate;
                //}
                //else
                //{
                //    markedEndPoint.UserSetMileage = data.MarkedStartPoint.UserSetMileage + (data.SamplePointCount - 1) * data.SampleRate;
                //}
                markedEndPoint.UserSetMileage = data.MarkedStartPoint.UserSetMileage + (data.SamplePointCount - 1) * data.SampleRate;
                data.MarkedEndPoint = markedEndPoint;
                data.RealDistance = data.MarkedEndPoint.UserSetMileage - data.MarkedStartPoint.UserSetMileage;
                _fixData.Add(data);

            }
        }

        private bool IsVaild()
        {
            return _userFixedTable.MarkedPoints.Count >= 2;
        }


    }
}
