using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class PeriodYearRangeDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}
