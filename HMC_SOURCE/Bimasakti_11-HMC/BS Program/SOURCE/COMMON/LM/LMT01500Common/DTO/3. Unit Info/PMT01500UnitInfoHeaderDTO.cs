using R_APICommonDTO;

namespace PMT01500Common.DTO._3._Unit_Info
{
    public class PMT01500UnitInfoHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CCHARGE_MODE_DESCR { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public string? CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string? CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public int ITOTAL_UNIT { get; set; }
    }
}
