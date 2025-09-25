using System;
using System.Collections.Generic;
using System.Linq;

namespace GLR00100Common.DTOs.Print
{
    public class GLR00100ReportResultDTO
    {
        public string Title { get; set; }
        public GLR00100ReportLabelDTO Label { get; set; }
        public GLR00100ReportHeaderDTO Header { get; set; }
        public List<GLR00100ResultActivityReportDTO> Data { get; set; }
        
        public List<GLR00100ReportBasedOnTransCodeDTO> DataByTransCode { get; set; } = new List<GLR00100ReportBasedOnTransCodeDTO>();
        public List<GLR00100ReportBasedOnRefNoDTO> DataByRefNo { get; set; } = new List<GLR00100ReportBasedOnRefNoDTO>();
        public List<GLR00100ReportBasedOnDateSubDTO> DataByDate { get; set; } = new List<GLR00100ReportBasedOnDateSubDTO>();
        public List<GLR00100TotalByCurrDTO> GrandTotalByCurr { get; set; } = new List<GLR00100TotalByCurrDTO>();
        
        public decimal NGRAND_TOTAL_DEBIT { get; set; } = 0m;
        public decimal NGRAND_TOTAL_CREDIT { get; set; } = 0m;
        public List<GLR00100ResultActivitySubReportDTO> SubData { get; set; }
    }
    
    public class GLR00100ReportBasedOnTransCodeDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public List<GLR00100ReportBasedOnRefNoDTO> Data { get; set; } = new List<GLR00100ReportBasedOnRefNoDTO>();
        public List<GLR00100TotalByCurrDTO> SubTotalByCurr { get; set; } = new List<GLR00100TotalByCurrDTO>();
        public decimal NTOTAL_DEBIT { get; set; } = 0m;
        public decimal NTOTAL_CREDIT { get; set; } = 0m;
    }
    
    public class GLR00100ReportBasedOnRefNoDTO
    {
        public string CREF_NO { get; set; } = "";
        public List<GLR00100ResultActivityReportDTO> Data { get; set; } = new List<GLR00100ResultActivityReportDTO>();
        public List<GLR00100TotalByCurrDTO> SubTotalByCurr { get; set; } = new List<GLR00100TotalByCurrDTO>();
        public decimal NTOTAL_DEBIT { get; set; } = 0m;
        public decimal NTOTAL_CREDIT { get; set; } = 0m;
    }

    public class GLR00100TotalByCurrDTO
    {
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NTOTAL_DEBIT { get; set; } = 0m;
        public decimal NTOTAL_CREDIT { get; set; } = 0m;
    }
    
    public class GLR00100ReportBasedOnDateSubDTO
    {
        public DateTime? DREF_DATE { get; set; }
        public List<GLR00100ResultActivityReportDTO> Data { get; set; }
        public List<GLR00100TotalByCurrDTO> SubTotalByCurr { get; set; } = new List<GLR00100TotalByCurrDTO>();
        public decimal NTOTAL_DEBIT { get; set; }
        public decimal NTOTAL_CREDIT { get; set; }
    }

    public class GLR00100ReportHeaderDTO
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

    public class GLR00100ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00100ReportResultDTO Data { get; set; }
    }
}