/// -------------------------------------------------------------------------------------------
/// FileName：ChannelsClass.cs
/// 说    明：通道配置相关操作
/// Version ：1.0
/// Date    ：2017/5/27
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using IntegratedDisplay.Models;
using System.Drawing;
using CitFileSDK;

namespace IntegratedDisplay
{
    /// <summary>
    /// 通道配置操作类
    /// </summary>
    public class ChannelManager
    {
        /// <summary>
        /// 所有通道显示的总高度==波形高度
        /// </summary>
        public static int channelTotalHeight = 650;

        /// <summary>
        /// 加载通道配置信息
        /// </summary>
        /// <param name="sFileName">配置文件路径</param>
        /// <param name="sChannelsName">通道名称</param>
        /// <returns>符合条件的通道配置信息集合</returns>
        public static List<ChannelsClass> LoadChannelsConfig(string sFileName, string[] channelNames)
        {
            List<ChannelsClass> channelList = new List<ChannelsClass>();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFileName);

                XmlNodeList xnl = xd.DocumentElement["channels"].ChildNodes;
                channelList = new List<ChannelsClass>(xnl.Count);
                for (int i = 0; i < xnl.Count; i++)
                {
                    string sName = xnl[i].Attributes["name"].InnerText;

                    for (int j = 0; j < channelNames.Length; j++)
                    {
                        if (channelNames[j].ToLower().Equals(sName.ToLower()))
                        {
                            ChannelsClass cc = new ChannelsClass();
                            cc.Id = int.Parse(xnl[i].Attributes["id"].InnerText);
                            cc.Name = sName;
                            cc.NonChineseName = xnl[i].Attributes["non-chinese_name"].InnerText;
                            cc.ChineseName = xnl[i].Attributes["chinese_name"].InnerText;
                            cc.Color = int.Parse(xnl[i].Attributes["color"].InnerText, NumberStyles.AllowHexSpecifier);
                            cc.IsVisible = bool.Parse(xnl[i].Attributes["visible"].InnerText);
                            cc.ZoomIn = float.Parse(xnl[i].Attributes["zoomin"].InnerText);
                            cc.Units = xnl[i].Attributes["units"].InnerText;
                            cc.IsMeaOffset = bool.Parse(xnl[i].Attributes["mea-offset"].InnerText);
                            cc.Location = int.Parse(xnl[i].Attributes["location"].InnerText);
                            cc.LineWidth = float.Parse(xnl[i].Attributes["line_width"].InnerText);
                            cc.Height = int.Parse(xnl[i].Attributes["sigleHeight"].InnerText);
                            channelList.Add(cc);
                            break;
                        }
                    }
                }
                channelList.TrimExcess();
                return channelList;
            }
            catch (Exception ex)
            {
                throw new Exception("通道配置文件加载错误，请检查！"+ex.Message);
            }

        }

        /// <summary>
        /// 通过xml配置加载通道配置
        /// </summary>
        /// <param name="sFileName">xml配置文件</param>
        /// <param name="channelDefinitionList">通道定义列表</param>
        /// <returns></returns>
        public static List<ChannelsClass> LoadChannelsConfig(string sFileName,List<ChannelDefinition> channelDefinitionList)
        {
            List<ChannelsClass> channelList = new List<ChannelsClass>();
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFileName);
                XmlNodeList xnl = xd.DocumentElement["channels"].ChildNodes;
                channelList = new List<ChannelsClass>(channelDefinitionList.Count);
                List<string> notInList = new List<string>();
                for (int j = 2; j < channelDefinitionList.Count; j++)
                {
                    bool isFind = false;
                    string sName = channelDefinitionList[j].sNameEn;
                    for (int i = 0; i < xnl.Count; i++)
                    {
                        
                        if (xnl[i].Attributes["name"].InnerText.ToLower().Equals(sName.ToLower()))
                        {
                            ChannelsClass cc = new ChannelsClass();
                            cc.Id = int.Parse(xnl[i].Attributes["id"].InnerText);
                            cc.Name = sName;
                            cc.NonChineseName = xnl[i].Attributes["non-chinese_name"].InnerText;
                            cc.ChineseName = xnl[i].Attributes["chinese_name"].InnerText;
                            cc.Color = int.Parse(xnl[i].Attributes["color"].InnerText, NumberStyles.AllowHexSpecifier);
                            cc.IsVisible = bool.Parse(xnl[i].Attributes["visible"].InnerText);
                            cc.ZoomIn = float.Parse(xnl[i].Attributes["zoomin"].InnerText);
                            cc.Units = xnl[i].Attributes["units"].InnerText;
                            cc.IsMeaOffset = bool.Parse(xnl[i].Attributes["mea-offset"].InnerText);
                            cc.Location = int.Parse(xnl[i].Attributes["location"].InnerText);
                            cc.LineWidth = float.Parse(xnl[i].Attributes["line_width"].InnerText);
                            channelList.Add(cc);
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                    {
                        int interLen = (100 / ((channelDefinitionList.Count - 2 + 1)));
                        int singleHeight = (channelTotalHeight / ((channelDefinitionList.Count - 2 + 1)));

                        ChannelsClass cc = new ChannelsClass();
                        cc.Id = j;
                        cc.Name = sName;
                        cc.NonChineseName = channelDefinitionList[j].sNameEn;
                        cc.ChineseName = channelDefinitionList[j].sNameCh;
                        cc.Color = int.Parse(GetRandomColor(), NumberStyles.AllowHexSpecifier);
                        cc.IsVisible = true;
                        if (cc.Name == "Superelevation" || cc.Name == "Speed")
                        {
                            cc.ZoomIn = 20;
                        }
                        else if (cc.Name == "LACC" || cc.Name == "VACC")
                        {
                            cc.ZoomIn = 0.01f;
                        }
                        else
                        {
                            cc.ZoomIn = 1;
                        }
                        cc.Units = channelDefinitionList[j].sUnit;
                        cc.IsMeaOffset = false;
                        cc.Location = (j - 1) * interLen + (j - 1);
                        cc.LineWidth = 1;
                        channelList.Add(cc);
                    }
                }
                channelList.TrimExcess();
                return channelList;
            }
            catch (Exception ex)
            {
                throw new Exception("通道配置文件加载错误，请检查！" + ex.Message);
            }
        }
        /// <summary>
        /// 保存通道配置信息
        /// </summary>
        /// <param name="sFileName">配置文件路径</param>
        /// <param name="channelList">通道配置信息集合</param>
        public static void SaveChannelsConfig(string sFileName,List<ChannelsClass> channelList)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(sFileName);
                XmlNodeList xnl = xd.DocumentElement["channels"].ChildNodes;
                for (int i = 0; i < xnl.Count; i++)
                {
                    //保存配置文件
                    string name = xnl[i].Attributes["name"].InnerText.ToLower();
                    for (int j = 0; j < channelList.Count; j++)
                    {
                        if (channelList[j].NonChineseName.ToLower().Equals(name))
                        {
                            xnl[i].Attributes["non-chinese_name"].Value = channelList[j].NonChineseName;
                            xnl[i].Attributes["chinese_name"].Value = channelList[j].ChineseName;
                            xnl[i].Attributes["color"].Value = channelList[j].Color.ToString("x");
                            xnl[i].Attributes["visible"].Value = channelList[j].IsVisible.ToString();
                            xnl[i].Attributes["zoomin"].Value = channelList[j].ZoomIn.ToString();
                            xnl[i].Attributes["mea-offset"].Value = channelList[j].IsMeaOffset.ToString();
                            xnl[i].Attributes["line_width"].Value = channelList[j].LineWidth.ToString();
                            xnl[i].Attributes["location"].Value = channelList[j].Location.ToString();
                            break;
                        }
                    }
                }
                xd.Save(sFileName);
            }
            catch(Exception ex)
            {
                MyLogger.LogError("保存通道配置时出错", ex);
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 创建波形配置文件
        /// </summary>
        /// <param name="sFileName">文件路径</param>
        /// <param name="iType"></param>
        /// <param name="dciL">通道定义集合</param>
        /// <returns>配置文件路径</returns>
        public static string CreateWaveXMLConfig(string sFileName,int sigleChannelHeight ,int iType, List<ChannelDefinition> dciL)
        {
            string sDestFileName = "";
            if (iType == 0)
            {
                if (Path.GetDirectoryName(sFileName).EndsWith("\\"))
                {
                    sDestFileName = Path.GetDirectoryName(sFileName) + "默认配置文件.xml";
                }
                else
                {
                    sDestFileName = Path.GetDirectoryName(sFileName) + "\\" + "默认配置文件.xml";
                }
            }
            else
            {
                sDestFileName = sFileName;
            }
            try
            {
                if (File.Exists(sDestFileName))
                {
                    File.Delete(sDestFileName);
                }
                if (dciL != null && dciL.Count > 0)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    XmlDeclaration xmldecl;
                    xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    xmldoc.AppendChild(xmldecl);
                    XmlElement xmlelem = xmldoc.CreateElement("", "app", "");
                    xmldoc.AppendChild(xmlelem);
                    XmlNode root = xmldoc.SelectSingleNode("app");
                    XmlElement xmlelem1 = xmldoc.CreateElement("channels");
                    xmlelem1.SetAttribute("count", (dciL.Count - 2).ToString());
                    root.AppendChild(xmlelem1);
                    XmlNode root1 = root.SelectSingleNode("channels");

                    int interLen = (100 / ((dciL.Count - 2 + 1)));
                    int singleHeight = (channelTotalHeight / ((dciL.Count - 2 + 1)));
                    for (int i = 2; i < dciL.Count; i++)
                    {
                        ChannelDefinition dci = dciL[i];
                        XmlElement xmlelem2 = xmldoc.CreateElement("channel");
                        xmlelem2.SetAttribute("id", dci.sID.ToString());
                        xmlelem2.SetAttribute("name", dci.sNameEn);
                        xmlelem2.SetAttribute("non-chinese_name", dci.sNameEn);
                        xmlelem2.SetAttribute("chinese_name", dci.sNameCh);
                        xmlelem2.SetAttribute("color", GetRandomColor());//"ff0000b4"
                        xmlelem2.SetAttribute("visible", "True");

                        if (dciL[i].sNameEn == "Superelevation" || dciL[i].sNameEn == "Speed")
                        {
                            xmlelem2.SetAttribute("zoomin", "20");
                        }
                        else if (dciL[i].sNameEn == "LACC" || dciL[i].sNameEn == "VACC")
                        {
                            xmlelem2.SetAttribute("zoomin", "0.01");
                        }
                        else
                        {
                            xmlelem2.SetAttribute("zoomin", "1");
                        }

                        xmlelem2.SetAttribute("units", dci.sUnit);
                        xmlelem2.SetAttribute("mea-offset", "False");
                        xmlelem2.SetAttribute("location", ((i - 1) * interLen + (i - 1)).ToString());
                        xmlelem2.SetAttribute("line_width", "1");
                        xmlelem2.SetAttribute("sigleHeight", singleHeight.ToString());
                        root1.AppendChild(xmlelem2);

                    }
                    xmldoc.Save(sDestFileName);
                }
            }
            catch(Exception ex)
            {
                throw ex;   
            }
            return sDestFileName;

        }


        private static string GetRandomColor()
        {
            int iSeed = 10;
            Random ro = new Random(iSeed);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random(Guid.NewGuid().GetHashCode());
           
            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;//0 : 380 - R - G;
            B = (B > 255) ? 255 : B;
            Color c = Color.FromArgb(R, G, B);
            return c.Name;
        }
    }
}
