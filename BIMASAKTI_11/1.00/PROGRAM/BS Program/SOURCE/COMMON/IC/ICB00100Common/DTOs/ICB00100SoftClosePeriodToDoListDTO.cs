using System;

namespace ICB00100Common.DTOs
{
    public class ICB00100SoftClosePeriodToDoListDTO
    {
        public long ISEQ_NO { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_DESCR { get; set; }
        public string CMODULE { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_DESCR { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CSOLUTION_DESCR { get; set; }
    }
}