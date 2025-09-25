using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMF00200ReportCommon
{
    public class PMF00201DTO
    {
        public int INO { get; set; }
        public string CALLOC_DEPT_CODE { get; set; }
        public string CALLOC_NO { get; set; }
        public string CALLOC_DATE { get; set; }
        public DateTime? DALLOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DESC { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
    }
}
