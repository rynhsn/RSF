namespace PMT01500Common.Utilities
{
    public class PMT01500GetHeaderParameterDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; } 
        public string? CTRANS_CODE { get; set; } = "";
        //Updated 18 Apr 2024
        public string? CBUILDING_ID { get; set; } = "";
        public string CCHARGE_MODE { get; set; } = "";
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        //Updated 19 Apr 2024 
        public string? CUNIT_NAME { get; set; } // For Parameter Charges Info and Deposit
        //IT'S FOR ChargesInfo
        public string? CCURRENCY_CODE { get; set; } = "";
        public string? CREF_DATE { get; set; } = "";
        //Updated 2 Mei 2024 : For Agreement Object
        public PMT01500FrontParameterForAgreementDTO? DataAgreement { get; set; } = new PMT01500FrontParameterForAgreementDTO();

    }
}