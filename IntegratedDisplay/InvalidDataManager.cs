/// -------------------------------------------------------------------------------------------
/// FileName：InvalidDataManager.cs
/// 说    明：无效数据相关操作
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using IntegratedDisplay.Models;
using System.Collections.Generic;

namespace IntegratedDisplay
{
    /// <summary>
    /// 无效数据操作类
    /// </summary>
    public class InvalidDataManager
    {
        /// <summary>
        /// 添加无效数据
        /// </summary>
        /// <param name="idfFile"></param>
        /// <param name="sStartPoint"></param>
        /// <param name="sEndPoint"></param>
        /// <param name="sStartMile"></param>
        /// <param name="sEndMile"></param>
        /// <param name="iType"></param>
        /// <param name="sMemo"></param>
        /// <param name="Type"></param>
        public void InvalidDataInsertInto(string idfFile, string sStartPoint, string sEndPoint, string sStartMile, string sEndMile, int iType, string sMemo, string Type)
        {
            int id = 0;//无效区段id
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    sqlconn.Open();

                    String sSql = "select max(Id) from InvalidData";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    OleDbDataReader oledbReader = sqlcom.ExecuteReader();
                    Boolean isNull = oledbReader.HasRows;//是否是第一条记录，第一条记录id为1；
                    if (isNull == false)
                    {
                        id = 1;
                    }
                    else
                    {
                        while (oledbReader.Read())
                        {
                            if (String.IsNullOrEmpty(oledbReader.GetValue(0).ToString()))
                            {
                                id = 1;
                            }
                            else
                            {
                                id = int.Parse(oledbReader.GetValue(0).ToString()) + 1;
                            }
                        }
                    }

                    sqlconn.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取无效区段id异常：" + ex.Message);
            }

            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sSql = "insert into InvalidData values(" + id.ToString() + ",'" + sStartPoint +
                        "','" + sEndPoint + "','" + sStartMile + "','" + sEndMile + "'," + iType.ToString() + ",'" + sMemo + "',0,'" + Type + "')";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效区段设置异常:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取无效区段类型
        /// </summary>
        /// <returns></returns>
        public DataTable GetValidDataType()
        {
            string connectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\InnerDB.idf;Persist Security Info=True;Mode=Share Exclusive;Jet OLEDB:Database Password=iicdc;";
            
            using (OleDbConnection sqlconn = new OleDbConnection(connectionStr))
            {
                string sSql = "select * from 无效区段类型 order by i_no";
                DataTable dt = new DataTable();
                sqlconn.Open();

                OleDbDataAdapter command = new OleDbDataAdapter(sSql, sqlconn);
                command.Fill(dt);
                
                sqlconn.Close();

                return dt;
            }
        }


        /// <summary>
        /// idf数据库操作：删除--InvalidData表格的一条数据
        /// </summary>
        /// <param name="idfFile"></param>
        /// <param name="sStartPoint"></param>
        /// <param name="sEndPoint"></param>
        public void InvalidDataDelete(string idfFile, string sStartPoint, string sEndPoint)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sSql = "delete from InvalidData where StartPoint='" + sStartPoint + "' and EndPoint='" + sEndPoint + "'";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效区段设置异常:" + ex.Message);
            }
        }


        /// <summary>
        /// idf数据库操作：修改--一条InvalidData表格的数据
        /// </summary>
        /// <param name="idfFile"></param>
        /// <param name="sStartPoint"></param>
        /// <param name="sEndPoint"></param>
        /// <param name="sStartMile"></param>
        /// <param name="sEndMile"></param>
        public void InvalidDataUpdate(string idfFile, string sStartPoint, string sEndPoint, string sStartMile, string sEndMile)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sSql = "update InvalidData set StartMile='" +
                        sStartMile + "',EndMile='" +
                        sEndMile + "' where StartPoint='" +
                        sStartPoint + "' and EndPoint='" + sEndPoint + "'";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效区段设置异常:" + ex.Message);
            }
        }

        /// <summary>
        ///idf数据库操作：查询--InvalidData表格
        /// </summary>
        /// <param name="idfFile"></param>
        /// <returns></returns>
        public List<InvalidData> InvalidDataList(string idfFile)
        {
            List<InvalidData> listIDC = new List<InvalidData>();
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sSql = "select * from InvalidData order by clng(StartPoint)";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBr = sqlcom.ExecuteReader();
                    int columnNum = oleDBr.FieldCount;
                    while (oleDBr.Read())
                    {
                        InvalidData idc = new InvalidData();
                        idc.iId = int.Parse(oleDBr.GetValue(0).ToString());
                        idc.sStartPoint = oleDBr.GetValue(1).ToString();
                        idc.sEndPoint = oleDBr.GetValue(2).ToString();
                        idc.sStartMile = oleDBr.GetValue(3).ToString();
                        idc.sEndMile = oleDBr.GetValue(4).ToString();
                        idc.iType = int.Parse(oleDBr.GetValue(5).ToString());
                        idc.sMemoText = oleDBr.GetValue(6).ToString();
                        idc.iIsShow = int.Parse(oleDBr.GetValue(7).ToString());
                        if (columnNum == 9)
                        {
                            idc.ChannelType = oleDBr.GetValue(8).ToString();
                        }
                        else
                        {
                            idc.ChannelType = "";
                        }

                        listIDC.Add(idc);
                    }
                    oleDBr.Close();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无效区段读取异常:" + ex.Message);
            }
            return listIDC;
        }
    }
}
