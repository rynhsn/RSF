using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03700Common.DTO_s
{
    public class TenantGridPopupDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_CLASSIFICATION_GROUP_ID { get; set; }
        public string CTENANT_CLASSIFICATION_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_CATEGORY_NAME { get; set; }
        public string CTENANT_TYPE_NAME { get; set; }
        public string CPHONE { get; set; }
        public string CEMAIL { get; set; }
        public string CUSER_ID { get; set; }
        public bool LCHECKED { get; set; }
    }
}
