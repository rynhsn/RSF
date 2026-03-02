namespace FAT00700Common.DTOs
{
    public class GetTransDateValidationParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CTRANSACTION_DATE { get; set; } = string.Empty;
    }
}

