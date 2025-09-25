using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportParameterDTO
    {
        public List<PMT02120FilteredHandoverDTO> FilteredHandoverData { get; set; }
        public string CCOMPANY_ID { get; set; }
        public bool LASSIGNMENT { get; set; }
        public bool LCHECKLIST { get; set; }
        public string CLANG_ID { get; set; }
    }
}
