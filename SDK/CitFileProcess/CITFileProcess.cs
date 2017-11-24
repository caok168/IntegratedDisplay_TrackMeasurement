﻿/// -------------------------------------------------------------------------------------------
/// FileName：CITFileProcess.cs
/// 说    明：CIT文件相关操作
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CitFileSDK
{
    /// <summary>
    /// CIT文件相关操作类
    /// </summary>
    public partial class CITFileProcess
    {

        #region ChannelDefinition

        #region ChannelDefinition 读取

        /// <summary>
        /// 获取所有channel的定义信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>通道定义集合</returns>
        public List<ChannelDefinition> GetChannelDefinitionList(string citFile)
        {
            // 这里要注意：通道id如果直接从文件中获取，可能得到错误的值（创建cit文件时可能写入错误的值）
            // 所以，channel id需要程序单独计算，计数从0开始，0，1，2，3，4......
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;
                    long ii = br.BaseStream.Length;
                    FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                    byte[] bChannelData = br.ReadBytes(fi.iChannelNumber * DataOffset.DataChannelLength);
                    List<ChannelDefinition> cdList = new List<ChannelDefinition>();
                    for (int i = 0; i < fi.iChannelNumber * DataOffset.DataChannelLength; i += DataOffset.DataChannelLength)
                    {
                        ChannelDefinition cd = GetChannelInfo(bChannelData, i);
                        if (i == DataOffset.DataChannelLength)
                        {
                            cd.fScale = 4;
                        }
                        cdList.Add(cd);
                    }

                    return cdList;
                }
            }
        }

        /// <summary>
        /// 获取通道的字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>获取通道字节数组</returns>
        public byte[] GetChannelDefinitionsBytes(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;

                    FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                    byte[] bChannelData = br.ReadBytes(fi.iChannelNumber * DataOffset.DataChannelLength);
                    return bChannelData;
                }
            }
        }

        #endregion

        #region ChannelDefinition 写入

        /// <summary>
        /// 将通道的字节数组写入文件中
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="bytes">字节数组</param>
        /// <returns>是否写入成功 true：成功；false：失败</returns>
        public bool WriteChannelDefinitionsBytes(string citFile, byte[] bytes)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        bw.BaseStream.Position = DataOffset.DataHeadLength;
                        bw.Write(bytes);
                        bw.Close();
                    }
                    fsWrite.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 将通道定义信息写入文件中
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="channelList"></param>
        /// <returns></returns>
        public bool WriteChannelDefinitionList(string citFile, List<ChannelDefinition> channelList)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        bw.BaseStream.Position = DataOffset.DataHeadLength;
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        bw.Close();
                    }
                    fsWrite.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #endregion


        #region 附加信息

        #region 获取附加信息

        /// <summary>
        /// 获取文件头补充信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public byte[] GetExtraInfo(string citFile)
        {
            FileInformation fi;

            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                    br.BaseStream.Position = DataOffset.DataHeadLength + fi.iChannelNumber * DataOffset.DataChannelLength;
                    byte[] extraByte = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
                    return extraByte;
                }
            }
        }

        #endregion

        #region 写入附加信息

        /// <summary>
        /// 向文件中写入文件头补充信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="info">补充信息</param>
        /// <returns></returns>
        public bool WriteExtraInfo(string citFile,byte[] info)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        FileInformation fi = GetFileInformation(citFile);
                        bw.BaseStream.Position = DataOffset.DataHeadLength + fi.iChannelNumber * DataOffset.DataChannelLength;

                        bw.Write((Byte)(info.Length));
                        bw.Write(info);
                        if (info.Length < 4)
                        {
                            for (int i = 0; i < (4 - info.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }
                        bw.Close();
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
        /// 向文件中写入文件头补充信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="info">补充信息</param>
        /// <returns></returns>
        public bool WriteExtraInfo(string citFile, string info)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        FileInformation fi = GetFileInformation(citFile);
                        bw.BaseStream.Position = DataOffset.DataHeadLength + fi.iChannelNumber * DataOffset.DataChannelLength;

                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(info);
                        bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }
                        bw.Close();
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

        #endregion

        #endregion


        #region 获取通道数据

        #region 里程信息

        /// <summary>
        /// 得到文件中的所有里程信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>所有公里信息</returns>
        public List<Milestone> GetAllMileStone(string citFile)
        {
            List<Milestone> listMilestone = new List<Milestone>();
            FileInformation fi = GetFileInformation(citFile);
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                    int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                    byte[] b = new byte[iChannelNumberSize];
                    long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                    bool isEncry = Encryption.IsEncryption(fi.sDataVersion);
                    listMilestone = new List<Milestone>((int)iArray);
                    ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
                    channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
                    ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
                    ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
                    if (kmChannel == null)
                    {
                        throw new Exception("通道定义错误，找不到通道Km");
                    }
                    if (kmChannel == null)
                    {
                        throw new Exception("通道定义错误，找不到通道Meter");
                    }
                    for (int i = 0; i < iArray; i++)
                    {

                        Milestone milestone = new Milestone();
                        milestone.mFilePosition = br.BaseStream.Position;

                        b = br.ReadBytes(iChannelNumberSize);
                        milestone.mFileEndPostion = br.BaseStream.Position;
                        if (isEncry)
                        {
                            b = ByteXORByte(b);
                        }

                        short km = BitConverter.ToInt16(b, 0);
                        short m = BitConverter.ToInt16(b, 2);

                        milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                        milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
                        listMilestone.Add(milestone);
                    }
                    br.Close();
                }
                fs.Close();
            }
            return listMilestone;
        }

        public byte[] ByteXORByte(byte[] b)
        {
            for (int iIndex = 0; iIndex < b.Length; iIndex++)
            {
                b[iIndex] = (byte)(b[iIndex] ^ 128);
            }
            return b;
        }

        /// <summary>
        /// 得到文件中的指定范文的里程信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>里程信息</returns>
        public List<Milestone> GetMileStoneByRange(string citFile, long startFilePos, long endFilePos)
        {
            List<Milestone> listMilestone = new List<Milestone>();

            FileInformation fi = GetFileInformation(citFile);
            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);
            long[] startAndEndPostion = GetPositons(citFile);
            if(startFilePos<startAndEndPostion[0])
            {
                startFilePos = startAndEndPostion[0];
            }
            if (endFilePos > startAndEndPostion[1])
            {
                endFilePos = startAndEndPostion[1];
            }
            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            br.BaseStream.Position = startFilePos;
            long iArray = (endFilePos - br.BaseStream.Position) / iChannelNumberSize;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if(kmChannel==null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            for (int i = 0; i < iArray; i++)
            {
                Milestone milestone = new Milestone();
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }

                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;

                listMilestone.Add(milestone);
            }

            br.Close();
            fs.Close();

            return listMilestone;
        }

        /// <summary>
        /// 得到文件中的指定范文的里程信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">计算完偏移后的开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <param name="endFilePos">返回结束位置</param>
        /// <returns>里程信息</returns>
        public List<Milestone> GetMileStoneByRange(string citFile, long startFilePos, int sampleNum, ref long endFilePos)
        {
            List<Milestone> listMilestone = new List<Milestone>();

            FileInformation fi = GetFileInformation(citFile);
            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            long[] startAndEndPoint = GetPositons(citFile);

            if (startFilePos < startAndEndPoint[0])
            {
                return listMilestone;
            }

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            
            endFilePos = startFilePos + sampleNum * iChannelNumberSize;
            br.BaseStream.Position = startFilePos;
            if (endFilePos > startAndEndPoint[1])
            {
                endFilePos = startAndEndPoint[1];
            }

            long iArray = (endFilePos - br.BaseStream.Position) / iChannelNumberSize;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            for (int i = 0; i < iArray; i++)
            {
                Milestone milestone = new Milestone();
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }

                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;

                listMilestone.Add(milestone);
            }

            br.Close();
            fs.Close();

            return listMilestone;
        }



        /// <summary>
        /// 找到第一个采样点，读取其里程信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>公里信息</returns>
        public Milestone GetStartMilestone(string citFile)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];

            Milestone milestone = new Milestone();
            milestone.mFilePosition = br.BaseStream.Position;

            b = br.ReadBytes(iChannelNumberSize);
            milestone.mFileEndPostion = br.BaseStream.Position;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            if (Encryption.IsEncryption(fi.sDataVersion))
            {
                b = Encryption.Translate(b);
            }

            short km = BitConverter.ToInt16(b, 0);
            short m = BitConverter.ToInt16(b, 2);

            milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
            milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;

            br.Close();
            fs.Close();

            return milestone;
        }
        
        /// <summary>
        /// 获取真实的里程数据，不含联络线
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns></returns>
        public Milestone GetRealStartMilestone(string citFile)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            long[] startAndEnd = GetPositons(citFile);
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            Milestone milestone = new Milestone();
            do
            {
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }

                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }
            while (milestone.mKm >= 3000 || br.BaseStream.Position >= startAndEnd[1]);

            br.Close();
            fs.Close();

            return milestone;
        }

        /// <summary>
        /// 找到最后一个采样点，读取其里程信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>公里信息</returns>
        public Milestone GetEndMilestone(string citFile)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            Milestone milestone = new Milestone();

            if (iArray > 1)
            {
                int num = (int)(iArray - 1);
                br.BaseStream.Position += iChannelNumberSize * num;

                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }

                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }

            br.Close();
            fs.Close();

            return milestone;
        }

        public Milestone GetRealEndMilestone(string citFile)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);
            long[] startAndEnd = GetPositons(citFile);
            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            long iArray = 0;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            Milestone milestone = new Milestone();
            iArray = (br.BaseStream.Length - startAndEnd[0]) / iChannelNumberSize;
            int i = 1;
            do
            {
                if (iArray > 1)
                {
                    br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);
                    int num = (int)(iArray - i);
                    br.BaseStream.Position += iChannelNumberSize * num;

                    milestone.mFilePosition = br.BaseStream.Position;

                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);

                    milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                    milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
                    i++;
                }
                else
                {
                    break;
                }
            }
            while (milestone.mKm >= 3000 && milestone.mFilePosition > startAndEnd[0]);

            br.Close();
            fs.Close();

            return milestone;
        }

        /// <summary>
        /// 根据文件指针获取对应的里程标
        /// </summary>
        /// <param name="citFile"></param>
        /// <param name="filePos"></param>
        /// <returns></returns>
        public Milestone GetAppointMilestone(string citFile, long filePos)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            Milestone milestone = new Milestone();

            if (br.BaseStream.Position <= filePos && br.BaseStream.Length > filePos)
            {
                br.BaseStream.Position = filePos;

                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }
            else if(br.BaseStream.Length >= filePos)
            {
                br.BaseStream.Position = br.BaseStream.Length - iChannelNumberSize;
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }
            else if(br.BaseStream.Position < filePos)
            {
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }

            br.Close();
            fs.Close();

            return milestone;
        }



        /// <summary>
        /// 根据文件指针获取对应的里程标
        /// </summary>
        /// <param name="citFile"></param>
        /// <param name="filePos"></param>
        /// <returns></returns>
        public Milestone GetAppointMilestone(string citFile, long startPos,int sampleNum)
        {
            FileInformation fi = GetFileInformation(citFile);

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

            int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
            ChannelDefinitionList channelDefintions = new ChannelDefinitionList();
            channelDefintions.channelDefinitionList = GetChannelDefinitionList(citFile);
            ChannelDefinition kmChannel = channelDefintions.GetChannelByName("km", "公里");
            ChannelDefinition mChannel = channelDefintions.GetChannelByName("m", "米");
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Km");
            }
            if (kmChannel == null)
            {
                throw new Exception("通道定义错误，找不到通道Meter");
            }
            Milestone milestone = new Milestone();
            long[] startAndEnd = GetPositons(citFile);

            long endFilePos = startPos + sampleNum * iChannelNumberSize;
            if (endFilePos>=startAndEnd[0]&&endFilePos<=startAndEnd[1])
            {
                br.BaseStream.Position = endFilePos;

                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }
            else if (endFilePos >= br.BaseStream.Length)
            {
                br.BaseStream.Position = startAndEnd[1];
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }
            else if (endFilePos < startAndEnd[0])
            {
                br.BaseStream.Position = startAndEnd[0];
                milestone.mFilePosition = br.BaseStream.Position;

                b = br.ReadBytes(iChannelNumberSize);
                milestone.mFileEndPostion = br.BaseStream.Position;
                if (Encryption.IsEncryption(fi.sDataVersion))
                {
                    b = Encryption.Translate(b);
                }
                short km = BitConverter.ToInt16(b, 0);
                short m = BitConverter.ToInt16(b, 2);

                milestone.mKm = km / kmChannel.fScale + kmChannel.fOffset;
                milestone.mMeter = m / mChannel.fScale + mChannel.fOffset;
            }

            br.Close();
            fs.Close();

            return milestone;
        }

        #endregion


        #region 获取指定通道的通道数据

        /// <summary>
        /// 获取指定通道的通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="channelId">通道号</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>通道数据</returns>
        public double[] GetOneChannelDataInRange(string citFile, int channelId, long startFilePos, long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startFilePos;

                long iArray = (endFilePos - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / cdList[channelId - 1].fScale + cdList[channelId - 1].fOffset);

                    fReturnArray[i] = fGL;
                }
                br.Close();
                fs.Close();

                return fReturnArray;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
        /// <summary>
        /// 获取指定通道的通道数据(从开始位置开始，获取指定个数的采样点)
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="channelId">通道号</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>通道数据</returns>
        public double[] GetOneChannelDataInRange(string citFile, int channelId, long startFilePos, int sampleNum)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);

                long endFilePos = startFilePos + sampleNum * iChannelNumberSize;

                br.Close();
                fs.Close();

                double[] fReturnArray = GetOneChannelDataInRange(citFile, channelId, startFilePos, endFilePos);

                return fReturnArray;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取指定通道的通道数据(从开始位置开始，获取指定个数的采样点)
        /// </summary>
        /// <param name="citFile">>cit文件路径</param>
        /// <param name="channelId">通道号</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <param name="endFilePos">返回结束位置</param>
        /// <returns>通道数据数组</returns>
        public double[] GetAppointChannelDataInRange(string citFile, int channelId, long startFilePos, int sampleNum,ref long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);

                endFilePos = startFilePos + sampleNum * iChannelNumberSize;

                
                double[] fReturnArray = null;
                if (sampleNum > 0)
                {
                    if (endFilePos > br.BaseStream.Length)
                    {
                        endFilePos = br.BaseStream.Length;
                    }
                    fReturnArray = GetOneChannelDataInRange(citFile, channelId, startFilePos, endFilePos);
                }
                else
                {
                    if (endFilePos < 0)
                    {
                        endFilePos = 0;
                    }
                    fReturnArray = GetOneChannelDataInRange(citFile, channelId, endFilePos, startFilePos);
                }
                br.Close();
                fs.Close();
                return fReturnArray;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }




        /// <summary>
        /// 根据开始里程和采样点个数获取指定通道的通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="channelId">通道号</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>通道数据</returns>
        public double[] GetAppointChannelDataInRangeByMilestone(string citFile, int channelId, float startMilestone, int sampleNum)
        {
            try
            {
                List<ChannelDefinition> m_dcil = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                m_dcil = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];

                List<double> listValue = new List<double>();

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);

                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    //增里程的情况
                    if (fi.iKmInc==0 && fGLMile >= startMilestone)
                    {
                        double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / m_dcil[channelId - 1].fScale + m_dcil[channelId - 1].fOffset);

                        listValue.Add(fGL);
                    }
                    //减里程的情况
                    if (fi.iKmInc == 1 && fGLMile <= startMilestone)
                    {
                        double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / m_dcil[channelId - 1].fScale + m_dcil[channelId - 1].fOffset);

                        listValue.Add(fGL);
                    }
                    if (listValue.Count == sampleNum)
                    {
                        break;
                    }
                }

                br.Close();
                fs.Close();

                double[] result = listValue.ToArray();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取指定通道的里程范围内的通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="channelId">通道号</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="endMilestone">结束里程</param>
        /// <returns>通道数据</returns>
        public double[] GetAppointChannelDataInRangeByMilestone(string citFile, int channelId, float startMilestone, float endMilestone)
        {
            try
            {
                List<ChannelDefinition> m_dcil = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                m_dcil = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];

                List<double> listValue = new List<double>();

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);

                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    if (fGLMile >= startMilestone && fGLMile < endMilestone)
                    {
                        double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / m_dcil[channelId - 1].fScale + m_dcil[channelId - 1].fOffset);

                        listValue.Add(fGL);
                    }
                }

                br.Close();
                fs.Close();

                double[] result = listValue.ToArray();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion


        #region 获取所有通道的数据

        /// <summary>
        /// 获取所有通道的数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>所有通道数据</returns>
        public List<double[]> GetAllChannelDataInRange(string citFile, long startFilePos, long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];

                long[] startAndEndPostion = GetPositons(citFile);
                if (startFilePos < startAndEndPostion[0])
                {
                    startFilePos = startAndEndPostion[0];
                }
                if (endFilePos > startAndEndPostion[1])
                {
                    endFilePos = startAndEndPostion[1];
                }

                br.BaseStream.Position = startFilePos;

                long iArray = (endFilePos - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];


                List<double[]> allList = new List<double[]>();
                for (int i = 0; i < fi.iChannelNumber; i++)
                {
                    double[] array = new double[iArray];
                    allList.Add(array);
                }

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    for (int channelId = 1; channelId < fi.iChannelNumber + 1; channelId++)
                    {
                        int value = (BitConverter.ToInt16(b, (channelId - 1) * 2));
                        float fScale = cdList[channelId - 1].fScale;

                        double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / cdList[channelId - 1].fScale + cdList[channelId - 1].fOffset);

                        allList[channelId - 1][i] = fGL;
                    }
                }
                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
        /// <summary>
        /// 获取所有通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">计算完偏移量后的开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>所有通道数据</returns>
        public List<double[]> GetAllChannelDataInRange(string citFile, long startFilePos, int sampleNum)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                long[] startAndEnd = GetPositons(citFile);
                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);
                List<double[]> allList = new List<double[]>();
                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                if (startFilePos < startAndEnd[0] || startFilePos > startAndEnd[1])
                {
                    for (int i = 0; i < cdList.Count; i++)
                    {
                        allList.Add(new double[sampleNum]);
                    }
                }
                else
                {
                    
                    br.BaseStream.Position = startFilePos;
                    long endFilePos = startFilePos + sampleNum * iChannelNumberSize;

                    br.Close();
                    fs.Close();
                    if (startFilePos > endFilePos)
                    {
                        allList = GetAllChannelDataInRange(citFile, endFilePos,startFilePos);
                    }
                    else
                    {
                        allList = GetAllChannelDataInRange(citFile, startFilePos, endFilePos);
                    }
                }
                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <param name="endFilePos">采样点结束的位置</param>
        /// <returns>指定范围的所有通道数据</returns>
        public List<double[]> GetAllChannelDataInRange(string citFile, long startFilePos, int sampleNum, ref long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startFilePos;
                endFilePos = startFilePos + sampleNum * iChannelNumberSize;

                br.Close();
                fs.Close();

                var allList = GetAllChannelDataInRange(citFile, startFilePos, endFilePos);

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }



        /// <summary>
        /// 根据开始里程和采样点个数获取所有通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>所有通道数据</returns>
        public List<double[]> GetAllChannelDataInRange(string citFile, float startMilestone, int sampleNum)
        {
            try
            {
                List<ChannelDefinition> cdList = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                cdList = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

                List<List<double>> listAll = new List<List<double>>();

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    //增里程的情况
                    if (fi.iKmInc == 0 && fGLMile >= startMilestone)
                    {
                        for (int channelId = 1; channelId < fi.iChannelNumber + 1; channelId++)
                        {
                            double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / cdList[channelId - 1].fScale + cdList[channelId - 1].fOffset);

                            listAll[channelId - 1].Add(fGL);

                        }
                    }
                    //减里程的情况
                    if (fi.iKmInc == 1 && fGLMile <= startMilestone)
                    {
                        for (int channelId = 1; channelId < fi.iChannelNumber + 1; channelId++)
                        {
                            double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / cdList[channelId - 1].fScale + cdList[channelId - 1].fOffset);

                            listAll[channelId - 1].Add(fGL);
                        }
                    }

                    if (listAll[0].Count == sampleNum)
                    {
                        break;
                    }
                }

                List<double[]> allList = new List<double[]>();

                for (int i = 0; i < listAll.Count; i++)
                {
                    allList.Add(listAll[i].ToArray());
                }

                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// 获取指定里程范围内的全部通道数据
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="endMilestone">结束里程</param>
        /// <returns>所有通道数据</returns>
        public List<double[]> GetAllChannelDataInRangeByMilestone(string citFile, float startMilestone, int endMilestone)
        {
            try
            {
                List<ChannelDefinition> cdList = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                cdList = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];

                List<double> listValue = new List<double>();

                List<double[]> allList = new List<double[]>();
                for (int i = 0; i < fi.iChannelNumber; i++)
                {
                    double[] array = new double[iArray];
                    allList.Add(array);
                }

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    if (fGLMile >= startMilestone && fGLMile < endMilestone)
                    {
                        for (int channelId = 1; channelId < fi.iChannelNumber + 1; channelId++)
                        {
                            double fGL = (BitConverter.ToInt16(b, (channelId - 1) * 2) / cdList[channelId - 1].fScale + cdList[channelId - 1].fOffset);

                            allList[channelId - 1][i] = fGL;
                        }
                    }
                }

                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion


        #region 获取通道的字节数组

        /// <summary>
        /// 根据开始位置、结束位置获取所有通道的字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>字节数组</returns>
        public byte[] GetChannelDataBytesInRange(string citFile, long startFilePos, long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startFilePos;

                byte[] bytes = br.ReadBytes(Convert.ToInt32(endFilePos - startFilePos));

                br.Close();
                fs.Close();

                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// 根据开始位置以及采样点个数获取所有通道的字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>字节数组</returns>
        public byte[] GetChannelDataBytesInRange(string citFile, long startFilePos, int sampleNum)
        {
            try
            {
                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                List<ChannelDefinition> cdList = GetChannelDefinitionList(citFile);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startFilePos;

                byte[] bytes = br.ReadBytes(sampleNum * iChannelNumberSize);

                br.Close();
                fs.Close();

                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据开始里程和采样点数获取所有通道的字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>字节数组</returns>
        public byte[] GetChannelDataBytesInRange(string citFile, float startMilestone, int sampleNum)
        {
            try
            {
                List<ChannelDefinition> cdList = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                cdList = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

                long startPosition = br.BaseStream.Position;

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    //增里程的情况
                    if (fi.iKmInc == 0 && fGLMile >= startMilestone)
                    {
                        startPosition = br.BaseStream.Position - iChannelNumberSize;
                        break;
                    }
                    //减里程的情况
                    if (fi.iKmInc == 1 && fGLMile <= startMilestone)
                    {
                        startPosition = br.BaseStream.Position - iChannelNumberSize;
                        break;
                    }
                }

                long endPosition = startPosition + sampleNum * iChannelNumberSize;

                br.BaseStream.Position = startPosition;

                byte[] bytes = br.ReadBytes(sampleNum * iChannelNumberSize);

                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据开始里程、结束里程获取所有通道的字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startMilestone">开始里程</param>
        /// <param name="endMilestone">结束里程</param>
        /// <returns>字节数组</returns>
        public byte[] GetChannelDataBytesInRangeByMilestone(string citFile, float startMilestone, int endMilestone)
        {
            try
            {
                List<ChannelDefinition> cdList = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                cdList = GetChannelDefinitionList(citFile);

                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

                long startPosition = br.BaseStream.Position;
                long endPosition = 0;

                bool startFirst=false;
                bool endFirst=false;
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / fi.iSmaleRate / 1000;//单位为公里

                    //增里程的情况
                    if (fi.iKmInc == 0 && fGLMile >= startMilestone)
                    {
                        if(!startFirst)
                        {
                            startPosition = br.BaseStream.Position - iChannelNumberSize;
                            startFirst=true;
                        }
                    }
                    if (fi.iKmInc == 0 && fGLMile > endMilestone)
                    {
                        if (!endFirst)
                        {
                            endPosition = br.BaseStream.Position - iChannelNumberSize;
                            endFirst = true;
                        }
                    }
                    //减里程的情况
                    if (fi.iKmInc == 1 && fGLMile <= startMilestone)
                    {
                        if (!startFirst)
                        {
                            startPosition = br.BaseStream.Position - iChannelNumberSize;
                            startFirst = true;
                        }
                    }
                    if (fi.iKmInc == 1 && fGLMile < endMilestone)
                    {
                        if (!endFirst)
                        {
                            endPosition = br.BaseStream.Position - iChannelNumberSize;
                            endFirst = true;
                        }
                    }
                }

                br.BaseStream.Position = startPosition;

                byte[] bytes = br.ReadBytes(Convert.ToInt32(endPosition - startPosition));

                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #endregion


        /// <summary>
        /// 获取cit文件的数据块的开始位置、结束位置
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>cit文件的数据块的开始位置结束位置double数组</returns>
        public long[] GetPositons(string citFile)
        {
            try
            {
                //List<ChannelDefinition> cdList = new List<ChannelDefinition>();

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));

                //cdList = GetChannelDefinitionList(citFile);

                //开始位置
                //long startPosition = br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));

                long startPosition = br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber,DataOffset.ExtraLength);
                //结束位置
                long endPosition = br.BaseStream.Length;

                long[] positions = new long[2];
                positions[0] = startPosition;
                positions[1] = endPosition;

                return positions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
        /// <summary>
        /// 获取cit文件当前里程的位置
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="mile">当前里程</param>
        /// <param name="isStrict">是否精准搜索</param>
        /// <returns>当前里程的位置</returns>
        public long GetCurrentPositionByMilestone(string citFile, float mile,bool isStrict)
        {
            try
            {
                long position = 0;

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs);

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                bool isFind = false;
                for (int i = 0; i < iArray; i++)
                {
                    position = br.BaseStream.Position;
                    b = br.ReadBytes(iChannelNumberSize);
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);

                    float currentMile = km * 1000 + m / (float)fi.iSmaleRate;

                    if (isStrict)
                    {
                        if (mile == currentMile)
                        {
                            isFind = true;
                            break;
                        }
                    }
                    else
                    {
                        //增里程
                        if (fi.iKmInc == 0)
                        {
                            if (mile <= currentMile)
                            {
                                isFind = true;
                                break;
                            }
                        }
                        //减里程
                        if (fi.iKmInc == 1)
                        {
                            if (mile >= currentMile)
                            {
                                isFind = true;
                                break;
                            }
                        }
                    }
                }

                br.Close();
                fs.Close();
                if(isFind)
                {
                    return position;
                }
                else
                {
                    return -1;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取cit文件当前里程的位置
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="mile">当前里程</param>
        /// <param name="isStrict">是否精准搜索</param>
        /// <param name="isIncludeFileLength">是否包含文件长度，是为采样点尾部，否为采样点头部</param>
        /// <returns>当前里程的位置</returns>
        public long GetCurrentPostionIncludeLengthByMilestone(string citFile, float mile, bool isStrict)
        {
            try
            {
                long position = 0;

                FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs);

                FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                br.BaseStream.Position = FileDataOffset.GetSamplePointStartOffset(fi.iChannelNumber, DataOffset.ExtraLength);

                int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                bool isFind = false;
                for (int i = 0; i < iArray; i++)
                {
                    //position = br.BaseStream.Position;
                    b = br.ReadBytes(iChannelNumberSize);

                    position = br.BaseStream.Position;

                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        b = Encryption.Translate(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);

                    float currentMile = km * 1000 + m / (float)fi.iSmaleRate;

                    if (isStrict)
                    {
                        if (mile == currentMile)
                        {
                            isFind = true;
                            break;
                        }
                    }
                    else
                    {
                        //增里程
                        if (fi.iKmInc == 0)
                        {
                            if (mile <= currentMile)
                            {
                                isFind = true;
                                break;
                            }
                        }
                        //减里程
                        if (fi.iKmInc == 1)
                        {
                            if (mile >= currentMile)
                            {
                                isFind = true;
                                break;
                            }
                        }
                    }
                }
                br.Close();
                fs.Close();
                if (isFind)
                {
                    return position;
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取指定采样点后的结束位置，如果溢出会返回溢出后的位置
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="startFilePos">数据起始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns>经过指定采样点后的结束位置</returns>
        public long GetAppointEndPostion(string citFile, long startFilePos, int sampleNum)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;

                        FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                        int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                        long[] startAndEnd = GetPositons(citFile);
                        
                        if (startFilePos >= startAndEnd[0])
                        {
                            br.BaseStream.Position = startFilePos;
                        }
                        
                        long endFilePos = startFilePos + sampleNum * iChannelNumberSize;
                        
                        
                        br.Close();
                        fs.Close();
                        return endFilePos;
                        //if (endFilePos >= startAndEnd[0] && endFilePos <= startAndEnd[1])
                        //{
                        //    return endFilePos;
                        //}
                        //else if (endFilePos < startAndEnd[0])
                        //{
                        //    return startAndEnd[0];
                        //}
                        //else
                        //{
                        //    return startAndEnd[1];
                        //}
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取文件中指定采样后的结束位置，如果溢出，不会超过文件范围
        /// </summary>
        /// <param name="citFile">cit文件</param>
        /// <param name="startFilePos">起始位置</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <returns></returns>
        public long GetAppointFileEndPostion(string citFile, long startFilePos, int sampleNum)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;

                        FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                        int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                        long[] startAndEnd = GetPositons(citFile);

                        if (startFilePos >= startAndEnd[0])
                        {
                            br.BaseStream.Position = startFilePos;
                        }

                        long endFilePos = startFilePos + sampleNum * iChannelNumberSize;


                        br.Close();
                        fs.Close();
                       
                        if (endFilePos >= startAndEnd[0] && endFilePos <= startAndEnd[1])
                        {
                            return endFilePos;
                        }
                        else if (endFilePos < startAndEnd[0])
                        {
                            return startAndEnd[0];
                        }
                        else
                        {
                            return startAndEnd[1];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取所有采样点的个数
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns>采样点总个数</returns>
        public long GetTotalSampleCount(string citFile)
        {
            try
            {
                long[] points = GetPositons(citFile);
                using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;

                        FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                        int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                        long sampleCount = (points[1] - points[0]) / iChannelNumberSize;
                        return sampleCount;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public long GetSampleCountByRange(string citFile,long startPostion,long endPostion)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;

                        FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                        int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                        long sampleCount = (endPostion - startPostion) / iChannelNumberSize;
                        return sampleCount;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取指定采样点个数
        /// </summary>
        /// <param name="citFile">cit文件</param>
        /// <param name="endPostion">结束位置</param>
        /// <returns>采样点个数</returns>
        public long GetAppointSampleCount(string citFile, long endPostion)
        {
            try
            {
                long[] points = GetPositons(citFile);
                using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;

                        FileInformation fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                        int iChannelNumberSize = BytesOfOneSamplePoint(fi.iChannelNumber);
                        long sampleCount = (endPostion - points[0]) / iChannelNumberSize;
                        return sampleCount;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
