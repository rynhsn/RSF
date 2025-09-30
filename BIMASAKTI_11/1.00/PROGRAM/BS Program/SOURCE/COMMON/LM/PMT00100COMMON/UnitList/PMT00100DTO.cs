using PMT00100COMMON.UtilityDTO;

namespace PMT00100COMMON.UnitList
{
    public class PMT00100DTO : BaseDTO
    {
        public string? CFLOOR_ID { get; set; }
        public string? CFLOOR_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_ID { get; set; }
        public string? CUNIT_TYPE_NAME { get; set; }
        public string? CUNIT_CATEGORY_ID { get; set; }
        public string? CUNIT_CATEGORY_NAME { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public string? CUNIT_VIEW_NAME { get; set; }
        public string? CSTRATA_STATUS { get; set; }
        public string? CSTRATA_STATUS_DESC { get; set; }
        public string? CLEASE_STATUS_DESC { get; set; }

    }
}
