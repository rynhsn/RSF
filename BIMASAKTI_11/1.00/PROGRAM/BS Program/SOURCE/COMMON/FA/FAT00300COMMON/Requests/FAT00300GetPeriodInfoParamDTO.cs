using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public  class FAT00300GetPeriodInfoParamDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CYEAR { get; set; } = string.Empty;
        public string CPERIOD_NO { get; set; } = string.Empty;
    }
}
