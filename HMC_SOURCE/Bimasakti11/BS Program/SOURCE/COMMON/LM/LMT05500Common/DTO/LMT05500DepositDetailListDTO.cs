using System;

namespace PMT05500COMMON.DTO
{
    public class LMT05500DepositDetailListDTO : LMT05500Header
    {
        public string? CSEQ_NO { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CDEPOSIT_TYPE { get; set; }
        public decimal NDEPOSIT_AMOUNT { get; set; }
        public string? CTRX_CODE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string CTRANS_HD_REC_ID { get; set; } = "";
    }
}