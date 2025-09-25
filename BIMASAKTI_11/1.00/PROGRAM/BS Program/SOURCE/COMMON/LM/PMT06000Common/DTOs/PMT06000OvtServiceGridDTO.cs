using System;

namespace PMT06000Common.DTOs
{
    public class PMT06000OvtServiceGridDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CPARENT_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CSEQ_NO { get; set; } = "";
        public string CSERVICE_ID { get; set; } = "";
        public string CSERVICE_NAME { get; set; } = "";
        public string CSERVICE_DISPLAY => CSERVICE_ID + " - " + CSERVICE_NAME;

        public string CDATE_IN { get; set; } = "";
        public string CTIME_IN { get; set; } = "";
        public string CDATE_OUT { get; set; } = "";
        public string CTIME_OUT { get; set; } = "";
        public DateTime? DDATE_IN { get; set; }
        public DateTime? DDATE_OUT { get; set; }
        public DateTime? DTIME_IN { get; set; }
        public DateTime? DTIME_OUT { get; set; }
        public string CSERVICE_DESCR { get; set; } = "";
    }
}