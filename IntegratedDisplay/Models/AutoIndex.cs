/// -------------------------------------------------------------------------------------------
/// FileName：AutoIndex.cs
/// 说    明：里程快速校正处理相关操作
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 里程快速校正处理类
    /// </summary>
    public class AutoIndex
    {
        public long milePos;
        public int km_current;
        public int meter_current;
        public int km_pre;
        public int meter_pre;
        public int meter_between;

        public float MileCurrent
        {
            get
            {
                return km_current + meter_current / 4;
            }
        }
    }
}
