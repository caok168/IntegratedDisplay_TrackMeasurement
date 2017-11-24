/// -------------------------------------------------------------------------------------------
/// FileName：CITFileProcess.cs
/// 说    明：CIT文件相关操作
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Text;

namespace CitFileSDK
{
    /// <summary>
    /// CIT文件相关操作类
    /// </summary>
    public partial class CITFileProcess
    {
        #region FileInformation 读方法

        /// <summary>
        /// 读取文件头信息
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public FileInformation GetFileInformation(string citFile)
        {
            FileInformation fi;
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;
                    fi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                }
            }
            return fi;
        }

        /// <summary>
        /// 读取文件头信息字节数组
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public byte[] GetFileInformationBytes(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;
                    byte[] bytes = br.ReadBytes(DataOffset.DataHeadLength);

                    return bytes;
                }
            }
        }

        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public int GetDataType(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iDataType), 0);

                    return value;
                }
            }
        }

        /// <summary>
        /// 获取文件版本号
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetDataVersion(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.DataVersion;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sDataVersion);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }

                    return sbValue.ToString();
                }
            }
        }

        /// <summary>
        /// 获取线路代码
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetTrackCode(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.TrackCode;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sTrackCode);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }

                    return sbValue.ToString();
                }
            }
        }

        /// <summary>
        /// 获取线路名
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetTrackName(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.TrackName;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sTrackName);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }

                    return sbValue.ToString();
                }
            }
        }

        /// <summary>
        /// 判断行别 是否为上行 【行别信息：1上行、2下行、3单线】
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsUpDir(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.Dir;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iDir), 0);
                    if (value == 1)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 判断行别 是否为下行
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsDownDir(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.Dir;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iDir), 0);
                    if (value == 2)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 判断行别 是否为单线
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsSingleDir(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.Dir;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iDir), 0);
                    if (value == 3)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 获取检测车号
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetTrainCode(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.TrainCode;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sTrain);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }

                    return sbValue.ToString();
                }
            }
        }

        /// <summary>
        /// 获取检测日期
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetDate(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.Date;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sDate);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }
                    return DateTime.Parse(sbValue.ToString()).ToString("yyyy-MM-dd");
                }
            }
        }

        /// <summary>
        /// 获取检测起始时间
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public string GetTime(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.Time;

                    byte[] bDataInfo = br.ReadBytes(FileInfomationLength.sTime);

                    StringBuilder sbValue = new StringBuilder();
                    for (int i = 1; i <= (int)bDataInfo[0]; i++)
                    {
                        sbValue.Append(UnicodeEncoding.Default.GetString(bDataInfo, i, 1));
                    }

                    return DateTime.Parse(sbValue.ToString()).ToString("HH:mm:ss");
                }
            }
        }

        /// <summary>
        /// 获取检测方向 是否为正向 0
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsForwardDir(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.RunDir;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iRunDir), 0);
                    if (value == 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 获取检测方向 是否为反向 1
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsBackwardDir(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.RunDir;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iRunDir), 0);
                    if (value == 1)
                        return true;
                    else
                        return false;
                }
            }
        }


        //增减里程 增里程0，减里程1
        /// <summary>
        /// 获取增减里程 判断是否为增里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsKmInc(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.KmInc;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iKmInc), 0);
                    if (value == 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 获取增减里程 判断是否为减里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool IsKmDecr(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.KmInc;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iKmInc), 0);
                    if (value == 1)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// 获取开始里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public float GetKmFrom(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.KmFrom;

                    float value = BitConverter.ToSingle(br.ReadBytes(FileInfomationLength.fkmFrom), 0);

                    return value;
                }
            }
        }

        /// <summary>
        /// 获取结束里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public float GetKmTo(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.KmTo;

                    float value = BitConverter.ToSingle(br.ReadBytes(FileInfomationLength.fkmTo), 0);

                    return value;
                }
            }
        }

        /// <summary>
        /// 获取采样数 ，距离采样>0, 时间采样小于0
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public int GetSmaleRate(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.SmaleRate;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iSmaleRate), 0);

                    return value;
                }
            }
        }

        /// <summary>
        /// 获取数据块中通道总数
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public int GetChannelNumber(string citFile)
        {
            using (FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = DataHeadOffset.ChannelNumber;

                    int value = BitConverter.ToInt32(br.ReadBytes(FileInfomationLength.iChannelNumber), 0);

                    return value;
                }
            }
        }

        #endregion

        #region FileInformation 写方法

        /// <summary>
        /// 将文件头信息写入Cit文件
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="fi">文件信息对象</param>
        /// <returns></returns>
        public bool WriteFileInformation(string citFile, FileInformation fi)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        bw.Write(GetBytesFromDataHeadInfo(fi));
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
        /// 将字节数组写入Cit文件
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public bool WriteFileInformationBytes(string citFile, byte[] bytes)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
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
        /// 写入文件类型
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="type">文件类型</param>
        /// <returns></returns>
        public bool WriteDataType(string citFile, int type)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(type);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = 0;
                        bw.Write(tmp);
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
        /// 写入文件版本号
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="version">文件版本号</param>
        /// <returns></returns>
        public bool WriteDataVersion(string citFile, string version)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(version);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        if (tmpBytes.Length < 20)
                        {
                            for (int i = 0; i < (20 - tmpBytes.Length); i++)
                            {
                                bw1.Write((byte)0);
                            }
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.DataVersion;
                        bw.Write(tmp);
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
        /// 写入线路代码
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="code">线路代码</param>
        /// <returns></returns>
        public bool WriteTrackCode(string citFile, string code)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(code);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        if (tmpBytes.Length < 20)
                        {
                            for (int i = 0; i < (20 - tmpBytes.Length); i++)
                            {
                                bw1.Write((byte)0);
                            }
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.TrackCode;
                        bw.Write(tmp);
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
        /// 写入线路名
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool WriteTrackName(string citFile, string name)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(name);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        if (tmpBytes.Length < 20)
                        {
                            for (int i = 0; i < (20 - tmpBytes.Length); i++)
                            {
                                bw1.Write((byte)0);
                            }
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.TrackName;
                        bw.Write(tmp);
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
        /// 写入行别 上行
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetUpDir(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(1);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.Dir;
                        bw.Write(tmp);
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
        /// 写入行别 下行
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetDownDir(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(2);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.Dir;
                        bw.Write(tmp);
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
        /// 写入行别 单线
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetSingleDir(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(3);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.Dir;
                        bw.Write(tmp);
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
        /// 写入检测车号
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="code">检测车号</param>
        /// <returns></returns>
        public bool WriteTrainCode(string citFile, string trainCode)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(trainCode);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw1.Write((byte)0);
                            }
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.TrackCode;
                        bw.Write(tmp);
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
        /// 写入检测日期
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="date">检测日期</param>
        /// <returns></returns>
        public bool WriteDate(string citFile, string date)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(date);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        for (int i = 0; i < 10 - tmpBytes.Length; i++)
                        {
                            bw1.Write((byte)0);
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.Date;
                        bw.Write(tmp);
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
        /// 写入起始时间
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="time">起始时间</param>
        /// <returns></returns>
        public bool WriteTime(string citFile, string time)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);

                        Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(time);
                        bw1.Write((Byte)(tmpBytes.Length));
                        bw1.Write(tmpBytes);
                        for (int i = 0; i < 8 - tmpBytes.Length; i++)
                        {
                            bw1.Write((byte)0);
                        }
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.Time;
                        bw.Write(tmp);
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
        /// 写入检测方向 正向
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetForwardDir(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(0);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.RunDir;
                        bw.Write(tmp);
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
        /// 写入检测方向 反向
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetBackwardDir(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(1);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.RunDir;
                        bw.Write(tmp);
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
        /// 写入增减里程 增里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetKmInc(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(0);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.KmInc;
                        bw.Write(tmp);
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
        /// 写入增减里程 减里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <returns></returns>
        public bool SetKmDecr(string citFile)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(1);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.KmInc;
                        bw.Write(tmp);
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

        //大部分情况下，fkmFrom, fkmTo这两个文件信息部分都被设置成了0.
        /// <summary>
        /// 写入开始里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="kmfrom">开始里程</param>
        /// <returns></returns>
        public bool SetKmFrom(string citFile, float kmfrom)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(kmfrom);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.KmFrom;
                        bw.Write(tmp);
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
        /// 写入结束里程
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="kmto">结束里程</param>
        /// <returns></returns>
        public bool SetKmTo(string citFile, float kmto)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(kmto);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.KmTo;
                        bw.Write(tmp);
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
        /// 写入采样数 距离采样>0, 时间采样小于0
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="rate">采样数</param>
        /// <returns></returns>
        public bool WriteSmaleRate(string citFile, int rate)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(rate);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.SmaleRate;
                        bw.Write(tmp);
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
        /// 写入数据块中的通道总数
        /// </summary>
        /// <param name="citFile">cit文件路径</param>
        /// <param name="number">通道总数</param>
        /// <returns></returns>
        public bool WriteChannelNumber(string citFile, int number)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        MemoryStream mStream = new MemoryStream();
                        BinaryWriter bw1 = new BinaryWriter(mStream, Encoding.UTF8);
                        bw1.Write(number);
                        bw1.Flush();
                        bw1.Close();
                        byte[] tmp = mStream.ToArray();
                        mStream.Flush();
                        mStream.Close();

                        bw.BaseStream.Position = DataHeadOffset.ChannelNumber;
                        bw.Write(tmp);
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

    }
}
