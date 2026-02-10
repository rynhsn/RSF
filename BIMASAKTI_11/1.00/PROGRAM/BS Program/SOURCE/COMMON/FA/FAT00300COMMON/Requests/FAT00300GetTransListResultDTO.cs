using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00300Common.Requests
{
    public class FAT00300GetTransListResultDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";
        public string CTRANS_STATUS_NAME { get; set; } = "";
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NDEPR_QTY { get; set; }
        public int IQTY { get; set; }
        public string CASSET_CODE { get; set; } = "";
        public string CASSET_NAME { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
    }
}
