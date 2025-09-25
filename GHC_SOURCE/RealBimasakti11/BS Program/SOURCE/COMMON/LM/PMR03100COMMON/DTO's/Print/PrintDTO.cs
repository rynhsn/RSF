using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.DTO_s.Print
{
    public class PrintDTO : BaseHeaderResult
    {
        public ReportDataDTO ReportData { get; set; }
    }
}
