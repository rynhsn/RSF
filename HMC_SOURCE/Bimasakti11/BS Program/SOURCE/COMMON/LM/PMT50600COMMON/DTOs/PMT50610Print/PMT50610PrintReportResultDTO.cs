using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610Print
{
    public class PMT50610PrintReportResultDTO
    {
        public PMT50610PrintReportColumnDTO Column { get; set; }
        public PMT50610PrintReportHeaderDTO Data { get; set; }
        public List<PMT50610PrintReportSubDetailDTO> SubDetail { get; set; }
    }

    public class PMT50610PrintReportResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMT50610PrintReportResultDTO ReportData { get; set; }
    }
}
