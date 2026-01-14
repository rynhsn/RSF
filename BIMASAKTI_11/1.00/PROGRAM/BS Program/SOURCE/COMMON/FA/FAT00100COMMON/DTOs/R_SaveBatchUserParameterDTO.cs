namespace FAT00100Common.DTOs
{
    /// <summary>
    /// User Parameter DTO for R_SaveBatch method - Expense Allocation
    /// Contains all custom user parameters needed by _BatchProcessAsync (excluding CCOMPANY_ID and CUSER_ID)
    /// </summary>
    public class R_SaveBatchUserParameterDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CTRANS_SEQ_NO { get; set; } = string.Empty;
        public string CPARENT_ID { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
    }
}

