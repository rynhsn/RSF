using System;
using System.Collections.Generic;
using System.Text;

namespace FAF00100COMMON.DTOs
{
    public class FAF00100GetAssetAllocResultDTO
    {
        public string CEXPENSE_DEPT_CODE  {get; set;} = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }   
    }
}
