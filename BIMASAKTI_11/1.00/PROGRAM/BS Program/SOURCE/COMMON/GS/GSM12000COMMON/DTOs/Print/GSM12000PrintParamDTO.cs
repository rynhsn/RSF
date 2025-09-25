using R_CommonFrontBackAPI.Log;

namespace GSM12000COMMON
{
    public class GSM12000PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CMESSAGE_TYPE { get; set; }
        public string CMESSAGE_NO_FROM { get; set; }
        public string CMESSAGE_NO_TO { get; set; }
        public bool LPRINT_INACTIVE { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public string TMESSAGE_DESCR_RTF { get; set; }
        public string  TADDITIONAL_DESCR_RTF { get; set; }
        public string CREPORT_FILETYPE { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public bool LIS_PRINT { get; set; } = true;
    }
}
