namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Result DTO for FAT01100GetTransCodeInfo method (RSP_GS_GET_TRANS_CODE_INFO)
    /// </summary>
    public class FAT01100GetTransCodeInfoResultDTO
    {
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;
        public string CMODULE_ID { get; set; } = string.Empty;
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CDEPT_DELIMITER { get; set; } = string.Empty;
        public bool LTRANSACTION_MODE { get; set; }
        public string CTRANSACTION_DELIMITER { get; set; } = string.Empty;
        public string CPERIOD_MODE { get; set; } = string.Empty;
        public string CPERIOD_DELIMITER { get; set; } = string.Empty;
        public string CYEAR_FORMAT { get; set; } = string.Empty;
        public int INUMBER_LENGTH { get; set; }
        public string CNUMBER_DELIMITER { get; set; } = string.Empty;
        public string CPREFIX { get; set; } = string.Empty;
        public string CPREFIX_DELIMITER { get; set; } = string.Empty;
        public string CSUFFIX { get; set; } = string.Empty;
        public string CSEQUENCE01 { get; set; } = string.Empty;
        public string CSEQUENCE02 { get; set; } = string.Empty;
        public string CSEQUENCE03 { get; set; } = string.Empty;
        public string CSEQUENCE04 { get; set; } = string.Empty;
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LUSE_THIRD_PARTY { get; set; }
        public string CAPPROVAL_MODE { get; set; } = string.Empty;
        public string CAPPROVAL_MODE_DESCR { get; set; } = string.Empty;
        public bool LAPPROVAL_DEPT { get; set; }
        public string CTABLE_NAME { get; set; } = string.Empty;
        public string CAFTER_APPROVAL_STATUS { get; set; } = string.Empty;
        public string CPROGRAM_ID { get; set; } = string.Empty;
        public string CREPORT_ID { get; set; } = string.Empty;
        public string CTHIRD_PARTY_VIEW_URL { get; set; } = string.Empty;
        public string CTHIRD_PARTY_API_URL { get; set; } = string.Empty;
    }
}
