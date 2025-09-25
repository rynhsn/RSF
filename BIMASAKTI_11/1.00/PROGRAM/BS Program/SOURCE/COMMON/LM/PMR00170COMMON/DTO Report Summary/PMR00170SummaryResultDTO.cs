using System;
using System.Collections.Generic;
using System.Text;
using BaseHeaderReportCOMMON;

namespace PMR00170COMMON
{
    public class PMR00170SummaryResultDTO
    {
        public string? Title { get; set; }
        public PMR00170ColumnSummaryDTO? ColumnSummary { get; set; }
        public PMR00170DataHeaderDTO? Header { get; set; }
        public PMR00170LabelDTO? Label { get; set; }
        public List<PMR00170DataTransactionDTO>? Data { get; set; }
    }
    public class PMR00170SummaryResultWithHeaderDTO : BaseHeaderResult
    {
        public PMR00170SummaryResultDTO? PMR00170SummaryResulDataFormatDTO { get; set; }
    }
}
