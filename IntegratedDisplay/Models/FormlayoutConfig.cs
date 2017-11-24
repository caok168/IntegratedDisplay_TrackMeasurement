using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IntegratedDisplay.Models
{
    [Serializable]
    public class FormlayoutConfig
    {
        public List<string> statusBarList { get; set; }
        //public Dictionary<string,List<myConfig>> itemList { get; set; }
        public FormlayoutConfig()
        {
            statusBarList = new List<string>();
        }
    }
}
