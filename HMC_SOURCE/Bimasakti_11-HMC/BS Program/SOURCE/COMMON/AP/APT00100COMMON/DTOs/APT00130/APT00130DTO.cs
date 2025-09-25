using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00130
{
    public class APT00130DTO
    {
        public string CREC_ID { get; set; } = "";
        public decimal NAMOUNT { get; set; } = 0;
        public decimal NDISCOUNT { get; set; } = 0;
        public bool LSUMMARY_DISCOUNT { get; set; } = false;
        public string CSUMMARY_DISC_TYPE { get; set; } = "";
        public decimal NSUMMARY_DISC_PCT { get; set; } = 0;
        public decimal NSUMMARY_DISCOUNT { get; set; } = 0;
        public decimal NADD_ON { get; set; } = 0;
        public decimal NTAXABLE_AMOUNT { get; set; } = 0;
        public decimal NTAX { get; set; } = 0;
        public decimal NOTHER_TAX { get; set; } = 0;
        public decimal NADDITION { get; set; } = 0;
        public decimal NDEDUCTION { get; set; } = 0;
        public decimal NTOTAL_AMOUNT { get; set; } = 0;
        public string CCURRENCY_CODE { get; set; } = "";
    }
}
