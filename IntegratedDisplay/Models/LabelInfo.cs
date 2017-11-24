/// -------------------------------------------------------------------------------------------
/// FileName：LabelInfo.cs
/// 说    明：标记类
/// Version ：1.0
/// Date    ：2017/6/5
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Drawing;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 标记类
    /// </summary>
    public class LabelInfo
    {
        /// <summary>
        /// 标注id
        /// </summary>
        public int iID { set; get; }

        /// <summary>
        ///  文件指针
        /// </summary>
        public string sMileIndex { set; get; }

        /// <summary>
        /// 里程位置
        /// </summary>
        public string sMile { set; get; }

        /// <summary>
        /// 标注信息
        /// </summary>
        public string sMemoText { set; get; }

        /// <summary>
        /// 标注日期
        /// </summary>
        public String logDate { set; get; }

        /// <summary>
        /// 标注的位置和占用的矩形框
        /// </summary>
        public int rectY { get; set; }
    }
}
