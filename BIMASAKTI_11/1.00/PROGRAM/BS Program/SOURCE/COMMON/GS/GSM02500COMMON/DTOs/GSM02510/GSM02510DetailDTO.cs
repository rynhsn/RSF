using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02510
{
    public class GSM02510DetailDTO
    {
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public decimal NTOTAL_FLOOR { get; set; } = 0;
        public decimal NTOTAL_ACTIVE_FLOOR { get; set; } = 0;
        public decimal NTOTAL_UNIT { get; set; } = 0;
        public decimal NTOTAL_ACTIVE_UNIT { get; set; } = 0;
        public bool LACTIVE { get; set; } = true;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }  
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
