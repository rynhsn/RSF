using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610Print
{
    public class PMT50610PrintReportFooterDTO
    {
        public string CTOTAL_AMOUNT_IN_WORDS { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public decimal NTAXABLE_AMOUNT { get; set; } = 0;
        public decimal NTAX { get; set; } = 0;
        public decimal NOTHER_TAX { get; set; } = 0;
        public decimal NADDITION { get; set; } = 0;
        public decimal NDEDUCTION { get; set; } = 0;
        public decimal NTRANS_AMOUNT { get; set; } = 0;
    }
}
