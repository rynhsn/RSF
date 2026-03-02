namespace FAT00700Common.DTOs
{
    public class GetUserActivityRightsParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CACTIVITY_CODE { get; set; } = string.Empty;
    }
}

