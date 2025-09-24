using PMR00220COMMON.Print_DTO.Detail.SubDetail;
using System.Collections.Generic;

namespace PMR00220COMMON.Print_DTO.Detail
{
    public class PMR00221ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00220LabelDTO Column { get; set; }
        public PMR00220ParamDTO HeaderParam { get; set; }
        public List<TransactionDTO> Data { get; set; }
    }
}
