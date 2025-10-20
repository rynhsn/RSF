using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.DTO_s
{
    public class PMR03400SPResultDTO
    {
        public string CCOMPANY_ID {  get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CTENANT_ID { get; set;} = string.Empty;
        public string CTENANT_NAME { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_DESC {  get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CTRANS_NAME { get; set; } = string.Empty;
        public string CCURRENCY { get; set; } = string.Empty;
        public decimal NBEG_BALANCE { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public string CFROM_PERIOD { get; set; } = string.Empty;
        public string CTO_PERIOD { get; set; } = string.Empty;
        public string CFILTER_VALUE { get; set; } = string.Empty;
    }   
}       
        