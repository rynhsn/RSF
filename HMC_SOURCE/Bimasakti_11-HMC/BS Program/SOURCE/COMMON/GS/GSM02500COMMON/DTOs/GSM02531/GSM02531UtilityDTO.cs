using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02531UtilityDTO
    {
        public string CSEQUENCE { get; set; }
        public string CMETER_NO { get; set; }
        public string CALIAS_METER_NO { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public decimal NCAPACITY { get; set; }
        public int IMETER_MAX_COUNT { get; set; }
        public bool LACTIVE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
