namespace PMT02000COMMON.Upload.Unit
{
    public class UnitUploadDTO
    {
        public int NO { get; set; } = 0;
        public string CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CLOI_REF_NO { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CUNIT_ID { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public int ISEQ_NO_ERROR { get; set; } = 0;
    }
}
