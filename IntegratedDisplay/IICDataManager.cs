/// -------------------------------------------------------------------------------------------
/// FileName：IICDataForm.cs
/// 说    明：IIC数据相关操作
/// Version ：1.0
/// Date    ：2017/6/1
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Data.OleDb;
using System.Windows.Forms;
using IntegratedDisplay.Models;
using System.Collections.Generic;
using CitFileSDK;
using System.IO;

namespace IntegratedDisplay
{
    /// <summary>
    /// IIC数据操作类
    /// </summary>
    public partial class IICDataManager
    {

        CITFileProcess citHelper = new CITFileProcess();

        /// <summary>
        /// 在iic当中创建新的TQI表和偏差表，用于重新计算
        /// </summary>
        /// <param name="IICFileName"></param>
        /// <returns></returns>
        public void CreateIICTable(string IICFileName)
        {
            //删除表
            DropFixTalbe(IICFileName);

            //创建fix_defects表
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + IICFileName + ";Persist Security Info=True"))
                {
                    sqlconn.Open();

                    //原来这里是拷贝所有记录
                    //考虑到很多超限值车上人员已经确认过是无效的，因此这里只拷贝有效的--20140114--和赵主任确认的结果
                    string sqlCreate = "select * into fix_defects from defects where valid<>'N'";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlcom.ExecuteNonQuery();

                    //段级系统要求要保留校正前的里程，因此把maxpost,maxminor拷贝到frompost,fromminor---20140225--严广学
                    sqlCreate = "update  fix_defects set frompost=maxpost";
                    sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlcom.ExecuteNonQuery();
                    sqlCreate = "update  fix_defects set fromminor=maxminor";
                    sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlcom.ExecuteNonQuery();

                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //创建fix_tqi表
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + IICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "CREATE TABLE fix_tqi (" +
    "SubCode varchar(255) NULL," +
    "RunDate date NULL," +
    "FromPost integer NULL," +
    "FromMinor real NULL," +
    "L_Prof_Value real NULL," +
    "R_Prof_Value real NULL," +
    "L_Align_Value real NULL," +
    "R_Align_Value real NULL," +
    "Gage_Value real NULL," +
    "Crosslevel_TQIValue real NULL," +
    "ShortTwist_Value real NULL," +
    "TQISum_Value real NULL," +
    "LATACCEL_Value real NULL," +
    "VERTACCEL_Value real NULL," +
    "AVERAGE_SPEED integer NULL," +
    "valid integer NULL" +
    ");";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 删除已经创建的表
        /// </summary>
        /// <param name="IICFileName">iic文件</param>
        public void DropFixTalbe(String IICFileName)
        {
            //删除已经创建的表            
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + IICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "drop table fix_defects";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {
                //直接使用原始的iic文件，里面没有fix_defects表，因此删除出错，但是不处理。
            }
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + IICFileName + ";Persist Security Info=True"))
                {
                    string sqlCreate = "drop table fix_tqi";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {
                //直接使用原始的iic文件，里面没有fix_tqi表，因此删除出错，但是不处理。
            }
        }


        /// <summary>
        /// 偏差修正
        /// </summary>
        /// <param name="citFilePath">cit文件路径</param>
        /// <param name="iicFilePath">iic文件路径</param>
        /// <param name="listIC">里程修正结果集合</param>
        /// <param name="cyjg">采样点</param>
        /// <param name="gjtds">通道数量</param>
        /// <param name="sKmInc">增减里程</param>
        /// <param name="listETC">偏差类型</param>
        public void ExceptionFix(string citFilePath, string iicFilePath, List<IndexSta> listIC, List<ExceptionType> listETC)
        {
            List<Defects> listDC = new List<Defects>();
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select RecordNumber,maxpost,maxminor from fix_defects where maxval2 is null or maxval2<>-200";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBdr = sqlcom.ExecuteReader();
                    while (oleDBdr.Read())
                    {
                        Defects dc = new Defects();
                        dc.iRecordNumber = int.Parse(oleDBdr.GetValue(0).ToString());
                        dc.iMaxpost = int.Parse(oleDBdr.GetValue(1).ToString());
                        dc.dMaxminor = double.Parse(oleDBdr.GetValue(2).ToString());
                        listDC.Add(dc);
                    }

                    oleDBdr.Close();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }

