namespace PMR00400Common.DTOs
{
    public class PMR00400ParamDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTYPE { get; set; } = "";
        public string CFROM_BUILDING_ID { get; set; } = "";
        public string CTO_BUILDING_ID { get; set; } = "";
        public string CFROM_UNIT_TYPE_ID { get; set; } = "";
        public string CTO_UNIT_TYPE_ID { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
    }

    public class PMR00400PrintBaseHeaderLogoDTO
    {
        public byte[] CLOGO { get; set; }
    }
}