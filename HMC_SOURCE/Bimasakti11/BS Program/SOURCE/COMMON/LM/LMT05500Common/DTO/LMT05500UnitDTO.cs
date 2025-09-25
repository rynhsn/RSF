using System;
using System.Text;

namespace PMT05500COMMON.DTO
{
    public class LMT05500UnitDTO : LMT05500Header
    {
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }

    }
}
