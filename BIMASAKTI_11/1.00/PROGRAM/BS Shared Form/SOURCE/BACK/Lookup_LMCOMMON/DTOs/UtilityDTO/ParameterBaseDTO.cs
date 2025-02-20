using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.UtilityDTO
{
    public class ParameterBaseDTO : BaseDTO
    {
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CAPPLICATION { get; set; }
        public string? CCLASS_ID { get; set; }
    }
}
