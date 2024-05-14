using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class UploadUnitUtilitySaveDTO
    {
        public int NO { get ; set; } = 0;
        public string CUTILITY_TYPE { get; set; } = "";
        public string CSEQUENCE { get; set; } = "";
        public string CMETER_NO { get; set; } = "";
        public string CALIAS_METER_NO { get; set; } = "";
        public decimal NCALCULATION_FACTOR { get; set; } = 0;
        public decimal NCAPACITY { get; set; } = 0;
        public int IMETER_MAX_RESET { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
    }
}
