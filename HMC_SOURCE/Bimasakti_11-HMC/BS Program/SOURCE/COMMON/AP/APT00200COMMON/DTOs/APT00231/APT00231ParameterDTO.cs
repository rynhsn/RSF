using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00231
{
    public class APT00231ParameterDTO
    {
        public APT00231DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CADDITIONAL_TYPE { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
    }
}
