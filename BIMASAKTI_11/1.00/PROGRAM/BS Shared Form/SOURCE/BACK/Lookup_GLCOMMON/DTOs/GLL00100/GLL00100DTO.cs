using System;

namespace Lookup_GLCOMMON.DTOs.GLL00100
{
    public class GLL00100DTO
    {
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CCURRENCY { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CSTATUS_NAME { get; set; }
    }
}
