﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200COMMON
{
    public class JournalDetailGridDTO
    {
        public string CREC_ID { get; set; }
        public int INO { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CBSIS { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public string CDBCR { get; set; }
        public decimal NDEBIT { get; set; }
        public decimal NCREDIT { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public decimal NLDEBIT { get; set; }
        public decimal NLCREDIT { get; set; }
        public decimal NBDEBIT { get; set; }
        public decimal NBCREDIT { get; set; }
    }
    public class JournalDetailMappingDTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CCENTER_CODE { get; set; }
        public char CDBCR { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
    }

}