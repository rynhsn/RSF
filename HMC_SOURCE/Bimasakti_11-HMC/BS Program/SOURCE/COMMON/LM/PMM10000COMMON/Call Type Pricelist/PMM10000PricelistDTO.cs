using System;
using System.Collections.Generic;
using System.Text;

namespace PMM10000COMMON.Call_Type_Pricelist
{
    public class PMM10000PricelistDTO
    {
        public string? CPRICELIST_ID { get; set; }
        public string? CPRICELIST_NAME { get; set; }
        public string? CCHARGES_ID { get; set; }
        public bool LTAXABLE { get; set; }
        public string? CUNIT { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public int IPRICE { get; set; }
        public bool LDISABLE { get; set; }
    }
}
