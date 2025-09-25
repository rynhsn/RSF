using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Facility
{
    public class GSM02502FacilityParameterDTO
    {
        public GSM02502FacilityDTO Data { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CACTION { get; set; }
    }
}
