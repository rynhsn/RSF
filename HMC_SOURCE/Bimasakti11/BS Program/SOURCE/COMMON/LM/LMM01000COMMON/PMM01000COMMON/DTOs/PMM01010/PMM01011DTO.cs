namespace PMM01000COMMON
{
    public class PMM01011DTO
    {
        // param
        public int SEQ_NO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_DATE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public int IUP_TO_USAGE { get; set; }
        public string CUSAGE_DESC { get; set; }
        public decimal NBUY_LWBP_CHARGE { get; set; }
        public decimal NBUY_WBP_CHARGE { get; set; }
        public decimal NLWBP_CHARGE { get; set; }
        public decimal NWBP_CHARGE { get; set; }
    }
}
