using System;

namespace FAM00200Common.DTOs
{
    public class FAM00200DTO
    {
        public string CACTION { get; set; }

        public string CTAX_TYPE_ID { get; set; }
        public string CTAX_TYPE_NAME { get; set; }
        public string CTAX_TYPE_ID_NAME { get; set; }
        public string CTAX_TYPE_TYPE { get; set; }
        public string CTAX_TYPE_TYPE_NAME { get; set; }
        public int IUSEFUL_LIFE { get; set; }
        public string CTAX_TYPE_DESC { get; set; }
        public bool LACTIVE { get; set; }
        public string CREC_ID { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
