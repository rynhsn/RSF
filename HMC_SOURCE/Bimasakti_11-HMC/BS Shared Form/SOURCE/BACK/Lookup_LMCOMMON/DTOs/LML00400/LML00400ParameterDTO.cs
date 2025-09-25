namespace Lookup_PMCOMMON.DTOs
{
    public class LML00400ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CCHARGE_TYPE_ID { get; set; } = "";
        //CR09
        public string CTAXABLE_TYPE { get; set; } = "0";
        public string CACTIVE_TYPE { get; set; } = "0";
        public string CTAX_DATE { get; set; } = "";
        public string? CSEARCH_TEXT { get; set; } = "";
    }
}