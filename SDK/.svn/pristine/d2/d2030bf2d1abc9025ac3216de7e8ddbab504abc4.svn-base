/// -------------------------------------------------------------------------------------------
/// FileName：Encryption.cs
/// 说    明：解密操作相关
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 解密操作类
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// 对加密的字节进行解码
        /// </summary>
        /// <param name="bTranslateData"></param>
        /// <returns></returns>
        public static byte Translate(byte bTranslateData)
        {
            bTranslateData = (byte)(bTranslateData ^ 128);

            return bTranslateData;
        }

        /// <summary>
        /// 对加密的字节数组进行解码
        /// </summary>
        /// <param name="bTranslateData"></param>
        /// <returns></returns>
        public static byte[] Translate(byte[] bTranslateData)
        {
            for (int iIndex = 0; iIndex < bTranslateData.Length; iIndex++)
            {
                bTranslateData[iIndex] = Translate(bTranslateData[iIndex]);
            }
            return bTranslateData;
        }

        /// <summary>
        /// 判断是否加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEncryption(string str)
        {
            if (str.StartsWith("3."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
