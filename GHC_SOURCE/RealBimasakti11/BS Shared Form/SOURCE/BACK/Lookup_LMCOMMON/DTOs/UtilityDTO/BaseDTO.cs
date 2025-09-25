using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.UtilityDTO
{
    public class BaseDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string? CUSER_ID { get; set; } 
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CLANGUAGE_ID { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
