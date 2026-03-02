namespace FAT00700Common.DTOs
{
    public class CheckOutstandingTransParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CASSET_CODE { get; set; } = string.Empty;
    }
}

