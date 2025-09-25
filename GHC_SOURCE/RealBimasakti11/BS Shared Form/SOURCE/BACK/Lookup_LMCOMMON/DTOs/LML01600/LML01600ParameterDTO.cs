using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01600
{
    public class LML01600ParameterDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CCALL_TYPE_ID { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";

    }
}
