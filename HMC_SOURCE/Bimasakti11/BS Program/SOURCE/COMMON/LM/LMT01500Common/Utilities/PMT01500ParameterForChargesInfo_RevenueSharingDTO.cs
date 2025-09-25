namespace PMT01500Common.Utilities
{
    public class PMT01500ParameterForChargesInfo_RevenueSharingDTO
    {
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CREF_NO { get; set; } = "";
        public string? CCHARGE_SEQ_NO { get; set; } = "";
        //Updated 26 Apr 2024 : For Invoicing and Total Area
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public string? CINVOICE_PERIOD_DESCRIPTION { get; set; } = "";

        //public string? CCHARGE_MODE { get; set; } = "";
        //public string? CBUILDING_ID { get; set; } = "";
        //public string? CFLOOR_ID { get; set; } = "";
        //public string? CUNIT_ID { get; set; } = "";
    }
}
