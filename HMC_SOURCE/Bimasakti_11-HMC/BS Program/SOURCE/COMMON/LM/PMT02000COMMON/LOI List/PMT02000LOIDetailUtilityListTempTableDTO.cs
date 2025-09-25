using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIDetailUtilityListTempTableDTO
    {
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CMETER_NO { get; set; }
        public string? CSTART_YEAR { get; set; }
        public string? CSTART_MONTH { get; set; }
        public decimal NMETER_START { get; set; }
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
    }
}
