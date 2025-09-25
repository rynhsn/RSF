using R_APICommonDTO;

namespace GLM00500Common.DTOs
{
    public class GLM00500GSMCompanyDTO : R_APIResultBaseDTO
    {
        public string CCOGS_METHOD { get; set; }
        public bool LENABLE_CENTER_IS { get; set; }
        public bool LENABLE_CENTER_BS { get; set; }
        public bool LPRIMARY_ACCOUNT { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
    }
}