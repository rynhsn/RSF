using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210
{
    public class GetCurrencyOrTaxRateParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CRATETYPE_CODE { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
    }
}
