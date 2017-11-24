/// -------------------------------------------------------------------------------------------
/// FileName：StatisticsData.cs
/// 说    明：无效标记统计相关操作
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 无效标记统计类
    /// </summary>
    public class StatisticsData
    {
        public int totalCount;
        public int totalGongli;
        public StatisticsData(int count, int gongli)
        {
            totalCount = count;
            totalGongli = gongli;
        }

        /// <summary>
        /// 原因类型
        /// </summary>
        public String reasonType;
        /// <summary>
        /// 同一类型的无效区段的个数
        /// </summary>
        public int sumcount = 0;
        /// <summary>
        /// 个数百分比
        /// </summary>
        public String countPercent
        {
            get
            {
                String value = String.Format("{0}%",(float)(sumcount)/totalCount*100);
                return value;
            }
        }
        /// <summary>
        /// 公里百分比
        /// </summary>
        public String gongliPercent
        {
            get
            {
                String value = String.Format("{0}%", (float)(sumGongli) / totalGongli * 100);
                return value;
            }
        }

        public int sumGongli = 0;
    }
}
