using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class GSM02502ParameterDTO
    {
        public GSM02502DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CUSER_LOGIN_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
