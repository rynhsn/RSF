using System.Collections.Generic;

namespace FAT00700Common.Print
{
    public class FAT00700ResultPrintDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public List<FAT00700PrintDataDTO> Data { get; set; }
    }

    public class FAT00700ResultWithBaseHeaderPrintDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public FAT00700ResultPrintDTO Data { get; set; }
        public FAT00700ColumnPrintDTO Column { get; set; }
        public FAT00700LabelDTO Label { get; set; }

    }
}
