using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210
{
    public class APT00210ParameterDTO
    {
        public APT00210DTO Data { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
