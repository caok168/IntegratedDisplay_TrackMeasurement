using IntegratedDisplay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntegratedDisplay
{
    public partial class IICDataManager
    {
        /// <summary>
        /// 判断IIc文件是否被修正过
        /// </summary>
        /// <returns>true：已修正；false：未修正</returns>

        /// <summary>
        /// 判断IIc文件是否被修正过
        /// </summary>
        /// <param name="iicFilePath">iic文件路径</param>
        /// <returns>true：已修正；false：未修正</returns>
        public Boolean IsIICFixed(String iicFilePath)
        {
            Boolean retVal = false;
            Boolean isHasFix = IsHasFixTable(iicFilePath);
            
            if (isHasFix == true)
            {
                try
                {
                    using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                    {
                        sqlconn.Open();

                        string sqlCreate = "select DISTINCT maxval2 from fix_defects ";
                        OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);

                        OleDbDataReader oldr = sqlcom.ExecuteReader();

                        int maxval2 = 0;

                        while (oldr.Read())
                        {
                            if (int.TryParse(oldr[0].ToString(), out maxval2))
                            {
                                if (maxval2 == -200)
                                {
                                    retVal = true;//里程已经修正
                                    break;
                                }
                            }
                        }

                        oldr.Close();
                        sqlconn.Close();
                        //return retVal;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    retVal = false;
                }
            }
            else
            {
                retVal = false;//里程未修正
            }

            return retVal;
        }


        /// <summary>
        /// 判断是否含有fix表
        /// </summary>
        /// <param name="iicFilePath">iic文件路径</param>
        /// <returns>true：含有；false：不含有</returns>
        public Boolean IsHasFixTable(String iicFilePath)
        {
            Boolean isHasFixTalbe = false;

            List<String> tableNames = GetTableNames(iicFilePath);

            foreach (String tableName in tableNames)
            {
                if (tableName.Contains("fix"))
                {
                    isHasFixTalbe = true;
                    break;
                }
            }

            return isHasFixTalbe;
        }


        /// <summary>
        /// TQI拷贝，把车上计算出来的tqi由纵向排列变为横向排列
        /// </summary>
        /// <param name="sIICFileName">iic文件名</param>
        /// <param name="sKmInc">增减里程</param>
        /// <param name="listIDC">无效区段</param>
        public void TQICopy(string sIICFileName, string sKmInc, List<InvalidData> listIDC)
        {
            int iKMLast = 0;
            int iMeterLast = 0;
            int iKMNow = 0;
            int iMeterNow = 0;
            String channelNameGeo = null;
            double channelTqiValueGeo = 0.0d;
            float basePost = 0f;
            int baseMinor = 0;

            //geo文件中的通道名
            string[] sTQIItem = new string[] { "L_STDSURF", "R_STDSURF", "L_STDALIGN",
                "R_STDALIGN", "STDGAUGE", "STDCROSSLEVEL", "STDTWIST", "STDLATACCEL", "STDVERTACCEL","MAXSPEED"};

            List<TQI> listTQI = new List<TQI>();
            TQI tqiCls = new TQI();

            String subCode = null; //ashx
            DateTime runDate = DateTime.Now;



            #region 获取TQI，并写入到listTQI
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select FromPost,FromMinor,TQIMetricName,TQIValue,BasePost,SubCode,RunDate from tqi order by BasePost";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBdr = sqlcom.ExecuteReader();

                    if (!oleDBdr.HasRows)
                    {
                        MessageBox.Show("TQI列表为空！");
                        return;
                    }

                    while (oleDBdr.Read())
                    {
                        basePost = float.Parse(oleDBdr.GetValue(4).ToString());
                        baseMinor = (int)(Math.Round(basePost, 2) * 1000) % 1000;
                        iKMNow = (int)basePost;
                        iMeterNow = baseMinor;
                        channelNameGeo = oleDBdr.GetValue(2).ToString();
                        channelTqiValueGeo = Math.Round(double.Parse(oleDBdr.GetValue(3).ToString()), 2);

                        subCode = oleDBdr.GetValue(5).ToString();
                        runDate = DateTime.Parse(oleDBdr.GetValue(6).ToString());

                        if ((iKMNow != iKMLast) || (iMeterNow != iMeterLast))
                        {
                            tqiCls = new TQI();

                            listTQI.Add(tqiCls);

                            tqiCls.iKM = iKMNow;
                            tqiCls.iMeter = iMeterNow;

                            tqiCls.subCode = subCode;
                            tqiCls.runDate = runDate;

                            iKMLast = iKMNow;
                            iMeterLast = iMeterNow;
                        }

                        #region TQI赋值
                        foreach (String tqiName in sTQIItem)
                        {
                            if (tqiName == channelNameGeo)
                            {
                                //左高低
                                if (channelNameGeo == "L_STDSURF")
                                {
                                    tqiCls.zgd = channelTqiValueGeo;
                                }
                                //右高低
                                if (channelNameGeo == "R_STDSURF")
                                {
                                    tqiCls.ygd = channelTqiValueGeo;
                                }
                                //左轨向
                                if (channelNameGeo == "L_STDALIGN")
                                {
                                    tqiCls.zgx = channelTqiValueGeo;
                                }
                                //右轨向
                                if (channelNameGeo == "R_STDALIGN")
                                {
                                    tqiCls.ygx = channelTqiValueGeo;
                                }
                                //轨距
                                if (channelNameGeo == "STDGAUGE")
                                {
                                    tqiCls.gj = channelTqiValueGeo;
                                }
                                //水平
                                if (channelNameGeo == "STDCROSSLEVEL")
                                {
                                    tqiCls.sp = channelTqiValueGeo;
                                }
                                //三角坑
                                if (channelNameGeo == "STDTWIST")
                                {
                                    tqiCls.sjk = channelTqiValueGeo;
                                }
                                //车体横向加速度
                                if (channelNameGeo == "STDLATACCEL")
                                {
                                    tqiCls.hj = channelTqiValueGeo;
                                }
                                //车体垂向加速度
                                if (channelNameGeo == "STDVERTACCEL")
                                {
                                    tqiCls.cj = channelTqiValueGeo;
                                }
                                //平均速度
                                if (channelNameGeo == "MAXSPEED")
                                {
                                    tqiCls.pjsd = (int)channelTqiValueGeo;
                                }


                                break;
                            }
                        }
                        #endregion
                    }
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
            #endregion

            #region 插入TQI
            try
            {

                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    for (int i = 0; i < listTQI.Count; i++)
                    {
                        sqlcom.CommandText = "insert into fix_tqi " +
                            "values('" + listTQI[i].subCode + "','" + listTQI[i].runDate + "'," + listTQI[i].iKM.ToString()
                            + "," + listTQI[i].iMeter.ToString()
                            + "," + listTQI[i].zgd.ToString()
                            + "," + listTQI[i].ygd.ToString()
                            + "," + listTQI[i].zgx.ToString()
                            + "," + listTQI[i].ygx.ToString()
                            + "," + listTQI[i].gj.ToString()
                            + "," + listTQI[i].sp.ToString()
                            + "," + listTQI[i].sjk.ToString()
                            + "," + listTQI[i].GetTQISum().ToString()
                            + "," + listTQI[i].hj.ToString()
                            + "," + listTQI[i].cj.ToString()
                            + "," + listTQI[i].pjsd.ToString()
                            + "," + listTQI[i].iValid.ToString() + ")";
                        sqlcom.ExecuteNonQuery();
                    }
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
            #endregion            

            #region 删除无效的TQI
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "TQISum_Value=0 or AVERAGE_SPEED=0";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.CommandText = "delete from fix_tqi where " + sqlCreate;
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
            #endregion


        }


        /// <summary>
        /// 把无效区的tqi置无效-- ---适用于未修正的iic（车上的tqi）
        /// </summary>
        /// <param name="sIICFileName"></param>
        /// <param name="sKmInc"></param>
        /// <param name="listIDC"></param>
        public void TQIInvalid(string sIICFileName, string sKmInc, List<InvalidData> listIDC)
        {
            #region 无效TQI滤除
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    for (int iVar = 0; iVar < listIDC.Count; iVar++)
                    {
                        int iStartMeter = (int)(float.Parse(listIDC[iVar].sStartMile) * 1000);
                        int iEndMeter = (int)(float.Parse(listIDC[iVar].sEndMile) * 1000);
                        //根据点获取里程

                        if (sKmInc.Contains("增"))
                        {
                            sqlCreate = " (FromPost*1000+fromminor)>=" + (iStartMeter - 200).ToString() +
                                " and (FromPost*1000+fromminor)<=" + (iEndMeter).ToString();
                        }
                        else
                        {
                            sqlCreate = "  (FromPost*1000+fromminor)<=" + (iStartMeter + 200).ToString() +
                                " and (FromPost*1000+fromminor)>=" + (iEndMeter).ToString();
                        }
                        sqlcom.CommandText = "update fix_tqi set valid=0 where " + sqlCreate;
                        sqlcom.ExecuteNonQuery();
                    }
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
            #endregion
        }


        public double[][] QueryLeiJiXuQian(string sSQL,string sIICFilePath)
        {
            double[][] dReturnValue = new double[2][];
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFilePath + ";Persist Security Info=True"))
            {
                List<double> l = new List<double>();
                OleDbCommand sqlcom = new OleDbCommand(sSQL, sqlconn);

                sqlconn.Open();
                OleDbDataReader sdr = sqlcom.ExecuteReader();

                while (sdr.Read())
                {
                    l.Add(sdr.GetFloat(0));
                }
                sdr.Close();
                sqlconn.Close();
                
                dReturnValue[0] = new double[l.Count];
                dReturnValue[1] = new double[l.Count];
                dReturnValue[0] = l.ToArray();
                for (int i = 0; i < l.Count; i++)
                {
                    dReturnValue[1][i] = double.Parse((((double)(i + 1) / l.Count) * 100).ToString("F02"));
                }
            }
            return dReturnValue;
        }

        public double[][] QuerySUDU(string sSQL, string sIICFilePath)
        {
            double[][] dReturnValue = new double[2][];
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFilePath + ";Persist Security Info=True"))
            {
                List<double> l1 = new List<double>();
                List<double> l2 = new List<double>();
                OleDbCommand sqlcom = new OleDbCommand(sSQL, sqlconn);

                sqlconn.Open();
                OleDbDataReader sdr = sqlcom.ExecuteReader();

                while (sdr.Read())
                {
                    l1.Add((int)sdr.GetValue(0));
                    l2.Add((int)sdr.GetValue(1));
                }
                sdr.Close();
                sqlconn.Close();

                dReturnValue[0] = new double[l1.Count];
                dReturnValue[1] = new double[l2.Count];
                dReturnValue[0] = l1.ToArray();
                dReturnValue[1] = l2.ToArray();
            }
            return dReturnValue;
        }

        public double[][] QueryLCFB(string sSQL, string sIICFilePath)
        {
            double[][] dReturnValue = new double[2][];
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sIICFilePath + ";Persist Security Info=True"))
            {
                List<double> l1 = new List<double>();
                List<double> l2 = new List<double>();
                OleDbCommand sqlcom = new OleDbCommand(sSQL, sqlconn);

                sqlconn.Open();
                OleDbDataReader sdr = sqlcom.ExecuteReader();

                while (sdr.Read())
                {
                    l1.Add(double.Parse(sdr.GetValue(0).ToString()));
                    l2.Add(double.Parse(sdr.GetValue(1).ToString()));
                }
                sdr.Close();
                sqlconn.Close();

                dReturnValue[0] = new double[l1.Count];
                dReturnValue[1] = new double[l2.Count];
                dReturnValue[0] = l1.ToArray();
                dReturnValue[1] = l2.ToArray();
            }
            return dReturnValue;
        }


        /// <summary>
        /// idf数据库表查询--获取所有的表格名
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <returns>所有的表名</returns>
        public List<String> GetTableNames(String filePath)
        {
            List<String> ret = new List<String>();
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Persist Security Info=True"))
            {
                sqlconn.Open();

                DataTable dt = sqlconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ret.Add(dt.Rows[i]["TABLE_NAME"].ToString());
                }

                sqlconn.Close();

            }

            return ret;
        }
    }
}
