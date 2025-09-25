using System;
using System.Collections.Generic;

namespace APR00100COMMON.DTO_s.Print
{
    public class APR00100SummaryByDate1DTO
    {
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CREF_NO { get; set; }

        public List<APR00100SummaryByDate2DTO> Detail2 { get; set; }
    }
}