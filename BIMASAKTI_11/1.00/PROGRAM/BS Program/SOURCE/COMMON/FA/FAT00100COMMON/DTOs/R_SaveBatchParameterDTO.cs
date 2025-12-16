using System.Collections.Generic;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for R_SaveBatch method - Expense Allocation
    /// Used by BatchViewModel to call batch processing
    /// </summary>
    public class R_SaveBatchParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public R_SaveBatchUserParameterDTO UserParameters { get; set; } = new R_SaveBatchUserParameterDTO();
        public List<FAT0010002CommonDTO>? Data { get; set; }
    }
}

