using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610
{
    public class PMT50610ParameterDTO
    {
        public PMT50610DTO Data { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        //public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
