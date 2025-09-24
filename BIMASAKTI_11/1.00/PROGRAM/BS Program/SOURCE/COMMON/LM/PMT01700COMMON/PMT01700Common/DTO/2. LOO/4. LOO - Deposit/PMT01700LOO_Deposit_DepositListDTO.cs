using System;

namespace PMT01700COMMON.DTO._2._LOO._4._LOO___Deposit
{
    public class PMT01700LOO_Deposit_DepositListDTO
    {
        //Update Version For Parameter
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CUSER_ID { get; set; }

        ///DATA
        public string? CDEPOSIT_ID { get; set; }
        public string? CDEPOSIT_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public decimal NDEPOSIT_AMT { get; set; }
        public bool LPAID { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        //CR 25/10/2024
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
    }
}
