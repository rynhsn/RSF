using BaseHeaderReportCOMMON;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120Print
{
    public class PMT02120PrintReportResultDTO
    {
        public PMT02120PrintReportColumnDTO Column { get; set; }
        //public List<PMT02120PrintReportDTO> Data { get; set; }
        
        public PMT02120PrintReportHeaderDTO Data { get; set; }
        public bool LASSIGNMENT { get; set; }
        public bool LCHECKLIST { get; set; }
        //public PMT02120PrintReportHeaderDTO HeaderData { get; set; }
        //public List<PMT02120PrintReportEmployeeDTO> EmployeeData { get; set; }
        //public List<PMT02120PrintReportChecklistDTO> ChecklistData { get; set; }
        //public PMT02120PrintReportFooterDTO FooterData { get; set; }
    }

    public class PMT02120PrintReportResultWithBaseHeaderPrintDTO : BaseHeaderResult
    {
        public PMT02120PrintReportResultDTO ReportData { get; set; }
    }
}
