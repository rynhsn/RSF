using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class SaveOtherUnitToExcelDTO
    {
        public int No { get; set; } = 0;
        public string OtherUnitId { get; set; } = "";
        public string OtherUnitName { get; set; } = "";
        public string OtherUnitType { get; set; } = "";
        public string Building { get; set; } = "";
        public string Floor { get; set; } = "";
        public string Location { get; set; } = "";
        public string UnitView { get; set; } = "";
        public decimal GrossSize { get; set; } = 0;
        public decimal NetSize { get; set; } = 0;
        public string LeaseStatus { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
