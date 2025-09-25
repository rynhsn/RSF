using PMR00600COMMON.DTO_s.Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s.PrintDetail
{
    public class PMR00601ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00601LabelDTO Label { get; set; }
        public PMR00601ParamDTO Param { get; set; }
        public List<PMR00601DataDTO> Data { get; set; }
    }
}
