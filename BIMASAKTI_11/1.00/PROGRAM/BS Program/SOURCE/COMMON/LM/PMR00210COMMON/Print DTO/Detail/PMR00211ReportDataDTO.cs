using PMR00210COMMON.Print_DTO.Detail.SubDetail;
using System.Collections.Generic;

namespace PMR00210COMMON.Print_DTO.Detail
{
    public class PMR00211ReportDataDTO
    {
        public string Title { get; set; }
        public string Header { get; set; }
        public PMR00210LabelDTO Column { get; set; }
        public PMR00210ParamDTO HeaderParam { get; set; }
        public List<TransactionDTO> Data { get; set; }
    }
}
