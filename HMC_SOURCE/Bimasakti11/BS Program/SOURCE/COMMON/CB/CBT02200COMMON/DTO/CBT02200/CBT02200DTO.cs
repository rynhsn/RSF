using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class CBT02200DTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CCB_CODE { get; set; } = "";
        public string CCB_NAME { get; set; } = "";
        public string CCB_ACCOUNT_NO { get; set; } = "";
        public string CCB_ACCOUNT_NAME { get; set; } = "";
        public string CPERIOD_MM { get; set; } = "";
        public int IPERIOD_YY { get; set; } = 0;
        public string CSTATUS { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
