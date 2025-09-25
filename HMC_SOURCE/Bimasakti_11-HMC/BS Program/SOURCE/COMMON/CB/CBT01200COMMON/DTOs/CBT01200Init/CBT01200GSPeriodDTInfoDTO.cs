using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200GSPeriodDTInfoDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }

    public class CBT01200ParamGSPeriodDTInfoDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
    }
}
