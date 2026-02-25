using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationBatchListDisplayDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; } 
    }
}
