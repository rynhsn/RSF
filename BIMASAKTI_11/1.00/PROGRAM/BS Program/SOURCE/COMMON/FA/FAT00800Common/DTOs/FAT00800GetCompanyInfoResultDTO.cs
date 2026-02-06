namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00800GetCompanyInfo method
    /// </summary>
    public class FAT00800GetCompanyInfoResultDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCOMPANY_NAME { get; set; } = string.Empty;
        public string CCOGS_METHOD { get; set; } = string.Empty;
        public int LENABLE_CENTER_IS { get; set; }
        public int LENABLE_CENTER_BS { get; set; }
        public int LPRIMARY_ACCOUNT { get; set; }
        public string CPRIMARY_CO_ID { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_NAME { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_NAME { get; set; } = string.Empty;
        public int LCASH_FLOW1 { get; set; }
        public int ITIMEZONE { get; set; }
        public string CDATETIME_NOW { get; set; } = string.Empty;
    }
}
