using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00100
{
    public class GetPeriodParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPERIOD_YY { get; set; } = "";
        public string CPERIOD_MM { get; set; } = "";
    }
}
