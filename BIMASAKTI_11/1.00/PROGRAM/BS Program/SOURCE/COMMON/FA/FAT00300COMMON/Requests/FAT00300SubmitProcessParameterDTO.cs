namespace FAT00300Common.Requests
{
    public class FAT00300SubmitProcessParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty;

    }
}







