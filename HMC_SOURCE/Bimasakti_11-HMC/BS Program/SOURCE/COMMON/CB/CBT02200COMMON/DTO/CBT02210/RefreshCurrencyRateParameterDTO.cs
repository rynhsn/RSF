using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class RefreshCurrencyRateParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CRATETYPE_CODE { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
    }
}
