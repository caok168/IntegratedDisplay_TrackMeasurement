namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 图层配置信息
    /// </summary>
    public class LayerConfigData
    {
        /// <summary>
        /// 图层名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示图层
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// 是否显示里程标签
        /// </summary>
        public bool IsMileageLabelVisible { get; set; }

        /// <summary>
        /// 是否显示通道标签
        /// </summary>
        public bool IsChannelLabelVisible { get; set; }

        /// <summary>
        /// 是否显示标注
        /// </summary>
        public bool IsAnnotationVisible { get; set; }

        /// <summary>
        /// 是否波形左右反转
        /// </summary>
        public bool IsReverse { get; set; }

        public LayerConfigData()
        {
            Name = "";
            IsVisible = true;
            IsMileageLabelVisible = true;
            IsChannelLabelVisible = true;
            IsAnnotationVisible = true;
            IsReverse = false;
        }
    }
}
