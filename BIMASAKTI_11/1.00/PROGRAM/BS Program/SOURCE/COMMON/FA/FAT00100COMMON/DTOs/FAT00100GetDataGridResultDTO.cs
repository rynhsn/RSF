using System;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Result DTO for GetDataGrid streaming method - matches RSP_FAT00100_GET_TRANS_LIST stored procedure result
    /// </summary>
    public class FAT00100GetDataGridResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREFERENCE_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CREF_DATE_DISPLAY { get; set; } = string.Empty; // Computed display property
        public DateTime? DREF_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; } = string.Empty;
        public string CSUPPLIER_NAME { get; set; } = string.Empty;
        public string CSUPPLIER_ID_NAME { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
    }
}

