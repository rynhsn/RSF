using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class JournalDetailActualGridDTO
    {
        public string CREC_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CDOC_SEQ_NO { get; set; }
        public string CREF_PRD { get; set; }
        public DateTime DREF_PRD { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_DATE_Display { get; set; }
        public DateTime DREF_DATE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NLTRANS_AMOUNT { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
