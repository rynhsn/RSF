using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00800COMMON.DTO_s.Print
{
    public class PMR00800ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00800LabelDTO Label { get; set; }
        public PMR00800ParamDTO Param { get; set; }
        public List<PMR00800SpResultDTO> Data { get; set; }
    }
}
