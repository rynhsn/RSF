using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetPeriodRangeParamDTO
    {
        public string  CCOMPANY_ID { get; set; } = string.Empty;
        public string  CCYEAR { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
    }
}
