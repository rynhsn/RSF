using System;

namespace GSM12000COMMON
{
    public class GSM12000ResultPrintSPDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CMESSAGE_TYPE { get; set; }
        public string CMESSAGE_TYPE_DESCR { get; set; }
        public string CMESSAGE_NO { get; set; }
        public string TMESSAGE_DESCRIPTION { get; set; }
        public string CADDITIONAL_DESCRIPTION { get; set; }
        public string TMESSAGE_DESCR_RTF { get; set; }
        public string  TADDITIONAL_DESCR_RTF { get; set; }
       public string  CMESSAGE_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CACTIVE_BY { get; set; }
        public DateTime? DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime? DINACTIVE_DATE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
