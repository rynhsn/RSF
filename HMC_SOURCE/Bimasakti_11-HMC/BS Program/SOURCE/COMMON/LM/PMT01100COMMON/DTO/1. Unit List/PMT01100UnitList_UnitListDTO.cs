using System;

namespace PMT01100Common.DTO._1._Unit_List
{
    public class PMT01100UnitList_UnitListDTO
    {
        public bool LSELECTED_UNIT { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        //Updated For Add Unit List in Offer
        public decimal NCOMMON_AREA_SIZE { get; set; }

        public string? CUNIT_VIEW_NAME { get; set; }
        public string? CLEASE_STATUS { get; set; }
        public string? CTENANT_NAME { get; set; }
        public int ITOTAL_LOO { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}
