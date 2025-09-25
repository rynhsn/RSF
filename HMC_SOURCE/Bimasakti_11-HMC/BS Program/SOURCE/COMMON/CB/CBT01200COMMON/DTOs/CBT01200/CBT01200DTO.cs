using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200DTO
    {
        private DateTime _REF_DATE;
        public string CREC_ID { get; set; }
        public int INO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE
                 {
                     get => _REF_DATE;
                     set => _REF_DATE = string.IsNullOrEmpty(CREF_DATE) ? DateTime.Now : DateTime.ParseExact(CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                 }
        public string CDOC_NO { get; set; }
        public DateTime DDOC_DATE { get; set; }

        public string? CDOC_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public bool LALLOW_APPROVE { get; set; }
        
        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        
        public string CCB_CODE { get; set; } = "";
        public string CCB_NAME { get; set; }
        public string CCB_ACCOUNT_NO { get; set; }
        public string CCB_ACCOUNT_NAME { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }

        public string CCREATE_BY { get; set; }
        public string CCREATE_DATE { get; set; }

        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CUPDATE_DATE { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
    public class CBT01200ParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPERIOD { get; set; }
        public string CSTATUS { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
