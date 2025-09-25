using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02120HandoverProcessUnitDTO
    {
        public int NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
    }
}
