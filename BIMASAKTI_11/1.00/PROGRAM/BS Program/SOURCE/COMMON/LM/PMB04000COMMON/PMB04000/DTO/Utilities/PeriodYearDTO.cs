using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMON.DTO.Utilities
{
    public class PeriodYearDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
}
