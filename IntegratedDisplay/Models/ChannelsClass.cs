/// -------------------------------------------------------------------------------------------
/// FileName：ChannelsClass.cs
/// 说    明：通道相关类
/// Version ：1.0
/// Date    ：2017/5/27
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System.Drawing;

namespace IntegratedDisplay.Models
{
    /// <summary>
    /// 通道相关类，包含通道的配置信息和该通道的所有采样点的信息
    /// </summary>
    public class ChannelsClass
    {
        /// <summary>
        /// 通道序号
        /// </summary>
        public int Id = -1;

        /// <summary>
        /// 原始通道名
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 通道英文名---和原始通道名一样
        /// </summary>
        public string NonChineseName = string.Empty;

        /// <summary>
        /// 通道中文名
        /// </summary>
        public string ChineseName = string.Empty;

        /// <summary>
        /// 通道颜色
        /// </summary>
        public int Color = 0;

        /// <summary>
        /// 通道是否显示
        /// </summary>
        public bool IsVisible = true;

        /// <summary>
        /// 显示比例
        /// </summary>
        public float ZoomIn = 1.0f;

        /// <summary>
        /// 通道单位
        /// </summary>
        public string Units = string.Empty;

        /// <summary>
        /// 是否包含偏移量
        /// </summary>
        public bool IsMeaOffset = false;

        /// <summary>
        /// 通道基线位置(波形显示时的垂直方向)
        /// </summary>
        public int Location = 1;

        /// <summary>
        /// 通道波形的线宽
        /// </summary>
        public float LineWidth = 2.0f;

        /// <summary>
        /// 是否波形上行反转
        /// </summary>
        public bool IsReverse = false;

        /// <summary>
        /// 通道显示---通道信息显示--右边的矩形框
        /// </summary>
        public Rectangle DisplayRect { set; get; }

        /// <summary>
        /// 通道拖拽框
        /// </summary>
        public Rectangle DragRect { set; get; }

        /// <summary>
        /// 通道颜色区
        /// </summary>
        public Rectangle ColorRect { set; get; }

        /// <summary>
        /// 该通道所有的数据--原始数据，直接从cit文件读取，没做处理
        /// </summary>
        public double[] Data { get; set; }

        /// <summary>
        /// 通道比例
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// 通道基线值
        /// </summary>
        public float Offset { get; set; }

        /// <summary>
        /// 通道高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 是否显示信息框
        /// </summary>
        public bool IsShowRect { get; set; }

        /// <summary>
        /// 放大时是否显示
        /// </summary>
        public bool IsZoomInView { get; set; }


    }
}
