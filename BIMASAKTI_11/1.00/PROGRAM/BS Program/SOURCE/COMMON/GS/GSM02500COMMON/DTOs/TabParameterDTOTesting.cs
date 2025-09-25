using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs
{
    public class TabParameterDTOTesting
    {
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_BUILDING_ID { get; set; } = "";
        public string CSELECTED_FLOOR_ID { get; set; } = "";
        public string CSELECTED_UNIT_ID { get; set; } = "";
        public bool IsCRUDModeHidden { get; set; } = true;
    }
}
