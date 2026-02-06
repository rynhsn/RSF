namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetCurrencyList method (streaming). Used with RSP_GS_GET_CURRENCY_LIST.
    /// </summary>
    public class FAT00800GetCurrencyListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
    }
}
