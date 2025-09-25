using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class CheckIsFloorUsedParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
    }
}
