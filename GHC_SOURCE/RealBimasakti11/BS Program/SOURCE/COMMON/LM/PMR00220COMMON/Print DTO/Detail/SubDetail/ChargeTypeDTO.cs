using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00220COMMON.Print_DTO.Detail.SubDetail
{
    public class ChargeTypeDTO
    {
        public string? CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public List<ChargeTypeUnitDTO>? ChargeTypeUnitDetail { get; set; }
    }
}
