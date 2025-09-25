namespace PMT02000COMMON.Upload.Utility
{
    public class UtilityUploadDTO
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
        public string? CCHARGES_TYPE_ID { get; set; }
        public string? CCHARGES_TYPE_NAME { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CMETER_NO { get; set; }
        public string? CSTART_INV_PRD { get; set; }
        public decimal NMETER_START { get; set; }
        public decimal NBLOCK1_START { get; set; }
        public decimal NBLOCK2_START { get; set; }
        public int ISEQ_NO_ERROR { get; set; } = 0;
    }
}
