using System;

namespace GSM02000Common.DTOs
{
    public class GSM02000DTO
    {
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string LACTIVE { get; set; }
        public string CDESCRIPTION { get; set; }
        public decimal NTAX_PERCENTAGE { get; set; }
        public string CROUNDING_MODE { get; set; }
        public int IROUNDING { get; set; } = 0;
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
    }
}