/// -------------------------------------------------------------------------------------------
/// FileName：CITFileWriteProcess.cs
/// 说    明：CIT文件相关操作
/// Version ：1.0
/// Date    ：2017/6/6
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitFileSDK
{
    /// <summary>
    /// CIT文件相关操作
    /// </summary>
    public partial class CITFileProcess
    {
        /// <summary>
        /// 向cit文件中写入文件头、数据块
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="fi">文件信息</param>
        /// <param name="channelList">通道定义集合</param>
        /// <param name="extraInfo">补充信息</param>
        /// <param name="channelData">通道数据字节数组集合</param>
        /// <returns>true：成功；false：失败</returns>
        public bool WriteCitFile(string citFile, FileInformation fi, List<ChannelDefinition> channelList, string extraInfo, List<byte[]> channelData)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头文件信息
                        bw.Write(GetBytesFromDataHeadInfo(fi));
                        //写文件头通道定义
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        //写文件头补充信息
                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        //bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }

                        //写通道数据
                        for (int i = 0; i < channelData.Count; i++)
                        {
                            bw.Write(channelData[i]);
                        }

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

        public bool WriteCitFile(string citFile, FileInformation fi, List<ChannelDefinition> channelList, string extraInfo)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头文件信息
                        bw.Write(GetBytesFromDataHeadInfo(fi));
                        //写文件头通道定义
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        //写文件头补充信息
                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        //bw.Write((Byte)(tmpBytes.Length));
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
        /// 向cit文件中写入文件头、数据块
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="fi">文件信息</param>
        /// <param name="channelList">通道定义集合</param>
        /// <param name="extraInfo">补充信息</param>
        /// <param name="arrayDone">通道数据数组集合</param>
        /// <returns>true：成功；false：失败</returns>
        private bool WriteCitFileTemp(string citFile, FileInformation fi, List<ChannelDefinition> channelList, string extraInfo, List<double[]> arrayDone)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头文件信息
                        bw.Write(GetBytesFromDataHeadInfo(fi));
                        //写文件头通道定义
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        //写文件头补充信息
                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        //bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }

                        int iChannelNumberSize = fi.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0;
                        Byte[] tmpDataBytes = new Byte[2];

                        long iArrayLen = arrayDone[0].Length;
                        for (int k = 0; k < iArrayLen; k++)
                        {
                            if (Encryption.IsEncryption(fi.sDataVersion))
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    tmpDataBytes =Encryption.Translate(BitConverter.GetBytes(tmpRmsData));
                                    dataList.AddRange(tmpDataBytes);
                                }
                            }
                            else
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                }
                            }
                            dataList.Clear();
                        }

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
        /// 向cit文件中写入文件头、数据块
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="fi">文件信息</param>
        /// <param name="channelList">通道定义集合</param>
        /// <param name="extraInfo">补充信息</param>
        /// <param name="arrayDone">通道数据数组集合</param>
        /// <returns>true：成功；false：失败</returns>
        public bool WriteCitFile(string citFile, FileInformation fi, List<ChannelDefinition> channelList, string extraInfo, List<double[]> arrayDone)
        {
            try
            {
                int iChannelNumberSize = fi.iChannelNumber * 2;
                byte[] dataArray = new byte[iChannelNumberSize];

                List<Byte[]> dataList = new List<Byte[]>();
                short tmpRmsData = 0;
                Byte[] tmpDataBytes = new Byte[2];

                long iArrayLen = arrayDone[0].Length;
                for (int k = 0; k < iArrayLen; k++)
                {
                    if (Encryption.IsEncryption(fi.sDataVersion))
                    {
                        for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                        {
                            tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                            tmpDataBytes = Encryption.Translate(BitConverter.GetBytes(tmpRmsData));
                            dataList.Add(tmpDataBytes);
                        }
                    }
                    else
                    {
                        for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                        {
                            tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                            dataList.Add(BitConverter.GetBytes(tmpRmsData));
                        }
                    }
                }

                bool isOk = WriteCitFile(citFile, fi, channelList, extraInfo, dataList);
               

                return isOk;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteCitFileHead(string citFile,FileInformation fi)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头文件信息
                        bw.Write(GetBytesFromDataHeadInfo(fi));
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool WriteCitChannelDefintion(string citFile,List<ChannelDefinition> channelDefinitionList)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头通道定义
                        bw.Write(GetBytesFromChannelDataInfoList(channelDefinitionList));
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool WriteCitExtraInfo(string citFile,string extraInfo)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头补充信息
                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        //bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool WriteCitChannelData(string citFile,List<double[]> channelData)
        {
            try
            {
                using (FileStream fs = new FileStream(citFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        FileInformation fileHead = GetFileInformation(citFile);
                        List<ChannelDefinition> channelList = GetChannelDefinitionList(citFile);
                        int iChannelNumberSize = fileHead.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0;
                        Byte[] tmpBytes = new Byte[2];

                        long iArrayLen = channelData[0].Length;
                        for (int k = 0; k < iArrayLen; k++)
                        {
                            
                            if (Encryption.IsEncryption(fileHead.sDataVersion))
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    tmpBytes = Encryption.Translate(BitConverter.GetBytes(tmpRmsData));
                                    dataList.AddRange(tmpBytes);
                                }
                            }
                            else
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                }
                            }
                            bw.Write(dataList.ToArray());
                            bw.Flush();
                            //bw.Flush();

                            dataList.Clear();
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

    }
}
