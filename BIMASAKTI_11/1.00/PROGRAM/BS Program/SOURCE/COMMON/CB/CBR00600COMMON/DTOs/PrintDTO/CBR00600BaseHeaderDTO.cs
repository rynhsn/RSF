using System;

namespace CBR00600COMMON
{
    public class CBR00600BaseHeaderDTO
    {
        public byte[] BLOGO_COMPANY { get; set; }
        public string CPRINT_CODE { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public DateTime DPRINT_DATE_COMPANY { get; set; }
        public string CPRINT_NAME { get; set; }
        public string CUSER_ID { get; set; }
    }
}
