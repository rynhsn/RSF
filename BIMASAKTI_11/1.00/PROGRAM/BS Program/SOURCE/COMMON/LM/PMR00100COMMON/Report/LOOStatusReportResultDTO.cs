using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.Report
{
    public class LOOStatusReportResultDTO
    {
        public string title { get; set; }
        public LOOStatusHeaderDTO Header { get; set; }
        public LOOStatusColumnDTO Column { get; set; }
        public List<LOOStatusDetail1DTO> DataLOOStatus { get; set; }

    }
    public class PMR00100LOOStatusResultWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public LOOStatusReportResultDTO PMR00100PrintData { get; set; }
    }
}
