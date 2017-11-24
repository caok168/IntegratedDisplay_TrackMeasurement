using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CitFileSDK;

namespace GeoFileProcess
{
    public class GeoFileHelper
    {
        const int GEO_FILE_VERSION = 0x3031;
        const int GEO_FILE_HEADER_SIZE = 0x2800;
        const int GEOMETRY_RECORD_ID = 0x5a5a;
        int iChannelLength = 0;
        int iChannelCount = 0;
        List<DataChannelInfo> dciL;
        DataHeaderInfo gfh;

        List<GEOCITChannelMapping> channelMappingList = new List<GEOCITChannelMapping>();

        CITFileProcess citHelper = new CITFileProcess();

        public GeoFileHelper()
        {
            
        }

        public bool InitChannelMapping(string csvFilePath)
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    StreamReader sr = new StreamReader(csvFilePath, Encoding.Default);
                    while (sr.Peek() != -1)
                    {
                        string[] sSplit = sr.ReadLine().Trim().Split(new char[] { '=' });
                        GEOCITChannelMapping channelMapping = new GEOCITChannelMapping();
                        channelMapping.sCIT = sSplit[0].Trim();
                        channelMapping.sGEO = sSplit[1].Trim();
                        channelMapping.sChinese = sSplit[2].Trim();
                        channelMappingList.Add(channelMapping);
                    }
                    if (channelMappingList.Count <= 0)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 获取文件头部
        /// </summary>
        /// <param name="lineCode">线路代码 4个字节</param>
        /// <param name="lineName">线路名称</param>
        /// <param name="train">检测车号</param>
        /// <param name="kmInc">增减里程 0：增，1：减</param>
        /// <param name="runDir">检测方向 0：正，1：反</param>
        /// <param name="dir">行别 1：上行，2：下行，3：单线</param>
        /// <returns></returns>
        public FileInformation GetFileHeadInfo(string lineCode, string lineName, string train, int kmInc, int runDir, int dir)
        {
            FileInformation citFileHeader = new FileInformation();
            citFileHeader.iDataType = 1;
            citFileHeader.sDataVersion = "3.0.0";
            citFileHeader.sTrackCode = lineCode;
            citFileHeader.sTrackName = lineName;
            citFileHeader.sTrain = train;
            citFileHeader.sDate = DateTime.Now.ToString("yyyy-MM-dd");
            citFileHeader.sTime = DateTime.Now.ToString("HH:mm:ss");
            citFileHeader.iSmaleRate = 4;
            citFileHeader.iRunDir = runDir;
            citFileHeader.iKmInc = kmInc;
            citFileHeader.iDir = dir;
            return citFileHeader;
        }

