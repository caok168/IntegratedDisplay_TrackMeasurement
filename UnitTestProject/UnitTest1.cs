using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CitIndexFileSDK.IntelligentMileageFix;
using CitIndexFileSDK;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
          
            IntelligentMilestoneFix fix = new IntelligentMilestoneFix();
            fix.FixParams.Add(new FixParam() { ChannelName = "Gage", ThreShold = 0.8f,Priority=1 });
            fix.FixParams.Add(new FixParam() { ChannelName = "Superelevation", ThreShold = 0.8f,Priority=0 });
            fix.FixParams.Add(new FixParam() { ChannelName = "L_Prof_SC", ThreShold = 0.8f,Priority=2 });
            fix.FixParams.Add(new FixParam() { ChannelName = "R_Prof_SC", ThreShold = 0.8f, Priority = 3 });
            fix.InitFixData(@"D:\cit\gjhx-beijing-shanghai-22012015-052640-1-(0-406).cit");
            fix.CheckData(@"D:\cit\gjhx-beijing-shanghai-27062014-221903.cit");
            fix.SaveToFile(@"D:\cit\gjhx-beijing-shanghai-27062014-221903.cit");
        }
    }
}
