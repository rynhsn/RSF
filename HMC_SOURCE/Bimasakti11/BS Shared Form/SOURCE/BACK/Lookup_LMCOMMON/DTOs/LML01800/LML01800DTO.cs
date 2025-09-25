using Lookup_PMCOMMON.DTOs.UtilityDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01800
{
    public class LML01800DTO : BaseDTO
    {
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CEMAIL { get; set; }
        public string? CTENANT_GROUP_ID { get; set; }
        public string? CTENANT_GROUP_NAME { get; set; }
        public string? CTENANT_CATEGORY_ID { get; set; }
        public string? CTENANT_CATEGORY_NAME { get; set; }
        public string? CTENANT_TYPE_ID { get; set; }
        public string? CTENANT_TYPE_DESCRIPTION { get; set; }
        public string? COWNER_OR_TENANT { get; set; }
    }
}
