using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50631
{
    public class PMT50631ParameterDTO
    {
        public PMT50631DTO Data { get; set; }
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
