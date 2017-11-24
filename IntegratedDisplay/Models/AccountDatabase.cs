using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegratedDisplay.Models
{
    public class AccountDatabase
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMileageVisible { get; set; }
        public bool IsChannelLabelVisible { get; set; }
        public List<AccountTotalData> AccountTotalDataList { get; set; }

        public AccountDatabase()
        {
            Name = "";
            IsVisible = true;
            IsMileageVisible = true;
            IsChannelLabelVisible = true;
            AccountTotalDataList = new List<AccountTotalData>();
        }

    }
}
