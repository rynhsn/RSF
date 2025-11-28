namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetLocalBaseCurr method
    /// </summary>
    public class FAT00800GetLocalBaseCurrResultDTO
    {
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
    }
}

