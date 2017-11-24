using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonFileSDK;
using System.Data;

namespace CitIndexFileSDK.MileageFix
{
    public class LongChainTable
    {
        /// <summary>
        /// 长短链集合
        /// </summary>
        private List<LongChain> _chains;

        /// <summary>
        /// 根据cit文件头部部分数据初始化长短链集合
        /// </summary>
        /// <param name="lineCode">线路编码</param>
        /// <param name="kmInc">增减里程</param>
        /// <param name="dir">上下行</param>
        public LongChainTable(string lineCode,int kmInc,int dir)
        {
            string dirDesc = "上行";
            switch(dir)
            {
                case 1:dirDesc = "上行";break;
                case 2:dirDesc = "下行";break;
                case 3:dirDesc = "单线";break;
            }

            switch (dir)
            {
                case 1: dirDesc = "上"; break;
                case 2: dirDesc = "下"; break;
                case 3: dirDesc = "单"; break;
            }

            string sql = "select * from 长短链 where 线编号='" + lineCode + "' and 行别='" + dirDesc + "' order by 公里 ";
            if (kmInc == 1)
            {
                sql += "desc";
            }
            DataTable dt = InnerFileOperator.Query(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                _chains = new List<LongChain>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    LongChain chain = new LongChain();
                    chain.Dir = dir;
                    chain.LineCode = lineCode;
                    chain.Km = float.Parse(dt.Rows[i]["公里"].ToString());
                    chain.ExtraLength = float.Parse(dt.Rows[i]["米数"].ToString());
                    _chains.Add(chain);
                }
            }
        }

        public List<LongChain> Chains
        {
            get
            {
                return _chains;
            }
        }

        /// <summary>
        /// 根据标注的里程获取之间的长短链
        /// </summary>
        /// <param name="startMileage">标注的起始里程</param>
        /// <param name="endMileage">标注的结束里程</param>
        /// <returns>里程之间的长短链</returns>
        public List<LongChain> GetChains(double startMileage,double endMileage)
        {
            List<LongChain> chains = new List<LongChain>();
            if (_chains != null && _chains.Count > 0)
            {
                foreach (var item in _chains)
                {
                    if (item.Km >= startMileage && item.Km <= endMileage)
                    {
                        chains.Add(item);
                    }
                }
            }
            return chains;
        }
    }
}
