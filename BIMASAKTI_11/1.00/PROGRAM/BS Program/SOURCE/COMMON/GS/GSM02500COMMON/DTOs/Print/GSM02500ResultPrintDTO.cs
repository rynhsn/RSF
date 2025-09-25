using System.Collections.Generic;

namespace GSM02500COMMON.DTOs
{
    public class GSM02500ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public List<GSM02500ResultPrintSPDTO> Data { get; set; }
    }

    public class GSM02500ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GSM02500ResultPrintDTO PropertyProfile { get; set; }
        public GSM02500ColumnDTO Column { get; set; }
    }
}
