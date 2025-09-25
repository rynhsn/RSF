using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.Print_DTO.Detail.SubDetail
{
    public class ChargeDTO
    {
        public string? CCHARGE_DETAIL_TYPE_NAME { get; set; }

        public List<ChargeTypeDTO>? ChargeTypeDetail { get; set; }
    }
}
