using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50630
{
    public class PMT50630ParameterDTO
    {
        public PMT50630DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";  
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
