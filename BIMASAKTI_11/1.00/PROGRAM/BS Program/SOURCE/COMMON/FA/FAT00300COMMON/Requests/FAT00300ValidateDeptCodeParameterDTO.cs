namespace FAT00300Common.Requests
{
    public class FAT00300ValidateDeptCodeParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CDEPT_CODE { get; set; } = string.Empty;
    }
}







