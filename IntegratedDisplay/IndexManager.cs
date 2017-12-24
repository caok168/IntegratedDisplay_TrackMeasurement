/// -------------------------------------------------------------------------------------------
/// FileName：IndexManager.cs
/// 说    明：索引信息相关操作
/// Version ：1.0
/// Date    ：2017/5/30
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegratedDisplay.Models;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using CitFileSDK;

namespace IntegratedDisplay
{
    /// <summary>
    /// 索引信息相关操作类
    /// </summary>
    public class IndexManager
    {
        /// <summary>
        /// 获取指定cit文件的全部索引
        /// </summary>
        /// <param name="idfFile">与cit文件同名的idf文件全路径名</param>
        /// <param name="sIndexID">此参数没有使用</param>
        /// <param name="sKmInc">公里标增减标志</param>
        /// <returns>cit文件索引信息</returns>
        public List<IndexOri> GetLayerIndexInfo(string idfFile, string sIndexID, string sKmInc)
        {
            List<IndexOri> listioc = new List<IndexOri>();
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select * from IndexOri order by val(indexmeter) ";
                    if (sKmInc.Equals("增"))
                    {
                        sqlCreate += "";
                    }
                    else
                    {
                        sqlCreate += " desc";
                    }
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBR = sqlcom.ExecuteReader();

                    while (oleDBR.Read())
                    {
                        IndexOri ioc = new IndexOri();
                        ioc.iId = oleDBR.GetInt32(0);
                        ioc.iIndexId = oleDBR.GetInt32(1);
                        ioc.IndexPoint = oleDBR.GetString(2);
                        ioc.IndexMeter = oleDBR.GetString(3);

                        listioc.Add(ioc);
                    }
                    sqlconn.Close();
                }
            }
            catch
            {

            }
            return listioc;

        }


        /// <summary>
        /// cit文件同名的数据库中德索引表(IndexOri)中的第一行插入一条数据
        /// </summary>
        /// <param name="idfFile">idf数据库文件路径</param>
        /// <param name="sID">索引表的主id</param>
        /// <param name="sIndexID">索引状态：0-原有的数据；1-新插入的数据</param>
        /// <param name="lPostion">索引对应的文件指针</param>
        /// <param name="sIndexKm">索引对应的里程数</param>
        /// <returns></returns>
        public int InsertLayerIndexInfo(string idfFile, string sID, string sIndexID, string lPostion, string sIndexKm)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select max(id)+1 from IndexOri";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sID = sqlcom.ExecuteScalar().ToString();
                    if (string.IsNullOrEmpty(sID))
                    {
                        sID = "1";
                    }
                    sqlconn.Close();
                }

                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "insert into IndexOri values(" + sID + ",0,'" + lPostion + "','" + sIndexKm + "')";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();

                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 删除cit文件同名的数据库中的索引表(IndexOri)的全部数据
        /// </summary>
        /// <param name="idfFile">cit文件同名的数据库的全路径文件</param>
        /// <returns></returns>
        public int deleteLayerIndexInfo(string idfFile)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "delete from IndexOri";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 删除cit文件同名的数据库中的索引表(IndexOri)的一条数据
        /// </summary>
        /// <param name="idfFile">cit文件同名的数据库的全路径文件名</param>
        /// <param name="sID">索引id</param>
        /// <returns></returns>
        public int deleteLayerIndexInfo(string idfFile, string sID)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "delete from IndexOri where id=" + sID;
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();

                    sqlconn.Close();
                }
            }
            catch
            {

            }
            return 0;
        }


        /// <summary>
        /// cit文件同名的数据库中德索引表(IndexOri)中插入一条数据
        /// </summary>
        /// <param name="sFilePath">与cit同名的idf数据库全路径文件名</param>
        /// <param name="sID">索引id</param>
        /// <param name="sIndexID">索引状态：0-原有的数据；1-新插入的数据</param>
        /// <param name="lPostion">索引对应的文件指针</param>
        /// <param name="sIndexKm">索引对应的里程数</param>
        /// <returns></returns>
        public int InsertLayerAllIndexInfo(string idfFile, string sID, string sIndexID, string lPostion, string sIndexKm)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sqlCreate = "insert into IndexOri values(" + sID + ",0,'" + lPostion + "','" + sIndexKm + "')";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }




        /// <summary>
        /// 从InnerDB数据库上获取cit文件上具体线路的长短链(只适用于一个cit文件)
        /// </summary>
        /// <param name="sKmInc">增里程还是减里程</param>
        /// <param name="sTrackCode">线路编号</param>
        /// <param name="sDir">行别</param>
        /// <returns>长短链信息</returns>
        public List<CDL> GetCDL(string sKmInc, string sTrackCode, string sDir)
        {
            List<CDL> listCDL = new List<CDL>();
            try
            {
                string connectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\InnerDB.idf;Persist Security Info=True;Mode=Share Exclusive;Jet OLEDB:Database Password=iicdc;";

                using (OleDbConnection sqlconn = new OleDbConnection(connectionStr))
                {
                    string sSql = "";
                    if (sKmInc.Contains("减"))
                    {
                        sSql = "desc";
                    }

                    OleDbCommand sqlcom = new OleDbCommand(" select * from 长短链 where 线编号='" + sTrackCode + "' and 行别='" + sDir + "' order by 公里 " + sSql, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader sdr = sqlcom.ExecuteReader();
                    while (sdr.Read())
                    {
                        CDL cdcl = new CDL();
                        cdcl.dKM = (float)sdr.GetValue(2);
                        cdcl.iMeter = (int)sdr.GetValue(3);
                        cdcl.sType = sdr.GetValue(4).ToString();
                        listCDL.Add(cdcl);
                    }
                    sdr.Close();
                    sqlconn.Close();
                }
            }
            catch
            {

            }
            return listCDL;
        }



        /// <summary>
        /// 根据标定的里程特征点信息创建索引,并插入到文件idf中的IndexSta的表格
        /// </summary>
        /// <param name="citFile">cit文件全路径名</param>
        /// <param name="idfFile">与cit文件同名的idf文件</param>
        /// <param name="sID"></param>
        /// <param name="listCDL">长短链信息</param>
        /// <param name="sKmInc">公里标增减标志</param>
        /// <param name="iChannelNumber">通道数</param>
        /// <param name="iSmaleRate">采样频率</param>
        /// <returns></returns>
        public int CreateIndexInfo(string citFile, string idfFile, string sID, List<CDL> listCDL, string sKmInc, int iChannelNumber, float iSmaleRate)
        {
            long lStartPosition = 0;
            long lEndPosition = 0;
            GetDataStartPositionEndPositionInfoIncludeIndex(ref lStartPosition, ref lEndPosition, citFile, iChannelNumber, -1, -1, false);
            //删除已创建的索引
            using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
            {
                string sSql = "delete from IndexSta";
                OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                sqlconn.Open();
                sqlcom.ExecuteNonQuery();
                sqlconn.Close();
            }

            //获取原始索引信息
            List<IndexOri> listIOC = GetLayerIndexInfo(idfFile, sID, sKmInc);

            if (listIOC.Count < 1)
            {
                return -20;
            }

            List<IndexSta> listIC = new List<IndexSta>();
            List<IndexSta> listProIC = new List<IndexSta>();

            int iID = 0;
            #region 处理索引
            if (sKmInc.Equals("增"))
            {
                for (int i = 0; i < listIOC.Count - 1; i++)
                {
                    iID++;
                    IndexSta ic = new IndexSta();

                    ic.iID = iID;
                    ic.iIndexID = int.Parse(sID);

                    ic.lStartPoint = long.Parse(listIOC[i].IndexPoint);
                    ic.lStartMeter = listIOC[i].IndexMeter;
                    ic.lEndPoint = long.Parse(listIOC[i + 1].IndexPoint);
                    ic.LEndMeter = listIOC[i + 1].IndexMeter;
                    ic.lContainsPoint = ((long.Parse(listIOC[i + 1].IndexPoint) - long.Parse(listIOC[i].IndexPoint)) / (iChannelNumber * 2));

                    int iCDLCount = 0;
                    int iCDLSumC = 0;
                    int iCDLSumD = 0;
                    for (int j = 0; j < listCDL.Count; j++)
                    {
                        if ((float.Parse(ic.lStartMeter) <= listCDL[j].dKM) &&
                            (float.Parse(ic.LEndMeter) > listCDL[j].dKM) && listCDL[j].sType.Equals("长链"))
                        {
                            iCDLCount++;
                            iCDLSumC += listCDL[j].iMeter;
                        }
                        if ((float.Parse(ic.lStartMeter) <= listCDL[j].dKM) &&
                            (float.Parse(ic.LEndMeter) > listCDL[j].dKM) && listCDL[j].sType.Equals("短链"))
                        {
                            iCDLSumD += listCDL[j].iMeter;
                        }
                    }
                    int iCDLMeter = 0;
                    iCDLMeter = iCDLSumC - (iCDLCount * 1000);
                    iCDLMeter = iCDLMeter - iCDLSumD;
                    ic.lContainsMeter = (float.Parse(listIOC[i + 1].IndexMeter) - float.Parse(listIOC[i].IndexMeter) + iCDLMeter / 1000.0).ToString("f3");

                    ic.sType = "正常";
                    listIC.Add(ic);
                }
            }
            else//减里程
            {
                for (int i = 0; i < listIOC.Count - 1; i++)
                {
                    iID++;
                    IndexSta ic = new IndexSta();

                    ic.iID = iID;
                    ic.iIndexID = int.Parse(sID);

                    ic.lStartPoint = long.Parse(listIOC[i].IndexPoint);
                    ic.lStartMeter = listIOC[i].IndexMeter;
                    ic.lEndPoint = long.Parse(listIOC[i + 1].IndexPoint);
                    ic.LEndMeter = listIOC[i + 1].IndexMeter;
                    ic.lContainsPoint = ((long.Parse(listIOC[i + 1].IndexPoint) - long.Parse(listIOC[i].IndexPoint)) / (iChannelNumber * 2));

                    int iCDLCount = 0;
                    int iCDLSumC = 0;
                    int iCDLSumD = 0;
                    for (int j = 0; j < listCDL.Count; j++)
                    {
                        if ((float.Parse(ic.lStartMeter) >= listCDL[j].dKM) &&
                            (float.Parse(ic.LEndMeter) < listCDL[j].dKM) && listCDL[j].sType.Equals("长链"))
                        {
                            iCDLCount++;
                            iCDLSumC += listCDL[j].iMeter;
                        }
                        if ((float.Parse(ic.lStartMeter) >= listCDL[j].dKM) &&
                            (float.Parse(ic.LEndMeter) < listCDL[j].dKM) && listCDL[j].sType.Equals("短链"))
                        {
                            iCDLSumD += listCDL[j].iMeter;

                        }
                    }
                    int iCDLMeter = 0;
                    iCDLMeter = iCDLSumC - (iCDLCount * 1000);
                    iCDLMeter = iCDLMeter - iCDLSumD;
                    ic.lContainsMeter = (float.Parse(listIOC[i].IndexMeter) - float.Parse(listIOC[i + 1].IndexMeter) + iCDLMeter / 1000.0).ToString("f3");

                    ic.sType = "正常";
                    listIC.Add(ic);
                }
            }
            #endregion
            //
            //处理长短链
            #region 处理长短链
            int iConut = 0;
            //listProIC = listIC;
            if (sKmInc.Equals("增"))
            {
                for (int k = 0; k < listIC.Count; k++)
                {
                    //判断每个索引区段内是否包含CDL
                    bool bCDL = false;

                    IndexSta ic3 = new IndexSta();
                    ic3 = listIC[k];
                    for (int j = 0; j < listCDL.Count; j++)
                    {

                        IndexSta ic1 = new IndexSta();
                        IndexSta ic2 = new IndexSta();

                        if ((float.Parse(ic3.lStartMeter) <= listCDL[j].dKM) &&
                            (float.Parse(ic3.LEndMeter) > listCDL[j].dKM))
                        {
                            bCDL = true;

                            //CDL前面
                            float iMeterLength = listCDL[j].dKM - float.Parse(ic3.lStartMeter);
                            iConut++;
                            ic1.iID = iConut;
                            ic1.iIndexID = ic3.iIndexID;
                            ic1.lStartPoint = ic3.lStartPoint;
                            ic1.lStartMeter = ic3.lStartMeter;
                            ic1.lContainsMeter = iMeterLength.ToString();
                            ic1.lContainsPoint =
                                (int)(Math.Ceiling((iMeterLength * 1000) /
                                ((float.Parse(listIC[k].lContainsMeter) * 1000) / listIC[k].lContainsPoint)));
                            //- (int)(ic3.dSmaleRat * 1000)
                            ic1.LEndMeter = listCDL[j].dKM.ToString();
                            ic1.lEndPoint = ic1.lStartPoint + ic1.lContainsPoint * iChannelNumber * 2;
                            ic1.sType = "正常";
                            listProIC.Add(ic1);


                            //CDL
                            iConut++;
                            if (listCDL[j].sType == "短链")
                            {
                                //短链信息分2段处理，长链信息分3段处理，其总长度没有损失---严广学
                            }
                            else
                            {
                                ic2.iIndexID = ic3.iIndexID;
                                ic2.iID = iConut;
                                ic2.lStartPoint = ic1.lEndPoint;
                                ic2.lStartMeter = listCDL[j].dKM.ToString();
                                ic2.lContainsMeter = (listCDL[j].iMeter / 1000.0).ToString("f3");
                                ic2.lContainsPoint = (int)(Math.Ceiling(listCDL[j].iMeter /
                                    ((float.Parse(listIC[k].lContainsMeter) * 1000) / listIC[k].lContainsPoint)));

                                ic2.lEndPoint = ic2.lStartPoint + ic2.lContainsPoint * iChannelNumber * 2;
                                ic2.LEndMeter = (listCDL[j].dKM + (listCDL[j].iMeter / 1000.0)).ToString();

                                ic2.sType = "长链";

                                listProIC.Add(ic2);
                            }

                            if (listCDL[j].sType == "短链")
                            {
                                ic3.lStartMeter = (listCDL[j].dKM + listCDL[j].iMeter / 1000.0).ToString();
                                ic3.lStartPoint = ic1.lEndPoint;
                                ic3.lContainsMeter = (float.Parse(ic3.lContainsMeter) -
                                    float.Parse(ic1.lContainsMeter)).ToString();
                                ic3.lContainsPoint = ic3.lContainsPoint - ic1.lContainsPoint;
                                ic3.sType = "正常";
                            }
                            else
                            {
                                ic3.lStartMeter = (listCDL[j].dKM + 1).ToString();
                                ic3.lStartPoint = ic2.lEndPoint;
                                ic3.lContainsMeter = (float.Parse(ic3.lContainsMeter) -
                                    float.Parse(ic2.lContainsMeter) - float.Parse(ic1.lContainsMeter)).ToString();
                                ic3.lContainsPoint = ic3.lContainsPoint - ic2.lContainsPoint - ic1.lContainsPoint;
                                ic3.sType = "正常";
                            }

                        }

                    }
                    //判断是否进行了长短链修正，没有则添加
                    if (!bCDL)
                    {
                        iConut++;
                        listIC[k].iID = iConut;
                        listProIC.Add(listIC[k]);
                    }
                    else
                    {
                        iConut++;
                        ic3.iID = iConut;
                        listProIC.Add(ic3);
                    }


                }

            }
            else//减里程
            {
                for (int k = 0; k < listIC.Count; k++)
                {
                    //判断每个索引区段内是否包含CDL
                    bool bCDL = false;

                    IndexSta ic3 = new IndexSta();
                    ic3 = listIC[k];
                    for (int j = 0; j < listCDL.Count; j++)
                    {

                        IndexSta ic1 = new IndexSta();
                        IndexSta ic2 = new IndexSta();

                        if ((float.Parse(ic3.lStartMeter) >= listCDL[j].dKM) &&
                            (float.Parse(ic3.LEndMeter) < listCDL[j].dKM))
                        {
                            bCDL = true;
                            if (listCDL[j].sType == "长链")
                            {
                                //CDL前面
                                float iMeterLength = float.Parse(ic3.lStartMeter) - listCDL[j].dKM - 1;
                                iConut++;
                                ic1.iID = iConut;
                                ic1.iIndexID = ic3.iIndexID;
                                ic1.lStartPoint = ic3.lStartPoint;
                                ic1.lStartMeter = ic3.lStartMeter;
                                ic1.lContainsPoint =
                                    (int)(Math.Ceiling((iMeterLength * 1000) /
                                    ((float.Parse(listIC[k].lContainsMeter) * 1000) / listIC[k].lContainsPoint)));
                                //- (int)(ic3.dSmaleRat * 1000)
                                ic1.LEndMeter = (listCDL[j].dKM + 1).ToString("f3");
                                ic1.lContainsMeter = (float.Parse(ic1.lStartMeter) - float.Parse(ic1.LEndMeter)).ToString("f3");
                                ic1.lEndPoint = ic1.lStartPoint + ic1.lContainsPoint * iChannelNumber * 2;
                                ic1.sType = "正常";
                                listProIC.Add(ic1);
                            }
                            else
                            {
                                float iMeterLength = float.Parse(ic3.lStartMeter) - listCDL[j].dKM - listCDL[j].iMeter / 1000f;
                                iConut++;
                                ic1.iID = iConut;
                                ic1.iIndexID = ic3.iIndexID;
                                ic1.lStartPoint = ic3.lStartPoint;
                                ic1.lStartMeter = ic3.lStartMeter;
                                ic1.lContainsPoint =
                                    (int)(Math.Ceiling((iMeterLength * 1000) /
                                    ((float.Parse(listIC[k].lContainsMeter) * 1000) / listIC[k].lContainsPoint)));
                                //- (int)(ic3.dSmaleRat * 1000)
                                ic1.LEndMeter = (listCDL[j].dKM + listCDL[j].iMeter / 1000f).ToString("f3");
                                ic1.lContainsMeter = (float.Parse(ic1.lStartMeter) - float.Parse(ic1.LEndMeter)).ToString("f3");
                                ic1.lEndPoint = ic1.lStartPoint + ic1.lContainsPoint * iChannelNumber * 2;
                                ic1.sType = "正常";
                                listProIC.Add(ic1);
                            }
                            //CDL
                            iConut++;
                            if (listCDL[j].sType == "短链")
                            {

                            }
                            else
                            {
                                ic2.iIndexID = ic3.iIndexID;
                                ic2.iID = iConut;
                                ic2.lStartPoint = ic1.lEndPoint;
                                ic2.lStartMeter = (listCDL[j].dKM + (listCDL[j].iMeter / 1000f)).ToString("f3");
                                ic2.lContainsMeter = (listCDL[j].iMeter / 1000.0).ToString("f3");
                                ic2.lContainsPoint = (int)(Math.Ceiling(listCDL[j].iMeter /
                                    ((float.Parse(listIC[k].lContainsMeter) * 1000) / listIC[k].lContainsPoint)));

                                ic2.lEndPoint = ic2.lStartPoint + ic2.lContainsPoint * iChannelNumber * 2;
                                ic2.LEndMeter = (listCDL[j].dKM).ToString();
                                ic2.sType = "长链";
                                listProIC.Add(ic2);
                            }


                            if (listCDL[j].sType == "短链")
                            {
                                ic3.lStartMeter = (listCDL[j].dKM).ToString();
                                ic3.lStartPoint = ic1.lEndPoint;
                                //ic3.lContainsMeter = (float.Parse(ic3.lStartMeter) - float.Parse(ic3.LEndMeter)).ToString("f3");
                                ic3.lContainsMeter = (float.Parse(ic3.lContainsMeter) -
                                    float.Parse(ic1.lContainsMeter)).ToString();
                                ic3.lContainsPoint = ic3.lContainsPoint - ic1.lContainsPoint;
                                ic3.sType = "正常";
                            }
                            else
                            {
                                ic3.lStartMeter = (listCDL[j].dKM).ToString();
                                ic3.lStartPoint = ic2.lEndPoint;
                                ic3.lContainsMeter = (float.Parse(ic3.lContainsMeter) -
                                    float.Parse(ic2.lContainsMeter) - float.Parse(ic1.lContainsMeter)).ToString();
                                ic3.lContainsPoint = ic3.lContainsPoint - ic2.lContainsPoint - ic1.lContainsPoint;
                                ic3.sType = "正常";
                            }
                        }

                    }
                    //判断是否进行了长短链修正，没有则添加
                    if (!bCDL)
                    {
                        iConut++;
                        listIC[k].iID = iConut;
                        listProIC.Add(listIC[k]);
                    }
                    else
                    {
                        iConut++;
                        ic3.iID = iConut;
                        listProIC.Add(ic3);
                    }


                }
            }
            # endregion
            //插入头尾点
            if (listProIC.Count >= 1)
            {
                //补头
                IndexSta ic1 = new IndexSta();
                ic1.iIndexID = 1;
                ic1.sType = "正常";
                ic1.lEndPoint = listProIC[0].lStartPoint;
                ic1.LEndMeter = listProIC[0].lStartMeter;
                ic1.lStartPoint = lStartPosition;
                ic1.lContainsPoint = (listProIC[0].lStartPoint - ic1.lStartPoint) / 2 / iChannelNumber;
                ic1.lContainsMeter = (ic1.lContainsPoint / iSmaleRate / 1000).ToString("f3");
                ic1.lStartMeter = sKmInc.Equals("增") ? (double.Parse(ic1.LEndMeter) - double.Parse(ic1.lContainsMeter)).ToString("f3") : (double.Parse(ic1.LEndMeter) + double.Parse(ic1.lContainsMeter)).ToString("f3");
                listProIC.Insert(0, ic1);
                //补尾
                IndexSta ic2 = new IndexSta();
                ic2.iIndexID = 1;
                ic2.sType = "正常";
                ic2.lEndPoint = lEndPosition;
                ic2.lStartPoint = listProIC[listProIC.Count - 1].lEndPoint;
                ic2.lStartMeter = listProIC[listProIC.Count - 1].LEndMeter;
                ic2.lContainsPoint = (lEndPosition - ic2.lStartPoint) / 2 / iChannelNumber;
                ic2.lContainsMeter = (ic2.lContainsPoint / iSmaleRate / 1000).ToString("f3");
                ic2.LEndMeter = sKmInc.Equals("增") ? (double.Parse(ic2.lStartMeter) + double.Parse(ic2.lContainsMeter)).ToString("f3") : (double.Parse(ic2.lStartMeter) - double.Parse(ic2.lContainsMeter)).ToString("f3");
                listProIC.Add(ic2);
                //重新排序
                for (int i = 0; i < listProIC.Count; i++)
                {
                    listProIC[i].iID = i + 1;
                }
            }
            //保存
            for (int i = 0; i < listProIC.Count; i++)
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=True"))
                {
                    string sSql = "insert into IndexSta values(" + listProIC[i].iID.ToString() + "," + listProIC[i].iIndexID.ToString() +
                        ",'" + listProIC[i].lStartPoint.ToString() + "','" + listProIC[i].lStartMeter.ToString() +
                        "','" + listProIC[i].lEndPoint.ToString() + "','" + listProIC[i].LEndMeter.ToString() + "','" +
                        listProIC[i].lContainsPoint.ToString() + "','" + listProIC[i].lContainsMeter.ToString() +
                        "','" + listProIC[i].sType + "')";
                    OleDbCommand sqlcom = new OleDbCommand(sSql, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
            //MessageBox.Show("创建成功！");
            return 0;

        }


        /// <summary>
        ///  idf数据库操作：查询--IndexSta表格
        /// </summary>
        /// <param name="idfFile">idf全路径文件名</param>
        /// <returns>修正索引信息</returns>
        public List<IndexSta> GetDataIndexInfo(string idfFile)
        {
            List<IndexSta> listIC = new List<IndexSta>();
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + idfFile + ";Persist Security Info=False"))
                {
                    OleDbCommand sqlcom = new OleDbCommand("select * from IndexSta order by id", sqlconn);
                    sqlconn.Open();
                    OleDbDataReader sqloledr = sqlcom.ExecuteReader();
                    while (sqloledr.Read())
                    {
                        IndexSta ic = new IndexSta();
                        ic.iID = (int)sqloledr.GetInt32(0);
                        ic.iIndexID = (int)sqloledr.GetInt32(1);
                        ic.lStartPoint = long.Parse(sqloledr.GetString(2));
                        ic.lStartMeter = sqloledr.GetString(3);
                        ic.lEndPoint = long.Parse(sqloledr.GetString(4));
                        ic.LEndMeter = sqloledr.GetString(5);
                        ic.lContainsPoint = long.Parse(sqloledr.GetString(6));
                        ic.lContainsMeter = sqloledr.GetString(7);
                        ic.sType = sqloledr.GetString(8);

                        listIC.Add(ic);
                    }
                    sqlconn.Close();
                }
            }
            catch
            {

            }
            return listIC;
        }


        /// <summary>
        /// 获取cit文件中通道数据的起始文件指针和结束文件指针
        /// 索引：
        ///      存在，则使用索引中的起始文件指针和结束文件指针
        ///      不存在，获取cit文件中通道数据的起始文件指针和结束文件指针
        /// </summary>
        /// <param name="lStartPosition">通道数据的起始文件指针</param>
        /// <param name="lEndtPosition">通道数据的结束文件指针</param>
        /// <param name="FileName">CIT波形文件名称</param>
        /// <param name="gjtds">通道总数</param>
        /// <param name="lFixStartPosition">索引中通道数据的起始文件指针</param>
        /// <param name="lFixEndPosition">索引中通道数据的结束文件指针</param>
        /// <param name="bIndex">是否使用索引</param>
        /// <returns></returns>
        public bool GetDataStartPositionEndPositionInfoIncludeIndex(ref long lStartPosition, ref long lEndtPosition, string FileName, int gjtds, long lFixStartPosition, long lFixEndPosition, bool bIndex)
        {
            try
            {
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs);
                byte[] b = new byte[gjtds * 2];
                br.ReadBytes(120);//120
                br.ReadBytes(65 * gjtds);//65
                br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));
                //if (lFixStartPosition == -1 && lFixEndPosition == -1 && !bIndex)---改为下面--ygx--20140113
                if (lFixStartPosition == -1 && lFixEndPosition == -1 && !bIndex)
                {
                    lStartPosition = br.BaseStream.Position;
                    lEndtPosition = br.BaseStream.Length;
                }
                else
                {
                    lStartPosition = lFixStartPosition;
                    lEndtPosition = lFixEndPosition;
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
            return true;
        }


        /// <summary>
        /// 根据点返回索引文件里对应的里程信息
        /// </summary>
        /// <param name="listIC">索引信息</param>
        /// <param name="lPosition">点的位置</param>
        /// <param name="tds">文件通道书</param>
        /// <param name="sKmInc">增减里程标</param>
        /// <returns>索引里程：单位为米</returns>
        public int PointToMeter(List<IndexSta> listIC, long lPosition, int tds, string sKmInc)
        {
            int iMeter = 0;

            //处理里程
            for (int i = 0; i < listIC.Count; i++)
            {
                if (lPosition >= listIC[i].lStartPoint && lPosition < listIC[i].lEndPoint)
                {
                    int iCount = 1;
                    long lCurPos = lPosition - listIC[i].lStartPoint;
                    int iIndex = 0;
                    if (listIC[i].sType.Contains("长链"))
                    {
                        int iKM = 0;
                        double dCDLMeter = float.Parse(listIC[i].lContainsMeter) * 1000;
                        if (sKmInc.Equals("减"))
                        {
                            iKM = (int)float.Parse(listIC[i].LEndMeter);
                        }
                        else
                        {
                            iKM = (int)float.Parse(listIC[i].lStartMeter);
                        }
                        for (iIndex = 0; iIndex < iCount && (lPosition + iIndex * tds * 2) < listIC[i].lEndPoint;)
                        {
                            float f = (lCurPos / tds / 2 + iIndex) * ((float.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));

                            Milestone wm = new Milestone();
                            if (sKmInc.Equals("减"))
                            {
                                wm.mKm = iKM;
                                wm.mMeter = (float)(dCDLMeter - f);
                            }
                            else
                            {
                                wm.mKm = iKM;
                                wm.mMeter = (float)(dCDLMeter + f);
                            }
                            wm.mFilePosition = (lPosition + (iIndex * tds * 2));
                            iMeter = Convert.ToInt32(wm.GetMeter());
                            return iMeter;
                        }
                    }
                    else
                    {
                        double dMeter = float.Parse(listIC[i].lStartMeter) * 1000;
                        for (iIndex = 0; iIndex < iCount && (lPosition + iIndex * tds * 2) < listIC[i].lEndPoint;)
                        {
                            float f = (lCurPos / tds / 2 + iIndex) * ((float.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));

                            Milestone wm = new Milestone();
                            if (sKmInc.Equals("减"))
                            {
                                wm.mKm = (int)((dMeter - f) / 1000);
                                wm.mMeter = (float)((dMeter - f) % 1000);
                            }
                            else
                            {
                                wm.mKm = (int)((dMeter + f) / 1000);
                                wm.mMeter = (float)((dMeter + f) % 1000);
                            }
                            wm.mFilePosition = (lPosition + (iIndex * tds * 2));
                            iMeter = Convert.ToInt32(wm.GetMeter());
                            return iMeter;
                        }
                    }
                    break;

                }

            }
            return iMeter;
        }

        /// <summary>
        /// 根据里程(公里)返回索引文件里对应的位置
        /// </summary>
        /// <param name="listIC">索引信息</param>
        /// <param name="meter">里程(公里)</param>
        /// <param name="channelNumber">通道数</param>
        /// <param name="iKmInc">增减里程标志，0：增，1：减</param>
        /// <returns>返回位置，未找到则为-1</returns>
        public long MeterToPoint(List<IndexSta> listIC, double meter, int channelNumber, int iKmInc)
        {
            long postion = -1;
            long startPostion = listIC[0].lStartPoint;
            long endPostion = listIC[listIC.Count - 1].lEndPoint;
            foreach (var index in listIC)
            {
                double m_startMeter = double.Parse(index.lStartMeter);
                double m_endMeter = double.Parse(index.LEndMeter);
                long m_startPoint = index.lStartPoint;
                long m_endPoint = index.lEndPoint;
                double m_containsMeter = double.Parse(index.lContainsMeter);
                long m_containsPoint = index.lContainsPoint;
                double m_meterPerPoint = m_containsMeter / m_containsPoint;
                if (iKmInc == 0)
                {
                    if (m_startMeter <= meter && m_endMeter >= meter)
                    {
                        postion = m_startPoint + (long)((meter - m_startMeter) / m_meterPerPoint) * channelNumber * 2 + 4000 * channelNumber * 2;
                        postion = (postion >= startPostion) ? postion : startPostion;
                        postion = (postion <= endPostion) ? postion : endPostion;
                        return postion;
                    }
                    else
                    {
                        postion = startPostion;
                    }

                }
                else if (iKmInc == 1)
                {
                    if (m_startMeter >= meter && m_endMeter <= meter)
                    {
                        postion = m_startPoint + (long)((meter - m_startMeter) / m_meterPerPoint) * channelNumber * 2 - 4000 * channelNumber * 2;
                        postion = (postion >= startPostion) ? postion : startPostion;
                        postion = (postion <= endPostion) ? postion : endPostion;
                        return postion;
                    }
                    else
                    {
                        postion = endPostion;
                    }
                }

            }
            return postion;
        }
    }
}
