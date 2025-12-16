namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetDataGrid streaming method
    /// </summary>
    public class FAT00100GetDataGridResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CTRANSACTION_DATE { get; set; } = string.Empty;
        public string CTRANSACTION_DATE_DISPLAY { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CTRANSACTION_PRD { get; set; } = string.Empty;
        public string CSTATUS_DESC { get; set; } = string.Empty;
    }
}

