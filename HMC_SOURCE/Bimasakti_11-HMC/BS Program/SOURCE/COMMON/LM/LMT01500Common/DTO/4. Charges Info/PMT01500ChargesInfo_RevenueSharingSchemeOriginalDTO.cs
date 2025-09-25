namespace PMT01500Common.DTO._4._Charges_Info
{
    public class PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        //Updated 26 Apr 2024 : add CCHARGE_SEQ_NO
        public string? CCHARGE_SEQ_NO { get; set; }
        public string CSEQ_NO { get; set; } = "";
        public string? CUSER_ID { get; set; }
        public decimal NMONTHLY_REVENUE_FROM { get; set; }  
        public decimal NMONTHLY_REVENUE_TO { get; set; }  
        public decimal NSHARE_PCT { get; set; }  
    }
}
