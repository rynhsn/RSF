using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110
{
    public class APT00110ParameterDTO
    {
        public APT00110DTO Data { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        //public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
