namespace PMB01600Common.DTOs
{
    public class PMB01600BillingStatementHeaderDTO
    {
        public bool LSELECTED { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CADDRESS { get; set; }
        public string CBILLING_EMAIL { get; set; }
        public string CPERIOD { get; set; }
        public string CDUE_DATE { get; set; }
        public string CBILL_DATE { get; set; }
        public string CAGREEMENT_ID { get; set; }
        public string CSTORAGE_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTOTAL_AMT { get; set; }
        public bool LDISTRIBUTE { get; set; }
    }
}
