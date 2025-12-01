namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetFAAcquisitionDetailAllocExpenPageList streaming method
    /// </summary>
    public class FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
    }
}

