using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs.Print
{
    public class APR00600ReportResultDTO
    {
        public string Title { get; set; }

        public APR00600ReportLabelDTO Label { get; set; }

        public APR00600ReportHeaderDTO Header { get; set; }
        public List<APR00600DataResultDTO> Data { get; set; }
    }
    public class APR00600DataResultDTO
    {
        public int INO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public string CSUPPLIER_ID_NAME { get; set; }
        public decimal NBEG_BAL { get; set; }
        public decimal NPURCHASE_INVOICE { get; set; }
        public int IINVOICE { get; set; }
        public decimal NPURCHASE_RETURN { get; set; }
        public int IPURCHASE_RETURN { get; set; }
        public decimal NPURCHASE_DEBIT_NOTE { get; set; }
        public int IPURCHASE_DEBIT_NOTE { get; set; }
        public decimal NPURCHASE_CREDIT_NOTE { get; set; }
        public int IPURCHASE_CREDIT_NOTE { get; set; }
        public decimal NPURCHASE_DEBIT_ADJ { get; set; }
        public int IPURCHASE_DEBIT_ADJ { get; set; }
        public decimal NPURCHASE_CREDIT_ADJ { get; set; }
        public int IPURCHASE_CREDIT_ADJ { get; set; }
        public decimal NSUPP_PAYMENT { get; set; }
        public int ISUPP_PAYMENT { get; set; }
        public decimal NALLOC_DISCOUNT { get; set; }
        public int IPURCHASE_DP { get; set; }
        public int NPURCHASE_DP { get; set; }
        public int NALLOC_DP { get; set; }
        public decimal NGAIN_LOSS { get; set; }
        public decimal NEND_BAL { get; set; }
        public string CFR_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CFILTER_VALUE { get; set; }
        public string CFILTER_BY { get; set; }
        public string CCURRENCY { get; set; }

    }

    public class APR00600ReportLabelDTO
    {
        public string PROPERTY { get; set; } = "Property";
        public string BUILDING { get; set; } = "Building";

        public string SUPPLIER { get; set; } = "Supplier";
        public string CCATEGORY_ID { get; set; } = "Category";
        public string CJRNGRP_CODE { get; set; } = "Journal Group";
        public string PERIOD { get; set; } = "Period";
        public string CURRENCY { get; set; } = "Currency";
        public string FILTER_BY { get; set; } = "Filter by";
        public string CSUPPLIER_ID_NAME { get; set; } = "Supplier";
        public string NBEG_BAL { get; set; } = "Beginning Balance";
        public string IINVOICE { get; set; } = "#Inv.";
        public string IPURCHASE_RETURN { get; set; } = "#Ret.";
        public string NPURCHASE_INVOICE { get; set; } = "Invoice";
        public string NPURCHASE_RETURN { get; set; } = "Return";
        public string IPURCHASE_DEBIT_NOTEA { get; set; } = "#D/N";
        public string IPURCHASE_CREDIT_NOTE { get; set; } = "#C/N";
        public string NPURCHASE_DEBIT_NOTE { get; set; } = "Debit Note";
        public string NPURCHASE_CREDIT_NOTE { get; set; } = "Credit Note";
        public string IPURCHASE_DEBIT_ADJ { get; set; } = "#D/A";
        public string IPURCHASE_CREDIT_ADJ { get; set; } = "#C/A";
        public string NPURCHASE_DEBIT_ADJ { get; set; } = "Debit Adjustment";
        public string NPURCHASE_CREDIT_ADJT { get; set; } = "Credit Adjustment";
        public string ISUPP_PAYMENT { get; set; } = "# Pymt.";
        public string NSUPP_PAYMENT { get; set; } = "Payment";
        public string NALLOC_DISCOUNT { get; set; } = "Payment Discount";
        public string NGAIN_LOSS { get; set; } = "Gain (Loss)";
        public string NEND_BAL { get; set; } = "Ending Balance";

        public string IPURCHASE_DP { get; set; } = "#DP";
        public string NPURCHASE_DP { get; set; } = "Down Payment";
        public string NALLOC_DP { get; set; } = "Alloc.DP";

        public string Grand_Total_Beginning_Balance { get; set; } = "Grand Total Beginning Balance";
        public string Grand_Total { get; set; } = "Grand Total ";
        public string Total { get; set; } = "Total ";
    }
    public class APR00600ReportHeaderDTO
    {
        public string CPROPERTY { get; set; } = "";
        public string CSUPPLIER_DISPLAY { get; set; } = "";
        public string CFROM_PERIOD { get; set; } = "";
        public string CTO_PERIOD { get; set; } = "";
        public string CPERIOD_DISPLAY { get; set; } = "";
        public string CCURRENCY { get; set; } = "";
        public string CFILTER_BY { get; set; } = "";
        public DateTime? DPERIOD { get; set; }
    }

    public class APR00600ReportWithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public APR00600ReportResultDTO Data { get; set; }
    }
}
