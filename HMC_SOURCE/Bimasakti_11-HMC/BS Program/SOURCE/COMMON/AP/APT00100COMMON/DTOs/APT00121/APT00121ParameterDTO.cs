using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00121
{
    public class APT00121ParameterDTO
    {
        public APT00121DTO Data { get; set; }
        public APT00111HeaderDTO Header { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
