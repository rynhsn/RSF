using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00600DTO
    {
        public string? CTENANT_ID { get; set; } = "";
        public string? CTENANT_NAME { get; set; } = "";
        public string? CTENANT_GROUP_NAME { get; set; } = "";
        public string? CTENANT_CATEGORY_NAME { get; set; } = "";
        public string? CTENANT_TYPE_NAME { get; set; } = "";
        public string? CUNIT_NAME { get; set; } = "";
        public string? CCUSTOMER_TYPE { get; set; } = "";
        public string? CCUSTOMER_TYPE_NAME { get; set; } = "";
        public string? CPHONE1 { get; set; }= "";
        public string? CEMAIL { get; set; }= "";
        public string? CPAYMENT_TERM_CODE { get; set; } = "";
        public string? CCURRENCY_CODE { get; set; } = "";
        public string? CTAX_TYPE { get; set; } = "";
        public bool LTAXABLE { get; set; } 
        public string? CTAX_ID { get; set; } = "";
        public string? CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; }
    }
}
