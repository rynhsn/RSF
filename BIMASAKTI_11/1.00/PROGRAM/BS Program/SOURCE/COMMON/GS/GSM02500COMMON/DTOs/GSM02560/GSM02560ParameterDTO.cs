using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02560
{
    public class GSM02560ParameterDTO
    {
        public GSM02560DTO Data { get; set; } 
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
