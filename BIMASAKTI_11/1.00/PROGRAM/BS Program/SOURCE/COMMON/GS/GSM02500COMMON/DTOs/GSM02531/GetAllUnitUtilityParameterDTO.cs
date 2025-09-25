using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class GetAllUnitUtilityParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_BUILDING_ID { get; set; } = "";
        public string CSELECTED_FLOOR_ID { get; set; } = "";
        public string CSELECTED_UNIT_ID { get; set; } = "";
    }
}
