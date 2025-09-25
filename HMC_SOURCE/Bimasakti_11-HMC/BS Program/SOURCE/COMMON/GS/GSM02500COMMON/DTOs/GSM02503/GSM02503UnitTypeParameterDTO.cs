using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503UnitTypeParameterDTO
    {
        public GSM02503UnitTypeDetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = ""; 
        public string CUSER_LOGIN_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";  
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
