using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class CheckIsUnitTypeCategoryUsedParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CUNIT_TYPE_CATEGORY_ID { get; set; } = "";
    }
}
