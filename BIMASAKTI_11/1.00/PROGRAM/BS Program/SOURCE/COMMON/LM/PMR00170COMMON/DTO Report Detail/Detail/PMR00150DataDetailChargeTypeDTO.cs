using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON.DTO_Report_Detail.Detail
{
    public class PMR00170DataDetailChargeTypeDTO
    {

        public string? CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public List<PMR00170DataDetailChargeTypeUnitDTO>? ChargeTypeUnitDetail { get; set; }
      
    }
}
