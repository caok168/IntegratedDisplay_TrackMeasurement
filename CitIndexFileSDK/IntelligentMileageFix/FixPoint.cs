using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK.IntelligentMileageFix
{
    /// <summary>
    /// 修正点
    /// </summary>
    public class FixPoint
    {
        /// <summary>
        /// 对比的点集合
        /// </summary>
        public double[] Points { get; set; }

        /// <summary>
        /// 修正的位置
        /// </summary>
        public long FixPostion { get; set; }

        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelID { get; set; }

        /// <summary>
        /// 原始里程
        /// </summary>
        public float OriginalMileage { get; set; }
    }
}
