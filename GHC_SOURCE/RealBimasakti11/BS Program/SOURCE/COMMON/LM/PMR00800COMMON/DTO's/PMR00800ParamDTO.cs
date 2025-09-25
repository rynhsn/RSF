using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00800COMMON.DTO_s
{
    public class PMR00800ParamDTO : PMR00800SpParamDTO
    {
        public string CREPORT_CULTURE { get; set; }
        public string CPERIOD_YYYY { get; set; }
        public string CPERIOD_MM { get; set; }
        public string CFROM_BUILDING_NAME { get; set; }
        public string CTO_BUILDING_NAME { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CBUILDING_DISPLAY => CFROM_BUILDING != CTO_BUILDING ? $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING}) - {CTO_BUILDING_NAME}({CTO_BUILDING})" : $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING})";
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public string CREPORT_FILETYPE { get; set; }

    }
}
