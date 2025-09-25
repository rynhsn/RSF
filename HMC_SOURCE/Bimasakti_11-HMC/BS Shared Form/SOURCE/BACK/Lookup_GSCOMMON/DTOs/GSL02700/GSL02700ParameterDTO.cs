namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02700ParameterDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public bool LEVENT { get; set; }
        public bool LAGREEMENT { get; set; }
        public bool LBOTH_EVENT_CASUAL { get; set; }
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CLEASE_STATUS_LIST { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
        public string CREMOVE_DATA_OTHER_UNIT_ID { get; set; } = "";
    }

}
