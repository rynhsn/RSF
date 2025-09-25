using System;

namespace Lookup_TXCOMMON.DTOs.TXL00200
{
    public class TXL00200DTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CBRANCH_CODE { get; set; }
        public string? CBOOKING_TAX_NO { get; set; }
        public string? CTAX_NO_STATUS { get; set; }
        public string? CTAX_NO_STATUS_DESCR { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
