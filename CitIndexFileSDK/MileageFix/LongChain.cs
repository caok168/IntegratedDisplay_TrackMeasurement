using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CitIndexFileSDK.MileageFix
{
    /// <summary>
    /// 长短链
    /// </summary>
    public class LongChain
    {
        private float _extraLength;

        public string LineCode { get; set; }
        public int Dir { get; set; }
        public float Km { get; set; }
        
        public float ExtraLength
        {
            get
            {
                return _extraLength;
            }

            set
            {
                if (value > 1000)
                {
                    _extraLength = value - 1000;
                }
                else if (value < 1000)
                {
                    _extraLength = - value;
                }
                else
                {
                    _extraLength = value;
                }
            }
        }


        public bool IsLongChain()
        {
            return _extraLength > 0;
        }

    }
}
