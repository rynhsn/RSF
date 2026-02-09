namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00800GetLastCurrencyRate method (RSP_GS_GET_LAST_CURRENCY_RATE)
    /// </summary>
    public class FAT00800GetLastCurrencyRateResultDTO
    {
        public string CCURRENCY_CODE { get; set; } = string.Empty;
        public string CRATETYPE_CODE { get; set; } = string.Empty;
        public string CRATE_DATE { get; set; } = string.Empty;
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }
}
