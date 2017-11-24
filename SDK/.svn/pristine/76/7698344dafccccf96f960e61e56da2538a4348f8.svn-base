using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CitFileSDK
{
    public partial class CITFileProcess
    {
        /// <summary>
        /// 把单行线都统一为增里程(包括正方向和反方向)
        /// </summary>
        /// <param name="citFileName">cit文件路径</param>
        public void ModifyCitMergeKmInc(String citFileName)
        {
            FileInformation citHeaderInfo = GetFileInformation(citFileName);

            bool isKmInc = IsCitKmInc(citFileName);

            //文件头中指示为增里程，且文件中确实为增里程，则不需要处理，直接返回。
            if (citHeaderInfo.iKmInc == 0 && isKmInc == true)
            {
                return;
            }

            //以下情况：有可能是文件头指示错误或是实际文件确实为减里程
            //统一为增里程
            if (citHeaderInfo.iKmInc != 0)
            {
                citHeaderInfo.iKmInc = 0;
            }


            try
            {
                #region 存取文件
                FileStream fsRead = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream fsWrite = new FileStream(citFileName + ".bak", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fsRead, Encoding.UTF8);
                BinaryWriter bw1 = new BinaryWriter(fsWrite, Encoding.UTF8);
                byte[] bHead = br.ReadBytes(120);
                byte[] bChannels = br.ReadBytes(65 * citHeaderInfo.iChannelNumber);
                byte[] bData = new byte[citHeaderInfo.iChannelNumber * 2];
                byte[] bDataNew = new byte[citHeaderInfo.iChannelNumber * 2];
                byte[] bTail = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));

                //bw1.Write(bHead);

                bw1.Write(GetBytesFromDataHeadInfo(citHeaderInfo));//文件头
                FileInformation citHeader = GetDataInfoHead(GetBytesFromDataHeadInfo(citHeaderInfo));


                bw1.Write(bChannels);
                bw1.Write(bTail.Length);
                bw1.Write(bTail);

                long startPos = br.BaseStream.Position;//记录数据开始位置的文件指针

                //增里程时，不反转
                if (isKmInc == true)
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        bw1.Write(br.ReadBytes(citHeaderInfo.iChannelNumber * 2));
                        //br.BaseStream.Position += m_dhi.iChannelNumber * 2;
                    }
                }
                else
                {
                    br.BaseStream.Position = br.BaseStream.Length - citHeaderInfo.iChannelNumber * 2;

                    while (br.BaseStream.Position >= startPos)
                    {
                        bw1.Write(br.ReadBytes(citHeaderInfo.iChannelNumber * 2));
                        br.BaseStream.Position -= citHeaderInfo.iChannelNumber * 2 * 2; //liyang: 这块怎么乘以4了 ？ 
                    }
                }

                //
                bw1.Close();
                br.Close();
                fsWrite.Close();
                fsRead.Close();
                //删除bak
                File.Delete(citFileName);
                File.Move(citFileName + ".bak", citFileName);
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return;
        }

        /// <summary>
        /// 把反方向检测转换为正方向检测
        /// </summary>
        /// <param name="citFileName">cit文件路径</param>
        public void ModifyCitReverseToForward(String citFileName)
        {
            FileInformation citHeaderInfo = GetFileInformation(citFileName);
            List<ChannelDefinition> channelList = GetChannelDefinitionList(citFileName);


            //左高低与右高低对调
            ChannelExchange(channelList, "L_Prof_SC", "R_Prof_SC", false);
            ChannelExchange(channelList, "L_Prof_SC_70", "R_Prof_SC_70", false);
            ChannelExchange(channelList, "L_Prof_SC_120", "R_Prof_SC_120", false);

            ChannelDefinition m_dci_a = new ChannelDefinition();

            //左轨向与右轨向对调，然后幅值*（-1）
            ChannelExchange(channelList, "L_Align_SC", "R_Align_SC", true);
            ChannelExchange(channelList, "L_Align_SC_70", "R_Align_SC_70", true);
            ChannelExchange(channelList, "L_Align_SC_120", "R_Align_SC_120", true);


            //水平、超高、三角坑、曲率、曲率变化率*（-1）
            for (int i = 0; i < channelList.Count; i++)
            {
                if (channelList[i].sNameEn.Equals("Crosslevel"))
                {
                    m_dci_a = channelList[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    channelList[i] = m_dci_a;
                }

                if (channelList[i].sNameEn.Equals("Superelevation"))
                {
                    m_dci_a = channelList[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    channelList[i] = m_dci_a;
                }

                if (channelList[i].sNameEn.Equals("Short_Twist"))
                {
                    m_dci_a = channelList[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    channelList[i] = m_dci_a;
                }

                if (channelList[i].sNameEn.Equals("Curvature"))
                {
                    m_dci_a = channelList[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    channelList[i] = m_dci_a;
                }

                if (channelList[i].sNameEn.Equals("Curvature_Rate"))
                {
                    m_dci_a = channelList[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    channelList[i] = m_dci_a;
                }

            }


            try
            {
                #region 存取文件
                FileStream fsRead = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream fsWrite = new FileStream(citFileName + ".bak", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fsRead, Encoding.UTF8);
                BinaryWriter bw1 = new BinaryWriter(fsWrite, Encoding.UTF8);
                byte[] bHead = br.ReadBytes(120);
                byte[] bChannels = br.ReadBytes(65 * citHeaderInfo.iChannelNumber);
                byte[] bData = new byte[citHeaderInfo.iChannelNumber * 2];
                byte[] bDataNew = new byte[citHeaderInfo.iChannelNumber * 2];
                byte[] bTail = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));

                //bw1.Write(bHead);

                bw1.Write(GetBytesFromDataHeadInfo(citHeaderInfo));//文件头
                //反向--转换为正向
                if (citHeaderInfo.iRunDir == 0)
                {
                    bw1.Write(bChannels);
                }
                else
                {
                    bw1.Write(GetBytesFromChannelDataInfoList(channelList));
                }

                bw1.Write(bTail.Length);
                bw1.Write(bTail);

                long startPos = br.BaseStream.Position;//记录数据开始位置的文件指针

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    bw1.Write(br.ReadBytes(citHeaderInfo.iChannelNumber * 2));
                }
                bw1.Close();
                br.Close();
                fsWrite.Close();
                fsRead.Close();
                //删除bak
                File.Delete(citFileName);
                File.Move(citFileName + ".bak", citFileName);
                #endregion
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

       /// <summary>
       /// 根据文件里的里程数据判断增减
       /// </summary>
       /// <param name="citFileName">cit文件</param>
       /// <returns>增为true，减为false</returns>
        public bool IsCitKmInc(String citFileName)
        {
            Milestone firstMile = GetStartMilestone(citFileName);
            Milestone endMile = GetEndMilestone(citFileName);

            if(firstMile.GetMeter()>endMile.GetMeter())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 交换通道数据
        /// </summary>
        /// <param name="m_dciL">要交换的通道数据</param>
        /// <param name="channel_L">左通道名称</param>
        /// <param name="channel_R">右通道名称</param>
        /// <param name="isInverted">是否反转</param>
        private void ChannelExchange(List<ChannelDefinition> m_dciL, string channel_L, string channel_R, bool isInverted)
        {
            int index_a = 0;
            int index_b = 0;
            ChannelDefinition m_dci_a = new ChannelDefinition();
            ChannelDefinition m_dci_b = new ChannelDefinition();
            foreach (ChannelDefinition m_dci in m_dciL)
            {
                if (m_dci.sNameEn == channel_L)
                {
                    index_a = m_dciL.IndexOf(m_dci);
                    m_dci_a = m_dci;
                    if (isInverted)
                    {
                        m_dci_a.fScale = m_dci_a.fScale * (-1);
                    }
                }

                if (m_dci.sNameEn == channel_R)
                {
                    index_b = m_dciL.IndexOf(m_dci);
                    m_dci_b = m_dci;
                    if (isInverted)
                    {
                        m_dci_b.fScale = m_dci_b.fScale * (-1);
                    }
                }
            }
            if (index_a < index_b)
            {
                m_dciL.RemoveAt(index_a);
                m_dciL.Insert(index_a, m_dci_b);

                m_dciL.RemoveAt(index_b);
                m_dciL.Insert(index_b, m_dci_a);
            }
            else if (index_a > index_b)
            {
                m_dciL.RemoveAt(index_b);
                m_dciL.Insert(index_b, m_dci_a);

                m_dciL.RemoveAt(index_a);
                m_dciL.Insert(index_a, m_dci_b);
            }
        }
    }
}
