using System;

namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetGridAlloc streaming method
    /// </summary>
    public class FAT00800GetGridAllocResultDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
    }
}

