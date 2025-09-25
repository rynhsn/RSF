using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00700ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
