namespace PMM01000COMMON
{
    public class PMM01000PrintParamDTO
    {
        public string CREPORT_CULTURE { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGE_TYPE_FROM { get; set; }
        public string CCHARGE_TYPE_TO { get; set; }
        public string CSHORT_BY { get; set; } = "01";
        public bool LPRINT_INACTIVE { get; set; }
        public bool LPRINT_DETAIL_ACC { get; set; }
        public string CUSER_ID { get; set; }

        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; } = true;
    }
}
