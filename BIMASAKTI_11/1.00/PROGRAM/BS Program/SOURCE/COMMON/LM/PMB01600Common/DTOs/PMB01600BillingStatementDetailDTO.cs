namespace PMB01600Common.DTOs
{
    public class PMB01600BillingStatementDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CPERIOD { get; set; }
        public string CSEQ_NO { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_DESC { get; set; }
        public string CINVOICE_ID { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
    }
}
