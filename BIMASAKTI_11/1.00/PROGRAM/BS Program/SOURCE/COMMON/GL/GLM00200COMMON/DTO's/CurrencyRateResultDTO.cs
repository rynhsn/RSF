using GLM00200COMMON.DTO_s.Helper_DTO_s;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class CurrencyRateResultDTO : GeneralParamDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATE_DATE { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }
}
