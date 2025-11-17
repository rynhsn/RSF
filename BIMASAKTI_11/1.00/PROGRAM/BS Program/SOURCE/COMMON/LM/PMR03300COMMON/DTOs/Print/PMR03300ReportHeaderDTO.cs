using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs.Print
{
    public class PMR03300ReportResultDTO
    {
        public string Title { get; set; }

        public PMR03300ReportLabelDTO Label { get; set; }

        public PMR03300ReportHeaderDTO Header { get; set; }
        public List<PMR03300DataResultDTO> Data { get; set; }
    }

    public class PMR03300DataResultDTO
    {
        public int INO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public string CCUSTOMER_ID_NAME { get; set; }
        public decimal NBEG_BAL { get; set; }
        public decimal NINVOICE { get; set; }
        public int IINVOICE { get; set; }
        public decimal NSALES_RETURN { get; set; }
        public int ISALES_RETURN { get; set; }
        public decimal NSALES_DEBIT_NOTE { get; set; }
        public int ISALES_DEBIT_NOTE { get; set; }
        public decimal NSALES_CREDIT_NOTE { get; set; }
        public int ISALES_CREDIT_NOTE { get; set; }
        public decimal NSALES_DEBIT_ADJ { get; set; }
        public int ISALES_DEBIT_ADJ { get; set; }
        public decimal NSALES_CREDIT_ADJ { get; set; }
        public int ISALES_CREDIT_ADJ { get; set; }
        public decimal NCUST_RECEIPT { get; set; }
        public int ICUST_RECEIPT { get; set; }
        public decimal NALLOC_DISCOUNT { get; set; }
        public decimal NGAIN_LOSS { get; set; }
        public decimal NEND_BAL { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CFILTER_VALUE { get; set; }
        public string CFILTER_BY { get; set; }
        public string CCURRENCY { get; set; }
    }

    public class PMR03300ReportLabelDTO
    {
        public string PROPERTY { get; set; } = "Property";
        public string BUILDING { get; set; } = "Building";

        public string CUSTOMER { get; set; } = "Customer";
        public string CCATEGORY_ID { get; set; } = "Category";
        public string CJRNGRP_CODE { get; set; } = "Journal Group";
        public string PERIOD { get; set; } = "Period";
        public string CURRENCY { get; set; } = "Currency";
        public string FILTER_BY { get; set; } = "Filter by";
        public string CCUSTOMER_ID_NAME { get; set; } = "Customer";
        public string NBEG_BAL { get; set; } = "Beginning Balance";
        public string IINVOICE { get; set; } = "#Inv.";
        public string ISALES_RETURN { get; set; } = "#Ret.";
        public string NINVOICE { get; set; } = "Invoice";
        public string NSALES_RETURN { get; set; } = "Return";
        public string ISALES_DEBIT_NOTEA { get; set; } = "#D/N";
        public string ISALES_CREDIT_NOTE { get; set; } =  "#C/N";
        public string NSALES_DEBIT_NOTE { get; set; } = "Debit Note";
        public string NSALES_CREDIT_NOTE { get; set; } = "Credit Note";
        public string ISALES_DEBIT_ADJ { get; set; } = "#D/A";
        public string ISALES_CREDIT_ADJ { get; set; } = "#C/A";
        public string NSALES_DEBIT_ADJ { get; set; } = "Debit Adjustment";
        public string NSALES_CREDIT_ADJT { get; set; } = "Credit Adjustment";
        public string ICUST_RECEIPT { get; set; } = "#Rcp.";
        public string NCUST_RECEIPT { get; set; } = "Receipt";
        public string NALLOC_DISCOUNT { get; set; } = "Receipt Discount";
        public string NGAIN_LOSS { get; set; } = "Gain (Loss)";
        public string NEND_BAL { get; set; } = "Ending Balance";
        public string Grand_Total_Beginning_Balance { get; set; } = "Grand Total Beginning Balance";
        public string Grand_Total { get; set; } = "Grand Total ";
        public string Total { get; set; } = "Total ";
    }

    public class PMR03300ReportHeaderDTO
    {
        public string CPROPERTY { get; set; } = "";
        public string CCUSTOMER_DISPLAY { get; set; } = "";
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public string CPERIOD_DISPLAY { get; set; } = "";
        public string CCURRENCY { get; set; } = "";
        public string CFILTER_BY { get; set; } = "";
        public DateTime? DPERIOD { get; set; }
    }

    public class PMR03300ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public PMR03300ReportResultDTO Data { get; set; }
    }
}
