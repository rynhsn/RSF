using System;

namespace PMT01300COMMON
{
    public class PMT01300PropertyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CUSER_ID { get; set; }
        public string CCURRENCY { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
