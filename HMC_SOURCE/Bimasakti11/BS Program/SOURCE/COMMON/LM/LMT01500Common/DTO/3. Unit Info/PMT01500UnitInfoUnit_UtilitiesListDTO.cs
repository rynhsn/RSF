using System;

namespace PMT01500Common.DTO._3._Unit_Info
{
    public class PMT01500UnitInfoUnit_UtilitiesListDTO
    {
        //NEEDED FOR Display
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CUSER_ID { get; set; }
        //Updated 15 May 2024 : Forget 2 DTOs
        public string? CMETER_NO { get; set; }
        public string? CSTATUS_DESCR { get; set; }


        //Real DTO
        public string? CUTILITY_TYPE_DESCR { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public Int32 IMETER_START { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
