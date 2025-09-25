using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs.APT00211;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00221
{
    public class APT00221ParameterDTO
    {
        public APT00221DTO Data { get; set; }
        public APT00211HeaderDTO Header { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
