using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610Print
{
    public class PMT50610PrintReportHeaderDTO
    {
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime DDOC_DATE { get; set; }
        public string CTENANT_NAME { get; set; } = "";
        public string CCUSTOMER_TYPE_NAME { get; set; } = "";
        public string CTENANT_ADDRESS { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CPAY_TERM_NAME { get; set; } = "";
        public string CTENANT_PHONE1 { get; set; } = "";
        public string CTENANT_FAX1 { get; set; } = "";
        public string CTENANT_EMAIL1 { get; set; } = "";
        public string CJRN_ID { get; set; } = "";
        public PMT50610PrintReportFooterDTO FooterData { get; set; }
        public List<PMT50610PrintReportDetailDTO> DetailData { get; set; }
    }
}
