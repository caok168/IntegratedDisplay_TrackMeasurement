namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 台帐信息
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 开始里程
        /// </summary>
        public long StartMileage { get; set; }
        /// <summary>
        /// 结束里程
        /// </summary>
        public long EndMileage { get; set; }
        /// <summary>
        /// 台帐所关注的信息
        /// </summary>
        public string AccountKeywords { get; set; }
        /// <summary>
        /// 台帐类型
        /// </summary>
        public string AccountType { get; set; }
    }
}
