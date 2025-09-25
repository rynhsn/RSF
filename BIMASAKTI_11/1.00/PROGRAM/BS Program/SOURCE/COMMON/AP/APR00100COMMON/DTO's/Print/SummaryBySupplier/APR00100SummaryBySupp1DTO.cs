using System;
using System.Collections.Generic;

namespace APR00100COMMON.DTO_s.Print
{
    public class APR00100SummaryBySupp1DTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CREF_NO { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public List<APR00100SummaryBySupp2DTO> Detail2 { get; set; }
    }
}