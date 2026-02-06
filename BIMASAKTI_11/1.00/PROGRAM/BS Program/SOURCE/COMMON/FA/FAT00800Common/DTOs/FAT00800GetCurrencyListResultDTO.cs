namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetCurrencyList method (streaming). Maps result set of RSP_GS_GET_CURRENCY_LIST.
    /// </summary>
    public class FAT00800GetCurrencyListResultDTO
    {
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CCURRENCY_NAME { get; set; } = string.Empty;
    }
}
