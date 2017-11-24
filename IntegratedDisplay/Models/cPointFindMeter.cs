/// -------------------------------------------------------------------------------------------
/// FileName：cPointFindMeter.cs
/// 说    明：iic修正时使用的数据类
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// iic修正时使用的数据类
    /// </summary>
    public class cPointFindMeter
    {
        /// <summary>
        /// 公里标：单位为厘米
        /// </summary>
        public long lMeter = 0;
        /// <summary>
        /// 里程对应的文件指针
        /// </summary>
        public long lLoc = 0;
    }
}
