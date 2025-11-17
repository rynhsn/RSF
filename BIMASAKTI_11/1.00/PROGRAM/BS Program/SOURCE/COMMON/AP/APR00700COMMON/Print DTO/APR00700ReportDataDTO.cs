using APR00700COMMON.DTO_s;
using APR00700OMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00700COMMON.Print_DTO
{
    public class APR00700ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public APR00700LabelDTO Column { get; set; }
        public APR00700ParamDTO HeaderParam { get; set; }
        public APR00700SPResultDTO HeaderData { get; set; }
        public List<APR00700SPResultDTO> Data { get; set; }
    }
}
