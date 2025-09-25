using System.Collections.Generic;

namespace PMB04000COMMONPrintBatch
{
    public class PMB04000ResultDataDTO
    {
        public PMB04000ColumnDTO? Column { get; set; }
        public PMB04000LabelDTO? Label { get; set; }
        public PMB04000BaseHeaderDTO? Header { get; set; }
        public List<PMB04000DataReportDTO>? Data { get; set; }
    }
}
