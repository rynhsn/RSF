using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210Print
{
    public class APT00210PrintReportHeaderDTO
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
        public APT00210PrintReportFooterDTO FooterData { get; set; }
        public List<APT00210PrintReportDetailDTO> DetailData { get; set; }
    }
}
