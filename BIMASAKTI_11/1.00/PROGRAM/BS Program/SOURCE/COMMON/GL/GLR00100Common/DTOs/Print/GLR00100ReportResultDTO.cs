using System.Collections.Generic;
using System.Linq;

namespace GLR00100Common.DTOs.Print
{
    public class GLR00100ReportResultDTO
    {
        public string Title { get; set; }

        public GLR00100ReportLabelDTO Label { get; set; }

        public GLR00100ReportHeaderBasedOnDateDTO Header { get; set; }
        public List<GLR00100ResultActivityReportDTO> Data { get; set; }
        // public List<GLR00100ReportBasedOnDateDTO> Data { get; set; }
        public List<GLR00100ResultActivitySubReportDTO> SubData { get; set; }
        // public List<GLR00100BasedOnDateSubDTO> SubData { get; set; }
    }

    public class GLR00100ReportHeaderBasedOnDateDTO
    {
        // public string CREPORT_TYPE { get; set; }
        public string CFROM_DEPT_CODE { get; set; }
        // public string CFROM_DEPT_NAME { get; set; }
        public string CTO_DEPT_CODE { get; set; }
        // public string CTO_DEPT_NAME { get; set; }
        public string CPERIOD_TYPE { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CREPORT_BASED_ON { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CFROM_REF_NO { get; set; }
        public string CTO_REF_NO { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
        
        public string CSORT_BY { get; set; }
        public bool LTOTAL_BY_REF_NO { get; set; }
        public bool LTOTAL_BY_DEPT { get; set; }
    }

    public class GLR00100ReportBasedOnDateSubDTO
    {
        public string CTRANS_CODE { get; set; }
        public List<GLR00100ResultActivityReportDTO> SUBDETAIL { get; set; }
    }

    public class GLR00100ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00100ReportResultDTO Data { get; set; }
    }
}