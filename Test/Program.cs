using CitFileSDK;
using CitIndexFileSDK;
using CitIndexFileSDK.MileageFix;
using CommonFileSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string citFile = @"H:\工作文件汇总\铁科院\程序\轨检\data\GNHS-HANGZHOU-NANJING-14052016-175302-1减变增.cit";
            string idfFile = @"H:\工作文件汇总\铁科院\程序\轨检\data\GNHS-HANGZHOU-NANJING-14052016-175302-1减变增.idf";

            IOperator indexOperator = new IndexOperator();
            indexOperator.IndexFilePath = idfFile;

            InnerFileOperator.InnerFilePath = @"H:\工作文件汇总\铁科院\程序\轨检\data\" + "InnerDB.idf";
            InnerFileOperator.InnerConnString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0}; Persist Security Info = True; Mode = Share Exclusive; Jet OLEDB:Database Password = iicdc; ";


            MilestoneFix _mileageFix = new MilestoneFix(citFile, indexOperator);

            _mileageFix.ReadMilestoneFixTable();
            

            List<MileStoneFixData> listFixData = new List<MileStoneFixData>();
            listFixData = _mileageFix.FixData;
        }
    }
}
