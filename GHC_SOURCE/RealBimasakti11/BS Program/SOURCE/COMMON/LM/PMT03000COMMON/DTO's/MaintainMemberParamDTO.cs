using System;
using System.Collections.Generic;
using System.Text;

namespace PMT03000COMMON.DTO_s
{
    public class MaintainMemberParamDTO
    {
        public string PARAM_BUILDING_ID { get; set; } = "";
        public string PARAM_BUILDING_NAME { get; set; } = "";
        public string PARAM_FLOOR_ID { get; set; } = "";
        public string PARAM_FLOOR_NAME { get; set; } = "";
        public string PARAM_UNIT_ID { get; set; } = "";
        public string PARAM_UNIT_NAME { get; set; } = "";
        public string PARAM_PROGRAM_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_TENANT_ID { get; set; } = "";
    }
}
