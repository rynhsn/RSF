namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetCurrency method
    /// </summary>
    public class FAT00800GetCurrencyResultDTO
    {
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }
}

