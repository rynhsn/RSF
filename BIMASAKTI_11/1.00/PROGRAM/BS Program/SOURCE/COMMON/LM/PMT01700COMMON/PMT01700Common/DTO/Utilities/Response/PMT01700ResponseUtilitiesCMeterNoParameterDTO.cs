using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO.Utilities.Response
{
    public class PMT01700ResponseUtilitiesCMeterNoParameterDTO : R_APIResultBaseDTO
    {
        public string? CMETER_NO { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public decimal NCAPACITY { get; set; }
    }
}
