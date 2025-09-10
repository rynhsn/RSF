using System.Collections.Generic;

namespace GSM12000COMMON
{
    public class GSM12000ResultPrintDTO
    {
        public string Title { get; set; }
        public List<GSM12000ResultPrintSPDTO> Data { get; set; }
    }

    public class GSM12000ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GSM12000ResultPrintDTO MessageMaster { get; set; }
        public GSM12000ColumnDTO Column { get; set; }
    }
}
