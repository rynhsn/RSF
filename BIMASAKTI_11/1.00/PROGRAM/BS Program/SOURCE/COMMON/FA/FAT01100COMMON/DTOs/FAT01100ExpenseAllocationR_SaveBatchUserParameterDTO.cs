using System;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationR_SaveBatchUserParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
        public string CPARENT_ID { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
    }
}
