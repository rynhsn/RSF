using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class GetSoftPeriodStartDateParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSOFT_PERIOD_YY { get; set; } = "";
        public string CSOFT_PERIOD_MM { get; set; } = ""; 
    }
}
