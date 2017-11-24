using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace IntegratedDisplay
{
    public class MyLogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(string errName, Exception ex)
        {
            logger.Error(errName + ":" + ex.Message + "堆栈：" + ex.StackTrace);
        }
    }
}
