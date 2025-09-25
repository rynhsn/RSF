using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300DTO
    {
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CPROPERTY_NAME { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public string CFLOOR_ID { get; set; } = string.Empty;
        public string CUNIT_ID { get; set; } = string.Empty;
        public string CUNIT_NAME { get; set; } = string.Empty;
        public string CCATEGORY { get; set; } = string.Empty;
        public string CUNIT_TYPE_ID { get; set; } = string.Empty;
        public decimal NOCCUPIABLE_AREA { get; set; } = 0;
        public decimal NCOMMON_AREA_SIZE { get; set; } = 0;
        public string CUNIT_STATUS { get; set; } = string.Empty;
        public string CAGREEMENT_NO { get; set; } = string.Empty;
        public string CAGREEMENT_STATUS { get; set; } = string.Empty;
        public string CTENANT { get; set; } = string.Empty;
        public string CTENANT_ID { get; set; } = string.Empty;
        public string CEND_DATE { get; set; } = string.Empty;
        public DateTime? DEND_DATE { get; set; }
        public DateTime? DINACTIVE_DATE { get; set;}
        public int IEXP_DAYS { get; set; } = 0;
        public string CEXP_DAYS { get; set; } = string.Empty;
    }
}
