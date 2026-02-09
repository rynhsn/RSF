namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00800GetLastCurrencyRate method (RSP_GS_GET_LAST_CURRENCY_RATE)
    /// </summary>
    public class FAT00800GetLastCurrencyRateParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CRATE_DATE { get; set; } = string.Empty;
    }
}
