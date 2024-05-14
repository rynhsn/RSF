using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02531UtilityDetailDTO
    {
        public string CSEQUENCE { get; set; } = "";
        public string CMETER_NO { get; set; } = "";
        public string CALIAS_METER_NO { get; set; } = "";
        public decimal NCALCULATION_FACTOR { get; set; } = 0;
        public decimal NCAPACITY { get; set; } = 0;
        public int IMETER_MAX_COUNT { get; set; } = 0;
        public bool LACTIVE { get; set; } = true;

        public void ResetToDefaultValues()
        { 
            CSEQUENCE = "";
            CMETER_NO = "";
            CALIAS_METER_NO = "";
            NCALCULATION_FACTOR = 0;
            NCAPACITY = 0;
            IMETER_MAX_COUNT = 0; 
            LACTIVE = true;
        }
    }
}
