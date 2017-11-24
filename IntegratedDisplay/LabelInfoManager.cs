
using IntegratedDisplay.Models;
/// -------------------------------------------------------------------------------------------
/// FileName：LabelInfoManager.cs
/// 说    明：标记相关操作
/// Version ：1.0
/// Date    ：2017/6/5
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace IntegratedDisplay
{
    /// <summary>
    /// 标记操作类
    /// </summary>
    public class LabelInfoManager
    {
        /// <summary>
        /// 向LabelInfo表格中插入数据
        /// </summary>
        /// <param name="idfFilePath">idf文件路径</param>
        /// <param name="meterIndex">文件指针</param>
        /// <param name="meter">公里标</param>
        /// <param name="memoText">标注内容</param>
        /// <param name="logDate">时间</param>
        public void Insert(string idfFilePath,string meterIndex,string meter,string memoText,string logDate,int Y)
        {
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFilePath + ";Persist Security Info=True"))
            {
                string sSql = "select max(id) from LabelInfo";
                OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                sqlconn.Open();
                object obj = sqlcom.ExecuteScalar();
                int iId;
                if (obj.Equals(null) || (obj == DBNull.Value))
                {
                    iId = 1;
                }
                else
                {
                    iId = (int)sqlcom.ExecuteScalar() + 1;
                }
                sSql = "insert into LabelInfo values(" + iId.ToString() + ",'" + meterIndex.ToString() + "','" + meter + "','" + memoText + "','" + logDate + "','" + Y + "')";
                sqlcom.CommandText = sSql;
                sqlcom.ExecuteNonQuery();
                sqlconn.Close();
            }
        }

        /// <summary>
        /// idf数据库操作：查询--LabelInfo表格
        /// </summary>
        /// <param name="idfFilePath">idf文件路径</param>
        /// <returns>LabelInfo表格 集合</returns>
        public List<LabelInfo> GetDataLabelInfo(string idfFilePath)
        {
            List<LabelInfo> listIC = new List<LabelInfo>();
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFilePath + ";Persist Security Info=False"))
                {
                    OleDbCommand sqlcom = new OleDbCommand("select * from LabelInfo", sqlconn);
                    sqlconn.Open();
                    OleDbDataReader sqloledr = sqlcom.ExecuteReader();
                    while (sqloledr.Read())
                    {
                        LabelInfo lic = new LabelInfo();
                        lic.iID = sqloledr.GetInt32(0);
                        lic.sMileIndex = sqloledr.GetString(1);
                        lic.sMile = sqloledr.GetString(2);
                        lic.sMemoText = sqloledr.GetString(3);
                        lic.logDate = sqloledr.GetDateTime(4).ToString();
                        //兼容之前的旧表数据，新加的Y坐标
                        if (sqloledr.FieldCount == 6)
                        {
                            lic.rectY = sqloledr.GetInt32(5);
                        }
                        else
                        {
                            lic.rectY = 0;
                        }

                        listIC.Add(lic);
                    }
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return listIC;
        }
    }
}
