namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetCurrencyList method (RSP_GS_GET_CURRENCY_LIST) - list
    /// </summary>
    public class FAT01100GetCurrencyListResultDTO
    {
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
    }
}
