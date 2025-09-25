using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02530ParameterDTO
    {
        public GSM02530DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_BUILDING_ID { get; set; } = "";
        public string CSELECTED_FLOOR_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
