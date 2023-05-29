using R_APICommonDTO;

namespace GSM05000Common.DTOs
{
    public class GSM05000NumberingHeaderDTO : R_APIResultBaseDTO
    {
        public string CTRANSACTION_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CPERIOD_MODE { get; set; }
        public string CPERIOD_MODE_DESCR { get; set; }
    }
}