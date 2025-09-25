using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HDM00600COMMON.DTO
{
    public class PricelistReadExcelDTO
    {
        public string PricelistID { get; set; }
        public string PricelistName { get; set; }
        public string Dept{ get; set; }
        public string ChargesID { get; set; }
        public string Unit { get; set; }
        public string Curr { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
    }
}
