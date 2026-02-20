namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetCurrencyList method (RSP_GS_GET_CURRENCY_LIST) - streaming
    /// </summary>
    public class FAT01100GetCurrencyListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
    }
}
