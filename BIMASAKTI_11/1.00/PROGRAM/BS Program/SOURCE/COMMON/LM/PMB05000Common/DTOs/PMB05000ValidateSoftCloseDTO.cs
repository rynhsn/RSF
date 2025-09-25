using System;

namespace PMB05000Common.DTOs
{
    public class PMB05000ValidateSoftCloseDTO
    {
        public long INO { get; set; }
        public string CDEPARTMENT { get; set; } = "";
        public string CTRANSACTION_NAME { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CSTATUS_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public string CSOLUTION { get; set; } = "";
    }
}