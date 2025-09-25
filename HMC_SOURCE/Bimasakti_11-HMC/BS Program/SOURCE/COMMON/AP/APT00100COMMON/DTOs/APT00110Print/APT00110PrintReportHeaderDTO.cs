using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportHeaderDTO
    {
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime DDOC_DATE { get; set; }
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CSUPPLIER_ADDRESS { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CPAY_TERM_NAME { get; set; } = "";
        public string CSUPPLIER_PHONE1 { get; set; } = "";
        public string CSUPPLIER_FAX1 { get; set; } = "";
        public string CSUPPLIER_EMAIL1 { get; set; } = "";
        public string CJRN_ID { get; set; } = "";
        public APT00110PrintReportFooterDTO FooterData { get; set; }
        public List<APT00110PrintReportDetailDTO> DetailData { get; set; }
    }
}
