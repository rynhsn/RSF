using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportDetailDTO
    {
        public int INO { get; set; } = 0;
        public string CPRODUCT_ID { get; set; } = "";
        public string CPRODUCT_NAME { get; set; } = "";
        public string CDETAIL_DESC { get; set; } = "";
        public decimal NTRANS_QTY { get; set; } = 0;
        public string CUNIT { get; set; } = "";
        public decimal NUNIT_PRICE { get; set; } = 0;
        public decimal NLINE_AMOUNT  { get; set; } = 0;
        public decimal NTOTAL_DISCOUNT { get; set; } = 0;
        public decimal NDIST_ADD_ON { get; set; } = 0;
        public decimal NLINE_TAXABLE_AMOUNT { get; set; } = 0;
    }
}
