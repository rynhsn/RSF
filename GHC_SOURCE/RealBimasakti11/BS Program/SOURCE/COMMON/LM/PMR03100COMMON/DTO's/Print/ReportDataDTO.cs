using PMR03100COMMON.DTO_s.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.DTO_s.Print
{
    public class ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public LabelDTO Label { get; set; }
        public ParamDTO Param { get; set; }
        public List<SPResultDTO> Data { get; set; }
    }
}
