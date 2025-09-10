using System;
using System.Collections.Generic;
using System.Text;
using BaseHeaderReportCOMMON;

namespace PMR00150COMMON
{
    public class PMR00150SummaryResultDTO
    {
        public string? Title { get; set; }
        public PMR00150ColumnSummaryDTO? ColumnSummary { get; set; }
        public PMR00150DataHeaderDTO? Header { get; set; }
        public PMR00150LabelDTO? Label { get; set; }
        public List<PMR00150DataTransactionDTO>? Data { get; set; }
    }
    public class PMR00150SummaryResultWithHeaderDTO : BaseHeaderResult
    {
        public PMR00150SummaryResultDTO? PMR00150SummaryResulDataFormatDTO { get; set; }
    }
}
