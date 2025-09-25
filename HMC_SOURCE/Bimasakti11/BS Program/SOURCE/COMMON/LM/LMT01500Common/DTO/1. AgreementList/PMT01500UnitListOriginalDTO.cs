using System;

namespace PMT01500Common.DTO._1._AgreementList
{
    public class PMT01500UnitListOriginalDTO
    {
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public Decimal NGROSS_AREA_SIZE { get; set; }
        public Decimal NNET_AREA_SIZE { get; set; }
        public Decimal NCOMMON_AREA_SIZE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
