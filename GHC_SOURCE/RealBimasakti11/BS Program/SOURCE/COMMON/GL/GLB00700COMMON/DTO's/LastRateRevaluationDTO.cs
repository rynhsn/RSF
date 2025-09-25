using System;
using System.Collections.Generic;
using System.Text;

namespace GLB00700COMMON.DTO_s
{
    public class LastRateRevaluationDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CLAST_AR_DATE { get; set; }
        public string CLAST_AP_DATE { get; set; }
        public string CLAST_CB_DATE { get; set; }

        public DateTime? DLAST_AR_DATE { get; set; }
        public DateTime? DLAST_AP_DATE { get; set; }
        public DateTime? DLAST_CB_DATE { get; set; }
    }
}
