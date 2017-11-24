using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK.IntelligentMileageFix
{
    public class FixParam:IComparable
    {
        /// <summary>
        /// 通道英文名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 通道中文名
        /// </summary>
        public string ChannelCNName { get; set; }
        /// <summary>
        /// 匹配阈值
        /// </summary>
        public float ThreShold { get; set; }
        /// <summary>
        /// 通道ID，可为空
        /// </summary>
        public int ChannelID { get; set; }
        /// <summary>
        /// 优先级，越小越高
        /// </summary>
        public int Priority { get; set; }

        int IComparable.CompareTo(object obj)
        {
            int result = 0;
            try
            {
                FixParam param = obj as FixParam;
                if (param != null)
                {
                    if (this.Priority < param.Priority)
                    {
                        result = -1;
                    }
                    else if (this.Priority > param.Priority)
                    {
                        result = 1;
                    }
                    return result;
                }
                else
                {
                    throw new NotSupportedException("类型不同，无法比较");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
        }
    }
}
