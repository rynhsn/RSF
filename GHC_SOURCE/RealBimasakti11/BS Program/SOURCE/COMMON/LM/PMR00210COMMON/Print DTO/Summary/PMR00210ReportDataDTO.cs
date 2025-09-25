using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PMR00210COMMON.Print_DTO
{
    public class PMR00210ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00210LabelDTO Column { get; set; }
        public PMR00210ParamDTO HeaderParam{ get; set; }
        public List<PMR00210DataDTO> Data { get; set; }

    }


}
