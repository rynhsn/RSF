using System;

namespace PMB00300Common.DTOs
{
    public class PMB00300RecalcChargesDTO
    {
        public string CSEQ_NO { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_TYPE_NAME { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public int IINTERVAL { get; set; }
        public string CPERIOD_MODE { get; set; } = "";
        public string CPERIOD_MODE_DESCRIPTION { get; set; } = "";
        public decimal NBASE_AMOUNT { get; set; }
        public decimal NMONTHLY_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}