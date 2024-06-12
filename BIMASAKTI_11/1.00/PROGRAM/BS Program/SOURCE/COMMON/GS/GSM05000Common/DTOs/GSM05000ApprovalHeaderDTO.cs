using R_APICommonDTO;

namespace GSM05000Common.DTOs
{
    public class GSM05000ApprovalHeaderDTO : R_APIResultBaseDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public bool LINCREMENTAL_FLAG { get; set; }
        public bool LAPPROVAL_FLAG { get; set; }
        public bool LAPPROVAL_DEPT { get; set; }
        public string CAPPROVAL_MODE { get; set; }
        public string CAPPROVAL_MODE_DESCR { get; set; }
    }
}