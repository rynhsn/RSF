using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO
    {
        public string CEXPENSE_DEPT_CODE { get; set; } = string.Empty;
        public string CEXPENSE_DEPT_NAME { get; set; } = string.Empty;
        public decimal NEXPENSE_PCT { get; set; }
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
    }
}
