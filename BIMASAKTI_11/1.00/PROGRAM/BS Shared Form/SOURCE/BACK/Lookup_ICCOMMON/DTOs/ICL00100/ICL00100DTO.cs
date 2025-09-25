using R_APICommonDTO;

namespace Lookup_ICCOMMON.DTOs.ICL00100
{
    public class ICL00100DTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREQUEST_CODE { get; set; }
        public string CREQUEST_NAME { get; set; }
        public string CALLOC_ID { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CWAREHOUSE_CODE { get; set; }
        public string CWAREHOUSE_NAME { get; set; }
    }
}