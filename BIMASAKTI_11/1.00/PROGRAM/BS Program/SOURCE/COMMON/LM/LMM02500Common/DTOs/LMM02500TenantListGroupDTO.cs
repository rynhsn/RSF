using System;

namespace LMM02500Common.DTOs
{
    public class LMM02500TenantListGroupDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_CATEGORY_NAME { get; set; }
        public string CTENANT_TYPE_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CPHONE1 { get; set; }
        public string CEMAIL { get; set; }
        public string CTENANT_GROUP_ID { get; set; }
        public string CTENANT_GROUP_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        
        public string CUSER_ID { get; set; }
    }
}