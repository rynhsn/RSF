using BaseAOC_BS11Common.DTO.Utilities.BaseDTO;
using System;

namespace PMT01900Common.DTO.CRUDBase
{
    public class PMT01900Deposit_DepositDetailDTO : BaseAOCDateTimeDTO
    {
        public string? CBUILDING_ID { get; set; }
        //For Db Parameter
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUSER_ID { get; set; }
        public bool LPAID { get; set; }
        //Real DTO : For Front
        public string? CSEQ_NO { get; set; }
        public string? CDEPOSIT_ID { get; set; }
        public string? CDEPOSIT_NAME { get; set; }
        public string? CDEPOSIT_DATE { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public decimal NDEPOSIT_AMT { get; set; }
        public string? CDESCRIPTION { get; set; } = "";
    }
}
