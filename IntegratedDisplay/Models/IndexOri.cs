/// -------------------------------------------------------------------------------------------
/// FileName：IndexOri.cs
/// 说    明：原始索引相关
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 原始索引信息__对应与cit同名的idf数据库中的IndexOri表
    /// </summary>
    public class IndexOri
    {
        /// <summary>
        /// 索引id
        /// </summary>
        public int iId;
        /// <summary>
        /// 索引类型：0-原有的数据；1-新插入的数据
        /// </summary>
        public int iIndexId;
        /// <summary>
        /// 索引对应的文件指针
        /// </summary>
        public string IndexPoint;
        /// <summary>
        /// 索引对应的里程数
        /// </summary>
        public string IndexMeter;
    }
}
