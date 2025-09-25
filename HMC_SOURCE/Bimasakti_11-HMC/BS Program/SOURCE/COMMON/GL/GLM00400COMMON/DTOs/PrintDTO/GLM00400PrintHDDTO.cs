using System;

namespace GLM00400COMMON
{
    public class GLM00400PrintHDDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CFROM_ALLOC_NO { get; set; }
        public string CTO_ALLOC_NO { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CYEAR { get; set; }

        // result
        public string CALLOC_NO { get; set; }
        public string CALLOC_NAME { get; set; }
        public string CDEPARTMENT { get; set; }
        public string CSOURCE_CENTER_CODE { get; set; }
        public string CSOURCE_CENTER { get; set; }
        public string CALLOC_ID { get; set; }

        public byte[] CLOGO { get; set; }
    }


}
