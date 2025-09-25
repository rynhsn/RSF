using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01000ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01000ColumnPrintDTO Column { get; set; }
        public List<PMM01000TopPrintDTO> Data { get; set; }
    }

    public class PMM01000ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01000ResultPrintDTO UtilitiesData { get; set; }
    }
}
