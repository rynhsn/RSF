using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00121
{
    public class APT00121HeaderDTO
    {
        public string CPROPERTY_NAME { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CSUPPLIER_SEQ_NO { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; } = "";
        public string CCURRENCY_NAME { get; set; } = "";
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NTAX_BASE_RATE { get; set; } = 0;
        public decimal NTAX_CURRENCY_RATE { get; set; } = 0;
    }
}
