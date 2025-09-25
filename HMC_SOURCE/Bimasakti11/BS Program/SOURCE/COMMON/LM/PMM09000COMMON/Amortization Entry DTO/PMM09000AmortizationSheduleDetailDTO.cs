using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.UtiliyDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Amortization_Entry_DTO
{
    public class PMM09000AmortizationSheduleDetailDTO : BaseDTO
    {
        public string? CCHARGES_TYPE { get; set; }
        public string? CCHARGES_TYPE_NAME { get; set; }
        public string? CCHARGES_ID { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CPERIOD { get; set; }
        public decimal NCHARGES_AMOUNT { get; set; }
        public decimal NAMOUNT { get; set; } = 0;
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CSTATUS_DESCR { get; set; }
    }
}
