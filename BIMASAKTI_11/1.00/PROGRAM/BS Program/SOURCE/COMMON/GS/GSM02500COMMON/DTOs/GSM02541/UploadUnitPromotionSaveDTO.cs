using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadUnitPromotionSaveDTO
    {
        public int NO { get; set; } = 0;
        public string UnitPromotionId { get; set; } = "";
        public string UnitPromotionName { get; set; } = "";
        public string UnitPromotionType { get; set; } = "";
        public string Building { get; set; } = "";
        public string Floor { get; set; } = "";
        public string Location { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
    }
}
