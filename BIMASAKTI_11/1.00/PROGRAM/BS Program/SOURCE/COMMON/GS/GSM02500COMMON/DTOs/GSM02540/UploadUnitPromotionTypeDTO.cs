using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class UploadUnitPromotionTypeDTO
    {
        public int No { get; set; } = 0;
        public string CompanyId { get; set; } = "";
        public string PropertyId { get; set; } = "";
        public string UnitPromotionTypeCode { get; set; } = "";
        public string UnitPromotionTypeName { get; set; } = "";
        public decimal GrossAreaSize { get; set; } = 0;
        public decimal NetAreaSize { get; set; } = 0;
        public decimal CommonArea { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
