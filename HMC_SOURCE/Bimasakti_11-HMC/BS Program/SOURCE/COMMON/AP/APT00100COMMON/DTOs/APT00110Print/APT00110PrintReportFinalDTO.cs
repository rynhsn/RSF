using APT00100COMMON.DTOs.APT00110Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportFinalDTO
    {
        public APT00110PrintReportHeaderDTO ReportHeader { get; set; }
        public List<APT00110PrintReportDetailDTO> ReportDetail { get; set; }
        public List<APT00110PrintReportSubDetailDTO> ReportSubDetail { get; set; }
        public APT00110PrintReportFooterDTO ReportFooter { get; set; }
    }
}
