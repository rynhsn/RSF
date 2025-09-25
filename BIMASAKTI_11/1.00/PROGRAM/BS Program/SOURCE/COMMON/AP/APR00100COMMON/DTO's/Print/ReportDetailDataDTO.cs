using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s.Print
{
    public class ReportDetailDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ReportLabelDTO Label { get; set; }
        public ReportParamDTO Param { get; set; }
        public List<DetailDataDTO> Data { get; set; }
    }
}
