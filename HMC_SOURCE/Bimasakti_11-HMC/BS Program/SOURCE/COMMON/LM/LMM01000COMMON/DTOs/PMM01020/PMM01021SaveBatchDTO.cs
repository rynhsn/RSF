namespace PMM01000COMMON
{
    public class PMM01021SaveBatchDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }

        // result
        public int IUP_TO_USAGE { get; set; }
        public string CUSAGE_DESC { get; set; }
        public decimal NBUY_USAGE_CHARGE { get; set; }
        public decimal NUSAGE_CHARGE { get; set; }
    }
}
