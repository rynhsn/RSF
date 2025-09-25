using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110Print
{
    public class APT00110PrintReportResultDTO
    {
        public APT00110PrintReportColumnDTO Column { get; set; }
        public APT00110PrintReportHeaderDTO Data { get; set; }
        public List<APT00110PrintReportSubDetailDTO> SubDetail { get; set; }
    }

    public class APT00110PrintReportResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public APT00110PrintReportResultDTO ReportData { get; set; }
    }
}
