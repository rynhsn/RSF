using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01900
{
    public class LML01900DTO : BaseDTO
    {
        public string? CSTAFF_ID { get; set; }
        public string? CSTAFF_NAME { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CSUPERVISOR { get; set; }
        public string? CSUPERVISOR_NAME { get; set; }
        public string? CPOSITION { get; set; }
        public string? CPOSITION_DESCRIPTION { get; set; }
        public string? CTYPE { get; set; }
        public string? CTYPE_DESCRIPTION { get; set; }
        public string? CEMAIL { get; set; }
        public string? CMOBILE_PHONE1 { get; set; }
        public bool LACTIVE { get; set; }
    }
}
