using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK.MileageFix
{
    public class MileStoneFixData
    {
        public string ID { get; set; }
        /// <summary>
        /// 标记的起始点
        /// </summary>
        public UserMarkedPoint MarkedStartPoint { get; set; }
        /// <summary>
        /// 标记的终止点
        /// </summary>
        public UserMarkedPoint MarkedEndPoint { get; set; }
        /// <summary>
        /// 标记点之间的长短链
        /// </summary>
        public List<LongChain> Chains { get; set; }
        /// <summary>
        /// 标记点之间的实际距离,单位为：米
        /// </summary>
        public float RealDistance { get; set; }
        /// <summary>
        /// 标记点之间的采样点个数
        /// </summary>
        public long SamplePointCount { get; set; }
        /// <summary>
        /// 两个采样点之间的距离
        /// </summary>
        public float SampleRate { get; set; }
    }
}
