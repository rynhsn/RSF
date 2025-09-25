using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class InitialProcessParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
        public string CCYEAR { get; set; } = "";
        public string CPERIOD_NO { get; set; } = "";
        public string CCURRENT_PERIOD_YY { get; set; } = "";
        public string CCURRENT_PERIOD_MM { get; set; } = "";
    }
}
