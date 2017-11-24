using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonFileSDK
{
    public class InnerFileOperator
    {
        /// <summary>
        /// InnerDb路径
        /// </summary>
        private static string _innerFilePath = string.Empty;

        private static readonly object lockObj = new object();

        /// <summary>
        /// InnerDB连接字符串
        /// </summary>
        private static string _innerConnString = string.Empty;

        /// <summary>
        /// InnerDb路径
        /// </summary>
        public static string InnerFilePath
        {
            get
            {
                return _innerFilePath;
            }

            set
            {
                _innerFilePath = value;
                if(File.Exists(_innerFilePath))
                {
                    _innerConnString = string.Format(_innerConnString, _innerFilePath);
                }
                else
                {
                    throw new FileNotFoundException("找不到文件:" + _innerFilePath);
                }
            }
        }

        public static string InnerConnString
        {
            get
            {
                return _innerConnString;
            }

            set
            {
                if (!string.IsNullOrEmpty(_innerFilePath) && !string.IsNullOrEmpty(value))
                {
                    _innerConnString = string.Format(value, _innerFilePath);
                    sqlConn = new OleDbConnection(_innerConnString);
                }
            }
        }

        private static OleDbConnection sqlConn = null;

        private static void OpenConn()
        {
            if (sqlConn.State != ConnectionState.Open)
            {
                sqlConn.Open();
            }
        }

        private static void CloseConn()
        {
            if (sqlConn.State != ConnectionState.Closed)
            {
                sqlConn.Close();
            }
        }

        /// <summary>
        /// 根据sql命令语句和数据库连接字符串，返回一个结果
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="dbConnString">数据库连接字符串</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText)
        {
            lock (lockObj)
            {
                object result = null;
                OpenConn();
                OleDbCommand sqlcom = new OleDbCommand(cmdText, sqlConn);
                result = sqlcom.ExecuteScalar();
                CloseConn();

                return result;
            }
        }


        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <returns>结果集</returns>
        public static DataTable Query(string queryString)
        {
            lock (lockObj)
            {
                DataTable dt = new DataTable();
                try
                {
                    OpenConn();
                    OleDbDataAdapter command = new OleDbDataAdapter(queryString, sqlConn);
                    command.Fill(dt);
                    CloseConn();
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                finally
                {
                    CloseConn();
                }
                return dt;
            }

        }

        /// <summary>
        /// 执行sql接口
        /// </summary>
        /// <param name="cmdText">sql命令</param>
        /// <returns>成功结果</returns>
        public static bool ExcuteSql(string cmdText)
        {
            lock (lockObj)
            {
                try
                {
                    OpenConn();
                    OleDbCommand sqlcom = new OleDbCommand(cmdText, sqlConn);
                    int i = sqlcom.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConn();
                }
            }
        }

        /// <summary>
        /// 检查是否存在表，没有则创建
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cmdText"></param>
        public static void CheckAndCreateTable(string tableName, string cmdText)
        {
            lock (lockObj)
            {
                try
                {
                    OpenConn();
                    bool isExist = sqlConn.GetSchema("Tables", new string[4] { null, null, tableName, "TABLE" }).Rows.Count > 0;

                    if (!isExist)
                    {
                        ExcuteSql(cmdText);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    CloseConn();
                }
            }

        }
    }
}
