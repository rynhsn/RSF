using System;

namespace PMT05500COMMON.DTO
{
    public class LMT05500DepositInfoFrontDTO : LMT05500Header
    {
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }

        public string CSEQ_NO { get; set; } = "";
        public string? CINVOICE_NO { get; set; }
        public bool LCONTRACTOR { get; set; }
        public string? CUNIT_TYPE { get; set; }

        public string? CCHARGE_MODE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }

        public string CCONTRACTOR_ID { get; set; } = "";
        public string CCONTRACTOR_NAME { get; set; } = "";
        public string? CDEPOSIT_ID { get; set; }
        public string? CDEPOSIT_NAME { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; } = DateTime.Now;
        public string? CLOCAL_CURRENCY { get; set; }
        public decimal NDEPOSIT_AMT { get; set; }
        public decimal NBASE_RATE_AMOUNT { get; set; }
        public decimal NCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public bool LPAYMENT { get; set; }
        public string CDESCRIPTION { get; set; } = "";

    }
}