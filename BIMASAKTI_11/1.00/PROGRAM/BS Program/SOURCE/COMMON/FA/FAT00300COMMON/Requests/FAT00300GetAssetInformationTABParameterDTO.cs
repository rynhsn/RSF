namespace FAT00300Common.Requests
{
    public class FAT00300GetAssetInformationTABParameterDTO
    {
        // Required standard properties (ALWAYS include)
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;

        public string CASSET_CODE { get; set; } = string.Empty;

        public string CLOCAL_CURRENCY { get; set; } = string.Empty;
        public string CBASE_CURRENCY { get; set; } = string.Empty;
    }
}







