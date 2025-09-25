using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200GSPeriodDTInfoDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }

    public class PMT04200ParamGSPeriodDTInfoDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
    }
}
