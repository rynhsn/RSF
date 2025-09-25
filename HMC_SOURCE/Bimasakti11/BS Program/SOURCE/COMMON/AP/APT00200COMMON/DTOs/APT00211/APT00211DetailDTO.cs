using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00211
{
    public class APT00211DetailDTO
    {
        public string CPROD_DEPT_CODE { get; set; } = "";
        public string CPROD_DEPT_NAME { get; set; } = "";
        public string CPRODUCT_ID { get; set; } = "";
        public string CPRODUCT_NAME { get; set; } = "";
        public decimal NDISC_AMOUNT { get; set; } = 0;
        public decimal NDIST_DISCOUNT { get; set; } = 0;
        public decimal NDIST_ADD_ON { get; set; } = 0;
        public decimal NTAX_AMOUNT { get; set; } = 0;
        public decimal NOTHER_TAX_AMOUNT { get; set; } = 0;
        public decimal NLINE_AMOUNT { get; set; } = 0;
    }
}
