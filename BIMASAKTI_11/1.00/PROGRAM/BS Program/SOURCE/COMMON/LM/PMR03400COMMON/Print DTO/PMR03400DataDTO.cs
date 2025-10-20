using PMR03400COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.Print_DTO
{
    public class PMR03400DataDTO : PMR03400SPResultDTO
    {
        public DateTime? DDOCUMENT_DETAIL_EXPIRED_DATE { get; set; }
        public DateTime? DDOCUMENT_DETAIL_DATE { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
    }
}
