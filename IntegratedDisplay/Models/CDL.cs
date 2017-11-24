/// -------------------------------------------------------------------------------------------
/// FileName：IndexOri.cs
/// 说    明：长短链相关信息
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 长短链类
    /// </summary>
    public class CDL
    {
        /*
         * 长链时：  dKM+iMeter---表示发生长链的公里标，实际长链=iMeter-1000.(长链时iMeter〉1000)
         *          
         * 短链时：  表示在dKM公里附近发生短链，短链=iMeter。
         * 
         * 
         * 
         * 
         */

        /// <summary>
        /// 单位--公里
        /// </summary>
        public float dKM;
        /// <summary>
        /// 单位--米
        /// </summary>
        public int iMeter;
        /// <summary>
        /// 长短链类型
        /// </summary>
        public string sType;
    }
}
