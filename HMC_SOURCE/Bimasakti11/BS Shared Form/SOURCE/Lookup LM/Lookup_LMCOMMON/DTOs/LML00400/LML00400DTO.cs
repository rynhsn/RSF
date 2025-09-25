using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00400DTO
    {
        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";

        //CR09
        public string CPROPERTY_ID { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_TYPE_DESCR { get; set; } = "";
        public bool LTAXABLE { get; set; }
        public bool LACTIVE { get; set; }
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_ID { get; set; } = "";
        public string CWITHHOLDING_TAX_NAME { get; set; } = "";
        public string COTHER_TAX_ID { get; set; } = "";
        public string COTHER_TAX_NAME { get; set; } = "";
        public decimal NOTHER_TAX_PCT { get; set; }
        public string CUOM { get; set; } = "";
        public bool LTAX_EXEMPTION { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; } = "";
        public decimal NTAX_EXEMPTION_PCT { get; set; } 


    }
}
