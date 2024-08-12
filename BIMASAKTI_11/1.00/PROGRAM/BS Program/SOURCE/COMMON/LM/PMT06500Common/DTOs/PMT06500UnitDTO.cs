using System;

namespace PMT06500Common.DTOs
{
    public class PMT06500UnitDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CPARENT_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CUNIT_NAME { get; set; } = "";
        public decimal NGROSS_AREA_SIZE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}