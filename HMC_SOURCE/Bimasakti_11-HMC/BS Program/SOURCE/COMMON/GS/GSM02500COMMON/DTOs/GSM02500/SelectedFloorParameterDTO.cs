using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class SelectedFloorParameterDTO
    {
        public SelectedFloorDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_BUILDING_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
