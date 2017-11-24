/// -------------------------------------------------------------------------------------------
/// FileName：CommControl.cs
/// 说    明：比较重要的补充的方法
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：jinxl
/// -------------------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace IntegratedDisplay
{
    internal class CommControl
    {
        /// <summary>
        /// 获取许可字符串
        /// </summary>
        /// <returns></returns>
        public static string GetLicense()
        {
            List<string> listS = new List<string>();
            StringBuilder sbID;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select ProcessorId from " + "Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData po in mo.Properties)
                {
                    sbID = new StringBuilder();
                    sbID.Append(po.Value.ToString().Trim());
                    //while (sbID.Length < 32)
                    //{
                    //    sbID.Append("0");
                    //}
                    listS.Add(sbID.ToString());
                    break;
                }

            }
            mos = new ManagementObjectSearcher("select SerialNumber from " + "Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                foreach (PropertyData po in mo.Properties)
                {
                    sbID = new StringBuilder();
                    sbID.Append(po.Value.ToString().Trim());
                    //while (sbID.Length < 32)
                    //{
                    //    sbID.Append("0");
                    //}
                    listS.Add(sbID.ToString());
                    break;
                }

            }
            return listS[0] + listS[1];
            
        }
    }
}
