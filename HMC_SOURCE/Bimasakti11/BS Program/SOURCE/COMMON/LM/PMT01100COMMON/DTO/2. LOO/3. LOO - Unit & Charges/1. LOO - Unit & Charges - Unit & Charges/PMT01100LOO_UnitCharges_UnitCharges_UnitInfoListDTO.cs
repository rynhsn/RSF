using System;

namespace PMT01100Common.DTO._2._LOO._3._LOO___Unit___Charges._1._LOO___Unit___Charges___Unit___Charges
{
    public class PMT01100LOO_UnitCharges_UnitCharges_UnitInfoListDTO
    {
        //For Db
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CUSER_ID { get; set; }
        //RealDTO
        public string? CUNIT_NAME { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}

