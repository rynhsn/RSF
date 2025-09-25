using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00131
{
    public class APT00131ParameterDTO
    {
        public APT00131DTO Data { get; set; }
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
