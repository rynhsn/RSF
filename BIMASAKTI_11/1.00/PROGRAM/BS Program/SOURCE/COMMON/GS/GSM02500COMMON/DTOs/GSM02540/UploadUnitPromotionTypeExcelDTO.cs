using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class UploadUnitPromotionTypeExcelDTO
    {
        public string UnitPromotionTypeCode { get; set; } = "";
        public string UnitPromotionTypeName { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public decimal GrossAreaSize { get; set; } = 0;
        public decimal NetAreaSize { get; set; } = 0;
    }
}
