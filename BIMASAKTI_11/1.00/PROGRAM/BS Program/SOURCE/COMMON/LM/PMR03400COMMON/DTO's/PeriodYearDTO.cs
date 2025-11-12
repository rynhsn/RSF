using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.DTO_s
{
    public class PeriodYearDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CMODE { get; set; } = string.Empty;
        public string CYEAR { get; set; } = string.Empty;
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}
