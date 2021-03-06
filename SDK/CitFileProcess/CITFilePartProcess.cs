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
        /// <summary>
        /// 获取采样点包含的字节数【一个采样点包含的字节数 =  采样点数目 x 2 . 每个采样点在文件中是2个字节】
        /// </summary>
        /// <param name="channelNum">采样点数目</param>
        /// <returns>采样点包含的字节数</returns>
        private int BytesOfOneSamplePoint(int channelNum)
        {
            return channelNum * 2;
        }


        #region FileInformation

        /// <summary>
        /// 读取cit文件头中的文件信息信息，并返回文件头信息结构体
        /// </summary>
        /// <param name="bDataInfo">文件头中包含文件信息的120个字节 </param>
        /// <returns>文件信息结构体</returns>
        private FileInformation GetDataInfoHead(byte[] bDataInfo)
        {
            FileInformation fi = new FileInformation();
            try
            {
                StringBuilder sbDataVersion = new StringBuilder();
                StringBuilder sbTrackCode = new StringBuilder();
                StringBuilder sbTrackName = new StringBuilder();
                StringBuilder sbTrain = new StringBuilder();
                StringBuilder sbDate = new StringBuilder();
                StringBuilder sbTime = new StringBuilder();

                //数据类型
                fi.iDataType = BitConverter.ToInt32(bDataInfo, 0); //iDataType：1轨检、2动力学、3弓网，

                //1+20个字节，数据版本
                for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.DataVersion]; i++)
                {
                    sbDataVersion.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.DataVersion + i, 1));
                }
                //1+4个字节，线路代码
                for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackCode]; i++)
                {
                    sbTrackCode.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackCode + i, 1));
                }
                //1+20个字节，线路名
                //for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackName]; i += 2)
                //{
                //    sbTrackName.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackName + i, 2));
                //}

                //一次性读完TrackName域中的内容
                sbTrackName.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackName + 1, (int)bDataInfo[DataHeadOffset.TrackName]));

                //检测方向
                fi.iDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.Dir);

                //1+20个字节，检测车号
                for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrainCode]; i++)
                {
                    sbTrain.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrainCode + i, 1));
                }
                //1+10个字节，检测日期
                for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Date]; i++)
                {
                    sbDate.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Date + i, 1));
                }
                //1+8个字节，检测时间
                for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Time]; i++)
                {
                    sbTime.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Time + i, 1));
                }

                fi.iRunDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.RunDir);
                fi.iKmInc = BitConverter.ToInt32(bDataInfo, DataHeadOffset.KmInc);
                fi.fkmFrom = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmFrom);
                fi.fkmTo = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmTo);
                fi.iSmaleRate = BitConverter.ToInt32(bDataInfo, DataHeadOffset.SmaleRate);
                if (fi.iSmaleRate < 0)
                {
                    // liyang: cit中的这个iSmaleRate是-2000， 这块算出来是20
                    fi.iSmaleRate = 4; //Math.Abs(fi.iSmaleRate) / 100;
                }
                fi.iChannelNumber = BitConverter.ToInt32(bDataInfo, DataHeadOffset.ChannelNumber);
                fi.sDataVersion = sbDataVersion.ToString();
                fi.sDate = DateTime.Parse(sbDate.ToString()).ToString("yyyy-MM-dd");
                fi.sTime = DateTime.Parse(sbTime.ToString()).ToString("HH:mm:ss");
                fi.sTrackCode = sbTrackCode.ToString();
                fi.sTrackName = sbTrackName.ToString();
                fi.sTrain = sbTrain.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fi;
        }

        /// <summary>
        /// 读取cit文件头中的文件信息信息，返回文件头信息的字节数组
        /// </summary>
        /// <param name="fi">文件信息结构体</param>
        /// <returns></returns>
        private Byte[] GetBytesFromDataHeadInfo(FileInformation fi)
        {
            List<Byte> byteList = new List<Byte>();

            MemoryStream mStream = new MemoryStream();

            BinaryWriter bw = new BinaryWriter(mStream, Encoding.UTF8);

            //文件类型---1
            bw.Write(fi.iDataType);
            //文件版本号----2
            Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sDataVersion);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 20)
            {
                for (int i = 0; i < (20 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }

            //线路代码----3
            tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sTrackCode);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 4)
            {
                for (int i = 0; i < (4 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }
            //线路名，英文最好----4
            tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sTrackName);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 20)
            {
                for (int i = 0; i < (20 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }
            //行别：1-上，2-下，3-单线----5
            bw.Write(fi.iDir);
            //检测车号---6
            tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sTrain);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 20 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测日期：yyyy-MM-dd-----7
            tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sDate);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 10 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测起始时间：HH:mm:ss----8
            tmpBytes = UnicodeEncoding.Default.GetBytes(fi.sTime);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 8 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测方向，正0，反1-----9
            bw.Write(fi.iRunDir);
            //增里程0，减里程1-----10
            bw.Write(fi.iKmInc);
            //开始里程-----11
            bw.Write(fi.fkmFrom);
            //结束里程，检测结束后更新----12
            bw.Write(fi.fkmTo);
            //采样数，（距离采样>0, 时间采样<0）----13
            bw.Write(fi.iSmaleRate);
            //数据块中通道总数----14
            bw.Write(fi.iChannelNumber);

            bw.Flush();
            bw.Close();

            byte[] tmp = mStream.ToArray();

            mStream.Flush();
            mStream.Close();

            byteList.AddRange(tmp);

            return byteList.ToArray();
        }

        #endregion

        #region ChannelDefinition

        /// <summary>
        /// 获取单个通道定义信息
        /// </summary>
        /// <param name="bDataInfo">包含通道定义信息的字节数组</param>
        /// <param name="start">起始下标</param>
        /// <returns>通道定义信息结构体对象</returns>
        private ChannelDefinition GetChannelInfo(byte[] bDataInfo, int start)
        {
            ChannelDefinition cd = new ChannelDefinition();
            StringBuilder sUnit = new StringBuilder();


            cd.sID = BitConverter.ToInt32(bDataInfo, start);//通道起点为0，导致通道id取的都是第一个通道的id，把0改为start，
                                                            //1+20   通道英文名
            cd.sNameEn = UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.NameEn + 1 + start, (int)bDataInfo[DataChannelOffset.NameEn + start]);
            //1+20    通道中文名
            cd.sNameCh = UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.NameCh + 1 + start, (int)bDataInfo[DataChannelOffset.NameCh + start]);
            //通道单位 1+10
            for (int i = 1; i <= (int)bDataInfo[DataChannelOffset.Unit + start]; i++)
            {
                sUnit.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.Unit + i + start, 1));
            }
            cd.sUnit = sUnit.ToString();

            //4  通道比例
            cd.fScale = BitConverter.ToSingle(bDataInfo, DataChannelOffset.Scale + start);
            //4   通道基线值
            cd.fOffset = BitConverter.ToSingle(bDataInfo, DataChannelOffset.Offset + start);


            return cd;
        }

        /// <summary>
        /// 将通道定义集合转换成字节数组
        /// </summary>
        /// <param name="cdList">通道定义集合</param>
        /// <returns>字节数组</returns>
        private Byte[] GetBytesFromChannelDataInfoList(List<ChannelDefinition> cdList)
        {
            List<Byte> byteList = new List<Byte>();

            if (cdList == null || cdList.Count == 0)
            {
                //throw new Exception("通道结构体为空");
                return byteList.ToArray();
            }

            MemoryStream mStream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(mStream, Encoding.UTF8);

            foreach (ChannelDefinition m_dci in cdList)
            {
                //1----轨检通道从1～1000定义,动力学从1001~2000,弓网从2001~3000
                bw.Write(m_dci.sID);
                //2----通道英文名，不足补空格
                Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sNameEn);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 20)
                {
                    for (int i = 0; i < (20 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }
                //3----通道中文名，不足补空格
                tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sNameCh);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 20)
                {
                    for (int i = 0; i < (20 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }
                //4----通道比例
                bw.Write(m_dci.fScale);
                //5----通道基线值
                bw.Write(m_dci.fOffset);
                //6----通道单位
                tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sUnit);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 10)
                {
                    for (int i = 0; i < (10 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }
            }

            bw.Flush();
            bw.Close();

            byte[] tmp = mStream.ToArray();

            mStream.Flush();
            mStream.Close();

            byteList.AddRange(tmp);

            return byteList.ToArray();
        }

        #endregion

    }
}
