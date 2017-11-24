using System.Collections.Generic;

namespace CitFileSDK
{
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
        /// <returns></returns>
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
        /// 根据通道号查询通道英文名称
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
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
        /// <param name="channelId"></param>
        /// <returns></returns>
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
        /// <param name="channelId"></param>
        /// <returns></returns>
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
        /// <param name="channelNameEn"></param>
        /// <param name="channelNameCh"></param>
        /// <returns></returns>
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
        /// <param name="channelId"></param>
        /// <returns></returns>
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
        /// <param name="channelNameEn"></param>
        /// <param name="channelNameCh"></param>
        /// <returns></returns>
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
        /// <param name="channelId"></param>
        /// <returns></returns>
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
        /// <param name="channelNameEn"></param>
        /// <param name="channelNameCh"></param>
        /// <returns></returns>
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
