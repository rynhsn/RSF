using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00600COMMON.DTO
{
    public class PricelistSaveToExcelDTO : PricelistReadExcelDTO
    {
        public int No { get; set; }
        public string Valid { get; set; }
        public string Notes { get; set; }
    }
}
