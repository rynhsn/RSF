using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s.Print
{
    public class ReportPrintSummaryDTO : BaseHeaderResult
    {
        public ReportSummaryDataDTO ReportData { get; set; }
    }
}
