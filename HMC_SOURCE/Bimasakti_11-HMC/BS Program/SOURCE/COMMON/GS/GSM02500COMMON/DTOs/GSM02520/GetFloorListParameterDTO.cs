using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class GetFloorListParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
    }
}