        /// <summary>
        /// 将GEO文件转换为CIT文件
        /// </summary>
        /// <param name="geoFile">GEO文件路径</param>
        /// <param name="citFile">CIT文件路径</param>
        /// <param name="citDataHeadInfo">cit头部信息</param>
        /// <returns>成功：true，失败：false</returns>
        public bool ConvertData(string geoFile, string citFile, FileInformation citDataHeadInfo)
        {
            try
            {
                int type = 1;
                //10号车通道对应表
                if (citDataHeadInfo.sTrain.Contains("999307"))
                {
                    type = 307;//iType=307时，geo转换有特殊情况
                }
                int iPostion = 18;
                QueryDataChannelInfoHead(geoFile);
                string mileageRange = GetExportDataMileageRange(geoFile);
                mileageRange = mileageRange.Substring(2);
                float startMileage = float.Parse(mileageRange.Substring(0, mileageRange.IndexOf("-")));
                float endMileage = float.Parse(mileageRange.Substring(mileageRange.IndexOf("-") + 1));
                if (citDataHeadInfo.iKmInc == 0)
                {
                    if (endMileage < startMileage)
                    {
                        float temp = startMileage;
                        startMileage = endMileage;
                        endMileage = temp;
                    }
                }
                else
                {
                    if (endMileage > startMileage)
                    {
                        float temp = startMileage;
                        startMileage = endMileage;
                        endMileage = temp;
                    }
                }
                citDataHeadInfo.fkmFrom = startMileage;
                citDataHeadInfo.fkmTo = endMileage;
                using (FileStream fs = new FileStream(geoFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;
                        gfh = GetDataInfoHead(br.ReadBytes(GEO_FILE_HEADER_SIZE));
                        //添加通道信息
                        iChannelLength = BitConverter.ToInt16(br.ReadBytes(2), 0);
                        iChannelCount = BitConverter.ToInt16(br.ReadBytes(2), 0);

                        citDataHeadInfo.iChannelNumber = 2 + channelMappingList.Count;

                        #region 通道定义

                        dciL = new List<DataChannelInfo>();
                        byte[] bChannelData = br.ReadBytes(iChannelLength - 2);
                        //读取通道
                        for (int j = 0; j < iChannelLength - 2;)
                        {
                            DataChannelInfo dci = GetChannelInfo(bChannelData, ref j);
                            dciL.Add(dci);
                            if (dciL.Count > iChannelCount)
                            {
                                break;
                            }
                            if (type == 10)
                            {
                                if (dci.sNameEn.ToLower().Contains("null"))
                                {
                                    break;
                                }
                            }
                        }

                        //通道匹配
                        int channelID = 2;
                        List<int> listID = new List<int>();
                        List<ChannelDefinition> channelList = new List<ChannelDefinition>();
                        channelList.Add(new ChannelDefinition { sID = 1, sNameEn = "Km", sNameCh = "Km", fScale = 1.0f, fOffset = 0.0f, sUnit = "" });
                        channelList.Add(new ChannelDefinition { sID = 2, sNameEn = "Meter", sNameCh = "Meter", fScale = 4.0f, fOffset = 0.0f, sUnit = "" });

                        for (int i = 0; i < channelMappingList.Count; i++)
                        {
                            for (int j = 0; j < dciL.Count; j++)
                            {
                                if (dciL[j].sNameEn.Equals(channelMappingList[i].sGEO))
                                {
                                    listID.Add(j);
                                    ChannelDefinition channelInfoItem = new ChannelDefinition();
                                    channelInfoItem.sID = channelID;
                                    channelInfoItem.sNameEn = channelMappingList[i].sCIT;
                                    channelInfoItem.sNameCh = channelMappingList[i].sChinese;
                                    channelInfoItem.fScale = dciL[j].fScale;
                                    channelInfoItem.fOffset = 0.0f;
                                    channelInfoItem.sUnit = dciL[j].sUnit;

                                    channelList.Add(channelInfoItem);

                                    ++channelID;
                                    break;
                                }
                            }
                        }

                        #endregion

                        #region 通道数据

                        List<byte[]> bytes = GetChannelData(br, gfh, type, listID, iPostion);

                        #endregion

                        citHelper.WriteCitFile(citFile, citDataHeadInfo, channelList, "", bytes);

                        br.Close();
                    }
                    fs.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// 获取数据文件里程范围
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public string GetExportDataMileageRange(string sFile)
        {
            try
            {
                FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;
                br.ReadBytes(GEO_FILE_HEADER_SIZE);
                br.ReadBytes(4);
                br.ReadBytes(iChannelLength - 2);
                int iB = (gfh.dataRecordLength);
                byte[] b = new byte[iB];
                string bRet = string.Empty;

                //定位头
                long iCurPos = 0;
                while (br.PeekChar() != -1)
                {

                    b = br.ReadBytes(iB);
                    short sI = BitConverter.ToInt16(b, 0);
                    if (sI != GEOMETRY_RECORD_ID)
                    {
                        continue;
                    }
                    short sKM = BitConverter.ToInt16(b, 8);
                    short sM = BitConverter.ToInt16(b, 10);

                    int iKM = sKM * 1000 + sM / 4;
                    if (!(iKM == 0 || (sKM > 3000)))
                    {
                        bRet += (Math.Abs(iKM / 1000.0)).ToString("f3");
                        iCurPos = br.BaseStream.Position;
                        break;
                    }


                }

                //定位
                int iReturnPosition = iB;
                while (iCurPos <= br.BaseStream.Position)
                {
                    br.BaseStream.Position = br.BaseStream.Length - iReturnPosition;
                    b = br.ReadBytes(iB);
                    short sI = BitConverter.ToInt16(b, 0);
                    if (sI != GEOMETRY_RECORD_ID)
                    {
                        iReturnPosition += (iB);
                        continue;
                    }
                    short sKM = BitConverter.ToInt16(b, 8);
                    short sM = BitConverter.ToInt16(b, 10);
                    int iKM = sKM * 1000 + sM / 4;
                    if (!(iKM == 0 || (sKM > 3000)))
                    {
                        bRet += ("-" + ((Math.Abs(iKM / 1000.0)).ToString("f3")));
                        break;
                    }
                    iReturnPosition += (iB);

                }

                br.Close();
                fs.Close();
                return "0," + bRet;
            }
            catch (Exception ex)
            {
                return "1,dw " + ex.Message;
            }
        }


        public string QueryDataChannelInfoHead(string sFile)
        {

            try
            {
                using (FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;
                        gfh = GetDataInfoHead(br.ReadBytes(GEO_FILE_HEADER_SIZE));
                        iChannelLength = BitConverter.ToInt16(br.ReadBytes(2), 0);
                        iChannelCount = BitConverter.ToInt16(br.ReadBytes(2), 0);
                        StringBuilder sbName = new StringBuilder();
                        dciL = new List<DataChannelInfo>();
                        byte[] bChannelData = br.ReadBytes(iChannelLength - 2);
                        int iGeometryRecordId = BitConverter.ToInt16(br.ReadBytes(2), 0);
                        //if (iGeometryRecordId != GEOMETRY_RECORD_ID)
                        //{
                        //    return "1,文件格式错误！";
                        //}
                        for (int i = 0; i < iChannelLength - 2; )
                        {
                            DataChannelInfo dci = GetChannelInfo(bChannelData, ref i);
                            dciL.Add(dci);
                            sbName.Append(dci.sNameEn + ",");

                        }
                        sbName.Remove(sbName.Length - 1, 1);
                        br.Close();
                        fs.Close();
                        return "0," + sbName.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "1,qc" + ex.Message;
            }
        }

        /// <summary>
        /// 获取原始通道数据 KM 和sample
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public List<int[]> GetMileChannelData(string sFile)
        {
            using (FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;
                    gfh = GetDataInfoHead(br.ReadBytes(GEO_FILE_HEADER_SIZE));
                    //添加通道信息
                    iChannelLength = BitConverter.ToInt16(br.ReadBytes(2), 0);
                    iChannelCount = BitConverter.ToInt16(br.ReadBytes(2), 0);

                    br.BaseStream.Position = 0;
                    br.ReadBytes(GEO_FILE_HEADER_SIZE);
                    br.ReadBytes(4);
                    br.ReadBytes(iChannelLength - 2);

                    int iChannelNumberSize = (gfh.dataRecordLength);
                    byte[] b = new byte[iChannelNumberSize];
                    //if (iType == 10)
                    //{
                    //    iChannelCount -= 1;
                    //}
                    //else
                    //{

                    //}

                    List<int> kmList = new List<int>();
                    List<int> mList = new List<int>();

                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {

                        b = br.ReadBytes(iChannelNumberSize);
                        int iGeometryRecordId = BitConverter.ToInt16(b, 0);
                        if (iGeometryRecordId != GEOMETRY_RECORD_ID)
                        {
                            continue;
                        }
                        short sKM = BitConverter.ToInt16(b, 4 * 2);
                        short sM = BitConverter.ToInt16(b, 5 * 2);

                        kmList.Add(sKM);
                        mList.Add(sM);
                    }
                    br.Close();
                    fs.Close();

                    List<int[]> listData = new List<int[]>();
                    listData.Add(kmList.ToArray());
                    listData.Add(mList.ToArray());

                    return listData;
                }
            }
        }

        #region 私有方法

        /// <summary>
        /// 获取头文件信息
        /// </summary>
        /// <param name="bDataInfo"></param>
        /// <returns></returns>
        private DataHeaderInfo GetDataInfoHead(byte[] bDataInfo)
        {
            DataHeaderInfo gfh = new DataHeaderInfo();

            gfh.fileVersion = BitConverter.ToInt16(bDataInfo, 0);
            gfh.dirFlag = BitConverter.ToInt16(bDataInfo, 2);
            gfh.dataRecordLength = BitConverter.ToInt32(bDataInfo, 4);
            gfh.sampleInterval = BitConverter.ToSingle(bDataInfo, 8);
            gfh.postUnits = BitConverter.ToSingle(bDataInfo, 12);

            //获取GEO Date
            StringBuilder sbDate = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                if (bDataInfo[42 + i] == 0)
                {
                    break;
                }
                sbDate.Append(UnicodeEncoding.Default.GetString(bDataInfo, 42 + i, 1));
            }
            gfh.Date = sbDate.ToString();

            //获取GEO Time
            StringBuilder sbTime = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                if (bDataInfo[54 + i] == 0)
                {
                    break;
                }
                sbTime.Append(UnicodeEncoding.Default.GetString(bDataInfo, 54 + i, 1));
            }
            gfh.Time = sbTime.ToString();

            //获取GEO Area
            StringBuilder sbArea = new StringBuilder();
            for (int i = 0; i < 60; i++)
            {
                if (bDataInfo[66 + i] == 0)
                {
                    break;
                }
                sbArea.Append(UnicodeEncoding.Default.GetString(bDataInfo, 66 + i, 1));
            }
            gfh.Area = sbArea.ToString();

            //获取GEO Division
            StringBuilder sbDivision = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                if (bDataInfo[130 + i] == 0)
                {
                    break;
                }
                sbDivision.Append(UnicodeEncoding.Default.GetString(bDataInfo, 130 + i, 1));
            }
            gfh.Division = sbDivision.ToString();

            //获取GEO Region
            StringBuilder sbRegion = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                if (bDataInfo[150 + i] == 0)
                {
                    break;
                }
                sbRegion.Append(UnicodeEncoding.Default.GetString(bDataInfo, 150 + i, 1));
            }
            gfh.Region = sbRegion.ToString();

