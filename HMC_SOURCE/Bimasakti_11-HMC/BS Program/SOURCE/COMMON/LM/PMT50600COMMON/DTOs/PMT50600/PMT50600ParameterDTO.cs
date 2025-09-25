using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50600
{
    public class PMT50600ParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CPERIOD_FROM { get; set; } = "";
        public string CPERIOD_TO { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
