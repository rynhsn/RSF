using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01050ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMM01050DTO HeaderData { get; set; }
        public List<PMM01051DTO> DetailWKData { get; set; }
        public List<PMM01051DTO> DetailWDData { get; set; }
    }

    public class PMM01050ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMM01050ResultPrintDTO RateOT { get; set; }
        public PMM01050ColumnPrintDTO Column { get; set; }
    }
}
