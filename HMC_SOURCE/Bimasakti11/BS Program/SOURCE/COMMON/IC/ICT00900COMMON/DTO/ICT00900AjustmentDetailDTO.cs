using System;
using System.Collections.Generic;
using System.Text;

namespace ICT00900COMMON.DTO
{
    public class ICT00900AjustmentDetailDTO : ICT00900AdjustmentDTO
    {
        public string? CPRODUCT_ID { get; set; }
        public string? CADJUST_METHOD_DESCR { get; set; }
        public string? CTRANS_CODE_DESCR { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public decimal NLBASE_RATE	 { get; set; }
        public string? NLCURRENCY_RATE_CODE { get; set; }
        public string? CLOCAL_CURRENCY_CODE { get; set; }
        public decimal NBCURRENCY_RATE_CODE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public string? CBASE_CURRENCY_CODE { get; set; }
        public string? CNOTES { get; set; }
    }
}
