using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01000
{
    public class LML01000DTO
    {
        public string? CBILLING_RULE_CODE  { get; set; }
        public string? CBILLING_RULE_NAME { get; set; }
        //CR 02 Okt 2024
        public decimal NMIN_BOOKING_FEE { get; set; }
        public bool LBOOKING_FEE_OVERWRITE { get; set; }
    }
}
