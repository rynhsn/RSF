using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML02000
{
    public class LML02000DTO : BaseDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public string? CCATEGORY_NAME { get; set; }
        public string? CCATEGORY_ID_NAME { get; set; }
        public int ILEVEL { get; set; }
        public int IQUERY_LEVEL { get; set; }
        public bool? LHAS_CHILD { get; set; }
    }
}
