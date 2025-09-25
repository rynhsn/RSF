using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class GetCBSystemParamDTO
    {
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public bool LCB_NUMBERING { get; set; }
        public bool LINPUT_CHEQUE_DATE { get; set; }
        public string CCB_LINK_DATE { get; set; }
    }
}
