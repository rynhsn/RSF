using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Upload.Utility
{
    public class UtilityErrorDTO
    {
        public int NO { get; set; } = 0;
        public string? Property { get; set; }
        public string? Transaction { get; set; }
        public string? Department { get; set; }
        public string? LOI_AgrmntNo { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }
        public string? Unit { get; set; }
        public string? ChargesTypeId { get; set; }
        public string? ChargesTypeName { get; set; }
        public string? ChargesId { get; set; }
        public string? ChargesName { get; set; }
        public string? MeterNo { get; set; }
        public string? StartPeriod { get; set; }
        public decimal MeterStart { get; set; }
        public decimal Block1Start { get; set; }
        public decimal Block2Start { get; set; }
        public string? Valid { get; set; }
        public string? Notes { get; set; }
    }
}
