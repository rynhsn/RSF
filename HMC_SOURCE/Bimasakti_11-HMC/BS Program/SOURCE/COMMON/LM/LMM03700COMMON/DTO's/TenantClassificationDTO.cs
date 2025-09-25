using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03700Common.DTO_s
{
    public class TenantClassificationDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTENANT_CLASSIFICATION_GROUP_ID { get; set; } = "";
        public string CTENANT_CLASSIFICATION_ID { get; set; } = "";
        public string CTENANT_CLASSIFICATION_NAME { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public string CUSER_ID { get; set; } = "";

    }
}
