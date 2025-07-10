using System.Collections.Generic;

namespace PMF00200ReportCommon
{
    public class PMF00200ResultPrintDTO
    {
        public PMF00200HeaderPrintDTO Header { get; set; }
        public List<PMF00201DTO> AllocList { get; set; }
        public List<PMF00202DTO> JournalList { get; set; }
    }

    public class PMF00200ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMF00200ResultPrintDTO Data { get; set; }
        public PMF00200ColumnPrintDTO Column {  get; set; }
    }
}
