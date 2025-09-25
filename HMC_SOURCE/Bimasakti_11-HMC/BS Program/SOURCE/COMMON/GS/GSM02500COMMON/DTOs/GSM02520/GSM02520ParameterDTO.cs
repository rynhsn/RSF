using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class GSM02520ParameterDTO
    {
        public GSM02520DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = ""; 
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
