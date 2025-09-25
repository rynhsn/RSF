using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetAgreementFormListDTO
    {
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CDEPT_NAME { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CAGREEMENT_NO { get; set; } = string.Empty;
        public string CUNIT_DESCRIPTION { get; set; } = string.Empty;
        public string CSTART_DATE { get; set; } = string.Empty;
        public DateTime? DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; } = string.Empty;
        public DateTime? DEND_DATE { get; set; }
        public int IEXPIRED_DAYS { get; set; } = 0;
        public string CEXPIRED_DAYS { get; set; } = string.Empty;
        public string CTENANT { get; set; } = string.Empty;
        public string CDOCUMENT_STATUS { get; set; } = string.Empty;
        public string CHO_ACTUAL_DATE { get; set; } = string.Empty;
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public bool LFORCED_HO { get; set; }
        public string CHO_DEPT_CODE { get; set; } = string.Empty;
        public string CHO_TRANS_CODE { get; set; } = string.Empty;
        public string CHO_REF_NO { get; set; } = string.Empty;
        public bool LHO_CHECKLIST { get; set; } = false;

    }
}
