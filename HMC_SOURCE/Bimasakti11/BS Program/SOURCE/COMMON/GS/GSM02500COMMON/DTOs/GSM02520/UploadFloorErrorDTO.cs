using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class UploadFloorErrorDTO
    {
        public int NO { get; set; } = 0;
        public string FloorCode { get; set; } = "";
        public string FloorName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string UnitType { get; set; } = "";
        public string UnitCategory { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}
