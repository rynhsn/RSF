using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class CBT02210DTO
    {
        public string CREC_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_PRD { get; set; } = "";
        public bool LALLOW_APPROVE { get; set; }
        public string CINPUT_TYPE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CCHEQUE_NO { get; set; } = "";
        public string CCB_CODE { get; set; } = "";
        public string CCB_NAME { get; set; } = "";
        public string CCB_ACCOUNT_NO { get; set; } = "";
        public string CCB_ACCOUNT_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; } = 0;
        public decimal NLBASE_RATE { get; set; } = 1;
        public decimal NLCURRENCY_RATE { get; set; } = 1;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NBBASE_RATE { get; set; } = 1;
        public decimal NBCURRENCY_RATE { get; set; } = 1;
        public string CBASE_CURRENCY_CODE { get; set; } = "";
        public decimal NDEBIT_AMOUNT { get; set; } = 0;
        public decimal NCREDIT_AMOUNT { get; set; } = 0;
        public string CCHEQUE_DATE { get; set; } = "";
        public DateTime? DCHEQUE_DATE { get; set; }
        public string CDUE_DATE { get; set; } = "";
        public DateTime? DDUE_DATE { get; set; }
        public string CCLEAR_DATE { get; set; } = "";
        public DateTime? DCLEAR_DATE { get; set; }
        public string CTRANS_DESC { get; set; } = "";
        public string CSTATUS_NAME { get; set; } = "";
        public string CSTATUS { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
