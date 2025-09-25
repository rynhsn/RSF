using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL03100DTO
    {
        public string CEXPENDITURE_ID { get; set; }
        public string CEXPENDITURE_NAME { get; set; } 
        public bool LTAXABLE { get; set; } 
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_ID { get; set; } 
        public string CWITHHOLDING_TAX_NAME { get; set; } 
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string COTHER_TAX_ID { get; set; }
        public string COTHER_TAX_NAME { get; set; }
        public decimal NOTHER_TAX_PCT { get; set; }
        public string CUNIT { get; set; }
    }
}
