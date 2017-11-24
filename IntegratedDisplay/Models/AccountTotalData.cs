using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IntegratedDisplay.Models
{
    public class AccountTotalData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NonChineseName { get; set; }
        public string ChineseName { get; set; }
        public int Color { get; set; }
        public bool IsVisible { get; set; }
        public int Location { get; set; }
        public float LineWidth { get; set; }
        public Rectangle DispalyRect { get; set; }
        public List<AccountInfo> AccountInfoList{get;set;}

        public AccountTotalData()
        {
            ID = -1;
            Name = "";
            NonChineseName = "";
            ChineseName = "";
            Color = 0;
            IsVisible = true;
            Location = 1;
            LineWidth = 1.0f;
            DispalyRect = Rectangle.Empty;
            AccountInfoList = new List<AccountInfo>();
        }
    }
}
