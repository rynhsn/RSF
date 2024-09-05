using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs
{
    public class TabParameterDTO
    {
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CSELECTED_BUILDING_ID { get; set; }
        public string CSELECTED_FLOOR_ID { get; set; }
        public string CSELECTED_UNIT_ID { get; set; }
        public string CSELECTED_OTHER_UNIT_ID { get; set; }

        //UNTUK FLOOR KE UNIT
        public string CDEFAULT_UNIT_TYPE { get; set; }
        public string CDEFAULT_UNIT_CATEGORY { get; set; }
    }
}
