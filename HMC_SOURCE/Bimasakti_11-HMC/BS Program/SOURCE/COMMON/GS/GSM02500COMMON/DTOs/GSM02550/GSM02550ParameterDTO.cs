using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02550
{
    public class GSM02550ParameterDTO
    {
        public GSM02550DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
