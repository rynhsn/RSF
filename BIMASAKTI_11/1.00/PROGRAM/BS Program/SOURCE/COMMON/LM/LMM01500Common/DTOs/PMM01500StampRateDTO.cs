using System;

namespace PMM01500COMMON
{
    public class PMM01500StampRateDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CSTAMP_CODE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal NMIN_AMT { get; set; }
        public decimal NSTAMP_AMT { get; set; }
        public string CREC_ID { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
