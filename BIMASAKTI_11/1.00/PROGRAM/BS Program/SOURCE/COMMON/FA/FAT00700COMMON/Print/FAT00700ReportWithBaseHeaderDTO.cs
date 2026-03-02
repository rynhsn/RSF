using System.Collections.Generic;
using BaseHeaderReportCOMMON;

namespace FAT00700Common.Print
{
    public class FAT00700ReportWithBaseHeaderDTO : BaseHeaderResult
    {
        public List<FAT00700PrintDataDTO> Data { get; set; } = new List<FAT00700PrintDataDTO>();
        public FAT00700ColumnPrintDTO Column { get; set; } = new FAT00700ColumnPrintDTO();
        public FAT00700LabelDTO Label { get; set; } = new FAT00700LabelDTO();
        public string Title { get; set; } = string.Empty;
    }
}

