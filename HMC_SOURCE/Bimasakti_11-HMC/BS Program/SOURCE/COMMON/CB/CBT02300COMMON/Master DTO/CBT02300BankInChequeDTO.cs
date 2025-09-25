using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300BankInChequeDTO
    {
        public bool LSELECTED { get; set; }
        public string? CREC_ID { get; set; }
        
        public string? CCHEQUE_NO { get; set; }
        public string? CCHEQUE_DATE { get; set; }
        public string? CDUE_DATE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? TRANSACTION_NAME { get; set; }
        public string? CTRANSACTION_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CSTATUS_NAME { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }

        public string? CHOLD_BY { get; set; }
        public DateTime? CHOLD_DATE { get; set; }
    }
}
