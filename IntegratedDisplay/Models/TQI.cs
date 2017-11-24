/// -------------------------------------------------------------------------------------------
/// FileName：TQI.cs
/// 说    明： TQI类
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// TQI类
    /// </summary>
    public class TQI
    {
        public double zgd = 0;
        public double ygd = 0;
        public double zgx = 0;
        public double ygx = 0;
        public double gj = 0;
        public double sp = 0;
        public double sjk = 0;
        public double hj = 0;
        public double cj = 0;
        public int pjsd = 0;
        public int iKM = 0;
        public float iMeter = 0;
        public double GetTQISum()
        {
            return zgd + ygd + zgx + ygx + gj + sp + sjk;
        }
        public int iValid = 1;

        public String subCode = null;
        public DateTime runDate = DateTime.Now;
    }
}
