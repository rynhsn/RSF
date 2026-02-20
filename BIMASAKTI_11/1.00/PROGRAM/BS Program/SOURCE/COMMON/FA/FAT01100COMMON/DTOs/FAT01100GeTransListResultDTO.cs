using System;

namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GeTransList method (RSP_FAT01100_GET_TRANS_LIST) - one row
    /// </summary>
    public class FAT01100GeTransListResultDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CREF_DATE { get; set; } = string.Empty;
        public string CTRANS_DESC { get; set; } = string.Empty;
        public string CTRANS_STATUS { get; set; } = string.Empty;
        public string CTRANS_STATUS_NAME { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_NAME { get; set; } = string.Empty;
        public string CCREATE_BY { get; set; } = string.Empty;
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = string.Empty;
        public DateTime DUPDATE_DATE { get; set; }
        public int IQTY { get; set; }
    }
}
