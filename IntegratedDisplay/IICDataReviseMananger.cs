using CommonFileSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegratedDisplay
{
    public partial class IICDataManager
    {
        /// <summary>
        /// iic数据数据对调
        /// </summary>
        /// <param name="iicFilePath"></param>
        /// <param name="tableName"></param>
        /// <param name="column_A"></param>
        /// <param name="channel_A"></param>
        /// <param name="column_B"></param>
        /// <param name="channel_B"></param>
        public void IICChannelSwap(string iicFilePath, String tableName, String column_A, String channel_A, String column_B, String channel_B)
        {
            //把一列中的某两个值对调
            try
            {
                
                DBOperator.CommonDbConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True";
                string sql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", tableName, column_A, "Temp", column_B, channel_A);
                DBOperator.ExcuteSqlInCommonDB(sql);
                sql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", tableName, column_A, channel_A, column_B, channel_B);
                DBOperator.ExcuteSqlInCommonDB(sql);

                sql = string.Format("update {0} set {1}='{2}' where {3}='{4}'", tableName, column_A, channel_B, column_B, "Temp");
                DBOperator.ExcuteSqlInCommonDB(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IICChannelFlip(String iicFilePath, String tableName, String column_A, String channel_A, String column_B, String channel_B)
        {
            #region 幅值乘(-1)
            try
            {
                DBOperator.CommonDbConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True";
                string sql = string.Format("update {0} set {1}={2}*(-1) where {3}='{4}'", tableName, column_A, channel_A, column_B, channel_B);
                DBOperator.ExcuteSqlInCommonDB(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }

        //IIC中的tqi中的里程统一减去0.2：单线由减里程调整为增里程后，200代表200--400
        public void IICTqi(String iicFilePath, String tableName, String column_A, String channel_A)
        {
            #region IIC中的tqi中的里程统一减去0.2：单线由减里程调整为增里程后，200代表200--400
            try
            {
                DBOperator.CommonDbConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True";

                string sql = string.Format("update {0} set {1}={2}-0.2", tableName, column_A, channel_A);
                DBOperator.ExcuteSqlInCommonDB(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }


    }
}
