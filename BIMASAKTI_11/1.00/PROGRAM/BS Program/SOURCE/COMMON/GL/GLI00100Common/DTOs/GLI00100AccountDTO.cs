using R_APICommonDTO;

namespace GLI00100Common.DTOs
{
    public class GLI00100AccountDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CBSIS { get; set; }
        public string CBSIS_SHORT_NAME { get; set; }
        public string CBSIS_LONG_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CDBCR_SHORT_NAME { get; set; }
        public string CDBCR_LONG_NAME { get; set; }
    }

    public class GLI00100AccountParameterDTO
    {
        public string CGLACCOUNT_NO { get; set; }
    }
}