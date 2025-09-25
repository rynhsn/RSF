using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s
{
    public class TotalDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public decimal TotalBeginningApplyAmount { get; set; }
        public decimal TotalRemainingAmount { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal TotalGainLossAmount { get; set; }
        public decimal TotalCashBankAmount { get; set; }
    }
}
