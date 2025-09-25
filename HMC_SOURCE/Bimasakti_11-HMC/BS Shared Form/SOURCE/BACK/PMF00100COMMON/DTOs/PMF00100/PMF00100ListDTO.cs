using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00100
{
    public class PMF00100ListDTO
    {
        public string CALLOC_ID { get; set; } = "";
        public int INO { get; set; } 
        public string CALLOC_NO { get; set; } = "";
        public string CALLOC_DATE { get; set; } = "";
        public DateTime DALLOC_DATE { get; set; }
        public string CTRANS_NAME { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CTRANS_STATUS_NAME { get; set; } = "";
        public string CAPPLIED_CURR_CODE { get; set; } = "";
        public decimal NAPPLIED_AMOUNT { get; set; } = 0;
        public string CALLOC_CURR_CODE { get; set; } = "";
        public decimal NALLOC_AMOUNT { get; set; } = 0;
        public decimal NALLOC_DISCOUNT { get; set; } = 0;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }

        //HIDDEN
        public string CCURRENCY_CODE { get; set; } = "";
        public string CTRANS_DESC { get; set; } = "";
        public decimal NBFOREX_GAINLOSS { get; set; } = 0;
        public decimal NLFOREX_GAINLOSS { get; set; } = 0;
        public decimal NTRANS_AMOUNT { get; set; } = 0;
    }
}
