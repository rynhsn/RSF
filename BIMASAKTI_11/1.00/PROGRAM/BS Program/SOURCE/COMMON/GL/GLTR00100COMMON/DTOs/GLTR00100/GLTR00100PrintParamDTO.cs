namespace GLTR00100COMMON
{
    public class GLTR00100PrintParamDTO
    {
        public string CREPORT_CULTURE { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }

        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; } = true;
    }
}
