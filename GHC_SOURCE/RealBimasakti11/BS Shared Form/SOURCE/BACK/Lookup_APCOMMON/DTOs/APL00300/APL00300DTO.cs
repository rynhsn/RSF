using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_APCOMMON.DTOs.APL00300
{
    public class APL00300DTO
    {
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; }
        public string CALIAS_ID { get; set; }
        public string CALIAS_NAME { get; set; }
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }
        
        public string CCATEGORY_NAME { get; set; }
        public bool LTAXABLE { get; set; }
        public string CCATEGORY_ID { get; set; } = "";
        public string COTHER_TAX_NAME { get; set; }
        public string COTHER_TAX_PCT { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public int IPURCHASE_UNIT { get; set; }
        public decimal NCONV_FACTOR1 { get; set; }
        public decimal NCONV_FACTOR2 { get; set; }
        public string CREC_ID { get; set; }

        public string Code { get; set; }
        public string Desc { get; set; }
        public string RadioButton { get; set; } = "A";
    }
}