using System;
using System.Collections.Generic;

namespace APR00100COMMON.DTO_s.Print
{
    public class APR00100SummaryByDateDTO
    {
     
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public List<APR00100SummaryByDate1DTO> Detail1 { get; set; }
    }
}