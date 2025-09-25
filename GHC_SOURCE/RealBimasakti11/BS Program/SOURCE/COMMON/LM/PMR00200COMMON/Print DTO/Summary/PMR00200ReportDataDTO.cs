using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PMR00200COMMON.Print_DTO
{
    public class PMR00200ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00200LabelDTO Column { get; set; }
        public PMR00200ParamDTO HeaderParam{ get; set; }
        public List<PMR00200DataDTO> Data { get; set; }

    }


}
