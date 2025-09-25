using PMR00200COMMON.Print_DTO.Detail.SubDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO.Detail
{
    public class PMR00210ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00200LabelDTO Column { get; set; }
        public PMR00200ParamDTO HeaderParam { get; set; }
        public List<TransactionDTO>? Data { get; set; }
    }
}
