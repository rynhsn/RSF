using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class SaveOtherUnitTypeToExcelDTO
    {
        public int No { get; set; } = 0;
        public string OtherUnitTypeCode { get; set; } = "";
        public string OtherUnitTypeName { get; set; } = "";
        public string Department { get; set; } = "";
        public string PropertyType { get; set; } = "";
        public decimal GrossAreaSize { get; set; } = 0;
        public decimal NetAreaSize { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
