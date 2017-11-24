using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK
{
    public class IndexOperator: IOperator
    {
        private  string _dbConnstring = "";

        private string _indexFilePath = "";

        public string IndexFilePath
        {
            get
            {
                return _indexFilePath;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _indexFilePath = value;
                    if (!File.Exists(_indexFilePath))
                    {
                        CreateDB(_indexFilePath);
                    }
                    _dbConnstring = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _indexFilePath + ";Jet OLEDB:Engine Type=5";
                }
            }
        }

        /// <summary>
        /// 根据路径创建一个数据库
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void CreateDB(string filePath)
        {
            System.Reflection.Assembly objAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var dbStream = objAssembly.GetManifestResourceStream("CitIndexFileSDK.DBProvider.db.accdb");
            if (dbStream != null)
            {
                byte[] dbResouce = new byte[dbStream.Length];
                dbStream.Read(dbResouce, 0, (int)dbStream.Length);
                var dbFileStream = new FileStream(filePath, FileMode.Create);
                dbFileStream.Write(dbResouce, 0, (int)dbStream.Length);
                dbFileStream.Close();
                dbStream.Close();
            }
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="queryString">查询字符串</param>
        /// <returns>结果集</returns>
        public DataTable Query(string queryString)
        {
            DataSet ds= AccessHelper.ExecuteDataSet(_dbConnstring, queryString, null);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 根据sql命令语句和数据库连接字符串，返回一个结果
        /// </summary>
        /// <param name="cmdText">sql命令语句</param>
        /// <param name="dbConnString">数据库连接字符串</param>
        /// <returns></returns>
        public  object ExecuteScalar(string cmdText)
        {
            object result = null;
            result = AccessHelper.ExecuteScalar(_dbConnstring, cmdText, null);
            return result;
        }

        /// <summary>
        /// 执行sql接口
        /// </summary>
        /// <param name="cmdText">sql命令</param>
        /// <returns>成功结果</returns>
        public bool ExcuteSql(string cmdText)
        {

            int i = AccessHelper.ExecuteNonQuery(_dbConnstring, cmdText, null);
            if (i > 0)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 检查是否存在表，没有则创建
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cmdText"></param>
        public void CheckAndCreateTable(string tableName, string cmdText)
        {
            AccessHelper.CheckAndCreateTable(_dbConnstring, tableName, cmdText, null);
        }
    }
}
