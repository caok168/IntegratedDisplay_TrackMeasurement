﻿/// -------------------------------------------------------------------------------------------
/// FileName：ChannelDefinition.cs
/// 说    明：文件通道定义类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 文件通道定义类
    /// </summary>
    public class ChannelDefinition
    {
        /// <summary>
        /// 通道Id：轨检通道从1～1000定义；动力学从1001~2000；弓网从2001~3000-----4个字节
        /// </summary>
        public int sID { get; set; }

        /// <summary>
        /// 通道名称英文，不足补空格-----1+20个字节
        /// </summary>
        public string sNameEn { get; set; }

        /// <summary>
        /// 通道名称中文，不足补空格-----1+20个字节
        /// </summary>
        public string sNameCh { get; set; }

        /// <summary>
        /// 通道比例-----4个字节
        /// </summary>
        public float fScale { get; set; }

        /// <summary>
        /// 通道基线值-----4个字节
        /// </summary>
        public float fOffset { get; set; }

        /// <summary>
        /// 通道单位，不足补空格-----1+10个字节
        /// </summary>
        public string sUnit { get; set; }

    }
}
