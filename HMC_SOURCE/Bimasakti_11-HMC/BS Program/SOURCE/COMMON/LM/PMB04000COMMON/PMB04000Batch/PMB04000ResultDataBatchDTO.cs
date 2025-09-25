using PPMB04000COMMMONPrintBatch.Distribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMMONPrintBatch.Distribute
{
    public class PMB04000ResultDataBatchDTO
    {
        public PMB04000ColumnDTO? Column { get; set; }
        public PMB04000LabelDTO? Label { get; set; }
        public PMB04000BaseHeaderDTO? Header { get; set; }
        public List<PMB04000DataReportBatchDTO>? Data { get; set; }
    }
}
