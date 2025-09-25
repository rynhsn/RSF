using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00100
{
    public class APT00100DetailDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CREF_ID { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CDUE_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public DateTime DDUE_DATE { get; set; }
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public decimal NTOTAL_AMOUNT { get; set; } = 0;
        public string CTRANS_DESC { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";
        public string CTRANS_STATUS_NAME { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
    }
}