            return gfh;
        }

        /// <summary>
        /// 获取通道信息
        /// </summary>
        /// <param name="bDataInfo"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private DataChannelInfo GetChannelInfo(byte[] bDataInfo, ref int start)
        {
            DataChannelInfo dci = new DataChannelInfo();
            StringBuilder sNameEn = new StringBuilder();
            StringBuilder sUnit = new StringBuilder();
            int iNameEnLen = (int)bDataInfo[start];
            for (int i = 1; i <= iNameEnLen; i++)
            {
                sNameEn.Append(UnicodeEncoding.Default.GetString(bDataInfo, i + start, 1));
            }
            start += (1 + iNameEnLen);
            int iUnitLen = (int)bDataInfo[start];
            for (int i = 1; i <= iUnitLen; i++)
            {
                sUnit.Append(UnicodeEncoding.Default.GetString(bDataInfo, i + start, 1));
            }
            start += (1 + iUnitLen);

            dci.sNameEn = sNameEn.ToString();
            dci.sUnit = sUnit.ToString();
            dci.fScale = BitConverter.ToInt32(bDataInfo, start);
            start += 4;
            while (start < bDataInfo.Length && (bDataInfo[start] == (byte)0))
            {
                start++;
            }

            return dci;
        }

        private BinaryWriter WriteKmMeter(BinaryWriter bw)
        {
            //公里，米
            bw.Write(1);
            bw.Write("Km");
            for (int i = 0; i < 18; i++)
            {
                bw.Write((byte)0);
            }
            bw.Write("Km");
            for (int i = 0; i < 18; i++)
            {
                bw.Write((byte)0);
            }
            bw.Write((float)1.0);
            bw.Write((float)0.0);
            for (int i = 0; i < 11; i++)
            {
                bw.Write((byte)0);
            }

            bw.Write(2);
            bw.Write("Meter");
            for (int i = 0; i < 15; i++)
            {
                bw.Write((byte)0);
            }
            bw.Write("Meter");
            for (int i = 0; i < 15; i++)
            {
                bw.Write((byte)0);
            }
            bw.Write((float)4.0);
            bw.Write((float)0.0);
            for (int i = 0; i < 11; i++)
            {
                bw.Write((byte)0);
            }

            return bw;
        }

