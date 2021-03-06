﻿/// -------------------------------------------------------------------------------------------
/// FileName：FileDataOffset.cs
/// 说    明：文件偏移类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 文件偏移操作类
    /// </summary>
    public class FileDataOffset
    {
        // 文件类型数据偏移位置
        public static int msDataTypeOffset = 0;
        // 文件版本号数据偏移位置
        public static int msDataVersionOffset = 4;
        // 线路代码数据偏移位置
        public static int msTrackCodeOffset = 25;
        // 线路名数据偏移位置
        public static int msTrackNameOffset = 30;
        // 行别数据偏移位置
        public static int msDirOffset = 51;
        // 检测车号数据偏移位置
        public static int msTrainCodeOffset = 55;
        // 检测日期数据偏移位置
        public static int msDateOffset = 76;
        // 检测起始时间数据偏移位置
        public static int msTimeOffset = 87;
        // 检测方向数据偏移位置
        public static int msRunDirOffset = 96;
        // 增减里程数据偏移位置
        public static int msKmIncOffset = 100;
        // 开始里程数据偏移位置
        public static int msKmFromOffset = 104;
        // 结束里程数据偏移位置
        public static int msKmToOffset = 108;
        // 采样频率数据偏移位置
        public static int msSmaleRateOffset = 112;
        // 通道个数数据偏移位置
        public static int msChannelNumberOffset = 116;
        // 通道定义数据偏移位置
        public static int msChannelDefOffset = 120;

        /// <summary>
        /// 获取补充信息的偏移量
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <returns></returns>
        public static int GetExtraInfoOffset(int channelNumber)
        {
            return DataOffset.DataHeadLength + DataOffset.DataChannelLength * channelNumber;
        }

        /// <summary>
        /// 获取采样点数据的偏移量
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="extraInfoSize"></param>
        /// <returns></returns>
        public static long GetSamplePointStartOffset(int channelNumber, int extraInfoSize)
        {
            return DataOffset.DataHeadLength + DataOffset.DataChannelLength * channelNumber + extraInfoSize;
        }
    }
}
