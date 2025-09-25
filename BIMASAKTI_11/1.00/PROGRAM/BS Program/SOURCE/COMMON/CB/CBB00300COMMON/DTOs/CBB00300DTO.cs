using System;
using System.Collections.Generic;
using System.Text;

namespace CBB00300COMMON.DTOs
{
    public class CBB00300DTO
    {
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public string CMIN_PERIOD_YY { get; set; }
        public string CMIN_PERIOD_MM { get; set; }
        public string CMAX_PERIOD_YY { get; set; }
        public string CMAX_PERIOD_MM { get; set; }
        public DateTime? DLAST_DATE { get; set; }
        public string CLAST_USER_ID { get; set; }
    }
}
