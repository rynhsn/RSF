namespace FAT00300Common.Requests
{
    public class FAT00300GetInitialProcessResultDTO
    {
        public string CDEFAULT_TRX_DEPT_CODE { get; set; } = string.Empty;
        public string CSOFT_PERIOD { get; set; } = string.Empty;
        public string CCURRENT_PERIOD { get; set; } = string.Empty;
        public string CGLLINK_DATE { get; set; } = string.Empty;
        public string CLOCAL_CURRENCY_CODE { get; set; } = string.Empty;
        public string CBASE_CURRENCY_CODE { get; set; } = string.Empty;
        public bool LCUST_PERIOD_FLAG { get; set; }
        public string CTRANS_DESC { get; set; } = string.Empty;
        public bool LTRANS_APPROVAL { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
    }
}







