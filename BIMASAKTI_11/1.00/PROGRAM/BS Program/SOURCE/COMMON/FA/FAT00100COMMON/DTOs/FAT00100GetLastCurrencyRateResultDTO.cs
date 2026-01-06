namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00100GetLastCurrencyRate method
    /// </summary>
    public class FAT00100GetLastCurrencyRateResultDTO
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
