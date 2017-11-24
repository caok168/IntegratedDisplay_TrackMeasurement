/// -------------------------------------------------------------------------------------------
/// FileName：DBOperator.cs
/// 说    明：数据库操作类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：jinxl
/// -------------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace IntegratedDisplay
{
    public class DBOperator
    {
        /// <summary>
        /// InnerDb连接字符串
        /// </summary>
        public static string InnderDBConnectString = string.Empty;

        /// <summary>
        /// Idf文件连接字符串
        /// </summary>
        public static string CommonDbConnectString = string.Empty;

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <returns>结果集</returns>
        public static DataTable Query(string queryString)
        {
            using (OleDbConnection connection = new OleDbConnection(InnderDBConnectString))
            {
                DataTable dt = new DataTable();
                try
                {
                    connection.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(queryString, connection);
                    command.Fill(dt);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return dt;
            }
        }



        /// <summary>
        /// 在IDf中执行sql语句
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <returns></returns>
        public static bool ExcuteSqlInCommonDB(string cmdText)
        {
            return ExcuteSql(cmdText, CommonDbConnectString);
        }

        /// <summary>
        /// 执行sql接口
        /// </summary>
        /// <param name="cmdText">sql命令</param>
        /// <returns>成功结果</returns>
        private static bool ExcuteSql(string cmdText,string dbConnString)
        {
            using (OleDbConnection sqlconn = new OleDbConnection(dbConnString))
            {
                sqlconn.Open();
                OleDbCommand sqlcom = new OleDbCommand(cmdText, sqlconn);
                int i = sqlcom.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                return false;
            }
        }



    }
}
