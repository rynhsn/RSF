using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.LOI_List
{
    public class PMT02000LOIHandoverUtilityDTO
    {
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CCHARGES_TYPE_NAME { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CMETER_NO { get; set; }
        public decimal NCAPACITY { get; set; }
        public decimal NCALCULATION_FACTOR { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CYEAR { get; set; }
        public int IYEAR { get; set; }
        public string? CMONTH { get; set; }
        public decimal NMETER_START { get; set; }
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
