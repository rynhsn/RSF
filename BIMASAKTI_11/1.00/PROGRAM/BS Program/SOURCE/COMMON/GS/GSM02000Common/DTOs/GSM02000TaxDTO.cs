using System;

namespace GSM02000Common.DTOs
{
    public class GSM02000TaxDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_DATE { get; set; }
        public DateTime DTAX_DATE { get; set; }
        public decimal NTAX_PERCENTAGE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        
        
    }
}