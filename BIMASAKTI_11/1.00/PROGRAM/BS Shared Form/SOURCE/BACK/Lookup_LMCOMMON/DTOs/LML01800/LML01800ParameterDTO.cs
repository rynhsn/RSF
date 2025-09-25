using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01800
{
    public class LML01800ParameterDTO : BaseDTO
    {
        public string? CBUILDING_ID { get; set; } = "";
        public string? CFLOOR_ID { get; set; } = "";
        public string? CUNIT_ID { get; set; } = "";
        public string? CTENANT_ID { get; set; } = "";
    }
}
