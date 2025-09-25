using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200GSTransInfoDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_ID { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CDEPT_DELIMITER { get; set; }
        public bool LTRANSACTION_MODE { get; set; }
        public string CTRANSACTION_DELIMITER { get; set; }
        public string CPERIOD_MODE { get; set; }
        public string CPERIOD_DELIMITER { get; set; }
        public string CYEAR_FORMAT { get; set; }
        public int INUMBER_LENGTH { get; set; }
        public string CNUMBER_DELIMITER { get; set; }
        public string CPREFIX { get; set; }
        public string CPREFIX_DELIMITER { get; set; }
        public string CSUFFIX { get; set; }
        public string CSEQUENCE01 { get; set; }
        public string CSEQUENCE02 { get; set; }
        public string CSEQUENCE03 { get; set; }
        public string CSEQUENCE04 { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LUSE_THIRD_PARTY { get; set; }
        public string CAPPROVAL_MODE { get; set; }
        public string CAPPROVAL_MODE_DESCR { get; set; }
        public bool LAPPROVAL_DEPT { get; set; }
        public string CTABLE_NAME { get; set; }
        public string CAFTER_APPROVAL_STATUS { get; set; }
        public string CPROGRAM_ID { get; set; }
        public string CREPORT_ID { get; set; }
        public string CTHIRD_PARTY_VIEW_URL { get; set; }
        public string CTHIRD_PARTY_API_URL { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
