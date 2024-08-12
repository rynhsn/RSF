using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00900FrontDTO
    {
        public string? CPROPERTY_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public decimal NTOTAL_REMAINING { get; set; }
        public string? CREC_ID { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CREF_PRD { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_NAME { get; set; }
        public string? CTRANS_DESC { get; set; }
        public string? CDUE_DATE { get; set; }
        public DateTime? DDUE_DATE { get; set; }
        public decimal NLTOTAL_REMAINING { get; set; }
        public decimal NBTOTAL_REMAINING { get; set; }
        public decimal NAR_REMAINING { get; set; }
        public decimal NLAR_REMAINING { get; set; }
        public decimal NBAR_REMAINING { get; set; }
        public decimal NTAX_REMAINING { get; set; }
        public decimal NLTAX_REMAINING { get; set; }
        public decimal NBTAX_REMAINING { get; set; }
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }
        public string? CUSTOMER { get; set; }
    }
}
