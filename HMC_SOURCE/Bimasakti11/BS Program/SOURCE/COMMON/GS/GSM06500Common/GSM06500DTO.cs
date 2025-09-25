using System;

namespace GSM06500Common
{
    public class GSM06500DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CPAY_TERM_CODE { get; set; }
        public string CPAY_TERM_NAME { get; set; }
        public int IPAY_TERM_DAYS { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;

        public string CACTION { get; set; }

    }
}
