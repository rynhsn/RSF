using System;

namespace GLM00400COMMON
{
    public class GLM00400PrintAccountDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CALLOC_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }

        // result
        public string CALLOC_NO { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CACCOUNT { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public string CBSIS_NAME { get; set; }
    }


}
