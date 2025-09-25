using System;

namespace Lookup_HDCOMMON.DTOs.HDL00500
{
    public class HDL00500DTO
    {
        public DateTime? CSTART_DISPLAY {get; set;}
        public DateTime? CEND_DATE_DISPLAY {get; set;}
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTASK_CODE { get; set; } = "";
        public string CTASK_NAME { get; set; } = "";
        public int IDURATION_TIME { get; set; } = 0;
        public string CDURATION_TYPE_DESCR { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CCALL_TYPE_NAME { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
    }
}