namespace PMT01700CommonReport
{
    public class PMTDataChargesReportDTO
    {
        public string? CCHARGES_SEQ_NO { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CITEM_NAME { get; set; }
        public int IQTY { get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
    }
}