            FileInformation fi = citHelper.GetFileInformation(citFilePath);

            //
            List<Milestone> listMilestone = citHelper.GetAllMileStone(citFilePath);
            List<cPointFindMeter> listcpfm = new List<cPointFindMeter>();

            for (int i = 0; i < listMilestone.Count; i++)
            {
                cPointFindMeter cpfm = new cPointFindMeter();
                cpfm.lLoc = listMilestone[i].mFilePosition;
                cpfm.lMeter = Convert.ToInt64(listMilestone[i].mKm * 100000 + listMilestone[i].mMeter * 100);

                listcpfm.Add(cpfm);
            }


            for (int i = 0; i < listDC.Count; i++)
            {
                for (int j = 0; j < listcpfm.Count; j++)
                {
                    if (listcpfm[j].lMeter == listDC[i].GetMeter())
                    {
                        int iValue = PointToMeter(listIC, listcpfm[j].lLoc, fi.iChannelNumber, fi.iKmInc);
                        if (iValue > 0)
                        {
                            listDC[i].bFix = true;
                            listDC[i].iMaxpost = iValue / 1000;
                            listDC[i].dMaxminor = iValue % 1000;
                        }
                        break;
                    }
                }
            }


            //将修正后的偏差数据存储到iic中

            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    for (int i = 0; i < listDC.Count; i++)
                    {
                        if (listDC[i].bFix)
                        {
                            sqlcom.CommandText = "update fix_defects set maxpost=" + listDC[i].iMaxpost.ToString() +
                                ",maxminor=" + listDC[i].dMaxminor.ToString() + ",maxval2=-200 where RecordNumber=" + listDC[i].iRecordNumber.ToString();
                            sqlcom.ExecuteNonQuery();
                        }
                    }
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }

        }


        /// <summary>
        /// 根据点返回索引文件里对应的里程信息
        /// </summary>
        /// <param name="listIC">索引信息</param>
        /// <param name="lPosition">点的位置</param>
        /// <param name="tds">文件通道书</param>
        /// <param name="sKmInc">增减里程标 【0增里程；1减里程】</param>
        /// <returns>索引里程：单位为米</returns>
        public int PointToMeter(List<IndexSta> listIC, long lPosition, int tds, int iKmInc)
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
                        //减里程
                        if (iKmInc==1)
                        {
                            iKM = (int)float.Parse(listIC[i].LEndMeter);
                        }
                        else
                        {
                            iKM = (int)float.Parse(listIC[i].lStartMeter);
                        }
                        for (iIndex = 0; iIndex < iCount && (lPosition + iIndex * tds * 2) < listIC[i].lEndPoint; )
                        {
                            float f = (lCurPos / tds / 2 + iIndex) * ((float.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));
                            
                            Milestone wm = new Milestone();
                            //减里程
                            if (iKmInc == 1)
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
                        for (iIndex = 0; iIndex < iCount && (lPosition + iIndex * tds * 2) < listIC[i].lEndPoint; )
                        {
                            float f = (lCurPos / tds / 2 + iIndex) * ((float.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));
                            Milestone wm = new Milestone();
                            //减里程
                            if (iKmInc == 1)
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
        /// TQI修正
        /// </summary>
        /// <param name="citFilePath">cit文件路径</param>
        /// <param name="iicFilePath">iic文件路径</param>
        /// <param name="listIC">索引数据类集合</param>
        /// <param name="iSmaleRate"></param>
        /// <param name="iChannelNumber">通道数</param>
        /// <param name="iKmInc">是否增减里程</param>
        /// <param name="listIDC">无效标记集合</param>
        /// <param name="sTrain"></param>
        public void TQIFix(string citFilePath, string iicFilePath, List<IndexSta> listIC, List<InvalidData> listIDC)
        {
            int iStartKM = 0;
            int iEndKM = 0;
            String subCode = null; //ashx
            DateTime runDate = DateTime.Now;

            int iChannelNumber;
            int iKmInc;
            int iSmaleRate;
            string sTrain = "";

            float[] fscale = null;
            FileInformation fi = null;

            GetSubCodeAndRunDate(iicFilePath, ref subCode, ref runDate);

            GetTQIMiles(iicFilePath, ref iStartKM, ref iEndKM);

            //获取通道序号
            int[] channelNums = GetChannelNumber(citFilePath, ref iStartKM, ref iEndKM, ref fscale, ref fi);

            iChannelNumber = fi.iChannelNumber;
            iKmInc = fi.iKmInc;
            iSmaleRate = fi.iSmaleRate;
            sTrain = fi.sTrain;

            //计算TQI
            List<TQI> listTQI = CalculateTQI(citFilePath, listIC, channelNums, fscale, fi);

            InsertTQI(iicFilePath, listTQI, subCode, runDate);

            DeleteTQINotInRange(iicFilePath, iStartKM, iEndKM, iKmInc);

            InvidTQIFilter(iicFilePath, listIDC, listIC, iChannelNumber, iKmInc);
        }


        /// <summary>
        /// 获取线路代码和检测时间
        /// </summary>
        /// <param name="iicFilePath">IIC文件路径</param>
        /// <param name="subCode">线路代码</param>
        /// <param name="runDate">检测时间</param>
        private void GetSubCodeAndRunDate(string iicFilePath,ref string subCode,ref DateTime runDate)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select DISTINCT SubCode,RunDate from tqi";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBdr = sqlcom.ExecuteReader();

                    if (oleDBdr.Read())
                    {
                        subCode = oleDBdr.GetValue(0).ToString();
                        runDate = DateTime.Parse(oleDBdr.GetValue(1).ToString());
                    }
                    oleDBdr.Close();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取TQI里程
        /// </summary>
        /// <param name="iicFilePath">IIC文件路径</param>
        /// <param name="iStartKM">开始里程</param>
        /// <param name="iEndKM">结束里程</param>
        private void GetTQIMiles(string iicFilePath, ref int iStartKM, ref int iEndKM)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "select min(FromPost),max(FromPost) from tqi";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    OleDbDataReader oleDBdr = sqlcom.ExecuteReader();
                    if (oleDBdr.Read())
                    {
                        iStartKM = int.Parse(oleDBdr.GetValue(0).ToString());
                        iEndKM = int.Parse(oleDBdr.GetValue(1).ToString());
                    }

                    oleDBdr.Close();
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取通道序号
        /// </summary>
        /// <param name="citFilePath">cit文件路径</param>
        /// <param name="iStartKM">TQI开始里程</param>
        /// <param name="iEndKM">TQI结束里程</param>
        /// <returns></returns>
        private int[] GetChannelNumber(string citFilePath,ref int iStartKM,ref int iEndKM,ref float[] fscale,ref FileInformation fi)
        {
            string[] sTQIItem = new string[] { "L_Prof_SC", "R_Prof_SC", "L_Align_SC",
                "R_Align_SC", "Gage", "Crosslevel", "Short_Twist", "LACC", "VACC","Speed"};
            int[] sTQIItemIndex = new int[sTQIItem.Length];
            
            fi = citHelper.GetFileInformation(citFilePath);
            List<ChannelDefinition> channelList = citHelper.GetChannelDefinitionList(citFilePath);

            fscale = new float[channelList.Count];

            for (int i = 0; i < sTQIItem.Length; i++)
            {
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (sTQIItem[i].Equals(channelList[j].sNameEn))
                    {
                        sTQIItemIndex[i] = j;
                        break;
                    }

                    fscale[j] = channelList[j].fScale;
                }
            }

            fscale[1] = 4;//有时为了加密数据。通道基线可能会设置成别的值。这里统一校正成4
            //减里程
            if (fi.iKmInc == 1)
            {
                int iChange = 0;
                iChange = iStartKM;
                iStartKM = iEndKM;
                iEndKM = iChange;
            }

            return sTQIItemIndex;
        }

        /// <summary>
        /// 计算TQI
        /// </summary>
        /// <param name="citFilePath">cit文件路径</param>
        /// <param name="listIC">索引数据类集合</param>
        /// <param name="channelNums">需要计算的通道号集合</param>
        /// <param name="fscale"></param>
        /// <param name="fi"></param>
        /// <returns></returns>
        private List<TQI> CalculateTQI(string citFilePath, List<IndexSta> listIC, int[] channelNums,float[] fscale,FileInformation fi)
        {
            int channelCount = fi.iChannelNumber;
            string sKmInc = "";
            if (fi.iKmInc == 0)
            {
                sKmInc = "增";
            }
            else if (fi.iKmInc == 1)
            {
                sKmInc = "减";
            }


            List<TQI> listTQI = new List<TQI>();
            for (int i = 0; i < listIC.Count; i++)
            {
                List<TQIMile> listWM = new List<TQIMile>();
                double dStartMile = 0d;
                double dEndMile = 0d;
                dStartMile = double.Parse(listIC[i].lStartMeter);
                dEndMile = double.Parse(listIC[i].LEndMeter);
                
                int iKM = (int)float.Parse(listIC[i].lStartMeter);
                int iMeter = (int)(float.Parse(listIC[i].lStartMeter) * 1000) - (iKM * 1000);
                if (sKmInc.Equals("增"))
                {
                    while (true)
                    {
                        TQIMile wm = new TQIMile();
                        int iMod = iMeter % 200;
                        wm.iMeter = iMeter + (200 - iMod);

                        wm.iKM = iKM;
                        if (listIC[i].sType.Equals("正常"))
                        {
                            if (wm.iMeter == 1000)
                            {
                                wm.iMeter = 0;
                                wm.iKM = iKM + 1;
                            }
                        }
                        else
                        {

                        }
                        wm.lPostion = GetNewIndexMeterPositon(listIC, wm.iKM * 1000 + wm.iMeter, channelCount, sKmInc, 0);
                        iMeter = wm.iMeter;
                        iKM = wm.iKM;
                        if ((iKM + iMeter / 1000f) < dEndMile)
                        { listWM.Add(wm); }
                        else
                        {
                            break;
                        }
                    }
                }
                else//jian
                {
                    if (listIC[i].sType.Equals("长链"))
                    {
                        iMeter = (int)(float.Parse(listIC[i].lContainsMeter) * 1000);
                    }
                    while (true)
                    {
                        TQIMile wm = new TQIMile();
                        int iMod = iMeter % 200;
                        wm.iMeter = iMeter - (iMod == 0 ? 200 : iMod);
                        wm.iKM = iKM;
                        if (listIC[i].sType.Equals("正常"))
                        {
                            if (wm.iMeter < 0)
                            {
                                wm.iMeter = 800;
                                wm.iKM = iKM - 1;
                            }
                        }
                        else
                        {

                        }
                        wm.lPostion = GetNewIndexMeterPositon(listIC, wm.iKM * 1000 + wm.iMeter, channelCount, sKmInc, 0);
                        iMeter = wm.iMeter;
                        iKM = wm.iKM;
                        if (wm.iMeter == 0 && listIC[i].sType.Equals("正常"))
                        {
                            wm.iMeter = 800;
                            wm.iKM -= 1;
                        }
                        else
                        {
                            wm.iMeter -= 200;
                        }
                        if ((iKM + iMeter / 1000f) > dEndMile)
                        { listWM.Add(wm); }
                        else
                        {
                            break;
                        }
                    }
                }
                //


                FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs);

                int iRate = (int)(200 / (float.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));
                for (int k = 0; k < listWM.Count; k++)
                {
                    if (listWM[k].lPostion == -1)
                    {
                        continue;
                    }
                    br.BaseStream.Position = listWM[k].lPostion;
                    double[][] fArray = new double[10][];
                    for (int j = 0; j < 10; j++)
                    {
                        fArray[j] = new double[iRate];
                    }
                    for (int l = 0; l < iRate; l++)
                    {
                        if (br.BaseStream.Position < br.BaseStream.Length)
                        {
                            byte[] b = br.ReadBytes(channelCount * 2);
                            if (Encryption.IsEncryption(fi.sDataVersion))
                            {
                                b = Encryption.Translate(b);
                            }
                            //处理数据通道
                            for (int n = 0; n < channelNums.Length; n++)
                            {
                                float fValue = float.Parse((BitConverter.ToInt16(b, channelNums[n] * 2)).ToString()) / fscale[channelNums[n]];
                                //sb.Append("," + fValue.ToString("f2"));
                                fArray[n][l] = fValue;
                            }
                        }
                    }
                    //计算
                    TQI tqic = new TQI();
                    tqic.zgd = Math.Round(CalcStardard(fArray[0]), 2);
                    tqic.ygd = Math.Round(CalcStardard(fArray[1]), 2);
                    tqic.zgx = Math.Round(CalcStardard(fArray[2]), 2);
                    tqic.ygx = Math.Round(CalcStardard(fArray[3]), 2);
                    tqic.gj = Math.Round(CalcStardard(fArray[4]), 2);
                    tqic.sp = Math.Round(CalcStardard(fArray[5]), 2);
                    tqic.sjk = Math.Round(CalcStardard(fArray[6]), 2);
                    tqic.hj = Math.Round(CalcStardard(fArray[7]), 2);
                    tqic.cj = Math.Round(CalcStardard(fArray[8]), 2);
                    tqic.pjsd = CalcAvgSpeed(fArray[9]);
                    tqic.iKM = listWM[k].iKM;
                    tqic.iMeter = listWM[k].iMeter;
                    listTQI.Add(tqic);
                }
            }

            return listTQI;
        }

        /// <summary>
        /// 插入TQI
        /// </summary>
        /// <param name="iicFilePath">IIC文件路径</param>
        /// <param name="listTQI">要插入的TQI集合</param>
        /// <param name="subCode">线路代码</param>
        /// <param name="runDate">检测时间</param>
        private void InsertTQI(string iicFilePath, List<TQI> listTQI,string subCode,DateTime runDate)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    for (int i = 0; i < listTQI.Count; i++)
                    {
                        sqlcom.CommandText = "insert into fix_tqi " +
                            "values('" + subCode + "','" + runDate + "'," + listTQI[i].iKM.ToString()
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 删除不在范围内的TQI
        /// </summary>
        /// <param name="iicFilePath">IIC文件路径</param>
        /// <param name="iStartKM">开始里程</param>
        /// <param name="iEndKM">结束里程</param>
        /// <param name="iKmInc">增减里程 0：增里程；1：减里程</param>
        private void DeleteTQINotInRange(string iicFilePath, int iStartKM, int iEndKM, int iKmInc)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    //增里程
                    if (iKmInc==0)
                    {
                        sqlCreate = " FromPost<" + iStartKM.ToString() + " or FromPost>" + iEndKM.ToString();
                    }
                    else
                    {
                        sqlCreate = " FromPost>" + iStartKM.ToString() + " or FromPost<" + iEndKM.ToString();
                    }
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
        }

        /// <summary>
        /// 无效TQI滤除【重新计算的tqi，都是用小里程表示。---已经和赵主任确认-20140526】
        /// </summary>
        /// <param name="iicFilePath">IIC文件路径</param>
        /// <param name="listIDC">无效数据集合</param>
        /// <param name="listIC">索引数据集合</param>
        /// <param name="iChannelNumber">通道数</param>
        /// <param name="iKmInc">增减里程 0：增里程；1：减里程</param>
        private void InvidTQIFilter(string iicFilePath, List<InvalidData> listIDC, List<IndexSta> listIC,int iChannelNumber,int iKmInc)
        {
            try
            {
                using (OleDbConnection sqlconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + iicFilePath + ";Persist Security Info=True"))
                {
                    string sqlCreate = "";
                    OleDbCommand sqlcom = new OleDbCommand(sqlCreate, sqlconn);
                    sqlconn.Open();
                    for (int iVar = 0; iVar < listIDC.Count; iVar++)
                    {
                        int iStartMeter = PointToMeter(listIC, long.Parse(listIDC[iVar].sStartPoint), iChannelNumber, iKmInc);
                        int iEndMeter = PointToMeter(listIC, long.Parse(listIDC[iVar].sEndPoint), iChannelNumber, iKmInc);
                        //根据点获取里程
                        if (iKmInc == 0)
                        {
                            sqlCreate = " (FromPost*1000+fromminor)>=" + (iStartMeter - 200).ToString() +
                                " and (FromPost*1000+fromminor)<=" + (iEndMeter).ToString();
                        }
                        else
                        {
                            sqlCreate = "  (FromPost*1000+fromminor)<=" + (iStartMeter).ToString() +
                                " and (FromPost*1000+fromminor)>=" + (iEndMeter - 200).ToString();   /*减里程时，iEndMeter是小里程*/
                        }
                        sqlcom.CommandText = "update fix_tqi set valid=0 where " + sqlCreate;
                        int tmp = sqlcom.ExecuteNonQuery();
                    }
                    sqlconn.Close();
                }
                Application.DoEvents();
            }
            catch
            {

            }
        }



        /// <summary>
        /// 索引修正条件下：
        /// 根据当前位置(单位：米)，获取相应的在修正之后的文件中的文件指针
        /// </summary>
        /// <param name="listIC">与数据库中IndexSta表对应的长短链索引数据类对象</param>
        /// <param name="iCurrentMeter">当前位置(单位：米)</param>
        /// <param name="tds">通道数</param>
        /// <param name="sKmInc">增减里程标志</param>
        /// <param name="lReviseValue">修正值(采样点的个数)</param>
        /// <returns>经索引修正后，当前位置(单位：米)在文件中的文件指针</returns>
        public long GetNewIndexMeterPositon(List<IndexSta> listIC, long iCurrentMeter, int tds, string sKmInc, long lReviseValue)
        {
            //增里程
            if (sKmInc.Contains("增"))
            {
                for (int i = 0; i < listIC.Count; i++)
                {
                    if (iCurrentMeter >= (long)(double.Parse(listIC[i].lStartMeter) * 1000) &&
                        iCurrentMeter <= (long)(double.Parse(listIC[i].LEndMeter) * 1000))
                    {
                        if (iCurrentMeter == (long)(double.Parse(listIC[i].LEndMeter) * 1000))
                        {
                            return listIC[i].lEndPoint;
                        }
                        long lDivMeter = (iCurrentMeter - (long)(double.Parse(listIC[i].lStartMeter) * 1000));
                        long lPos = (long)Math.Ceiling(lDivMeter / (double.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint));
                        return listIC[i].lStartPoint + (lReviseValue * 2 * tds) + (lPos * 2 * tds);
                    }
                }
            }
            else//减里程
            {
                for (int i = 0; i < listIC.Count; i++)
                {
                    if (iCurrentMeter <= (long)(double.Parse(listIC[i].lStartMeter) * 1000) &&
                        iCurrentMeter >= (long)(double.Parse(listIC[i].LEndMeter) * 1000))
                    {

                        if (iCurrentMeter == (long)(double.Parse(listIC[i].LEndMeter) * 1000))
                        {
                            return listIC[i].lEndPoint;
                        }

                        long lDivMeter = ((long)(double.Parse(listIC[i].lStartMeter) * 1000) - iCurrentMeter);
                        long lPos = (long)Math.Ceiling((lDivMeter / ((double.Parse(listIC[i].lContainsMeter) * 1000 / listIC[i].lContainsPoint))));
                        return listIC[i].lStartPoint + (lReviseValue * 2 * tds) + (lPos * 2 * tds);
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 标准差计算
        /// </summary>
        /// <param name="dItems">数组</param>
        /// <returns>标准差值</returns>
        private double CalcStardard(double[] dItems)
        {
            double dResult = 0;
            double dSum = 0;
            for (int i = 0; i < dItems.Length; i++)
            {
                dSum += dItems[i];
            }
            dSum /= dItems.Length;
            double dAve = 0;
            for (int i = 0; i < dItems.Length; i++)
            {
                dAve += Math.Pow((dItems[i] - dSum), 2);
            }
            dAve /= dItems.Length;
            dResult = Math.Pow(dAve, 0.5);

            return dResult;
        }

        /// <summary>
        /// 计算平均速度
        /// </summary>
        /// <param name="dSpeed">速度数组</param>
        /// <returns>返回平均速度</returns>
        private int CalcAvgSpeed(double[] dSpeed)
        {
            int iSpeed = 0;
            double dSum = 0.0;
            for (int i = 0; i < dSpeed.Length; i++)
            {
                dSum += dSpeed[i];
            }
            iSpeed = (int)(dSum / dSpeed.Length);
            return iSpeed;
        }

    }
}
