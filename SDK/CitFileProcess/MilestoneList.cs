/// -------------------------------------------------------------------------------------------
/// FileName：MilestoneList.cs
/// 说    明：里程相关操作
/// Version ：1.0
/// Date    ：2017/5/26
/// Author  ：CaoKai
/// -------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace CitFileSDK
{
    /// <summary>
    /// 里程操作类
    /// </summary>
    public class MilestoneList
    {
        /// <summary>
        /// 里程数据集合
        /// </summary>
        public List<Milestone> milestoneList { get; set; }

        /// <summary>
        /// 是否增里程
        /// </summary>
        public bool mIsKmIncr { get; set; }

        /// <summary>
        /// 获取指定里程的位置
        /// </summary>
        /// <param name="mileStone">里程</param>
        /// <returns>里程所在的位置</returns>
        public long GetMilestoneFilePosition(float mileStone)
        {
            long position = 0;
            float temp=99999;
            for (int i = 0; i < milestoneList.Count; i++)
            {
                float value = Math.Abs(milestoneList[i].GetMeter()-mileStone);
                if (value < temp)
                {
                    temp = value;
                }
                if (value > temp)
                {
                    if (i > 0)
                    {
                        position = milestoneList[i - 1].mFilePosition;
                    }
                    else
                    {
                        position = milestoneList[0].mFilePosition;
                    }
                }
            }

            return position;
        }


        /// <summary>
        /// 获取指定范围的里程信息
        /// </summary>
        /// <param name="startms">开始里程</param>
        /// <param name="endms">结束里程</param>
        /// <returns>里程集合</returns>
        public List<Milestone> GetMilestoneRange(float startms, float endms)
        {
            List<Milestone> listMilestoneNew = new List<Milestone>();
            for (int i = 0; i < milestoneList.Count; i++)
            {
                float meter = milestoneList[i].GetMeter();
                if (meter >= startms && meter < endms)
                {
                    listMilestoneNew.Add(milestoneList[i]);
                }
            }
            return listMilestoneNew;
        }

        /// <summary>
        /// 获取里程的开始里程
        /// </summary>
        /// <returns></returns>
        public float GetStart()
        {
            if (milestoneList.Count > 0)
                return milestoneList[0].GetMeter();
            else
                return 0;
        }

        /// <summary>
        /// 获取里程的结束里程
        /// </summary>
        /// <returns></returns>
        public float GetEnd()
        {
            if (milestoneList.Count > 0)
                return milestoneList[milestoneList.Count - 1].GetMeter();
            else
                return 0;
        }
    }
}
