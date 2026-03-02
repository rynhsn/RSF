namespace FAT00700Common.DTOs
{
    public class ValidateFoundDeptParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANGID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CDEPT_CODE { get; set; } = string.Empty;
    }
}

