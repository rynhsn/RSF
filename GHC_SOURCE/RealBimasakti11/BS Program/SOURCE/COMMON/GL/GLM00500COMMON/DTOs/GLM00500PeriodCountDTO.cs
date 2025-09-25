using R_APICommonDTO;

namespace GLM00500Common.DTOs
{
    public class GLM00500PeriodCountDTO : R_APIResultBaseDTO
    {
        public int INO_PERIOD { get; set; }
    }
    
    public class GLM00500PeriodInfoDTO
    {
        public string CYEAR { get; set; }
        public bool LPERIOD_MODE { get; set; }
        public int INO_PERIOD { get; set; }
        public bool LVALID { get; set; }
    }
}