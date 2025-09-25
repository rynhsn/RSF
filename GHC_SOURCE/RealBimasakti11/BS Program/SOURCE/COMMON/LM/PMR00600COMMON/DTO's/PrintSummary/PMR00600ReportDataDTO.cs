using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s.Print
{
    public class PMR00600ReportDataDTO
    {
        public string Title {  get; set; }
        public string Header { get; set; }
        public PMR00600LabelDTO Label { get; set; }
        public PMR00600ParamDTO Param { get; set; }
        public List<PMR00600DataDTO> Data { get; set; }
    }
}
