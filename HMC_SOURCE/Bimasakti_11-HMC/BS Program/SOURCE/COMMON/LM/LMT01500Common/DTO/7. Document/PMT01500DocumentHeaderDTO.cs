using R_APICommonDTO;

namespace PMT01500Common.DTO._7._Document
{
    public class PMT01500DocumentHeaderDTO : R_APIResultBaseDTO
    {
        public string? CREF_NO { get; set; }
        public string? CUNIT_NAME_LIST { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CCURRENCY_CODE { get; set; }
    }
}
