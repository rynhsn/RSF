using System;

namespace PMT01500Common.DTO._3._Unit_Info
{
    public class PMT01500UnitInfoUnitInfoDetailDTO
    {
        //Parameter External
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CUSER_ID { get; set; }
        //FOR LIST
        public string? CFLOOR_NAME { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        //Real DTOs
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public Decimal NGROSS_AREA_SIZE { get; set; }
        public Decimal NNET_AREA_SIZE { get; set; }
        public Decimal NCOMMON_AREA_SIZE { get; set; }
        public Decimal NACTUAL_AREA_SIZE { get; set; }
    }
}
