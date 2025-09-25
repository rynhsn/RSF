using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class JournalCommitApprovalPARAM
    {
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CAPPROVE_BY { get; set; }
        public string CJRN_ID_LIST { get; set; }
        public string CNEW_STATUS { get; set; }
        public bool LAUTO_COMMIT { get; set; }
        public bool LUNDO_COMMIT { get; set; }
        public EModeCmmtAprvJRN EMODE { get; set; }
    }
    public enum EModeCmmtAprvJRN {
        Approval,
        Commit
    }
}
