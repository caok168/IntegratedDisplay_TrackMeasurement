/// -------------------------------------------------------------------------------------------
/// FileName：Defects.cs
/// 说    明：偏差相关信息
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 偏差类
    /// </summary>
    public class Defects
    {
        public int iRecordNumber = 0;
        /// <summary>
        /// 单位为公里
        /// </summary>
        public int iMaxpost = 0;
        /// <summary>
        /// 单位为米
        /// </summary>
        public double dMaxminor = 0;
        public bool bFix = false;
        /// <summary>
        /// 获取公里标，单位为厘米
        /// </summary>
        /// <returns></returns>
        public int GetMeter()
        {
            return iMaxpost * 100000 + (int)(dMaxminor * 100);
        }
    }
}
