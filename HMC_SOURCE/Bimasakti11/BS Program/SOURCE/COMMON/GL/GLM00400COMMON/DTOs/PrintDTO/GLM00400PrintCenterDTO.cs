using System;
using System.Collections.Generic;

namespace GLM00400COMMON
{
    public class GLM00400PrintCenterDTO
    {
        // param
        public string CYEAR { get; set; }
        public string CALLOC_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }

        // result
        public string CPERIOD { get; set; }
        public string CALLOC_PERIOD { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CTARGET_CENTER { get; set; }
        public decimal NVALUE { get; set; }
        public List<GLM00400PrintCenterDetailDTO> CenterDetail { get; set; }
    }


}
