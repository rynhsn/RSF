using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON.DTO_Report_Detail.Detail
{
    public class PMR00150DataDetailChargeTypeDTO
    {

        public string? CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public List<PMR00150DataDetailChargeTypeUnitDTO>? ChargeTypeUnitDetail { get; set; }
      
    }
}
