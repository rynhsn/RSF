using PMT01700COMMON.DTO.Utilities.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO.Utilities
{
    public class PMT01700EventCallBackDTO
    {
        public bool LUSING_PROPERTY_ID { get; set; }
        public bool LCRUD_MODE { get; set; }
        public string? CCRUD_MODE { get; set; }
        public PMT01700ParameterFrontChangePageDTO? ODATA_PARAMETER { get; set; }
    }
}
