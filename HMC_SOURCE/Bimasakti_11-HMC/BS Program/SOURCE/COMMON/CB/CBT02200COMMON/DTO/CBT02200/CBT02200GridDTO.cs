using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class CBT02200GridDTO
    {
        public string CREC_ID { get; set; } = "";
        public bool LALLOW_APPROVE { get; set; }
        public string CREF_PRD { get; set; } = "";
        public string CSTATUS { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public string CCHEQUE_NO { get; set; } = "";
        public string CCHEQUE_DATE { get; set; } = "";
        public DateTime DCHEQUE_DATE { get; set; }
        public string CDUE_DATE { get; set; } = "";
        public DateTime DDUE_DATE { get; set; }
        public string CTRANS_DESC { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; }
        public string CSTATUS_NAME { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}
