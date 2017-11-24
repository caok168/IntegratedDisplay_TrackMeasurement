using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IntegratedDisplay.Models
{
    public class ChartConfig
    {
        public string ChartTitle { get; set; }
        public Font ChartTitleFont { get; set; }

        public string AxesYTitle { get; set; }
        public Font AxesYFont { get; set; }
        //public int AxesYMax { get; set; }
        public int AxesYMin { get; set; }
        public int AxesYStep { get; set; }

        public string AxesXTitle { get; set; }
        public Font AxesXFont { get; set; }
        //public int AxesXMax { get; set; }
        public int AxesXMin { get; set; }
        public int AxesXStep { get; set; }
    }
}
