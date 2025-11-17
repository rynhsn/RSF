using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs
{
    public class APR00600GetPeriodDtListDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CPERIOD_NAME { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }
}
