using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetPeriodInfoResultDTO
    {
        public string CCYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public string CEND_DATE { get; set; }    = string.Empty;
    }
}
