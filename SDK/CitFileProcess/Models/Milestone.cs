/// -------------------------------------------------------------------------------------------
/// FileName：Milestone.cs
/// 说    明：里程定义类
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------

namespace CitFileSDK
{
    /// <summary>
    /// 里程类
    /// </summary>
    public class Milestone
    {
        /// <summary>
        /// 公里
        /// </summary>
        public float mKm { get; set; }

        /// <summary>
        /// 米
        /// </summary>
        public float mMeter { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public long mFilePosition = 0;

        /// <summary>
        /// 该里程点结束的位置
        /// </summary>
        public long mFileEndPostion = 0;

        /// <summary>
        /// 获取里程（米）
        /// </summary>
        /// <returns></returns>
        public float GetMeter()
        {
            return mKm * 1000 + mMeter;
        }

        /// <summary>
        /// 获取里程（米）
        /// </summary>
        /// <param name="scale">比例</param>
        /// <returns></returns>
        public float GetMeter(float scale)
        {
            return mKm * 1000 + mMeter / scale;
        }
        public string GetMeterString()
        {
            return GetMeter().ToString();
        }

        public Milestone()
        {
            mFilePosition = -1;
            mFileEndPostion = -1;
        }
    }
}
