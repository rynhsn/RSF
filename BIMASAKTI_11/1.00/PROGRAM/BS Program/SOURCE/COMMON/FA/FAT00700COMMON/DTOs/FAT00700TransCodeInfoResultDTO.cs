using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00700Common.DTOs
{
    public class FAT00700TransCodeInfoResultDTO
    {
        // C = string (default string.Empty)
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CTRANSACTION_NAME { get; set; } = string.Empty;
        public string CMODULE_ID { get; set; } = string.Empty;
        public string CDEPT_DELIMITER { get; set; } = string.Empty;
        public string CTRANSACTION_DELIMITER { get; set; } = string.Empty;
        public string CPERIOD_MODE { get; set; } = string.Empty;
        public string CPERIOD_DELIMITER { get; set; } = string.Empty;
        public string CYEAR_FORMAT { get; set; } = string.Empty;
        public string CNUMBER_DELIMITER { get; set; } = string.Empty;
        public string CPREFIX { get; set; } = string.Empty;
        public string CPREFIX_DELIMITER { get; set; } = string.Empty;
        public string CSUFFIX { get; set; } = string.Empty;
        public string CSEQUENCE01 { get; set; } = string.Empty;
        public string CSEQUENCE02 { get; set; } = string.Empty;
        public string CSEQUENCE03 { get; set; } = string.Empty;
        public string CSEQUENCE04 { get; set; } = string.Empty;
        public string CAPPROVAL_MODE { get; set; } = string.Empty;
        public string CAPPROVAL_MODE_DESCR { get; set; } = string.Empty;
        public string CTABLE_NAME { get; set; } = string.Empty;
        public string CAFTER_APPROVAL_STATUS { get; set; } = string.Empty;
        public string CPROGRAM_ID { get; set; } = string.Empty;
        public string CREPORT_ID { get; set; } = string.Empty;
        public string CTHIRD_PARTY_VIEW_URL { get; set; } = string.Empty;
        public string CTHIRD_PARTY_API_URL { get; set; } = string.Empty;

        // L = bool
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public bool LTRANSACTION_MODE { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LUSE_THIRD_PARTY { get; set; }
        public bool LAPPROVAL_DEPT { get; set; }

        // I = int
        public int INUMBER_LENGTH { get; set; }
    }
}
