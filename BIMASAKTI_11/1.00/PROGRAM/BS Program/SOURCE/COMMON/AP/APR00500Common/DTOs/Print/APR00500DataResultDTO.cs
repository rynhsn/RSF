using System;
using System.Collections.Generic;

namespace APR00500Common.DTOs.Print
{
    public class APR00500ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public APR00500ReportResultDTO Data { get; set; }
    }
    
    public class APR00500ReportResultDTO
    {
        public string Title { get; set; }

        public APR00500ReportLabelDTO Label { get; set; }

        public APR00500ReportHeaderDTO Header { get; set; }
        public List<APR00500DataResultDTO> Data { get; set; }
    }

    public class APR00500ReportLabelDTO
    {
        public string PROPERTY { get; set; } = "Property";
        public string CUT_OFF_DATE { get; set; } = "Cut Off Date";
        public string DEPT { get; set; } = "Department";
        public string SUB_TOTAL { get; set; } = "Sub Total";
        public string TOTAL_PRD { get; set; } = "Total Period";
        public string GRAND_TOTAL { get; set; } = "Grand Total";
        public string REF_NO { get; set; } = "Reference No";
        public string REF_DATE { get; set; } = "Reference Date";
        public string SUPPLIER { get; set; } = "Supplier";
        // public string INV_PRD { get; set; } = "Invoice Period";
        public string INVOICE_PRD { get; set; } = "Invoice Period";
        public string DUE_DATE { get; set; } = "Due Date";
        public string CURRENCY { get; set; } = "Currency";
        public string TOTAL_AMT { get; set; } = "Total Amount";
        public string DISCOUNT { get; set; } = "Discount";
        public string TAX { get; set; } = "Tax";
        public string ADD_ON { get; set; } = "Add On";
        public string ADDITION { get; set; } = "Addition";
        public string DEDUCTION { get; set; } = "Deduction";
        public string INV_AMT { get; set; } = "Invoice Amount";
        public string REMAINING { get; set; } = "Remaining";
        public string DAYS_LATE { get; set; } = "Days Late";
        public string UNIT { get; set; } = "Unit";
    }
    
    public class APR00500ReportHeaderDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public DateTime? DCUT_OFF_DATE { get; set; }
        public string CCUT_OFF_DATE_DISPLAY { get; set; } = "";
    }
    
    public class APR00500DataResultDTO
    {
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPARTMENT_CODE { get; set; } = "";
        public string CREFERENCE_NO { get; set; } = "";
        public string CREFERENCE_DATE { get; set; } = "";
        public string CREFERENCE_DATE_DISPLAY { get; set; } = "";
        public DateTime? DREFERENCE_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CINVOICE_PERIOD { get; set; } = "";
        public string CDUE_DATE { get; set; } = "";
        public string CDUE_DATE_DISPLAY { get; set; } = "";
        public DateTime? DDUE_DATE { get; set; }
        public string CCURRENCY { get; set; } = "";
        public decimal NTOTAL_AMOUNT { get; set; }
        public decimal NDISCOUNT { get; set; }
        public decimal NADD_ON { get; set; }
        public decimal NTAX { get; set; }
        public decimal NADDITION { get; set; }
        public decimal NDEDUCTION { get; set; }
        public decimal NINVOICE_AMOUNT { get; set; }
        public decimal NREMAINING { get; set; }
        public int IDAYS_LATE { get; set; }
        public string CUNIT { get; set; } = "";
    }
}