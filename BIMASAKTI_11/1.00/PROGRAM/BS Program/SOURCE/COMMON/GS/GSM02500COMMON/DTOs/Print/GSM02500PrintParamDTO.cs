using R_CommonFrontBackAPI.Log;

namespace GSM02500COMMON.DTOs
{
    public class GSM02500PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }

        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; } = true;
    }
}
