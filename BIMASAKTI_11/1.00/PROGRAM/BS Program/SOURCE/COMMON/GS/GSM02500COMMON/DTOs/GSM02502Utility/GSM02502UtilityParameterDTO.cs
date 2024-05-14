using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Utility
{
    public class GSM02502UtilityParameterDTO
    {
        public GSM02502UtilityDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_UNIT_TYPE_CATEGORY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
