using System;

namespace PMT01500Common.DTO._6._Deposit
{
    public class PMT01500DepositListDTO
    {
        public string? CSEQ_NO { get; set; }
        public string? CDEPOSIT_ID { get; set; }
        public string? CDEPOSIT_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; }
        public Decimal NDEPOSIT_AMT { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
