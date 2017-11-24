using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CitFileSDK;
using System.IO;
using CitIndexFileSDK.MileageFix;

namespace CitIndexFileSDK.IntelligentMileageFix
{
    public class IntelligentMilestoneFix
    {
        private CITFileProcess _citProcess = new CITFileProcess();
        /// <summary>
        /// 修正文件取点个数单侧
        /// </summary>
        private int _fixedSamplingCount = 100;
        /// <summary>
        /// 要修正文件单侧去点个数
        /// </summary>
        private int _targetSamplingCount = 2000;
        /// <summary>
        /// 要对比的通道英文名以及阈值
        /// </summary>
        public List<FixParam> FixParams { get; set; }

        /// <summary>
        /// 修正文件单侧取点个数
        /// </summary>
        public int FixedSamplingCount
        {
            get
            {
                return _fixedSamplingCount;
            }

            set
            {
                _fixedSamplingCount = value;
            }
        }
        /// <summary>
        /// 目标文件单侧取点个数
        /// </summary>
        public int TargetSamplingCount
        {
            get
            {
                return _targetSamplingCount;
            }

            set
            {
                _targetSamplingCount = value;
            }
        }
        /// <summary>
        /// 得到的修正点
        /// </summary>
        private Dictionary<UserMarkedPoint, List<FixPoint>> _fixedData = new Dictionary<UserMarkedPoint, List<FixPoint>>();
        /// <summary>
        /// 修正结果
        /// </summary>
        private Dictionary<UserMarkedPoint, List<CorrelationResult>> _CorrResult = new Dictionary<UserMarkedPoint, List<CorrelationResult>>();

        public IntelligentMilestoneFix()
        {
            FixParams = new List<FixParam>();
        }

        public void InitFixData(string fixedCitFile, string fixedIdfFile = null, bool diffFileName = false)
        {
            List<ChannelDefinition> channelDefinition = _citProcess.GetChannelDefinitionList(fixedCitFile);
            for (int i = 0; i < FixParams.Count; i++)
            {
                FixParams[i].ChannelID = channelDefinition.Where(p => p.sNameEn == FixParams[i].ChannelName).ToList()[0].sID;
            }
            FileInformation citFile = _citProcess.GetFileInformation(fixedCitFile);
            _fixedData.Clear();
           
            string idfFIle= fixedCitFile.Replace(".cit", ".idf");

            if (diffFileName)
                idfFIle = fixedIdfFile;

            IndexOperator indexOperator = new IndexOperator();
            if (File.Exists(idfFIle))
            {
                indexOperator.IndexFilePath = idfFIle;
                UserFixedTable fixTable = new UserFixedTable(indexOperator, citFile.iKmInc);
                if (fixTable.MarkedPoints.Count > 0)
                {                  
                    foreach (var point in fixTable.MarkedPoints)
                    {
                        //左半边数据点个数，包括中间点
                        List<FixPoint> fixPoint = new List<FixPoint>();
                        int leftFixedCount = FixedSamplingCount + 1;
                        long leftStartPostion = _citProcess.GetAppointFileEndPostion(fixedCitFile, point.FilePointer, -1 * leftFixedCount);
                        long realLeftCount = _citProcess.GetSampleCountByRange(fixedCitFile, leftStartPostion, point.FilePointer);
                        long rightEndPostion = _citProcess.GetAppointFileEndPostion(fixedCitFile, point.FilePointer, FixedSamplingCount);
                        long realRightCount = _citProcess.GetSampleCountByRange(fixedCitFile, point.FilePointer, rightEndPostion);
                        Milestone mile = _citProcess.GetAppointMilestone(fixedCitFile, point.FilePointer);
                        foreach (var item in FixParams)
                        {
                            FixPoint fixP = new FixPoint();
                            fixP.FixPostion = leftFixedCount;
                            double[] data = _citProcess.GetOneChannelDataInRange(fixedCitFile, item.ChannelID,leftStartPostion, ((int)(realLeftCount+realRightCount)));
                            fixP.Points = data;
                            fixP.ChannelID = item.ChannelID;
                            fixP.OriginalMileage = mile.GetMeter();
                            fixPoint.Add(fixP);
                        }
                        _fixedData.Add(point, fixPoint);
                    }
                }
            }
        }

