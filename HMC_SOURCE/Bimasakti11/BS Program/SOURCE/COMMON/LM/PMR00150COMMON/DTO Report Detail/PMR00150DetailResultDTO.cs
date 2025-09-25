using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON.DTO_Report_Detail
{
    public class PMR00150DetailResultDTO
    {
        public string? Title { get; set; }
        public PMR00150ColumnDetailDTO? ColumnDetail { get; set; }
        public PMR00150DataHeaderDTO? Header { get; set; }
        public PMR00150LabelDTO? Label { get; set; }
        public PMR00150ParameterVisibleDTO? ParameterVisible { get; set; }
        public List<PMR00150DataDetailTransactionDTO>? Data { get; set; }
    }
    public class PMR00150DetailResultWithHeaderDTO : BaseHeaderResult
    {
        public PMR00150DetailResultDTO? PMR00150DetailResultDataFormatDTO { get; set; }
    }
}
