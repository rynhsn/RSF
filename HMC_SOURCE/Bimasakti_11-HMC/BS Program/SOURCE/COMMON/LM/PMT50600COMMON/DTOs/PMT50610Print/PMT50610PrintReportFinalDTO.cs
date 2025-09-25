using PMT50600COMMON.DTOs.PMT50610Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610Print
{
    public class PMT50610PrintReportFinalDTO
    {
        public PMT50610PrintReportHeaderDTO ReportHeader { get; set; }
        public List<PMT50610PrintReportDetailDTO> ReportDetail { get; set; }
        public List<PMT50610PrintReportSubDetailDTO> ReportSubDetail { get; set; }
        public PMT50610PrintReportFooterDTO ReportFooter { get; set; }
    }
}
