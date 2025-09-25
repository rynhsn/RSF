using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s.Print
{
    public class ReportSummaryDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ReportLabelDTO Label { get; set; }
        public ReportParamDTO Param { get; set; }
        public List<APR00100DataResultDTO> Data { get; set; }
        public List<APR00100SummaryByDateDTO> SummaryDate { get; set; }
        
    }
}
