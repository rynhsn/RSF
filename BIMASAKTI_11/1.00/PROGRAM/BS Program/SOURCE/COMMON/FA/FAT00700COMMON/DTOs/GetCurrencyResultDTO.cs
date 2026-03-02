namespace FAT00700Common.DTOs
{
    public class GetCurrencyResultDTO
    {
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
    }
}

