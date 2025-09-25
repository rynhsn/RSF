using BaseHeaderReportCOMMON;
using PMR02000COMMON.DTO_s.Print.Grouping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print
{
    public class ReportDetailDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public ReportLabelDTO Label { get; set; }
        public ReportParamDTO Param { get; set; }
        public List<DeptDTO> Data { get; set; }
        public List<SubtotalCurrenciesDTO> GrandTotal { get; set; }
    }
}
