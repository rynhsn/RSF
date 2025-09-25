using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300DetailLeftDTO
    {
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CUNIT_ID { get; set; } = string.Empty;
        public string CUNIT_NAME { get; set; } = string.Empty;
        public string CUTILITY_TYPE { get; set; } = string.Empty;
        public string CMETER_NO { get; set; } = string.Empty;
        public string CSEQ_NO { get; set; } = string.Empty;
        public decimal CF { get; set; } 
        public string CLAST_INVOICE_PRD { get; set; } = string.Empty;
        public string CLAST_INVOICE_NO { get; set; } = string.Empty;
        public decimal NAMOUNT { get; set; } = 0;
        public decimal NREMAINING { get; set; } = 0 ;
    }
}
