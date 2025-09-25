using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PMR00220COMMON.Print_DTO
{
    public class PMR00220ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00220LabelDTO Column { get; set; }
        public PMR00220ParamDTO HeaderParam{ get; set; }
        public List<PMR00220DataDTO> Data { get; set; }

    }


}
