using System.Collections.Generic;

namespace PMR00400Common.DTOs.Print
{
    public class PMR00400ReportResultDTO
    {
        public string Title { get; set; }

        public PMR00400LabelDTO Label { get; set; }

        public PMR00400ParamDTO Header { get; set; }
        public List<PMR00400DataDTO> Data { get; set; }
    }
    
    
    public class PMR00400ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMR00400ReportResultDTO Data { get; set; }
    }
}