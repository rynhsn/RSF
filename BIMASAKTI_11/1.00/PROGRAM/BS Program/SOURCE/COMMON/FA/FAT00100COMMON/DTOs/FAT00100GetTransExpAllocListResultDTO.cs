using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT00100GetTransExpAllocList method
    /// </summary>
    public class FAT00100GetTransExpAllocListResultDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public bool LALLOW_DELETE { get; set; }
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}
