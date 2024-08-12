namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02400ParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public bool LAGREEMENT { get; set; }
        public string CPROGRAM_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CSEARCH_TEXT { get; set; }
    }

}
