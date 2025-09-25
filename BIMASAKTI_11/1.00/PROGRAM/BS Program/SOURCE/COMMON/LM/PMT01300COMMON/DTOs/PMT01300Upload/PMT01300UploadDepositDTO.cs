using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300UploadDepositDTO
    {
        public int NO { get; set; } = 0;
        public string CDOC_NO { get; set; } = "";
        public bool LCONTRACTOR { get; set; } = false;
        public string CCONTRACTOR_ID { get; set; } = "";
        public string CDEPOSIT_ID { get; set; } = "";
        public string CDEPOSIT_DATE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NDEPOSIT_AMT { get; set; } = 0;
        public bool LPAID { get; set; } = false;
        public string CDESCRIPTION { get; set; } = "";
        public int ISEQ_NO_ERROR { get; set; } = 0;
        public bool LERROR { get; set; }
    }
}
