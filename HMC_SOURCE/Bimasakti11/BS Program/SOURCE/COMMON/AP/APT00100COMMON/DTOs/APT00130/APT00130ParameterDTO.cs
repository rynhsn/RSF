using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00130
{
    public class APT00130ParameterDTO
    {
        public APT00130DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";  
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
