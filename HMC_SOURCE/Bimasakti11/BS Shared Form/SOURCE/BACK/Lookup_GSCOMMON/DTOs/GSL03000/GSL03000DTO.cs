using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL03000DTO
    {
        public string CPRODUCT_ID { get; set; }
        public string CPRODUCT_NAME { get; set; } 
        public string CALIAS_ID { get; set; } 
        public string CALIAS_NAME { get; set; } 
        public bool LTAXABLE { get; set; } 
        public string CCATEGORY_ID { get; set; } 
        public string CCATEGORY_NAME { get; set; } 
        public string CUNIT1 { get; set; }
        public string CUNIT2 { get; set; }
        public string CUNIT3 { get; set; }
        public int IPURCHASE_UNIT { get; set; }
        public decimal NCONV_FACTOR1 { get; set; }
        public decimal NCONV_FACTOR2 { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public string COTHER_TAX_NAME { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }
        public string CREC_ID { get; set; }

        //CR23
        public bool LACTIVE { get; set; }
        public bool LIN { get; set; }
        public bool LOUT { get; set; }
        public bool LBUY { get; set; }
        public bool LSELL { get; set; }
        public string CTAX_CHARGES_TYPE { get; set; }
        public string CTAX_CHARGES_ID { get; set; }
        public string CTAX_CHARGES_NAME { get; set; }
        public decimal NUNIT1_COST { get; set; }
        public string CLAST_RECALC_DATE { get; set; }
        public DateTime? DLAST_RECALC_DATE { get; set; }
        public string CLAST_RECALC_BY { get; set; }
    }
}
