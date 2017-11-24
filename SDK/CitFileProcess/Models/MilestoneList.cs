using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileSDK
{
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
        /// <param name="mileStone"></param>
        /// <returns></returns>
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
        /// <param name="startms"></param>
        /// <param name="endms"></param>
        /// <returns></returns>
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
