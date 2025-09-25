namespace PMM01000COMMON.Print
{
    public class PMM01050PrintParamDTO
    {
        public string CREPORT_CULTURE { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_DATE { get; set; }
        public string CUSER_ID { get; set; }

        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; } = true;
    }
}
