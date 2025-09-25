using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300ChequeInfoDTO : R_APIResultBaseDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CREC_ID { get; set; }
        public string? CLANGUAGE_ID { get; set; }

        public string? CFRTO_BANK_CODE { get; set; }
        public string? CCHEQUE_NO { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CTRANS_DESC { get; set; }

        public string? CBANK_IN_BY { get; set; }

        public string? CUNDO_CLEAR_BY { get; set; }
        public string? CCLEAR_BY { get; set; }
        public string? CREASON { get; set; }

        //DATE FROM BACK
        public string? CCHEQUE_DATE { get; set; }
        public string? CBANK_IN_DATE { get; set; }
        public string? CDUE_DATE { get; set; }
        public string? CHOLD_DATE { get; set; }
        public string? CUNDO_CLEAR_DATE { get; set; }
        public string? CCLEAR_DATE { get; set; }
    }
}
