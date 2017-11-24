/// ---------------------------------------------------------------------------------------------------------------------------------------
/// FileName:WaveformDragData.cs
/// 说    明：波形通道拖动类
/// Version:  1.0 
/// Date:     2017/5/27  
/// Author:   Jinxl
//// --------------------------------------------------------------------------------------------------------------------------------------
namespace IntegratedDisplay.Models
{
    public class WaveformDragData
    {
        /// <summary>
        /// 选择通道的Y值
        /// </summary>
        public int SelectDragItemY { get; set; }

        /// <summary>
        /// 选中项在所有数据中的索引
        /// </summary>
        public int SelectDragDataIndex { get; set; }
        /// <summary>
        /// 选中项在通道的索引
        /// </summary>
        public int SelectDragChannel { get; set; }

        /// <summary>
        /// 初始化波形拖动数据
        /// </summary>
        public WaveformDragData()
        {
            SelectDragItemY = -1;
            SelectDragDataIndex = -1;
            SelectDragChannel = -1;
        }
    }
}
