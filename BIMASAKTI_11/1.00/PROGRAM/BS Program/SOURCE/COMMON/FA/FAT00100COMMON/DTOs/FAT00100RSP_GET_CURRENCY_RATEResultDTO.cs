namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for RSP_GET_CURRENCY_RATE method
    /// </summary>
    public class FAT00100RSP_GET_CURRENCY_RATEResultDTO
    {
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }
}

