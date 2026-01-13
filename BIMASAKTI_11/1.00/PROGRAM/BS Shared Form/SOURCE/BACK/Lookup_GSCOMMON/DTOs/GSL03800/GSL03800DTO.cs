using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL03800DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CLOCATION_ID { get; set; }
        public string CLOCATION_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CREC_ID { get; set; }
        public string CDESCRIPTION { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
