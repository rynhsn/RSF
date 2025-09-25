using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00900InitialProcessDTO : R_APIResultBaseDTO
    {
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }
    public class MonthDTO
    {
        public string? Id { get; set; }
    }
    public class PeriodDTO
    {
        public string? CCODE { get; set; }
        public string? CNAME { get; set; }
    }
}
