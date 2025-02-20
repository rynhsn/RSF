using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01900
{
    public class LML01900ParamaterDTO : BaseDTO
    {
        public string? CSUPERVISOR_ID { get; set; } = "";
        public string? CSUPERVISOR_NAME { get; set; } = "";
        public bool LSUPERVISOR { get; set; }
        public string? CDEPT_CODE { get; set; } = "";
        public string? CDEPT_NAME { get; set; } = "";
        public string? CACTIVE_INACTIVE { get; set; } = "";
        public string? CSTAFF_TYPE_ID { get; set; } = "";
        public string? CSTAFF_TYPE_NAME { get; set; } = "";
        public string? CSTAFF_ID { get; set; } = "";

    }
}
