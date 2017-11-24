using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK.MileageFix
{
    public class UserMarkedPoint
    {

        public string ID { get; set; }
        public long FilePointer { get; set; }
        /// <summary>
        /// 设置的标准点里程(米)
        /// </summary>
        public float UserSetMileage { get; set; }

    }
}
