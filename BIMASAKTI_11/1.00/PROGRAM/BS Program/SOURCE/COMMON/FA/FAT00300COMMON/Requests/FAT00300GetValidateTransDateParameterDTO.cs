namespace FAT00300Common.Requests
{
    public class FAT00300GetValidateTransDateParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CTRANSACTION_DATE { get; set; } = string.Empty;
    }
}







