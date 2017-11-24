using CitFileSDK;
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
            string citpath = @"H:\工作文件汇总\铁科院\程序\车载加速度\数据文件\CitData_170413050607_GJGX\CitData_170413050607_GJGX.cit";

            CITFileProcess cithelper = new CITFileProcess();
            var fileInfomation = cithelper.GetFileInformation(citpath);

            var channelList = cithelper.GetChannelDefinitionList(citpath);

            var channelData = cithelper.GetAllMileStone(citpath);

            var type = cithelper.GetDataType(citpath);
            var DataVersion = cithelper.GetDataVersion(citpath);

            var date = cithelper.GetDate(citpath);
        }
    }
}
