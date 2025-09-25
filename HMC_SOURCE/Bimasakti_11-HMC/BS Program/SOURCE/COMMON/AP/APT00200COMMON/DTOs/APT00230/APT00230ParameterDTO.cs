using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00230
{
    public class APT00230ParameterDTO
    {
        public APT00230DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";  
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
