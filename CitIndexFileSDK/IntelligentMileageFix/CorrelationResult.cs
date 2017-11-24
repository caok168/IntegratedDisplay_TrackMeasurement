using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitIndexFileSDK.IntelligentMileageFix
{
    public class CorrelationResult
    {
        public long FilePointer { get; set; }
        public bool IsFind { get; set; }
        public int ChannelID { get; set; }
        public string ChannelName { get; set; }
    }
}
