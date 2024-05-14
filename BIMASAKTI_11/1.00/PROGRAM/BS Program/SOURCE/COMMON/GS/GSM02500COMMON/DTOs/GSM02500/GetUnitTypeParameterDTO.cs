using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class GetUnitTypeParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
