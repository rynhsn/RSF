using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02300DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; } 
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public string CUNIT_VIEW_ID { get; set; }
        public string CUNIT_VIEW_NAME { get; set; }
        public string CUNIT_CATEGORY_ID { get; set; }
        public string CUNIT_CATEGORY_NAME { get; set; }
        public string CUNIT_TYPE_ID { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }

}
