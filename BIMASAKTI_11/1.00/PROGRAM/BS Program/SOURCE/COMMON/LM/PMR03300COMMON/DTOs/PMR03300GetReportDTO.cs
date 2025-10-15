using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs
{
    public class PMR03300GetReportDTO
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
        public string CFR_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CFILTER_VALUE { get; set; }
        public string CFILTER_BY { get; set; }
        public string CCURRENCY { get; set; }
    }
}
