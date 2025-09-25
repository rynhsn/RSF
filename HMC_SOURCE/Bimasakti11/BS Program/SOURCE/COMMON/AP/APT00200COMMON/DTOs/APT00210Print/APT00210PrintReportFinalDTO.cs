using APT00200COMMON.DTOs.APT00210Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210Print
{
    public class APT00210PrintReportFinalDTO
    {
        public APT00210PrintReportHeaderDTO ReportHeader { get; set; }
        public List<APT00210PrintReportDetailDTO> ReportDetail { get; set; }
        public List<APT00210PrintReportSubDetailDTO> ReportSubDetail { get; set; }
        public APT00210PrintReportFooterDTO ReportFooter { get; set; }
    }
}
