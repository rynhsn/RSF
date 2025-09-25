using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02531UtilityParameterDTO
    {
        public GSM02531UtilityDetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_BUILDING_ID { get; set; } = ""; 
        public string CSELECTED_FLOOR_ID { get; set; } = "";
        public string CSELECTED_UNIT_ID { get; set; } = "";
        public string CSELECTED_UTILITIES_TYPE_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
