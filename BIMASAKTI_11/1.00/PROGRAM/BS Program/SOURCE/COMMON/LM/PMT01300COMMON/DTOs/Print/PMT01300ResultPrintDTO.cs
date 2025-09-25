using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01300ResultPrintDTO
    {
        public PMT01300HeaderPrintDTO Header { get; set; }
        public List<PMT01300DetailPrintDTO> Detail { get; set; }
        public PMT01300FooterPrintDTO Footer { get; set; }
        public List<PMT01300SubDetailPrintDTO> SubDetail { get; set; }
    }

    public class PMT01300ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMT01300ResultPrintDTO Invoice { get; set; }
        public PMT01300ColumnPrintDTO Column {  get; set; }
    }
}
