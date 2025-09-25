using PMR00210COMMON.DTO_s;
using System;

namespace PMR00210COMMON
{
    public class PMR00210DataDTO : PMR00210SPResultDTO
    {
        public DateTime? DDOCUMENT_DETAIL_EXPIRED_DATE { get; set; }
        public DateTime? DDOCUMENT_DETAIL_DATE { get; set; }
        public DateTime? DDEPOSIT_DETAIL_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
    }
}
