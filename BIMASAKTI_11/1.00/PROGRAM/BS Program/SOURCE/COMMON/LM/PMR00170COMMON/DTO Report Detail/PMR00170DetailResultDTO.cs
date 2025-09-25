using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON.DTO_Report_Detail
{
    public class PMR00170DetailResultDTO
    {
        public string? Title { get; set; }
        public PMR00170ColumnDetailDTO? ColumnDetail { get; set; }
        public PMR00170DataHeaderDTO? Header { get; set; }
        public PMR00170LabelDTO? Label { get; set; }
        public PMR00170ParameterVisibleDTO? ParameterVisible { get; set; }
        public List<PMR00170DataDetailTransactionDTO>? Data { get; set; }
    }
    public class PMR00170DetailResultWithHeaderDTO : BaseHeaderResult
    {
        public PMR00170DetailResultDTO? PMR00170DetailResultDataFormatDTO { get; set; }
    }
}
