/// -------------------------------------------------------------------------------------------
/// FileName：IndexSta.cs
/// 说    明：长短链索引数据
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 与数据库中IndexSta表对应的长短链索引数据类
    /// </summary>
    public class IndexSta
    {
        /// <summary>
        /// 长短链索引id
        /// </summary>
        public int iID { get; set; }

        /// <summary>
        /// 这个值估计没什么特殊含义
        /// </summary>
        public int iIndexID { get; set; }

        /// <summary>
        /// 长短链对应的起始文件指针
        /// </summary>
        public long lStartPoint { get; set; }

        /// <summary>
        /// 长短链对应的起始公里标
        /// </summary>
        public string lStartMeter { get; set; }

        /// <summary>
        /// 长短链对应的终止文件指针
        /// </summary>
        public long lEndPoint { get; set; }

        /// <summary>
        /// 长短链对应的终止公里标
        /// </summary>
        public string LEndMeter { get; set; }

        /// <summary>
        /// 长短链所包含的采样点数
        /// </summary>
        public long lContainsPoint { get; set; }

        /// <summary>
        /// 长短链所包含的公里数（单位为公里）
        /// </summary>
        public string lContainsMeter { get; set; }

        /// <summary>
        /// 长短链类别
        /// </summary>
        public string sType { get; set; }
    }
}
