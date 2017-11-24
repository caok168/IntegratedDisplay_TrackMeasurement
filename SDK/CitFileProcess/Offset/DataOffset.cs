/// -------------------------------------------------------------------------------------------
/// FileName：DataOffset.cs
/// 说    明：文件信息偏移类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 文件信息偏移类
    /// </summary>
    public class DataOffset
    {
        /// <summary>
        /// 文件头 文件信息长度（120）
        /// </summary>
        public static int DataHeadLength = 120;

        /// <summary>
        /// 文件头 通道定义长度（65）
        /// </summary>
        public static int DataChannelLength = 65;

        /// <summary>
        /// 文件头 补充信息长度（4）
        /// </summary>
        public static int ExtraLength = 4;
    }
}
