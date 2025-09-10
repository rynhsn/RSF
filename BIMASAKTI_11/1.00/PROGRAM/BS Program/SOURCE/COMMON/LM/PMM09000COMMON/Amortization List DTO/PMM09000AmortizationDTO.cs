using PMM09000COMMON.UtiliyDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM09000COMMON.Amortization_List_DTO
{
    public class PMM09000AmortizationDTO : BaseDTO
    {
        public string? CTENANT_NAME { get; set; }
         public string? CTRANS_TYPE { get; set; }
         public string? CREF_NO { get; set; }
        public string? CCHARGES_TYPE_NAME { get; set; }
        public string? CCHARGES_NAME { get; set; }
        public string? CINV_NO { get; set; }
        public string? CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public decimal NCHARGES_AMOUNT { get; set; }
        public string? CGENERATE_MODE { get; set; }
        public string? CSTATUS { get; set; }
    }
}