        /// <summary>
        /// 获取通道数据
        /// </summary>
        /// <param name="br"></param>
        /// <param name="gfh"></param>
        /// <param name="iType"></param>
        /// <param name="listID"></param>
        /// <param name="iPostion"></param>
        /// <returns></returns>
        private List<byte[]> GetChannelData(BinaryReader br, DataHeaderInfo gfh, int iType, List<int> listID, int iPostion)
        {
            List<byte[]> bytes = new List<byte[]>();

            br.BaseStream.Position = 0;
            br.ReadBytes(GEO_FILE_HEADER_SIZE);
            br.ReadBytes(4);
            br.ReadBytes(iChannelLength - 2);

            int iChannelNumberSize = (gfh.dataRecordLength);
            byte[] b = new byte[iChannelNumberSize];
            if (iType == 10)
            {
                iChannelCount -= 1;
            }
            while (br.BaseStream.Position < br.BaseStream.Length)
            {

                b = br.ReadBytes(iChannelNumberSize);
                int iGeometryRecordId = BitConverter.ToInt16(b, 0);
                if (iGeometryRecordId != GEOMETRY_RECORD_ID)
                {
                    continue;
                }
                short sKM = BitConverter.ToInt16(b, 4 * 2);
                short sM = BitConverter.ToInt16(b, 5 * 2);
                byte[] bResult = new byte[4 + listID.Count * 2];

                byte[] b1 = BitConverter.GetBytes(BitConverter.ToInt16(b, 4 * 2));
                bResult[0] = b1[0];
                bResult[1] = b1[1];
                b1 = BitConverter.GetBytes(BitConverter.ToInt16(b, 5 * 2));
                bResult[2] = b1[0];
                bResult[3] = b1[1];
                //12
                for (int i = 0; i < listID.Count; i++)
                {
                    if (iType == 307)
                    {
                        if (listID[i] > 22)
                        {
                            b1 = BitConverter.GetBytes(BitConverter.ToInt16(b, iPostion + 12 + listID[i] * 2));
                        }
                        else
                        {
                            b1 = BitConverter.GetBytes(BitConverter.ToInt16(b, iPostion + listID[i] * 2));
                        }

                    }
                    else
                    {
                        b1 = BitConverter.GetBytes(BitConverter.ToInt16(b, iPostion + listID[i] * 2));

                    }
                    bResult[4 + i * 2] = b1[0];
                    bResult[4 + i * 2 + 1] = b1[1];

                }
                bResult = Encryption.Translate(bResult);

                bytes.Add(bResult);
            }
            return bytes;
        }

        #endregion
    }
}
