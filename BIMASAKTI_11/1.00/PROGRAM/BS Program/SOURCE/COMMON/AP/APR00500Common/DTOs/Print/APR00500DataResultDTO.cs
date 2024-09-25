namespace APR00500Common.DTOs.Print
{
    public class APR00500DataResultDTO
    {
        public string CDEPARTMENT_CODE { get; set; }
        public string CREFERENCE_NO { get; set; }
        public string CREFERENCE_DATE { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CINVOICE_PERIOD { get; set; }
        public string CDUE_DATE { get; set; }
        public string CCURRENCY { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NTAX { get; set; }
        public decimal NADDITION { get; set; }
        public decimal NDEDUCTION { get; set; }
        public decimal NINVOICE_AMOUNT { get; set; }
        public decimal NREMAINING { get; set; }
        public int IDAYS_LATE { get; set; }
        public string CUNIT { get; set; }
    }
}