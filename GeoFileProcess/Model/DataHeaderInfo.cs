using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFileProcess
{
    public class DataHeaderInfo
    {
        /// <summary>
        /// 文件版本
        /// </summary>
        public short fileVersion { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public short dirFlag { get; set; }

        /// <summary>
        /// 数据记录长度
        /// </summary>
        public int dataRecordLength { get; set; }

        /// <summary>
        /// 采样间隔
        /// </summary>
        public float sampleInterval { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public float postUnits { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        public string Area { get; set; }

        public string Division { get; set; }

        public string Region { get; set; }
    }
}
