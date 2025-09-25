using System;

namespace GLM00400COMMON
{
    public class GLM00410DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CREC_ID_ALLOCATION_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // result
        public string CALLOC_NO { get; set; }
        public bool LACTIVE { get; set; } = true;
        public string CALLOC_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CDEPARTMENT_NAME { get; set; }
        public string CSOURCE_CENTER_CODE { get; set; }
        public string CSOURCE_CENTER_NAME { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public bool LALLOW_EDIT { get; set; }
        public bool ON_CRUD_MODE { get; set; }
    }


}
