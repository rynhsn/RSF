using System;
using System.Collections.Generic;

namespace APR00300Common.DTOs.Print
{
    public class APR00300DataResultDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CREF_PRD { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public decimal NORIGINAL_AMOUNT { get; set; }
        public decimal NOUTSTANDING_AMOUNT { get; set; }
        public decimal NAGE_NOT_DUE_AMOUNT { get; set; }
        public decimal NAGE_CURRENT_AMOUNT { get; set; }
        public decimal NAGE_MORE_THAN_30_DAYS_AMOUNT { get; set; }
        public decimal NAGE_MORE_THAN_60_DAYS_AMOUNT { get; set; }
        public decimal NAGE_MORE_THAN_90_DAYS_AMOUNT { get; set; }
        public decimal NAGE_MORE_THAN_120_DAYS_AMOUNT { get; set; }
        public decimal NAGE_UNALLOCATED_PAYMENT { get; set; }
    }

    public class APR00300ReportDataDTO
    {
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public List<APR00300GroupPeriodDTO> PERIODS { get; set; } // Jika date based on = "P"
        public List<APR00300DataResultDTO> INVOICES { get; set; } // Jika date based on = "C"
        public List<APR00300GroupCurrencyDTO> CURRENCIES { get; set; }  // Jika date based on = "C"
        public decimal NGRAND_TOTAL { get; set; }
    } 

    public class APR00300GroupPeriodDTO
    {
        public string CREF_PRD { get; set; }
        public List<APR00300DataResultDTO> INVOICES { get; set; }
        public List<APR00300GroupCurrencyDTO> CURRENCIES { get; set; }
        public decimal NSUB_TOTAL { get; set; }
    }
    
    public class APR00300GroupCurrencyDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public decimal NSUB_TOTAL { get; set; }
    }
    
    public class APR00300ReportHeaderDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public DateTime? DSTATEMENT_DATE { get; set; }
        public DateTime? DCUT_OFF_DATE { get; set; }
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public bool LINCLUDE_ZERO_BALANCE { get; set; }
        public bool LSHOW_AGE_TOTAL { get; set; }
        
        public string CDATE_BASED_ON { get; set; } = "";
    }
    
    public class APR00300ReportLabelDTO
    {
        public string PROPERTY { get; set; } = "Property";
        public string STATEMENT_DATE { get; set; } = "Statement Date";
        public string PERIOD { get; set; } = "Period";
        public string SUPPLIER { get; set; } = "Supplier";
        public string CUT_OFF_DATE { get; set; } = "Cut Off Date";
        public string INCLUDE_ZERO_BALANCE { get; set; } = "Include Zero Balance";
        public string SHOW_AGE_TOTAL { get; set; } = "Show Age Total";
        public string INVOICE_NO { get; set; } = "Invoice No";
        public string INVOICE_DATE { get; set; } = "Invoice Date";
        public string DESCRIPTION { get; set; } = "Description";
        public string CURR { get; set; } = "Curr";
        public string AMOUNT { get; set; } = "Amount";
        public string ORIGINAL { get; set; } = "Original";
        public string OUTSTANDING { get; set; } = "Outstanding";
        public string SUB_TOTAL { get; set; } = "Sub Total";
        public string AGE_TOTAL { get; set; } = "Age Total";
        public string NOT_DUE { get; set; } = "Not Due";
        public string CURRENT { get; set; } = "Current";
        public string MORE_THAN_30_DAYS { get; set; } = "> 30 Days";
        public string MORE_THAN_60_DAYS { get; set; } = "> 60 Days";
        public string MORE_THAN_90_DAYS { get; set; } = "> 90 Days";
        public string MORE_THAN_120_DAYS { get; set; } = "> 120 Days";
        public string UNALLOCATED_PAYMENT { get; set; } = "Unallocated Payment";
        
        public string SUB_TOTAL_PERIOD { get; set; } = "Sub Total Period";
        public string GRAND_TOTAL_SUPPLIER { get; set; } = "Grand Total Supplier";
    }

    public class APR00300ReportResultDTO
    {
        public string Title { get; set; }

        public APR00300ReportLabelDTO Label { get; set; }

        public APR00300ReportHeaderDTO Header { get; set; }
        public List<APR00300ReportDataDTO> Data { get; set; }
    }
    
    public class APR00300ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public APR00300ReportResultDTO Data { get; set; }
    }
}