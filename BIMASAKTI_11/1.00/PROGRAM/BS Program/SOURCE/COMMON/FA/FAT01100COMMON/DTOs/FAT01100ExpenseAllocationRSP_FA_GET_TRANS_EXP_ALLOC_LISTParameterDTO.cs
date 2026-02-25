using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPARENT_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
