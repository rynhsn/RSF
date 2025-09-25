using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02510
{
    public class GSM02510ParameterDTO
    {
        public GSM02510DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CLOGIN_USER_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CACTION { get; set; }
    }
}
