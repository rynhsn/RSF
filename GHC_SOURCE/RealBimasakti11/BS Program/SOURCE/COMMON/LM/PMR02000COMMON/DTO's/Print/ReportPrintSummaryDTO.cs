using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print
{
    public class ReportPrintSummaryDTO : BaseHeaderResult
    {
        public ReportSummaryDataDTO ReportData { get; set; }
    }
}
