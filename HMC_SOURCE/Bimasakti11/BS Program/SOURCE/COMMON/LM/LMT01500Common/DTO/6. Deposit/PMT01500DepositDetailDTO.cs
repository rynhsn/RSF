using System;

namespace PMT01500Common.DTO._6._Deposit
{
    public class PMT01500DepositDetailDTO
    {
        //External Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //Updated 18 Apri 2024
        public string? CCHARGE_MODE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        //Real DTOs
        public string? CSEQ_NO { get; set; } = "";
        public bool LCONTRACTOR { get; set; }
        public string? CCONTRACTOR_ID { get; set; }= "";
        public string? CCONTRACTOR_NAME { get; set; }
        public string? CDEPOSIT_ID { get; set; }
        public string? CDEPOSIT_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public Decimal NDEPOSIT_AMT { get; set; }
        public string? CDESCRIPTION { get; set; } = "";
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
