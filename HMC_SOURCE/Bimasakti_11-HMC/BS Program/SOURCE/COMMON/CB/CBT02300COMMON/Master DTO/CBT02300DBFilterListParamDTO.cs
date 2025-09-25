using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300DBFilterListParamDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CLANGUAGE_ID { get; set; }

        public string? CTRANS_TYPE { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string? CDEPT_NAME { get; set; } = "";
        public string? CCB_CODE { get; set; } = "";
        public string? CCB_NAME { get; set; } 
        public string? CCB_ACCOUNT_NO { get; set; } = "";
        public string? CCB_ACCOUNT_NAME { get; set; } 

        public DateTime? CDATE_FRONT { get; set; } = DateTime.Now;
        public string? CDATE { get; set; }
    }
}
