using R_APICommonDTO;

namespace GSM02000Common.DTOs
{
    public class GSM02000ActiveInactiveDTO : R_APIResultBaseDTO
    {
        public string CTAX_ID { get; set; }
        public bool LACTIVE { get; set; }
    }
}