namespace PMT05500COMMON.DTO
{
    public class LMT05500DBParameter
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CPROPERTY_ID { get; set; } = "";


        public string? CDEPT_CODE { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CREF_NO { get; set; } = "";
        public string? CSEQ_NO { get; set; } = "";

        public string? CCHARGE_MODE { get; set; } = "";
        public string? CBUILDING_ID { get; set; } = "";
        public string? CFLOOR_ID { get; set; } = "";
        public string? CUNIT_ID { get; set; } = "";
        public string? CUNIT_DESCRIPTION { get; set; } = "";
        public string? CULTURE { get; set; }
        public string? CTRANS_STATUS_LIST { get; set; } = "";
        //CR 02 Okt 2024
        public bool LINVOICED { get; set; }
        public string? CPROGRAM_ID { get; set; } = "";
    }
}
