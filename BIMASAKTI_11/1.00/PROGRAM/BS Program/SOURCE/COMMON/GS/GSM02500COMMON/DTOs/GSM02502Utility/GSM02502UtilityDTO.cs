using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Utility
{
    public class GSM02502UtilityDTO
    {
        public string CUTILITY_TYPE { get; set; } = "";
        public string CUTILITY_TYPE_DESCR { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
