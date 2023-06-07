using System;

namespace GSM05000Common.DTOs
{
    public class GSM05000NumberingGridDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTRANSACTION_CODE { get; set; }
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string? CDEPT_NAME { get; set; } = "";
        public int ISTART_NUMBER { get; set; }
        public int ILAST_NUMBER { get; set; } = 1;
        public string CCREATE_BY { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CACTION { get; set; }
        public string CUSER_ID { get; set; }
        public string CPERIOD { get; set; } 
    }
}