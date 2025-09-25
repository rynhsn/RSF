using R_APICommonDTO;

namespace GLM00400COMMON
{
    public class GLM00431DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CREC_ID_ALLOCATION_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CBSIS_SHORT_NAME { get; set; }
        public string CDBCR { get; set; }
        public string CACCOUNT_LIST { get; set; }

    }


}
