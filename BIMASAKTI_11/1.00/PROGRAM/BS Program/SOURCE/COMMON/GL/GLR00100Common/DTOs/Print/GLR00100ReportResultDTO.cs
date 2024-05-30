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
        
        // public string CFROM_PERIOD_DISPLAY { get; set; }
        // public string CTO_PERIOD_DISPLAY { get; set; }
        

        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CFROM_REF_NO { get; set; }
        public string CTO_REF_NO { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
    }

    public class GLR00100ReportBasedOnDateDTO
    {
        public string CREF_DATE { get; set; }
        public List<GLR00100ReportBasedOnDateSubDTO> DETAIL { get; set; }
        public decimal NTOTAL_DEBIT { get; set; }
        public decimal NTOTAL_CREDIT { get; set; }
    }

    public class GLR00100ReportBasedOnDateSubDTO
    {
        public string CTRANS_CODE { get; set; }
        public List<GLR00100ResultActivityReportDTO> SUBDETAIL { get; set; }
    }

    public class GLR00100SubReportBasedOnDateDTO
    {
        public string CCURRENCY_TYPE_NAME { get; set; }
        public static List<GLR00100ResultActivitySubReportDTO> DETAIL { get; set; }
        public decimal NGRAND_TOTAL_DEBIT { get; set; } = DETAIL.Sum(x => x.NTOTAL_DEBIT);
        public decimal NGRAND_TOTAL_CREDIT { get; set; } = DETAIL.Sum(x => x.NTOTAL_CREDIT);
    }

    public class GLR00100ReportWithBaseHeaderBasedOnDateDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00100ReportResultDTO Data { get; set; }
    }
}