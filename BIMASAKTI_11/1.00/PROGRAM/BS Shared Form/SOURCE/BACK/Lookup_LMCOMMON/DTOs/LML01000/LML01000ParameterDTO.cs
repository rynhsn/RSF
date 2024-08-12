using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01000
{
    public class LML01000ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CBILLING_RULE_TYPE { get; set; } = "";
        public string CUNIT_TYPE_CTG_ID { get; set; } = "";
        public bool LACTIVE_ONLY { get; set; }
        public string CUSER_ID { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