        public bool RunMilestoneFix(string targetCitFile, bool diffFileName = false)
        {
            try
            {
                CheckData(targetCitFile);
                SaveToFile(targetCitFile, diffFileName);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CheckData(string targetCitFile)
        {
            _CorrResult.Clear();
            //左半边数据点个数，包括中间点
            foreach (var kvp in _fixedData)
            {
                int leftTargetCount = TargetSamplingCount + 1;

                long filePostion = _citProcess.GetCurrentPositionByMilestone(targetCitFile, kvp.Value[0].OriginalMileage, true);
                if (filePostion != -1)
                {

                    long targetStartPostion = _citProcess.GetAppointEndPostion(targetCitFile, filePostion, -1 * leftTargetCount);
                    long targetStartCount = _citProcess.GetSampleCountByRange(targetCitFile, targetStartPostion, filePostion);

                    long targetEndPostion = _citProcess.GetAppointEndPostion(targetCitFile, filePostion, TargetSamplingCount);
                    long targetEndCount = _citProcess.GetSampleCountByRange(targetCitFile, filePostion, targetEndPostion);
                    
                   

                    List<CorrelationResult> correlationResult = new List<CorrelationResult>();
                    foreach (var item in _fixedData[kvp.Key])
                    {
                        int index = -1;
                        double lastValue = 0;
                        double[] newArr = _citProcess.GetOneChannelDataInRange(targetCitFile, item.ChannelID, targetStartPostion, (int)(targetEndCount + targetStartCount));
                        double[] carr = new double[item.Points.Length];
                        for (int i = 0; i < newArr.Length - item.Points.Length; i++)
                        {
                            Array.Clear(carr, 0, carr.Length);
                            Array.Copy(newArr, i, carr, 0, carr.Length);
                            //double per = Correlation.Pearson(item.Points, carr);
                            double per = correlationCalc(item.Points, carr);
                            if (per > lastValue)
                            {
                                lastValue = per;
                                index = i;
                            }
                        }
                        FixParam key = FixParams.FirstOrDefault(p => p.ChannelID == item.ChannelID);
                        CorrelationResult result = new CorrelationResult();
                        if (key!=null && lastValue > key.ThreShold)
                        {
                            
                            result.FilePointer = targetStartPostion + index + item.FixPostion;
                            result.IsFind = true;
                            result.ChannelID = key.ChannelID;
                            result.ChannelName = key.ChannelName;
                           
                        }
                        else
                        {
                            result.IsFind = false;
                        }
                        correlationResult.Add(result);
                    }
                    _CorrResult.Add(kvp.Key, correlationResult);
                }
            }
        }

        private void SaveToFile(string targetCitFile, bool diffFileName = false)
        {
            string idfFile = targetCitFile.Replace(".cit", ".idf");
            if (diffFileName)
                idfFile = targetCitFile.Replace(".cit", "_MileageFix.idf");

            IndexOperator indexOperator = new IndexOperator();
            indexOperator.IndexFilePath = idfFile;
            FileInformation citHeaderInfo = _citProcess.GetFileInformation(targetCitFile);
            UserFixedTable fixedTable = new UserFixedTable(indexOperator, citHeaderInfo.iKmInc);
            fixedTable.Clear();
            foreach (var kvp in _CorrResult)
            {
                FixParams.Sort();
                bool isFind = false;
                UserMarkedPoint markedPoint = null;
                foreach (var item in FixParams)
                {
                    CorrelationResult corrResult = kvp.Value.FirstOrDefault(p => p.ChannelID == item.ChannelID);
                    if (corrResult != null)
                    {
                        if (item.Priority == 0 && !isFind)
                        {
                            isFind = true;
                            continue;
                        }
                        if (isFind)
                        {
                            markedPoint = new UserMarkedPoint();
                            markedPoint.FilePointer = corrResult.FilePointer;
                            markedPoint.UserSetMileage = kvp.Key.UserSetMileage;
                            break;
                        }
                    }
                }
                if (markedPoint != null)
                {
                    fixedTable.MarkedPoints.Add(markedPoint);
                }
            }
            if(fixedTable.MarkedPoints.Count>0)
            {
                fixedTable.Save();
            }
        }

        private double correlationCalc(double[] fixedData, double[] targetData)
        {
            if (targetData.Length > 0 
                && fixedData.Length > 0 
                && fixedData.Length == targetData.Length)
            {
                double x = 0;
                double y = 0;
                double z = 0;

                for (int i = 0; i < fixedData.Length; i++)
                {

                    x += (fixedData[i] * targetData[i]);
                    y += Math.Pow(targetData[i], 2);
                    z += Math.Pow(fixedData[i], 2);
                }
                double vv = (x / ((Math.Sqrt(y) * Math.Sqrt(z))));
                return vv;
            }
            return 0;
        }




    }

}
