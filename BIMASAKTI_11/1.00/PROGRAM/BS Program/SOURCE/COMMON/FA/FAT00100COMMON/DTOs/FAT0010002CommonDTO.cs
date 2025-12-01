namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Common DTO for FAT0010002 - Expense Allocation
    /// </summary>
    public class FAT0010002CommonDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
    }
}

