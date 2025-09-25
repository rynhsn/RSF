using System;
using System.Collections.Generic;
using System.Text;

namespace GFF00900COMMON.DTOs
{
    public class GFF00900ParameterDTO
    {
        public string IAPPROVAL_CODE { get; set; }
        public List<RSP_ACTIVITY_VALIDITYDataDTO> Data { get; set; }
    }
}
