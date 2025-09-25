using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01100
{
    public class LML01100DTO
    {
        public string CTC_CODE { get; set; } = "";
        public string CTC_DESCRIPTION { get; set; } = "";
        public bool LHAVE_MESSAGE { get; set; }
        public bool LACTIVE { get; set; }
        public string CCOMPANY_ID { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
