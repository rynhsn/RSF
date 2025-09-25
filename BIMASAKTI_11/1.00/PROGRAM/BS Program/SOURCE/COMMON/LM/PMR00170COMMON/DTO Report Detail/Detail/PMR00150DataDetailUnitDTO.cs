using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataDetailUnitDTO
    {
        public string? CUNIT_DETAIL_ID { get; set; }
        public decimal NUNIT_DETAIL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_PRICE { get; set; }
        public decimal NUNIT_TOTAL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_TOTAL_PRICE { get; set; }
    }
}
