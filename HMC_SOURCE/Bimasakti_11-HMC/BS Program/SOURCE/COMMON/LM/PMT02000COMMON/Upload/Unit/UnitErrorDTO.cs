using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Upload.Unit
{
    public class UnitErrorDTO
    {
        public int NO { get; set; } = 0;
        public string? Property { get; set; }
        public string? Transaction { get; set; }
        public string? Department { get; set; }
        public string? LOI_AgrmntNo { get; set; }
        public string? Building { get; set; }
        public string? Floor { get; set; }
        public string? Unit { get; set; }
        public decimal ActualSize { get; set; }
        public string? Valid { get; set; }
        public string? Notes { get; set; }
    }
}
