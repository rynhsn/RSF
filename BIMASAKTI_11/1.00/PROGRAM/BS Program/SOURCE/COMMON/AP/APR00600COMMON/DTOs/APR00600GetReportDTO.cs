using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs
{
    public class APR00600GetReportDTO
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
}
