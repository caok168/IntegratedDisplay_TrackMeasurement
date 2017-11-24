using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.LinearAlgebra;

namespace IntegratedDisplay
{
    /// <summary>
    /// 自动对齐功能类
    /// </summary>
    public class AutoTranslationClass
    {
        //public DataProcessAdvanceClass advDataProcess;

        public AutoTranslationClass()
        {
            // advDataProcess = new DataProcessAdvanceClass();
        }

        #region 波形自动对齐matlab
        /// <summary>
        /// 自动对齐
        /// </summary>
        /// <param name="ww">输入参考波形1的数据</param>
        ///WW(1,:)：左高低
        ///WW(2,:)：右高低
        ///WW(3,:)：左轨向
        ///WW(4,:)：右轨向
        ///WW(5,:)：轨距------(总共5列数据)
        /// <param name="ww1">参考波形2的数据</param>
        /// <returns>移动的点数</returns>
        /// 备注：波形1不动，波形2减去点数
        ///       波形2不动，波形1加上点数
        //public int AutoTranslation(float[][] ww, float[][] ww1)
        //{
        //    int retVal = 0;

        //    int rowLen_ww = ww.GetLength(0);
        //    int colLen_ww = ww[0].Length;
        //    MWNumericArray d_ww = new MWNumericArray(MWArrayComplexity.Real,rowLen_ww,colLen_ww);
        //    //matlab中矩阵的下标是从1开始的，而C#是从0开始的
        //    for (int i = 0; i < rowLen_ww; i++)
        //    {
        //        for (int j = 0; j < colLen_ww;j++ )
        //        {
        //            d_ww[i + 1, j + 1] = ww[i][j];
        //        }
        //    }

        //    int rowLen_ww1 = ww1.GetLength(0);
        //    int colLen_ww1 = ww1[0].Length;
        //    MWNumericArray d_ww1 = new MWNumericArray(MWArrayComplexity.Real, rowLen_ww1, colLen_ww1);
        //    //matlab中矩阵的下标是从1开始的，而C#是从0开始的
        //    for (int i = 0; i < rowLen_ww1; i++)
        //    {
        //        for (int j = 0; j < colLen_ww1; j++)
        //        {
        //            d_ww1[i + 1, j + 1] = ww1[i][j];
        //        }
        //    }

        //    try
        //    {
        //        //调用算法
        //        MWArray resultArray = advDataProcess.sub_preprocessing_alignment_data(d_ww, d_ww1);
        //        //MWArray resultArray = null;
        //        double[,] tmpArray = (double[,])(resultArray.ToArray());

        //        retVal = (int)tmpArray[0,0];
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }


        //    return retVal;
        //}
        #endregion
        
        public int AutoTranslation(double[][] ww, double[][] ww1)
        {
            int retVal = 0;
            int corrLength = 300;
            int findCount = 0;
            int index = 0;
            //波形个数
            for (int i = 0; i < ww.Length; i++)
            {
                //第一个波形的点数
                for (int j = 0; j < (ww1[i].Length-corrLength); j++)
                {
                    double lastCorr = 0;
                    double[] sourceArray = new double[corrLength];
                    Array.Copy(ww1[i], j, sourceArray, 0, corrLength);
                    //第二个波形的点数
                    for (int k = 0; k < (ww[i].Length-corrLength); k++)
                    {
                        double[] targetArray = new double[corrLength];
                        Array.Copy(ww[i], k, targetArray, 0, corrLength);
                        double cor = Correlation.Pearson(sourceArray, targetArray);
                        if (lastCorr != 1 && lastCorr < cor)
                        {
                            lastCorr = cor;
                            index = j - k;
                        }
                    }
                    if (lastCorr > 0.8)
                    {
                        if (findCount <= 0)//当前通道找到，进行下一个通道查找
                        {
                            findCount++;
                            break;
                        }
                        if (findCount >= 1 && retVal == index)//查找到，并且与前一次的记录值相同，认定为已经找到了，否则认为不稳定，继续查找
                        {
                            return retVal;
                        }
                        else//第一次查找到，记录
                        {
                            retVal = index;
                        }

                    }
                   
                }
               
            }

            return retVal;
        }
        

        //public double Correlation(double[] array1, double[] array2)
        //{
        //    double[] array_xy = new double[array1.Length];
        //    double[] array_xp2 = new double[array1.Length];
        //    double[] array_yp2 = new double[array1.Length];
        //    for (int i = 0; i < array1.Length; i++)
        //        array_xy[i] = array1[i] * array2[i];
        //    for (int i = 0; i < array1.Length; i++)
        //        array_xp2[i] = Math.Pow(array1[i], 2.0);
        //    for (int i = 0; i < array1.Length; i++)
        //        array_yp2[i] = Math.Pow(array2[i], 2.0);
        //    double sum_x = 0;
        //    double sum_y = 0;
        //    foreach (double n in array1)
        //        sum_x += n;
        //    foreach (double n in array2)
        //        sum_y += n;
        //    double sum_xy = 0;
        //    foreach (double n in array_xy)
        //        sum_xy += n;
        //    double sum_xpow2 = 0;
        //    foreach (double n in array_xp2)
        //        sum_xpow2 += n;
        //    double sum_ypow2 = 0;
        //    foreach (double n in array_yp2)
        //        sum_ypow2 += n;
        //    double Ex2 = Math.Pow(sum_x, 2.00);
        //    double Ey2 = Math.Pow(sum_y, 2.00);

        //    return (array1.Length * sum_xy - sum_x * sum_y) /
        //    Math.Sqrt((array1.Length * sum_xpow2 - Ex2) * (array1.Length * sum_ypow2 - Ey2));
        //}
    }
}
