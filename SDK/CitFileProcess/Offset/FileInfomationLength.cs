﻿/// -------------------------------------------------------------------------------------------
/// FileName：FileInfomationLength.cs
/// 说    明：文件字段长度类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 文件字段长度类
    /// </summary>
    public class FileInfomationLength
    {
        /// <summary>
        /// 文件类型 iDataType：1轨检、2动力学、3弓网----4个字节
        /// </summary>
        public static int iDataType = 4;

        /// <summary>
        /// 文件版本号，用X.X.X表示 第一位大于等于3代表加密后,只加密数据块部分---1+20个字节，第一个字节表示实际长度，以下类同
        /// </summary>
        public static int sDataVersion = 20;

        /// <summary>
        /// 线路代码，同PWMIS----1+4个字节
        /// </summary>
        public static int sTrackCode = 4;

        /// <summary>
        /// 线路名  英文最好---1+20个字节
        /// </summary>
        public static int sTrackName = 20;

        /// <summary>
        /// 行别：1上行、2下行、3单线----4个字节
        /// </summary>
        public static int iDir = 4;

        /// <summary>
        /// 检测车号，不足补空格---1+20个字节
        /// </summary>
        public static int sTrain = 20;

        /// <summary>
        /// 检测日期：yyyy-MM-dd---1+10个字节
        /// </summary>
        public static int sDate = 20;

        /// <summary>
        /// 检测起始时间：HH:mm:ss---1+8个字节
        /// </summary>
        public static int sTime = 8;

        /// <summary>
        /// 检测方向，正0，反1----4个字节
        /// </summary>
        public static int iRunDir = 4;

        /// <summary>
        /// 增里程0，减里程1----4个字节
        /// </summary>
        public static int iKmInc = 4;

        /// <summary>
        /// 开始里程----4个字节
        /// </summary>
        public static int fkmFrom = 4;

        /// <summary>
        /// 结束里程，检测结束后更新----4个字节
        /// </summary>
        public static int fkmTo = 4;

        /// <summary>
        /// 采样数，距离采样>0, 时间采样小于0 ----4个字节
        /// </summary>
        public static int iSmaleRate = 4;

        /// <summary>
        /// 数据块中通道总数----4个字节
        /// </summary>
        public static int iChannelNumber = 4;
    }
}
