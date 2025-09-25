using PMR00220COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00220COMMON
{
    public class PMR00220DataDTO : PMR00220SPResultDTO
    {
        public DateTime? DDOCUMENT_DETAIL_EXPIRED_DATE { get; set; }
        public DateTime? DDOCUMENT_DETAIL_DATE { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
    }
}
