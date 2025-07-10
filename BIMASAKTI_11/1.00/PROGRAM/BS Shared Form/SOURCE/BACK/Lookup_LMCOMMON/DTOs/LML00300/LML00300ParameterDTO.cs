namespace Lookup_PMCOMMON.DTOs
{
    public class LML00300ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public bool LINCLUDE_ADMIN { get; set; } = true;
        public bool LINCLUDE_MANAGEMENT { get; set; } = true;
        public string  CSEARCH_TEXT { get; set; } = "";
    }
}