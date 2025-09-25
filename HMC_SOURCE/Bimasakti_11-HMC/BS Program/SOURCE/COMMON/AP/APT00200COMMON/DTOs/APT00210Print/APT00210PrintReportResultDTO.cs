using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210Print
{
    public class APT00210PrintReportResultDTO
    {
        public APT00210PrintReportColumnDTO Column { get; set; }
        public APT00210PrintReportHeaderDTO Data { get; set; }
        public List<APT00210PrintReportSubDetailDTO> SubDetail { get; set; }
    }

    public class APT00210PrintReportResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public APT00210PrintReportResultDTO ReportData { get; set; }
    }
}
