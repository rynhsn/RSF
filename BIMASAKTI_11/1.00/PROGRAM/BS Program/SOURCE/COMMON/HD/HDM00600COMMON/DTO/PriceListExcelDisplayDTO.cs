using System;

namespace HDM00600COMMON.DTO
{
    public class PriceListExcelDisplayDTO : PricelistDTO
    {
        public int INO { get; set; }
        public string CVALID { get; set; }
        public string CNOTES { get; set; }
        public DateTime? DSTART_DATE { get; set; }
    }
}
