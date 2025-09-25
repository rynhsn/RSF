using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300BankInChequeFrontDTO
    {
        public bool LSELECTED { get; set; }
        public string? CREC_ID { get; set; }

        //CONVERT
        public DateTime? DCHEQUE_DATE { get; set; }
        public DateTime? DDUE_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }

        public string? CCHEQUE_NO { get; set; }
        public string? CREF_NO { get; set; }
        public string? TRANSACTION_NAME { get; set; }
        public string? CTRANSACTION_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CSTATUS_NAME { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
