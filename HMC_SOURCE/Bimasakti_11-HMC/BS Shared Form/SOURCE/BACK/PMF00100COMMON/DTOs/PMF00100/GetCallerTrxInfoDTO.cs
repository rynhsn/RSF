using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00100
{
    public class GetCallerTrxInfoDTO
    {
        public string CCURRENCY_CODE { get; set; } = "";
        public string CTRANSACTION_NAME { get; set; } = "";
        public decimal NAR_REMAINING { get; set; } = 0;
        public decimal NTAX_REMAINING { get; set; } = 0;
        public decimal NTOTAL_REMAINING { get; set; } = 0;
        public decimal NTAX_CURRENCY_RATE { get; set; } = 0;
        public decimal NTAX_BASE_RATE { get; set; } = 0;
        public decimal NLAR_REMAINING { get; set; } = 0;
        public decimal NLTAX_REMAINING { get; set; } = 0;
        public decimal NLTOTAL_REMAINING { get; set; } = 0;
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public decimal NBAR_REMAINING { get; set; } = 0;
        public decimal NBTAX_REMAINING { get; set; } = 0;
        public decimal NBTOTAL_REMAINING { get; set; } = 0;
        public decimal NBBASE_RATE { get; set; } = 0;
        public decimal NBCURRENCY_RATE { get; set; } = 0;
    }
}
