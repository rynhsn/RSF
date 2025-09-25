using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02504
{
    public class GSM02504ParameterDTO
    {
        public GSM02504DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
