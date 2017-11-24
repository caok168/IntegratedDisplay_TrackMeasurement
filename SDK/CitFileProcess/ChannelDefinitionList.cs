/// -------------------------------------------------------------------------------------------
/// FileName：DataOffset.cs
/// 说    明：文件通道相关操作
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System.Collections.Generic;

namespace CitFileSDK
{
    /// <summary>
    /// 通道定义相关操作类
    /// </summary>
    public class ChannelDefinitionList
    {
        /// <summary>
        /// 通道定义集合
        /// </summary>
        public List<ChannelDefinition> channelDefinitionList { get; set; }

        /// <summary>
        /// 根据通道名称查找通道号
        /// </summary>
        /// <param name="channelNameEn">通道英文名称</param>
        /// <param name="channelNameCh">通道中文名称</param>
        /// <returns>通道号：-1查找失败；-1之外查找到的通道名称对应的通道号</returns>
        public int GetChannelIdByName(string channelNameEn, string channelNameCh)
        {
            int channelNumber = -1;
            for (int i = 0; i < channelDefinitionList.Count; i++)
            {
                if ((channelDefinitionList[i].sNameEn.Equals(channelNameEn) || channelDefinitionList[i].sNameCh.Equals(channelNameCh)))
                {
                    channelNumber = i + 1;
                    break;
                }
            }

            return channelNumber;
        }

        /// <summary>
        /// 根据通道名称获取通道
        /// </summary>
        /// <param name="channelNameEn">通道英文名称</param>
        /// <param name="channelNameCh">通道中文名称</param>
        /// <returns>通道定义：null查找失败；null之外查找到的通道定义</returns>
        public ChannelDefinition GetChannelByName(string channelNameEn, string channelNameCh)
        {
            ChannelDefinition channelDefin = null;
            for (int i = 0; i < channelDefinitionList.Count; i++)
            {
                //增加判断StartsWith是因为有的cit里米通道为meter，有的为m
                if ((channelDefinitionList[i].sNameEn.ToUpper().Equals(channelNameEn.ToUpper()) 
                    || channelDefinitionList[i].sNameCh.ToUpper().Equals(channelNameCh.ToUpper()))
                    || channelDefinitionList[i].sNameEn.ToUpper().StartsWith(channelNameEn.ToUpper()))
                {
                    //channelNumber = i + 1;
                    channelDefin = channelDefinitionList[i];
                    break;
                }
            }

            return channelDefin;
        }

        /// <summary>
        /// 根据通道号查询通道英文名称
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <returns>通道英文名称</returns>
        public string GetChannelEnNameById(int channelId)
        {
            string channelName = "";
            if (channelDefinitionList.Count >= channelId)
            {
                channelName = channelDefinitionList[channelId - 1].sNameEn;
            }
            return channelName;
        }

        /// <summary>
        /// 根据通道号查询通道中文名称
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <returns>通道中文名称</returns>
        public string GetChannelChNameById(int channelId)
        {
            string channelName = "";
            if (channelDefinitionList.Count >= channelId)
            {
                channelName = channelDefinitionList[channelId - 1].sNameCh;
            }
            return channelName;
        }

        /// <summary>
        /// 根据通道号查询通道比例
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <returns>通道比例</returns>
        public float GetChannleScale(int channelId)
        {
            float channelScale = 0;
            if (channelDefinitionList.Count >= channelId)
            {
                channelScale = channelDefinitionList[channelId - 1].fScale;
            }
            return channelScale;
        }

        /// <summary>
        /// 根据通道名称获取通道比例
        /// </summary>
        /// <param name="channelNameEn">通道英文名称</param>
        /// <param name="channelNameCh">通道中文名称</param>
        /// <returns>通道比例</returns>
        public float GetChannelScale(string channelNameEn, string channelNameCh)
        {
            float channelScale = 0;
            for (int i = 0; i < channelDefinitionList.Count; i++)
            {
                if (channelDefinitionList[i].sNameEn == channelNameEn || channelDefinitionList[i].sNameCh == channelNameCh)
                {
                    channelScale = channelDefinitionList[i].fScale;
                    break;
                }
            }
            return channelScale;
        }

        /// <summary>
        /// 根据通道号获取通道基准线
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <returns>通道基准线</returns>
        public float GetChannelOffset(int channelId)
        {
            float channelOffset = 0;
            if (channelDefinitionList.Count >= channelId)
            {
                channelOffset = channelDefinitionList[channelId - 1].fOffset;
            }
            return channelOffset;
        }

        /// <summary>
        /// 根据通道名称获取通道基准线
        /// </summary>
        /// <param name="channelNameEn">通道英文名称</param>
        /// <param name="channelNameCh">通道中文名称</param>
        /// <returns>通道基准线</returns>
        public float GetChannelOffset(string channelNameEn, string channelNameCh)
        {
            float channelOffset = 0;
            for (int i = 0; i < channelDefinitionList.Count; i++)
            {
                if (channelDefinitionList[i].sNameEn == channelNameEn || channelDefinitionList[i].sNameCh == channelNameCh)
                {
                    channelOffset = channelDefinitionList[i].fOffset;
                    break;
                }
            }
            return channelOffset;
        }

        /// <summary>
        /// 根据通道号获取通道单位
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <returns>通道单位</returns>
        public string GetChannelUnit(int channelId)
        {
            string channelUnit = "";
            if (channelDefinitionList.Count >= channelId)
            {
                channelUnit = channelDefinitionList[channelId - 1].sUnit;
            }
            return channelUnit;
        }

        /// <summary>
        /// 根据通道名称获取通道单位
        /// </summary>
        /// <param name="channelNameEn">通道英文名称</param>
        /// <param name="channelNameCh">通道中文名称</param>
        /// <returns>通道单位</returns>
        public string GetChannelUnit(string channelNameEn, string channelNameCh)
        {
            string channelUnit = "";
            for (int i = 0; i < channelDefinitionList.Count; i++)
            {
                if (channelDefinitionList[i].sNameEn == channelNameEn || channelDefinitionList[i].sNameCh == channelNameCh)
                {
                    channelUnit = channelDefinitionList[i].sUnit;
                    break;
                }
            }
            return channelUnit;
        }
    }
}
