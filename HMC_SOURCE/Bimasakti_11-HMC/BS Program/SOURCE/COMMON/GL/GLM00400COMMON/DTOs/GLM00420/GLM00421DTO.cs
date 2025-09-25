using R_APICommonDTO;

namespace GLM00400COMMON
{
    public class GLM00421DTO : R_APIResultBaseDTO
    {
        // param
        public string CREC_ID_ALLOCATION_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }

        // result
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string CCENTER_LIST { get; set; }
    }


}
