using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class SaveUnitUtilityToExcelDTO
    {
        public int No { get; set; } = 0;
        public string UtilityType { get; set; } = "";
        public string SeqNo { get; set; } = "";
        public string MeterNo { get; set; } = "";
        public string AliasMeterNo { get; set; } = "";
        public decimal CalculationFactor { get; set; } = 0;
        public decimal Capacity { get; set; } = 0;
        public int MaxResetValue { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
