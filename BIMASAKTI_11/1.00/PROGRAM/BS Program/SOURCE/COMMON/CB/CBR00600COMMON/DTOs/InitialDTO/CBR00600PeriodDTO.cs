using R_APICommonDTO;

namespace CBR00600COMMON
{
    public class CBR00600PeriodDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }

        // result
        public string CCYEAR { get; set; } 
        public string CPERIOD_NO { get; set; }
        public string CPERIOD_NO_DISPLAY { get; set; }
    }
}
