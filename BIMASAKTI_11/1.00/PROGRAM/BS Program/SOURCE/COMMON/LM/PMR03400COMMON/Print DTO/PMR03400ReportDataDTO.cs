using PMR03400COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.Print_DTO
{
    public class PMR03400ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR03400LabelDTO Column { get; set; }
        public PMR03400ParamDTO HeaderParam { get; set; }
        public List<PMR03400SPResultDTO> Data { get; set; }
    }
}
